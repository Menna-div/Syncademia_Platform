using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace syncademia
{
    public partial class MentorSubjectsForm : Form
    {
        private string mentorUsername, mentorEmail, mentorPassword, mentorFullName, mentorPhone, mentorRank;
        private List<string> allSubjects;
        private Dictionary<string, string> subjectMapping;

        public MentorSubjectsForm(string username, string email, string password, string name, string phone, string rank)
        {
            InitializeComponent();
            LoadLogo();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            mentorUsername = username;
            mentorEmail = email;
            mentorPassword = password;
            mentorFullName = name;
            mentorPhone = phone;
            mentorRank = rank;

            // Build the mapping (same as earlier)
            subjectMapping = new Dictionary<string, string>
            {
                { "Computer Science (CS)", "CS" },
                { "Discrete Math (DM)", "DM" },
                { "Programming", "programming" },
                { "Physics", "physics" },
                { "English", "english" },
                { "Human Rights (HR)", "HR" },
                { "Object Oriented Programming (OOP)", "OOP" },
                { "Information Systems (IS)", "IS" },
                { "Information Technology (IT)", "IT" },
                { "Logic", "logic" },
                { "Probability & Statistics (1)", "PS (1)" },
                { "Calculus", "calculus" },
                { "Operating Systems (1)", "OS (1)" },
                { "Data Structures & Algorithms (DSA)", "DSA" },
                { "Linear Algebra (LA)", "LA" },
                { "Computer Organization & Architecture (COA)", "COA" },
                { "Web Programming (WP)", "WP" },
                { "Probability & Statistics (2)", "PS (2)" },
                { "Database Systems (DBS)", "DBS" },
                { "Data Communication (DC)", "DC" },
                { "Computer Graphics (CG)", "CG" },
                { "Social & Ethical Professional Issues (SEPI)", "SEPI" },
                { "Elective (1)", "Elective (1)" },
                { "Software Engineering (1)", "SE (1)" },
                { "Computer Networks Security (CNS)", "CNS" },
                { "Advanced Algorithms & Design (AAD)", "AAD" },
                { "Logic Programming (LP)", "LP" },
                { "Algorithmic Languages (AL)", "AL" },
                { "Artificial Intelligence (1)", "AI (1)" },
                { "Digital Signal Processing (DSP)", "DSP" },
                { "Programmable Logic Devices (PLD)", "PLD" },
                { "Operating Systems (2)", "OS (2)" },
                { "CS Elective (1)", "CS_Elective (1)" },
                { "Information Technology (IT2)", "IT_2" },
                { "Computer Networks (CN)", "CN" },
                { "System Analysis & Logical Design (SALD)", "SALD" },
                { "Database Systems (2)", "DBS (2)" },
                { "Research Methods (RM)", "RM" },
                { "Intelligent Information Systems (IIS)", "IIS" },
                { "Electronic Business Information Systems (EBIS)", "EBIS" },
                { "Network Programming (NP)", "NP" },
                { "System Development & Implementation (SDI)", "SDI" },
                { "IS Elective (1)", "IS_Elective (1)" },
                { "IT CN", "IT_CN" },
                { "Software Engineering (SE3)", "SE_2" },
                { "Database Systems (2 IT)", "IT_DBS (2)" },
                { "Electronics", "Electronics" },
                { "Research Methods (RM3)", "RM_2" },
                { "Scientific Computing (SC)", "SC" },
                { "Image Processing (IP)", "IP" },
                { "Network Programming (NP3)", "NP_2" },
                { "Human-Machine Interaction (HMI)", "HMI" },
                { "IT Elective (1)", "IT_Elective (1)" },
                { "Natural Language Processing (NLP)", "NLP" },
                { "Distributed Systems (DS)", "DS" },
                { "Artificial Intelligence (2)", "AI (2)" },
                { "Stochastic Processes (STCS)", "STCS" },
                { "CS Elective (2)", "CS_Elective (2)" },
                { "CS_Graduation Project (1)", "CS_GP (1)" },
                { "Knowledge-Based Systems (KBS)", "KBS" },
                { "Cloud Computing (CC)", "CC" },
                { "Compiler & Language Tools (CALT)", "CALT" },
                { "CS Elective (3)", "CS_Elective(3)" },
                { "CS Elective (4)", "CS_Elective (4)" },
                { "CS_Graduation Project (2)", "CS_GP (2)" },
                { "IS_Multimedia", "IS_Multimedia" },
                { "IS_Microcontrollers (MC)", "IS_MC" },
                { "Distributed Databases (DDBS)", "DDBS" },
                { "Project Management (PM)", "PM" },
                { "IS Elective (2)", "IS_Elective (2)" },
                { "IS_Graduation Project (1)", "IS_GP (1)" },
                { "Cybersecurity (CS)", "IS_CS" },
                { "Data Mining (DM)", "IS_DM" },
                { "Geographical Information Systems (GIS)", "GIS" },
                { "Strategic IT (STIS)", "STIS" },
                { "IS Elective (3)", "IS_Elective (3)" },
                { "IS_Graduation Project (2)", "IS_GP (2)" },
                { "IT_Multimedia", "IT_Multimedia" },
                { "IT_Microcontrollers (MC)", "IT_MC" },
                { "Parallel Computing (PR)", "PR" },
                { "Mobile Intelligence (MI)", "MI" },
                { "IT_Elective (2)", "IT_Elective (2)" },
                { "IT_Graduation Project (1)", "IT_GP (1)" },
                { "Cloud Security (CS)", "IT_CS" },
                { "IT_Data Mining (DM)", "IT_DM" },
                { "Mobile Applications (MA)", "MA" },
                { "Strategic IT (STIT)", "STIT" },
                { "IT_Elective (3)", "IT_Elective (3)" },
                { "IT_Graduation Project (2)", "IT_GP (2)" }
            };

            allSubjects = new List<string>(subjectMapping.Keys);
            foreach (string subj in allSubjects)
                lvSubjects.Items.Add(subj);
        }

        private void LoadLogo()
        {
            try
            {
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "main_logo.png");
                if (File.Exists(logoPath))
                    guna2PictureBox1.Image = System.Drawing.Image.FromFile(logoPath);
            }
            catch { }
        }

        private async void btnSignUp_Click(object sender, EventArgs e)
        {
            List<string> selectedDisplay = new List<string>();
            foreach (ListViewItem item in lvSubjects.Items)
                if (item.Checked) selectedDisplay.Add(item.Text);

            if (selectedDisplay.Count == 0)
            {
                MessageBox.Show("Please select at least one subject you teach.", "No Subjects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> selectedForSheet = new List<string>();
            foreach (string display in selectedDisplay)
                selectedForSheet.Add(subjectMapping[display]);

            // Show loading overlay
            LoadingOverlay overlay = new LoadingOverlay("Creating mentor account...");
            overlay.ShowWithAnimation(this);
            try
            {
                var login = new Login();
                var (success, message) = await login.mentor_signup(
                    mentorUsername, mentorEmail, mentorPassword,
                    mentorFullName, mentorPhone, mentorRank, selectedForSheet);
                overlay.Close();

                if (success)
                {
                    MentorSignUpResult.Success = true;
                    MentorSignUpResult.LoggedInUser = login;
                    // Close the basic info form (owner) and then this form
                    if (this.Owner != null) this.Owner.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sign-up failed: " + message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                overlay.Close();
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}