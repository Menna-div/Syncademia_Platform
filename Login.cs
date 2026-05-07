using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace syncademia
{
    public class Login
    {
        public int id;
        public bool canSignUp = true;
        public bool loggedIn = false;
        public bool is_mentor = false;
        public bool is_student = false;
        public bool is_mentor_login = false;
        Schoollogic initializer = new Schoollogic();
        


        
        
        


        public async Task<(bool success, string message)> signup(string username, string email, string password, string name, string phone, double gpa, int year, string Major)
        {   
            
            if (await dataBase.IsUsernameTaken("Students", username) || await dataBase.IsEmailTaken(email))
                return (false, "Taken!");

            id = await dataBase.GetLatestId("Students") + 1;
            await Subjects.Initialize(Major, id, name, year);

            var values = new List<IList<object>> { 
                new List<object> { 
                    id, username, email, password, name, Major, year, 0.0, phone, 0  // percentage default 0
                } 
            };
            await dataBase.WriteSheet($"Students!A{id + 1}", values);

            // CRITICAL: Set the flags
            loggedIn = true;
            is_student = true;
            is_mentor = false;

            return (true, "Success");
        }

        public async Task<(bool success, string message)> mentor_signup(string username, string email, string password, string name, string phone, string rank, List<string> subjects)
        {
            if (await dataBase.IsUsernameTaken("Mentors", username) || await dataBase.IsEmailTaken(email))
                return (false, "Taken!");

            id = await dataBase.GetLatestId("Mentors") + 1;

            var values = new List<IList<object>> { 
                new List<object> { id, username, email, phone, password, string.Join(",", subjects), name, rank } 
            };
            await dataBase.WriteSheet($"Mentors!A{id + 1}", values);

            // CRITICAL: Set the flags
            loggedIn = true;
            is_mentor = true;
            is_student = false;

            return (true, "Success");
        }


        // ---------- VALIDATION METHODS ----------

        public async Task check_email(string email)
        {
            if (!email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
                loggedIn = false;
                canSignUp = false;
                return;
            }
            if (await dataBase.IsEmailTaken(email))
            {
                Console.WriteLine("sorry, this email is already taken, please try again");
                loggedIn = false;
                canSignUp = false;
                return;
            }
        }

        public void check_password(string password) // no network call – stays sync
        {
            if (password.Length < 6)
            {
                Console.WriteLine("Password must be at least 6 characters long. Please try again.");
                loggedIn = false;
                canSignUp = false;
            }
        }

        public async Task check_username(bool student, string username)
        {
            if (username.Length < 3)
            {
                Console.WriteLine("Username must be at least 3 characters long. Please try again.");
                loggedIn = false;
                canSignUp = false;
                return;
            }
            if (username.Contains(" "))
            {
                Console.WriteLine("Username cannot contain spaces. Please try again.");
                loggedIn = false;
                canSignUp = false;
                return;
            }
            if (student && await dataBase.IsUsernameTaken("Students", username))
            {
                Console.WriteLine("sorry, this username is already taken, please try again");
                loggedIn = false;
                canSignUp = false;
                return;
            }
            else if (!student && await dataBase.IsUsernameTaken("Mentors", username))
            {
                Console.WriteLine("sorry, this username is already taken, please try again");
                loggedIn = false;
                canSignUp = false;
                return;
            }
        }

        // ---------- LOGIN METHODS ----------

        public async Task check_login_email(string email)
        {
            var values = await dataBase.ReadSheet("Students!C:C");
            if (values != null)
            {
                foreach (var row in values.Skip(1))
                {
                    if (row.Count < 1) continue;
                    if (row[0].ToString() == email)
                    {
                        is_mentor_login = false;
                        return;
                    }
                }
            }

            var mentorValues = await dataBase.ReadSheet("Mentors!C:C");
            if (mentorValues != null)
            {
                foreach (var row in mentorValues.Skip(1))
                {
                    if (row.Count < 1) continue;
                    if (row[0].ToString() == email)
                    {
                        is_mentor_login = true;
                        return;
                    }
                }
            }

            canSignUp = false;
            Console.WriteLine("Email not found.");
        }

        public async Task check_login_password(string password, string email)
        {
            if (is_mentor_login)
            {
                await check_Mentor_login_password(password, email);
            }
            else
            {
                var values = await dataBase.ReadSheet("Students!C:D");
                if (values == null) return;

                foreach (var row in values.Skip(1))
                {
                    if (row.Count < 2) continue;
                    if (row[0].ToString() == email)
                    {
                        if (row[1].ToString() == password)
                        {
                            await signin_Student(email, password);
                        }
                        else
                        {
                            canSignUp = false;
                            Console.WriteLine("Invalid password. Please try again.");
                        }
                        return;
                    }
                }
            }
        }

        public async Task check_Mentor_login_password(string password, string email)
        {
            var values = await dataBase.ReadSheet("Mentors!A:F");
            if (values == null) return;

            foreach (var row in values.Skip(1))
            {
                if (row.Count < 5) continue;
                if (row[2].ToString() == email)
                {
                    if (row[4].ToString() == password)
                    {
                        await signin_Mentor(email, password);
                    }
                    else
                    {
                        canSignUp = false;
                        Console.WriteLine("Invalid password. Please try again.");
                    }
                    return;
                }
            }
        }

        public async Task signin_Student(string email, string password)
        {
            var values = await dataBase.ReadSheet("Students!A:H");
            if (values == null) return;

            foreach (var row in values.Skip(1))
            {
                if (row.Count < 4) continue;
                if (row[2].ToString() == email && row[3].ToString() == password)
                {
                    Console.WriteLine("Welcome back!");
                    if (int.TryParse(row[0].ToString(), out int studentId))
                        id = studentId;
                    loggedIn = true;
                    is_student = true;
                    is_mentor = false;
                    return;
                }
            }
        }

        public async Task signin_Mentor(string email, string password)
        {
            var values = await dataBase.ReadSheet("Mentors!A:H");
            if (values == null) return;

            foreach (var row in values.Skip(1))
            {
                if (row.Count < 5) continue;
                if (row[2].ToString() == email && row[4].ToString() == password)
                {
                    Console.WriteLine("Welcome back!");
                    if (int.TryParse(row[0].ToString(), out int mentorId))
                        id = mentorId;
                    loggedIn = true;
                    is_mentor = true;
                    is_student = false;
                    return;
                }
            }
        }

        // ---------- INFO RETRIEVAL (synchronous, uses Schoollogic) ----------

        public async Task<List<string>> GetStudent(int studentId)
        {
            try
            {
                var allData = await dataBase.ReadSheet("Students!A:I");
                if (allData == null || allData.Count <= 1)
                {
                    MessageBox.Show("No data found in Students sheet.");
                    return null;
                }

                for (int i = 1; i < allData.Count; i++) // Start from row 1 (skip header)
                {
                    var row = allData[i];
                    if (row.Count < 1) continue;
                    if (row[0].ToString() == studentId.ToString())
                    {
                        // Map columns to our list (order: ID, Name, Username, Email, Major, Year, Phone, Percentage)
                        List<string> studentInfo = new List<string>
                        {
                            row[0].ToString(),               // A - ID
                            row.Count > 4 ? row[4].ToString() : "",  // E - Name
                            row.Count > 1 ? row[1].ToString() : "",  // B - Username
                            row.Count > 2 ? row[2].ToString() : "",  // C - Email
                            row.Count > 5 ? row[5].ToString() : "",  // F - Major
                            row.Count > 6 ? row[6].ToString() : "",  // G - Year
                            row.Count > 8 ? row[8].ToString() : "",  // I - Phone
                            row.Count > 7 ? row[7].ToString() : "0"  // H - Percentage
                        };
                        return studentInfo;
                    }
                }
                MessageBox.Show($"Student ID {studentId} not found in sheet.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"GetStudent error: {ex.Message}");
            }
            return null;
        }

        public async Task<List<string>> getmentor(int id, Form here)
        {
            LoadingOverlay loading = new LoadingOverlay("Loading...");
            loading.ShowWithAnimation(here);
            here.Enabled = false;
            try
            {
                // بنرجع البيانات اللي جبناها من Schoollogic
                return await initializer.Getmentorinfo(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exeption Error: " + ex);
                return null;
            }
            finally
            {
                loading.Close();
                here.Enabled = true;
            }
            
        }

        public async Task ViewAssignments(int grade, int semester)
        {
            await initializer.ViewAssignments(grade, semester);
        }

        // ---------- PASSWORD RESET WITH EMAIL ----------

        // داخل ملف Login.cs
        public string generated_code; // المتغير الذي يخزن الكود ليتم مقارنته لاحقاً

        public async Task SendVerificationCode(string subject, string email)
        {
            Random random = new Random();
            generated_code = random.Next(100000, 999999).ToString(); // توليد كود من 6 أرقام
            
            // استخدام SendGrid لإرسال الرسالة
            var client = new SendGrid.SendGridClient("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");
            var msg = new SendGrid.Helpers.Mail.SendGridMessage()
            {
                From = new SendGrid.Helpers.Mail.EmailAddress("moaaz.mossad159@gmail.com", "Syncademia"),
                Subject = subject,
                PlainTextContent = $"Welcome to Syncademia! Your registration code is: {generated_code}"
            };
            msg.AddTo(new SendGrid.Helpers.Mail.EmailAddress(email));
            await client.SendEmailAsync(msg);
        }

        public async Task ResetPassword(string email, string new_password)
        {
            var studentsvalues = await dataBase.ReadSheet("Students!A:D");
            if (studentsvalues != null)
            {
                for (int i = 1; i < studentsvalues.Count; i++)
                {
                    if (studentsvalues[i].Count > 2 && studentsvalues[i][2].ToString() == email)
                    {
                        int row = i + 1;
                        await dataBase.WriteSheet($"Students!D{row}", new List<IList<object>>
                        {
                            new List<object> { new_password }
                        });
                        return;
                    }
                }
            }

            var mentorValues = await dataBase.ReadSheet("Mentors!A:E");
            if (mentorValues != null)
            {
                for (int i = 1; i < mentorValues.Count; i++)
                {
                    if (mentorValues[i].Count > 2 && mentorValues[i][2].ToString() == email)
                    {
                        int row = i + 1;
                        await dataBase.WriteSheet($"Mentors!E{row}", new List<IList<object>>
                        {
                            new List<object> { new_password }
                        });
                        return;
                    }
                }
            }
        }
    }
}