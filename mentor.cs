using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace syncademia
{
    public class mentor
    {
        public string rank;
        public string name;
        public string email;
        public string phone;
        public List<string> subjects;

        public mentor(string rank, string name, string email, string phone, List<string> subjects)
        {
            this.rank = rank;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.subjects = subjects;
        }

        public async Task AddByMentor(int mentorId)
        {
            Console.WriteLine("\nYour subjects:");
            for (int i = 0; i < subjects.Count; i++)
                Console.WriteLine($"[{i + 1}] {subjects[i]}");

            Console.Write("Choose a subject number: ");
            if (!int.TryParse(Console.ReadLine(), out int subjectChoice) || subjectChoice < 1 || subjectChoice > subjects.Count)
            {
                Console.WriteLine("❌ Invalid subject number.");
                return;
            }
            string chosenSubject = subjects[subjectChoice - 1];

            (int grade, int year) = Subjects.GetGradeAndSemesterForSubject(chosenSubject);

            Console.Write("Enter Assignment Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("\nPaste your Google Drive file link and press Enter:");
            Console.WriteLine("(Make sure the file sharing is set to 'Anyone with the link can view')");
            string driveLink = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(driveLink) || !driveLink.StartsWith("http"))
            {
                Console.WriteLine("❌ Invalid link. Please paste a valid Google Drive link.");
                return;
            }

            int newId = await dataBase.GetLatestId("Assignments") + 1;
            int newRow = newId + 1;

            var values = new List<IList<object>>
            {
                new List<object> { newId, mentorId, chosenSubject, title, driveLink, grade, year }
            };

            await dataBase.WriteSheet($"Assignments!A{newRow}", values);
            Console.WriteLine($"✅ Assignment '{title}' saved successfully!");
        }

        public async Task GenerateAttendanceQR(int mentorId)
        {
            Console.WriteLine("\nYour subjects:");
            for (int i = 0; i < subjects.Count; i++)
                Console.WriteLine($"[{i + 1}] {subjects[i]}");

            Console.Write("Choose a subject number: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > subjects.Count)
            {
                Console.WriteLine("❌ Invalid subject number.");
                return;
            }
            string chosenSubject = subjects[choice - 1];

            Console.Write("Enter bonus grade points (0 for no bonus): ");
            if (!double.TryParse(Console.ReadLine(), out double bonus))
            {
                Console.WriteLine("❌ Invalid bonus value. Using 0.");
                bonus = 0;
            }

            await AttendanceSystem.GenerateQRCode(mentorId, chosenSubject, bonus);
        }

        public async Task EditStudentAttendance()
        {
            Console.Write("Enter student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("❌ Invalid student ID.");
                return;
            }

            Console.Write("Enter the subject: ");
            string subject = Console.ReadLine();

            await AttendanceSystem.ViewStudentAttendance(studentId, subject);

            Console.WriteLine("\nYour subjects:");
            for (int i = 0; i < subjects.Count; i++)
                Console.WriteLine($"[{i + 1}] {subjects[i]}");

            Console.Write("Choose a subject number: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > subjects.Count)
            {
                Console.WriteLine("❌ Invalid subject number.");
                return;
            }
            string chosenSubject = subjects[choice - 1];

            Console.Write("Add sessions (+) or remove (-) how many? (e.g. 2 or -1): ");
            if (!int.TryParse(Console.ReadLine(), out int delta))
            {
                Console.WriteLine("❌ Invalid delta value.");
                return;
            }

            await AttendanceSystem.ManualAttendance(studentId, chosenSubject, delta);
        }

        public async Task EditStudentGrades()
        {
            Console.Write("\nEnter student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("❌ Invalid student ID.");
                return;
            }

            string sheet = await AttendanceSystem.getSheet(studentId);
            if (sheet == "false")
            {
                Console.WriteLine("❌ Student not found. Make sure the student has signed up and their grade is saved.");
                return;
            }

            Console.WriteLine("\nYour subjects:");
            for (int i = 0; i < subjects.Count; i++)
                Console.WriteLine($"[{i + 1}] {subjects[i]}");

            Console.Write("Choose a subject number: ");
            if (!int.TryParse(Console.ReadLine(), out int subjectChoice) || subjectChoice < 1 || subjectChoice > subjects.Count)
            {
                Console.WriteLine("❌ Invalid subject choice.");
                return;
            }
            string chosenSubject = subjects[subjectChoice - 1];

            var headers = await dataBase.ReadSheet($"{sheet}!1:1");
            if (headers == null || headers.Count == 0)
            {
                Console.WriteLine("❌ Could not read sheet headers.");
                return;
            }

            int subjectColumn = -1;
            for (int i = 0; i < headers[0].Count; i++)
            {
                if (headers[0][i].ToString().Trim().ToLower() == chosenSubject.Trim().ToLower())
                {
                    subjectColumn = i;
                    break;
                }
            }

            if (subjectColumn == -1)
            {
                Console.WriteLine($"❌ Subject '{chosenSubject}' not found in the {sheet} sheet headers.");
                Console.WriteLine("   Make sure the subject name in your profile matches exactly what's in the sheet header.");
                return;
            }

            var idColumn = await dataBase.ReadSheet($"{sheet}!A:A");
            if (idColumn == null)
            {
                Console.WriteLine("❌ Could not read student IDs from sheet.");
                return;
            }

            int studentRow = -1;
            for (int i = 1; i < idColumn.Count; i++)
            {
                if (idColumn[i].Count < 1) continue;
                if (idColumn[i][0].ToString() == studentId.ToString())
                {
                    studentRow = i + 1;
                    break;
                }
            }

            if (studentRow == -1)
            {
                Console.WriteLine($"❌ Student ID {studentId} not found in the {sheet} sheet.");
                Console.WriteLine("   The student may not have been added to the grade sheet yet.");
                return;
            }

            string[] fieldNames   = { "total", "MID", "Practical", "Oral", "Final", "Bonus", "Attendance" };
            int[]    fieldOffsets = {  0,       1,      2,           3,      4,       5,       6            };

            for (int f = 0; f < fieldNames.Length; f++)
            {
                string colLetter = AttendanceSystem.ColumnIndexToLetter(subjectColumn + fieldOffsets[f]);
                var cellData = await dataBase.ReadSheet($"{sheet}!{colLetter}{studentRow}");
                string cellValue = (cellData != null && cellData.Count > 0 && cellData[0].Count > 0)
                                    ? cellData[0][0].ToString()
                                    : "0";
                Console.WriteLine($"  [{f + 1}] {fieldNames[f],-12}: {cellValue}");
            }

            Console.WriteLine("───────────────────────────────────────────────\n");

            Console.Write("Choose which field to edit [1-6]: ");
            if (!int.TryParse(Console.ReadLine(), out int fieldChoice) || fieldChoice < 1 || fieldChoice > 6)
            {
                Console.WriteLine("❌ Invalid choice.");
                return;
            }
            fieldChoice--; // zero‑based

            Console.Write($"Enter new value for {fieldNames[fieldChoice + 1]}: ");
            string newValue = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(newValue))
            {
                Console.WriteLine("❌ Value cannot be empty.");
                return;
            }

            string targetColLetter = AttendanceSystem.ColumnIndexToLetter(subjectColumn + fieldOffsets[fieldChoice + 1]);
            await dataBase.WriteSheet($"{sheet}!{targetColLetter}{studentRow}", new List<IList<object>>
            {
                new List<object> { newValue }
            });

            // Recalculate overall percentage and update Students sheet
            double overallPercentage = await AttendanceSystem.CalculateOverallPercentage(studentId);
            await dataBase.UpdateStudentPercentage(studentId, overallPercentage);
        }
    }
}