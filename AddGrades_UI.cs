using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace syncademia
{
    public partial class AddGrades : UserControl
    {
        private int _mentorId;

        public AddGrades(int mentorId)
        {
            InitializeComponent();
            _mentorId = mentorId;

            // CENTER FIX
            this.Resize += (s, e) => CenterMyControls();
            CenterMyControls();
        }

        private async void txtStudentID_TextChanged(object sender, EventArgs e)
        {
            string idText = txtStudentID.Text.Trim();
            if (int.TryParse(idText, out int studentId))
            {
                Form parentForm = this.FindForm();
                var overlay = new LoadingOverlay("Loading subjects...");
                overlay.ShowWithAnimation(parentForm);
                try
                {
                    await LoadStudentSubjects(studentId);
                    overlay.Close();
                }
                catch (Exception ex)
                {
                    overlay.Close();
                    MessageBox.Show("Error loading subjects: " + ex.Message);
                }
            }
            else
            {
                cmbSubjectName.Items.Clear();
            }
        }

        private async Task LoadStudentSubjects(int studentId)
        {
            try
            {
                string sheet = await AttendanceSystem.getSheet(studentId);
                if (sheet == "false") return;

                var headers = await dataBase.ReadSheet($"'{sheet}'!1:1");
                if (headers == null || headers.Count == 0) return;

                var studentSubjects = new List<string>();
                for (int col = 2; col < headers[0].Count; col += 7)
                {
                    string subjectName = headers[0][col]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(subjectName))
                        studentSubjects.Add(subjectName);
                }

                var mentorData = await dataBase.ReadSheet("Mentors!A:H");
                List<string> mentorSubjects = new List<string>();

                if (mentorData != null)
                {
                    foreach (var row in mentorData.Skip(1))
                    {
                        if (row.Count < 1) continue;
                        if (!int.TryParse(row[0].ToString(), out int id)) continue;
                        if (id != _mentorId) continue;

                        string subjectsRaw = row.Count > 5 ? row[5].ToString() : "";
                        mentorSubjects = subjectsRaw
                            .Split(',')
                            .Select(s => s.Trim())
                            .Where(s => s != "")
                            .ToList();
                        break;
                    }
                }

                cmbSubjectName.Items.Clear();
                foreach (var subject in studentSubjects)
                {
                    if (mentorSubjects.Any(m => m.Equals(subject, StringComparison.OrdinalIgnoreCase)))
                        cmbSubjectName.Items.Add(subject);
                }

                if (cmbSubjectName.Items.Count == 0)
                    MessageBox.Show("No common subjects found between this student and your profile.",
                        "No Subjects", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading subjects: " + ex.Message);
            }
        }

        private async void btnSubmitGrade_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtStudentID.Text, out int studentId) || cmbSubjectName.SelectedItem == null)
                {
                    MessageBox.Show("Please provide a valid Student ID and select a subject.");
                    return;
                }

                string subject = cmbSubjectName.SelectedItem.ToString();
                string gradeType = cmbGradeType.SelectedItem.ToString();
                string newValue = txtGradeValue.Text.Trim();

                if (string.IsNullOrEmpty(newValue))
                {
                    MessageBox.Show("Please enter the grade value.");
                    return;
                }

                string sheet = await AttendanceSystem.getSheet(studentId);

                Form parentForm = this.FindForm();
                var overlay = new LoadingOverlay("Updating grade...");
                overlay.ShowWithAnimation(parentForm);
                try
                {
                    await PerformGradeUpdate(sheet, studentId, subject, gradeType, newValue);
                    overlay.Close();
                    MessageBox.Show($"Grade updated successfully for student {studentId} in {subject}!");
                    txtGradeValue.Clear();
                }
                catch (Exception ex)
                {
                    overlay.Close();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async Task PerformGradeUpdate(string sheet, int studentId, string subject, string field, string value)
        {
            var headers = await dataBase.ReadSheet($"{sheet}!1:1");
            int subjectCol = -1;
            if (headers != null && headers.Count > 0)
            {
                for (int i = 0; i < headers[0].Count; i++)
                {
                    if (headers[0][i].ToString().Trim().Equals(subject.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        subjectCol = i;
                        break;
                    }
                }
            }
            if (subjectCol == -1)
                throw new Exception($"Subject '{subject}' not found in sheet headers.");

            var fieldOffsets = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"MID", 1},
                {"Practical", 2},
                {"Oral", 3},
                {"Final", 4},
                {"Bonus", 5}
            };
            if (!fieldOffsets.TryGetValue(field, out int offset))
                throw new Exception("Invalid grade type – must be MID, Practical, Oral, Final or Bonus.");

            var idColumn = await dataBase.ReadSheet($"{sheet}!A:A");
            int studentRow = -1;
            if (idColumn != null)
            {
                for (int i = 1; i < idColumn.Count; i++)
                {
                    if (idColumn[i].Count > 0 && idColumn[i][0].ToString() == studentId.ToString())
                    {
                        studentRow = i + 1;
                        break;
                    }
                }
            }
            if (studentRow == -1)
                throw new Exception($"Student ID {studentId} not found in sheet.");

            string targetColLetter = AttendanceSystem.ColumnIndexToLetter(subjectCol + offset);
            await dataBase.WriteSheet(
                $"{sheet}!{targetColLetter}{studentRow}",
                new List<IList<object>> { new List<object> { value } });

            double overallPercentage = await AttendanceSystem.CalculateOverallPercentage(studentId);
            await dataBase.UpdateStudentPercentage(studentId, overallPercentage);
        }

        private void CenterMyControls()
        {
            foreach (Control ctrl in this.Controls)
                ctrl.Left = (this.Width - ctrl.Width) / 2;
        }
    }
}