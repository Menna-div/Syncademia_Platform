using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class Uc_Grading_StudentHome : UserControl
    {
        public Uc_Grading_StudentHome()
        {
            InitializeComponent();

            // Make the grid fully read‑only
            dgvSubject.ReadOnly = true;
            dgvSubject.AllowUserToAddRows = false;
            dgvSubject.AllowUserToDeleteRows = false;
            dgvSubject.RowHeadersVisible = false;
            dgvSubject.ColumnHeadersVisible = true;   // keep headers so student knows the columns
        }

        /// <summary>
        /// Loads the student's grades from the appropriate grade sheet.
        /// </summary>
        public async Task LoadGradesForStudent(int studentId)
        {
            try
            {
                // 1. Get student info (year / major)
                var school = new Schoollogic();
                List<string> studentInfo = await school.Getstudentsinfo(studentId);
                if (studentInfo == null || studentInfo.Count < 6)
                {
                    MessageBox.Show("Could not retrieve student details.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string major = studentInfo[5];          // index 5 = Major
                int year = 0;
                int.TryParse(studentInfo[4], out year); // index 4 = Year

                if (year == 0)
                {
                    MessageBox.Show("Invalid student year.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Determine the correct sheet name (same logic as everywhere)
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

                // 3. Read the header row and find the student's row
                var headers = await dataBase.ReadSheet($"{sheet}!1:1");
                if (headers == null || headers.Count == 0)
                {
                    MessageBox.Show("No subjects found in the grade sheet.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var headerRow = headers[0];

                // Find student row (column A)
                int studentRow = -1;
                var idColumn = await dataBase.ReadSheet($"{sheet}!A:A");
                if (idColumn != null)
                {
                    for (int i = 1; i < idColumn.Count; i++)   // skip header
                    {
                        if (idColumn[i]?.Count > 0 &&
                            idColumn[i][0]?.ToString() == studentId.ToString())
                        {
                            studentRow = i + 1;   // Google Sheets rows start at 1
                            break;
                        }
                    }
                }

                if (studentRow == -1)
                {
                    MessageBox.Show("Your grades have not been published yet.", "No Grades",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 4. Read the entire student row (columns A to last possible column)
                string lastColLetter = AttendanceSystem.ColumnIndexToLetter(155);
                var rowData = await dataBase.ReadSheet($"{sheet}!A{studentRow}:{lastColLetter}{studentRow}");
                if (rowData == null || rowData.Count == 0)
                {
                    MessageBox.Show("No grade data found for you.", "No Grades",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var cells = rowData[0];

                dgvSubject.Rows.Clear();

                for (int col = 2; col < headerRow.Count; col += 7)
                {
                    string subjectName = headerRow[col]?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(subjectName)) continue;

                    string SafeGet(int offset)
                    {
                        int idx = col + offset;
                        return (idx < cells.Count) ? cells[idx]?.ToString()?.Trim() ?? "—" : "—";
                    }

                    string total     = SafeGet(0);
                    string midterm   = SafeGet(1);
                    string practical = SafeGet(2);
                    string oral      = SafeGet(3);
                    string final     = SafeGet(4);
                    string bonus     = SafeGet(5);    // ← added

                    dgvSubject.Rows.Add(subjectName, midterm, oral, practical, final, bonus, total);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading grades: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}