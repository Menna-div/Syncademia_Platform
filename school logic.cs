using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace syncademia
{
    public class Schoollogic
    {
        public List<Student> students = new List<Student>();
        public List<mentor> mentors = new List<mentor>();

        // Synchronous – only adds to local list (no network call)
        public void AddStudent(int id, string name, double gpa, string phone, string Major, int year)
        {
            students.Add(new Student(id, name, gpa, phone, Major, year));
            Console.WriteLine("student added successfully!");
        }

        // Synchronous – only adds to local list
        public void add_mentor(string rank, string name, string email, string phone, List<string> subjects)
        {
            mentors.Add(new mentor(rank, name, email, phone, subjects));
        }

        // Async – reads from Google Sheets
        // Async – reads from Google Sheets and RETURNS the data
        public async Task<List<string>> Getstudentsinfo(int studentid)
        {
            var values = await dataBase.ReadSheet("Students!A:H");
            if (values == null) return null; // Returns null if no data

            foreach (var row in values.Skip(1)) // skip header
            {
                if (row.Count < 1) continue;

                if (int.TryParse(row[0].ToString(), out int id) && id == studentid)
                {
                    // We found the student! Package the data into a list and send it back.
                    List<string> studentData = new List<string>
                    {
                        row.Count > 4 ? row[4].ToString() : "N/A", // Name (Index 0)
                        row[0].ToString(),                         // ID (Index 1)
                        row.Count > 1 ? row[1].ToString() : "N/A", // Username (Index 2)
                        row.Count > 2 ? row[2].ToString() : "N/A", // Email (Index 3)
                        row.Count > 6 ? row[6].ToString() : "N/A", // Year (Index 4)
                        row.Count > 5 ? row[5].ToString() : "N/A"  // Department/Semester (Index 5)
                    };
                    return studentData; 
                }
            }
            return null; // Return null if ID not found
        }

        public async Task<List<string>> Getmentorinfo(int id)
        {
            var values = await dataBase.ReadSheet("Mentors!A:H");
            if (values == null) return null;

            foreach (var row in values.Skip(1)) // skip header
            {
                if (row.Count < 1) continue;

                if (int.TryParse(row[0].ToString(), out int mentorId) && mentorId == id)
                {
                    // ترتيب البيانات في شيت المينتور:
                    // 0=ID, 1=Username, 2=Email, 3=Phone, 4=Password, 5=Subjects, 6=Name, 7=Rank
                    List<string> mentorData = new List<string>
                    {
                        row.Count > 6 ? row[6].ToString() : "N/A", // Name (Index 0)
                        row.Count > 1 ? row[1].ToString() : "N/A", // Username (Index 1)
                        row.Count > 2 ? row[2].ToString() : "N/A", // Email (Index 2)
                        row.Count > 3 ? row[3].ToString() : "N/A", // Phone (Index 3)
                        row.Count > 7 ? row[7].ToString() : "N/A", // Rank (Index 4)
                        row.Count > 5 ? row[5].ToString() : "N/A"  // Subjects (Index 5)
                    };
                    return mentorData;
                }
            }
            return null; // لو ملقاش المينتور
        }

        public async Task ViewAssignments(int studentGrade, int studentSemester)
        {
            var values = await dataBase.ReadSheet("Assignments!A:G");
            if (values == null || values.Count <= 1)
            {
                Console.WriteLine("No assignments yet.");
                return;
            }

            bool found = false;
            Console.WriteLine("\n--- Your Assignments ---");

            foreach (var row in values.Skip(1))
            {
                if (row.Count < 7) continue;

                if (int.TryParse(row[5].ToString(), out int grade) &&
                    int.TryParse(row[6].ToString(), out int semester) &&
                    grade == studentGrade && semester == studentSemester)
                {
                    found = true;
                    Console.WriteLine($"\nTitle:   {(row.Count > 3 ? row[3] : "N/A")}");
                    Console.WriteLine($"Subject: {(row.Count > 2 ? row[2] : "N/A")}");
                    Console.WriteLine($"Link:    {(row.Count > 4 ? row[4] : "N/A")}");
                }
            }

            if (!found)
                Console.WriteLine("No assignments for your grade and semester yet.");
        }
    }
}