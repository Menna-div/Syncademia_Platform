using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class Uc_Subject_StudentHome : UserControl
    {
        public Uc_Subject_StudentHome()
        {
            InitializeComponent();

            // Make the grid read-only and remove the header row
            dgvSubject.ReadOnly = true;
            dgvSubject.ColumnHeadersVisible = false;
            dgvSubject.AllowUserToAddRows = false;
            dgvSubject.AllowUserToDeleteRows = false;
            dgvSubject.RowHeadersVisible = false;
        }

        /// <summary>
        /// Loads the subjects for the given student from the appropriate grade sheet.
        /// </summary>
        public async Task LoadSubjectsForStudent(int studentId)
        {
            try
            {
                // 1. Get the student's year and department
                var school = new Schoollogic();
                List<string> studentInfo = await school.Getstudentsinfo(studentId);
                if (studentInfo == null || studentInfo.Count < 6)
                {
                    MessageBox.Show("Could not retrieve student details.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string major = studentInfo[5];  // index 5 = Major
                int year = 0;
                int.TryParse(studentInfo[4], out year); // index 4 = Year

                if (year == 0)
                {
                    MessageBox.Show("Invalid student year.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Determine the correct sheet name (same logic as AttendanceSystem)
                string sheet;
                if (year == 1)
                    sheet = "Grade_1";
                else if (year == 2)
                    sheet = "Grade_2";
                else if (year == 3 || year == 4)
                {
                    if (string.IsNullOrEmpty(major))
                    {
                        MessageBox.Show("Major is required for year 3/4.", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    sheet = $"{major.ToUpper()}_{year}";
                }
                else
                {
                    MessageBox.Show("Invalid year.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. Read the header row of that sheet
                var headers = await dataBase.ReadSheet($"{sheet}!1:1");
                if (headers == null || headers.Count == 0)
                {
                    MessageBox.Show("No subjects found in the grade sheet.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var headerRow = headers[0];

                // 4. Extract subject names (every 7 columns starting from column index 2)
                List<string> subjects = new List<string>();
                for (int col = 2; col < headerRow.Count; col += 7)
                {
                    string subjectName = headerRow[col]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(subjectName))
                        subjects.Add(subjectName);
                }

                // 5. Populate the DataGridView
                dgvSubject.Rows.Clear();
                foreach (string subj in subjects)
                {
                    dgvSubject.Rows.Add(subj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}