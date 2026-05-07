using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;
using QRCoder;

namespace syncademia
{
    public class AttendanceSystem
    {
        // ══════════════════════════════════════════════════════════════
        //  PRIVATE HELPER METHODS
        // ══════════════════════════════════════════════════════════════

        private static async Task<int> FindSubjectColumn(string sheet, string subject)
        {
            var headers = await dataBase.ReadSheet($"{sheet}!1:1");
            if (headers == null || headers.Count == 0) return -1;

            for (int i = 0; i < headers[0].Count; i++)
            {
                if (headers[0][i].ToString().Trim().ToLower() == subject.Trim().ToLower())
                    return i;
            }
            return -1;
        }

        private static async Task<int> FindStudentRow(string sheet, int studentId)
        {
            var idColumn = await dataBase.ReadSheet($"{sheet}!A:A");
            if (idColumn == null) return -1;

            for (int i = 1; i < idColumn.Count; i++)
            {
                if (idColumn[i].Count < 1) continue;
                if (idColumn[i][0].ToString() == studentId.ToString())
                    return i + 1; // +1 because Google Sheets rows start at 1
            }
            return -1;
        }

        private static async Task<string> ReadCell(string sheet, string columnLetter, int row)
        {
            var data = await dataBase.ReadSheet($"{sheet}!{columnLetter}{row}");
            if (data != null && data.Count > 0 && data[0].Count > 0)
                return data[0][0].ToString();
            return "0"; // default to "0" if the cell is empty
        }

        private static async Task WriteCell(string sheet, string columnLetter, int row, object value)
        {
            await dataBase.WriteSheet($"{sheet}!{columnLetter}{row}", new List<IList<object>>
            {
                new List<object> { value }
            });
        }

        private static void PrintSubjectGrades(string subjectName, IList<object> dataRow, int col)
        {
            string Get(int offset)
            {
                int idx = col + offset;
                if (idx >= dataRow.Count) return "—";
                string val = dataRow[idx].ToString().Trim();
                return val == "" ? "—" : val;
            }

            Console.WriteLine($"\n  ┌─ {subjectName}");
            Console.WriteLine($"  │  MID:         {Get(1)}");
            Console.WriteLine($"  │  Practical:   {Get(2)}");
            Console.WriteLine($"  │  Oral:        {Get(3)}");
            Console.WriteLine($"  │  Final:       {Get(4)}");
            Console.WriteLine($"  │  Bonus:       {Get(5)}");
            Console.WriteLine($"  └─ Attendance:  {Get(6)}");
        }

        private static async Task PrintGradesFromSheet(int studentId, string sheet)
        {
            var headers = await dataBase.ReadSheet($"{sheet}!1:1");
            if (headers == null || headers.Count == 0)
            {
                Console.WriteLine($"❌ Could not read headers from {sheet}.");
                return;
            }
            var headerRow = headers[0];

            int studentRow = await FindStudentRow(sheet, studentId);
            if (studentRow == -1)
            {
                Console.WriteLine($"❌ No grades recorded in {sheet} yet.");
                return;
            }

            string lastCol = ColumnIndexToLetter(155); // covers all possible subject columns
            var rowData = await dataBase.ReadSheet($"{sheet}!A{studentRow}:{lastCol}{studentRow}");
            if (rowData == null || rowData.Count == 0)
            {
                Console.WriteLine("❌ No grade data found.");
                return;
            }
            var dataRow = rowData[0];

            Console.WriteLine($"\n══════════════════════════════════════════════════");
            Console.WriteLine($"  Grades — {sheet.Replace("_", " ")}");
            Console.WriteLine($"══════════════════════════════════════════════════");

            bool anySubjectFound = false;
            for (int col = 2; col < headerRow.Count; col += 7)
            {
                string subjectName = headerRow[col].ToString().Trim();
                if (string.IsNullOrEmpty(subjectName)) continue;

                anySubjectFound = true;
                PrintSubjectGrades(subjectName, dataRow, col);
            }

            if (!anySubjectFound)
                Console.WriteLine("  No subjects found in this sheet.");

            Console.WriteLine($"\n══════════════════════════════════════════════════\n");
        }

