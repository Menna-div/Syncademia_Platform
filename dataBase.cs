using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Sockets;

namespace syncademia
{
    // Column enums – no more magic numbers
    public enum StudentColumns
    {
        ID         = 0,
        Username   = 1,
        Email      = 2,
        Password   = 3,
        Name       = 4,
        Department = 5,
        Year       = 6
    }

    public enum MentorColumns
    {
        ID = 0,
        Username = 1,
        Email = 2
    }

    public class dataBase
    {
        public static string spreadsheetId = "1sjNsk_t7tCcJoVZbXutzB3PfeKBNGTtoeRDsNES8ZIU";

        private static SheetsService _sheetsService;
        private static readonly object _lock = new object();

        public static SheetsService GetSheetsService()
        {
            if (_sheetsService == null)
            {
                lock (_lock)
                {
                    if (_sheetsService == null)
                    {
                        string[] Scopes = { SheetsService.Scope.Spreadsheets };
                        string ApplicationName = "Syncademia";

                        GoogleCredential credential;
                        using (var stream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncademiadatabase-81cdfbf2319f.json"), FileMode.Open, FileAccess.Read))
                        {
                            credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                        }

                        _sheetsService = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = ApplicationName,
                        });
                    }
                }
            }
            return _sheetsService;
        }

        // Helper method to check if exception is network-related
        private static bool IsNetworkException(Exception ex)
        {
            return ex is HttpRequestException ||
                   ex is TaskCanceledException ||
                   (ex.InnerException != null && (ex.InnerException is SocketException || ex.InnerException is HttpRequestException));
        }

        public static async Task<IList<IList<object>>> ReadSheet(string range)
        {
            try
            {
                var service = GetSheetsService();
                var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
                var response = await request.ExecuteAsync();
                return response.Values;
            }
            catch (Exception ex)
            {
                if (IsNetworkException(ex))
                    throw new Exception("Internet connection lost. Please check your network and try again.", ex);
                else
                    throw new Exception($"Failed to read from Google Sheets. Error: {ex.Message}", ex);
            }
        }

        public static async Task WriteSheet(string range, List<IList<object>> values)
        {
            try
            {
                var service = GetSheetsService();
                var valueRange = new ValueRange { Values = values };
                var request = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
                request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                await request.ExecuteAsync();
            }
            catch (Exception ex)
            {
                if (IsNetworkException(ex))
                    throw new Exception("Internet connection lost. Please check your network and try again.", ex);
                else
                    throw new Exception($"Failed to write to Google Sheets. Error: {ex.Message}", ex);
            }
        }

        public static string GetColumnLetter(int columnIndex)
        {
            string columnLetter = "";
            int temp = columnIndex + 1;
            while (temp > 0)
            {
                int modulo = (temp - 1) % 26;
                columnLetter = Convert.ToChar(65 + modulo).ToString() + columnLetter;
                temp = (int)((temp - modulo) / 26);
            }
            return columnLetter;
        }

        public static async Task<int> GetLatestId(string sheet)
        {
            try
            {
                string columnLetter = GetColumnLetter((int)StudentColumns.ID);
                var values = await ReadSheet($"{sheet}!{columnLetter}:{columnLetter}");
                if (values == null || values.Count <= 1)
                    return 0;

                string lastValue = values[values.Count - 1][0]?.ToString();
                if (int.TryParse(lastValue, out int id))
                    return id;
                return 0;
            }
            catch (Exception ex)
            {
                if (IsNetworkException(ex))
                    throw new Exception("Internet connection lost. Please check your network and try again.", ex);
                else
                    throw new Exception("Failed to get latest ID. " + ex.Message, ex);
            }
        }

        public static async Task UpdateStudentPercentage(int studentId, double percentage)
        {
            try
            {
                var allData = await ReadSheet("Students!A:I");
                if (allData == null || allData.Count <= 1) return;

                for (int i = 1; i < allData.Count; i++)
                {
                    var row = allData[i];
                    if (row.Count > 0 && row[0].ToString() == studentId.ToString())
                    {
                        int rowIndex = i + 1;
                        string colLetter = GetColumnLetter(7); // Column H = Percentage
                        await WriteSheet($"Students!{colLetter}{rowIndex}",
                            new List<IList<object>> { new List<object> { percentage.ToString("0.00") } });
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (IsNetworkException(ex))
                    throw new Exception("Internet connection lost. Please check your network and try again.", ex);
                else
                    throw new Exception("Failed to update student percentage: " + ex.Message, ex);
            }
        }

        public static async Task<bool> IsEmailTaken(string email)
        {
            try
            {
                string studentsCol = GetColumnLetter((int)StudentColumns.Email);
                var studentValues = await ReadSheet($"Students!{studentsCol}:{studentsCol}");
                if (studentValues != null)
                {
                    foreach (var row in studentValues)
                    {
                        if (row != null && row.Count > 0 && row[0]?.ToString() == email)
                            return true;
                    }
                }

                string mentorsCol = GetColumnLetter((int)MentorColumns.Email);
                var mentorValues = await ReadSheet($"Mentors!{mentorsCol}:{mentorsCol}");
                if (mentorValues != null)
                {
                    foreach (var row in mentorValues)
                    {
                        if (row != null && row.Count > 0 && row[0]?.ToString() == email)
                            return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                if (IsNetworkException(ex))
                    throw new Exception("Internet connection lost. Please check your network and try again.", ex);
                else
                    throw new Exception("Failed to check email. " + ex.Message, ex);
            }
        }

        public static async Task<bool> IsUsernameTaken(string sheet, string username)
        {
            try
            {
                int columnIndex;
                if (sheet == "Students")
                    columnIndex = (int)StudentColumns.Username;
                else if (sheet == "Mentors")
                    columnIndex = (int)MentorColumns.Username;
                else
                    throw new ArgumentException("Invalid sheet name. Use 'Students' or 'Mentors'.");

                string columnLetter = GetColumnLetter(columnIndex);
                var values = await ReadSheet($"{sheet}!{columnLetter}:{columnLetter}");

                if (values == null) return false;

                foreach (var row in values)
                {
                    if (row != null && row.Count > 0 && row[0]?.ToString() == username)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (IsNetworkException(ex))
                    throw new Exception("Internet connection lost. Please check your network and try again.", ex);
                else
                    throw new Exception("Failed to check username. " + ex.Message, ex);
            }
        }
    }
}