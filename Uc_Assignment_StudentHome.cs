using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class Uc_Assignment_StudentHome : UserControl
    {
        public Uc_Assignment_StudentHome()
        {
            InitializeComponent();

            dgvSubject.ReadOnly = true;
            dgvSubject.AllowUserToAddRows = false;
            dgvSubject.AllowUserToDeleteRows = false;
            dgvSubject.RowHeadersVisible = false;
        }

        public async Task LoadAssignmentsForStudent(int studentId)
        {
            try
            {
                // 1. Get student info
                var school = new Schoollogic();
                List<string> studentInfo = await school.Getstudentsinfo(studentId);
                if (studentInfo == null || studentInfo.Count < 6)
                {
                    MessageBox.Show("Could not retrieve student details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string major = studentInfo[5]?.Trim();   // Column F (CS, IS, IT)
                int year = 0;
                int.TryParse(studentInfo[4], out year);   // Column G (1, 2, 3, 4)
                if (year == 0)
                {
                    MessageBox.Show("Invalid student year.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. For years 3/4, we need a valid department code from the student
                int studentDeptId = 0;
                if (year >= 3)
                {
                    if (string.IsNullOrEmpty(major))
                    {
                        MessageBox.Show("Major is required for year 3/4.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    switch (major.ToUpper())
                    {
                        case "CS": studentDeptId = 5; break;
                        case "IS": studentDeptId = 6; break;
                        case "IT": studentDeptId = 7; break;
                        default:
                            MessageBox.Show("Unknown major: " + major, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
                }

                // 3. Read assignments
                var allAssignments = await dataBase.ReadSheet("Assignments!A:G");
                if (allAssignments == null || allAssignments.Count <= 1)
                {
                    MessageBox.Show("No assignments found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvSubject.Rows.Clear();
                bool found = false;

                for (int i = 1; i < allAssignments.Count; i++)
                {
                    var row = allAssignments[i];
                    if (row.Count < 7) continue;

                    // Parse the assignment's year and department code
                    if (!int.TryParse(row[6]?.ToString(), out int sheetYear)) continue; // column G: Year
                    int sheetDept = 0;
                    if (row[5] != null) int.TryParse(row[5].ToString(), out sheetDept); // column F: Department code (0 for year 1/2)

                    // Filter condition
                    bool matches = false;
                    if (year <= 2)
                    {
                        // Year 1 or 2: only year must match
                        matches = (sheetYear == year);
                    }
                    else // year 3 or 4
                    {
                        // Year and department must both match
                        matches = (sheetYear == year && sheetDept == studentDeptId);
                    }

                    if (matches)
                    {
                        string subject = row[2]?.ToString() ?? "";
                        string title   = row[3]?.ToString() ?? "";
                        string link    = row[4]?.ToString() ?? "";

                        int rowIndex = dgvSubject.Rows.Add(subject, title, link);
                        dgvSubject.Rows[rowIndex].Cells[2].Style.ForeColor = Color.Blue;
                        dgvSubject.Rows[rowIndex].Cells[2].Style.Font = new Font(dgvSubject.Font, FontStyle.Underline | FontStyle.Bold);
                        dgvSubject.Rows[rowIndex].Cells[2].Tag = link;
                        found = true;
                    }
                }

                if (!found)
                    MessageBox.Show("No assignments matching your year and department yet.", "No Assignments", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading assignments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        // Click anywhere in a cell → open link if it's the "Link" column (index 2)
        private void dgvSubject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)   // Column2 = Link
            {
                string url = dgvSubject.Rows[e.RowIndex].Cells[2].Value?.ToString();
                if (!string.IsNullOrEmpty(url))
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