        // ── Helper: map chosen year + department to sheet name ────────────
        // used by both ViewMyGrades and ViewAllStudentGrades to avoid repeating logic
        private static string ResolveSheetName(string enrolledGradeStr, int chosenYear)
        {
            if (chosenYear == 1) return "Grade_1";
            if (chosenYear == 2) return "Grade_2";
            // year 3 or 4 → department-specific sheet e.g. "CS_3", "IS_4"
            return $"{enrolledGradeStr}_{chosenYear}";
        }


        // ══════════════════════════════════════════════════════════════
        //  QR CODE METHODS
        // ══════════════════════════════════════════════════════════════

        public static async Task<string> GenerateQRCode(int mentorId, string subject, double bonusGrade)
        {
            int random = new Random().Next(100000, 999999);
            string code = $"SYN-{mentorId}-{random}";

            int newId  = await dataBase.GetLatestId("QRCodes") + 1;
            int newRow = newId + 1; // +1 because row 1 is the header

            await dataBase.WriteSheet($"QRCodes!A{newRow}", new List<IList<object>>
            {
                new List<object>
                {
                    newId, code, mentorId, subject, bonusGrade,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    "" // UsedByStudents starts empty
                }
            });

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData      qrCodeData  = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            AsciiQRCode     qrCode      = new AsciiQRCode(qrCodeData);
            string          art         = qrCode.GetGraphic(1);

            Console.WriteLine("\n═══════════════════════════════════════");
            Console.WriteLine($"  Subject: {subject}");
            if (bonusGrade > 0)
                Console.WriteLine($"  Bonus: +{bonusGrade} points");
            Console.WriteLine($"  Code: {code}");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine(art);
            Console.WriteLine("Students can scan this QR or type the code above.");
            Console.WriteLine("═══════════════════════════════════════\n");

            return code;
        }

        public static async Task<(bool success, string message)> UseQRCode(int studentId, string code, Form here)
        {
            LoadingOverlay load = new LoadingOverlay("Searching for the code...");
            load.ShowWithAnimation(here);

            try
            {
                var rows = await dataBase.ReadSheet("QRCodes!A:G");
                if (rows == null || rows.Count <= 1)
                    return (false, "❌ Invalid code.");

                for (int i = 1; i < rows.Count; i++)
                {
                    var row = rows[i];
                    if (row.Count < 5) continue;
                    if (row[1].ToString() != code) continue;

                    string subject    = row[3].ToString();
                    double bonusGrade = 0;
                    if (row.Count > 4 && row[4].ToString() != "")
                        double.TryParse(row[4].ToString(), out bonusGrade);

                    string usedBy  = row.Count > 6 ? row[6].ToString() : "";
                    var usedList   = usedBy.Split(',').Select(s => s.Trim()).Where(s => s != "").ToList();

                    if (usedList.Contains(studentId.ToString()))
                        return (false, "❌ You already used this QR code.");

                    usedList.Add(studentId.ToString());
                    string newUsedBy = string.Join(",", usedList);
                    int    sheetRow  = i + 1;
                    await WriteCell("QRCodes", "G", sheetRow, newUsedBy);
                    await AddAttendance(studentId, subject, 1, bonusGrade);

                    string message = $"✅ Attendance recorded for '{subject}'!";
                    if (bonusGrade > 0)
                        message += $"\n🎉 You received +{bonusGrade} bonus points!";

                    return (true, message);
                }

                return (false, "❌ Code not found. Please check and try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return (false, "❌ An error occurred."); // 👈 compiler needs this
            }
            finally
            {
                // always runs — closes overlay and re-enables form no matter what
                load.Close();
                here.Enabled = true;
            }
        }


        // ══════════════════════════════════════════════════════════════
        //  ATTENDANCE METHODS
        // ══════════════════════════════════════════════════════════════

        public static async Task ManualAttendance(int studentId, string subject, int attendanceDelta)
        {
            await AddAttendance(studentId, subject, attendanceDelta, 0);
            Console.WriteLine($"✅ Attendance updated for student {studentId} in '{subject}'.");
        }

        private static async Task AddAttendance(int studentId, string subject, int attendanceDelta, double bonusDelta)
        {
            string sheet = await getSheet(studentId);
            if (sheet == "false")
            {
                Console.WriteLine("❌ Could not find student's grade sheet.");
                return;
            }

            int subjectColumn = await FindSubjectColumn(sheet, subject);
            if (subjectColumn == -1)
            {
                Console.WriteLine($"❌ Subject '{subject}' not found in sheet.");
                return;
            }

            int studentRow = await FindStudentRow(sheet, studentId);
            if (studentRow == -1)
            {
                Console.WriteLine($"❌ Student ID {studentId} not found in sheet.");
                return;
            }

            // ── Update Attendance ──────────────────────────────────────────────
            string attendanceLetter = ColumnIndexToLetter(subjectColumn + 6);
            int currentAttendance = 0;
            int.TryParse(await ReadCell(sheet, attendanceLetter, studentRow), out currentAttendance);
            await WriteCell(sheet, attendanceLetter, studentRow, currentAttendance + attendanceDelta);

            // ── Update Bonus ───────────────────────────────────────────────────
            if (bonusDelta > 0)
            {
                string bonusLetter = ColumnIndexToLetter(subjectColumn + 5);
                double currentBonus = 0;
                double.TryParse(await ReadCell(sheet, bonusLetter, studentRow), out currentBonus);
                await WriteCell(sheet, bonusLetter, studentRow, currentBonus + bonusDelta);
            }

            
            // Recalculate overall percentage and update Students sheet
            double overallPercentage = await CalculateOverallPercentage(studentId);
            await dataBase.UpdateStudentPercentage(studentId, overallPercentage);
        }

        public static async Task ViewMyAttendance(int studentId, string subject)
        {
            string sheet = await getSheet(studentId);
            if (sheet == "false")
            {
                Console.WriteLine("❌ Could not find student's grade sheet.");
                return;
            }

            int subjectColumn = await FindSubjectColumn(sheet, subject);
            if (subjectColumn == -1) { Console.WriteLine($"❌ Subject '{subject}' not found."); return; }

            int studentRow = await FindStudentRow(sheet, studentId);
            if (studentRow == -1) { Console.WriteLine("❌ Student not found."); return; }

            string attendance = await ReadCell(sheet, ColumnIndexToLetter(subjectColumn + 6), studentRow);
            string bonus      = await ReadCell(sheet, ColumnIndexToLetter(subjectColumn + 5), studentRow);

            Console.WriteLine("\n─── Your Attendance ───────────────────");
            Console.WriteLine($"  Subject:    {subject}");
            Console.WriteLine($"  Attendance: {attendance} session(s)");
            Console.WriteLine($"  Bonus:      +{bonus} points");
            Console.WriteLine("───────────────────────────────────────\n");
        }

        public static async Task ViewStudentAttendance(int studentId, string subject)
        {
            string sheet = await getSheet(studentId);
            if (sheet == "false")
            {
                Console.WriteLine("❌ Could not find student's grade sheet.");
                return;
            }

            int subjectColumn = await FindSubjectColumn(sheet, subject);
            if (subjectColumn == -1) { Console.WriteLine($"❌ Subject '{subject}' not found."); return; }

            int studentRow = await FindStudentRow(sheet, studentId);
            if (studentRow == -1) { Console.WriteLine($"❌ Student ID {studentId} not found."); return; }

            string attendance = await ReadCell(sheet, ColumnIndexToLetter(subjectColumn + 6), studentRow);
            string bonus      = await ReadCell(sheet, ColumnIndexToLetter(subjectColumn + 5), studentRow);

            Console.WriteLine($"\n─── Attendance for student ID {studentId} ───");
            Console.WriteLine($"  Subject:    {subject}");
            Console.WriteLine($"  Attendance: {attendance} session(s)");
            Console.WriteLine($"  Bonus:      +{bonus} points");
            Console.WriteLine("───────────────────────────────────────\n");
        }


        // ══════════════════════════════════════════════════════════════
        //  GRADE VIEWING METHODS
        // ══════════════════════════════════════════════════════════════

        public static async Task ViewMyGrades(int studentId)
        {
            var studentsData = await dataBase.ReadSheet("Students!A:H");
            if (studentsData == null) { Console.WriteLine("❌ Could not read Students sheet."); return; }

            string enrolledDepartment = "";
            int enrolledYear = 0;

            foreach (var row in studentsData.Skip(1))
            {
                if (row.Count < 7) continue; // Must have at least 7 columns now
                if (row[0].ToString() != studentId.ToString()) continue;
                
                enrolledDepartment = row[5].ToString().Trim(); // Column F (CS, IS, IT)
                int.TryParse(row[6].ToString(), out enrolledYear); // Column G (1, 2, 3, 4)
                break;
            }

            if (enrolledYear == 0) { Console.WriteLine("❌ Student not found or invalid year."); return; }

            // ── CS, IS, IT students → can view years 1, 2, 3, 4 ─────────────
            if (enrolledDepartment == "CS" || enrolledDepartment == "IS" || enrolledDepartment == "IT")
            {
                Console.WriteLine($"\nYou are in department {enrolledDepartment}.");
                Console.WriteLine("Which year's grades would you like to view?");
                Console.WriteLine("  [1] Year 1\n  [2] Year 2\n  [3] Year 3\n  [4] Year 4");
                Console.Write("Your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int chosenYear) || chosenYear < 1 || chosenYear > 4)
                {
                    Console.WriteLine("❌ Invalid choice. Please pick a year from 1 to 4.");
                    return;
                }

                await PrintGradesFromSheet(studentId, ResolveSheetName(enrolledDepartment, chosenYear));
                return;
            }

            // ── Year 1 and 2 students → can view up to their current year ────
            Console.WriteLine($"\nYou are currently in year {enrolledYear}.");
            Console.WriteLine("Which year's grades would you like to view?");
            for (int y = 1; y <= enrolledYear; y++)
                Console.WriteLine($"  [{y}] Year {y}");
            Console.Write("Your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int picked) || picked < 1 || picked > enrolledYear)
            {
                Console.WriteLine($"❌ Invalid choice. You can only view years 1 to {enrolledYear}.");
                return;
            }

            await PrintGradesFromSheet(studentId, ResolveSheetName(enrolledDepartment, picked));
        }

        public static async Task ViewAllStudentGrades(int studentId)
        {
            var studentsData = await dataBase.ReadSheet("Students!A:H");
            if (studentsData == null) { Console.WriteLine("❌ Could not read Students sheet."); return; }

            string enrolledDepartment = "";
            int enrolledYear = 0;

            foreach (var row in studentsData.Skip(1))
            {
                if (row.Count < 7) continue; 
                if (row[0].ToString() != studentId.ToString()) continue;
                
                enrolledDepartment = row[5].ToString().Trim(); 
                int.TryParse(row[6].ToString(), out enrolledYear); 
                break;
            }

            if (enrolledYear == 0) { Console.WriteLine($"❌ Student ID {studentId} not found or invalid year."); return; }

            // ── CS, IS, IT students → can view years 1, 2, 3, 4 ─────────────
            if (enrolledDepartment == "CS" || enrolledDepartment == "IS" || enrolledDepartment == "IT")
            {
                Console.WriteLine($"\nStudent {studentId} is in department {enrolledDepartment}.");
                Console.WriteLine("Which year's grades would you like to view?");
                Console.WriteLine("  [1] Year 1\n  [2] Year 2\n  [3] Year 3\n  [4] Year 4");
                Console.Write("Your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int chosenYear) || chosenYear < 1 || chosenYear > 4)
                {
                    Console.WriteLine("❌ Invalid choice. Please pick a year from 1 to 4.");
                    return;
                }

                await PrintGradesFromSheet(studentId, ResolveSheetName(enrolledDepartment, chosenYear));
                return;
            }

            // ── Year 1 and 2 students → can view up to their current year ────
            Console.WriteLine($"\nStudent {studentId} is in year {enrolledYear}.");
            Console.WriteLine("Which year's grades would you like to view?");
            for (int y = 1; y <= enrolledYear; y++)
                Console.WriteLine($"  [{y}] Year {y}");
            Console.Write("Your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int picked) || picked < 1 || picked > enrolledYear)
            {
                Console.WriteLine($"❌ Invalid choice. You can only view years 1 to {enrolledYear}.");
                return;
            }

            await PrintGradesFromSheet(studentId, ResolveSheetName(enrolledDepartment, picked));
        }

        /// <summary>
        /// Calculates the student's overall percentage based only on subjects
        /// that have a non‑zero total (grades have been entered).
        /// </summary>
        public static async Task<double> CalculateOverallPercentage(int studentId)
        {
            string sheet = await getSheet(studentId);
            if (sheet == "false") return 0;

            var headers = await dataBase.ReadSheet($"{sheet}!1:1");
            if (headers == null || headers.Count == 0) return 0;
            var headerRow = headers[0];

            int studentRow = await FindStudentRow(sheet, studentId);
            if (studentRow == -1) return 0;

            string lastCol = ColumnIndexToLetter(155);
            var rowData = await dataBase.ReadSheet($"{sheet}!A{studentRow}:{lastCol}{studentRow}");
            if (rowData == null || rowData.Count == 0) return 0;
            var dataRow = rowData[0];

            double totalMarks = 0;
            int gradedSubjectsCount = 0;

            for (int col = 2; col < headerRow.Count; col += 7) // subjects start at column 2
            {
                string subjectName = headerRow[col]?.ToString()?.Trim();
                if (string.IsNullOrEmpty(subjectName)) continue;

                // Read the total value (stored in the same column as the subject name, offset 0)
                double total = 0;
                if (col < dataRow.Count)
                {
                    string totalStr = dataRow[col]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(totalStr) &&
                        double.TryParse(totalStr, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out total))
                    {
                        // Only count subjects that have a positive total (grades have been assigned)
                        if (total > 0)
                        {
                            totalMarks += total;
                            gradedSubjectsCount++;
                        }
                    }
                }
            }

            if (gradedSubjectsCount == 0) return 0;

            // Percentage = average of the totals (each subject is out of 100)
            double percentage = totalMarks / gradedSubjectsCount;
            return Math.Clamp(percentage, 0, 100);
        }


        // ══════════════════════════════════════════════════════════════
        //  UTILITY METHODS
        // ══════════════════════════════════════════════════════════════

        public static async Task<string> getSheet(int studentId)
        {
            var values = await dataBase.ReadSheet("Students!A:G");
            if (values == null) { Console.WriteLine("❌ Could not read Students sheet."); return "false"; }

            foreach (var row in values.Skip(1))
            {
                if (row.Count < 7) continue; // need at least 7 columns (A to G)
                if (row[0].ToString() != studentId.ToString()) continue;

                string department = row[5].ToString().Trim(); // column F = CS, IS, IT
                string yearStr    = row[6].ToString().Trim(); // column G = 1, 2, 3, 4

                if (!int.TryParse(yearStr, out int year))
                {
                    Console.WriteLine($"❌ Invalid year '{yearStr}' for student {studentId}.");
                    return "false";
                }

                // year 1 and 2 are shared sheets for everyone
                if (year == 1) return "Grade_1";
                if (year == 2) return "Grade_2";

                // year 3 and 4 are department-specific
                if (department == "CS" || department == "IS" || department == "IT")
                    return $"{department}_{year}"; // e.g. CS_3, IS_4, IT_3

                Console.WriteLine($"❌ Unknown department '{department}' for student {studentId}.");
                return "false";
            }

            Console.WriteLine($"❌ Student ID {studentId} not found.");
            return "false";
        }

        // ── Convert column index to letter: 0 → A, 26 → AA ───────────────
        public static string ColumnIndexToLetter(int index)
        {
            if (index < 0) return "false";
            string letter = "";
            index++;
            while (index > 0)
            {
                int remainder = (index - 1) % 26;
                letter = (char)('A' + remainder) + letter;
                index  = (index - 1) / 26;
            }
            return letter;
        }
    }
}