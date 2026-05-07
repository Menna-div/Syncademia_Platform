using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;
using syncademia;

public class Subjects
{
    // ── Static factory: call this instead of new Subjects(...) ───────────
    public static async Task Initialize(string grade, int id, string name, int year)
    {
        string sheet;

        if (year == 1)
            sheet = "Grade_1";
        else if (year == 2)
            sheet = "Grade_2";
        else if (year == 3 || year == 4)
        {
            if (string.IsNullOrEmpty(grade))
            {
                Console.WriteLine("❌ Department is required for year 3 and 4.");
                return;
            }
            // builds sheet name like CS_3, IT_4, IS_3 etc.
            sheet = $"{grade.ToUpper()}_{year}";
        }
        else
        {
            Console.WriteLine("❌ Invalid year.");
            return;
        }

        await updateDB(id, name, sheet);
    }

    // ── Writes the student row to the correct year sheet ─────────────────
    public static async Task updateDB(int id, string name, string sheet)
    {
        var newRow = await dataBase.ReadSheet($"'{sheet}'!A:A"); 
        
        // Safely calculate the target row even if the sheet is completely empty
        int rowCount = (newRow == null) ? 0 : newRow.Count;
        int targetRow = rowCount + 1;

        var values = new List<IList<object>>
        {
            new List<object> { id, name } 
        };

        await dataBase.WriteSheet($"'{sheet}'!A{targetRow}", values);
    }

    // ── Maps a subject name to its grade and semester ─────────────────────
    public static (int grade, int year) GetGradeAndSemesterForSubject(string subjectName)
    {
        var g1   = new List<string> { "CS", "DM", "physics", "programming", "english", "HR", "OOP", "IS", "IT", "logic", "PS (1)", "calculus" };
        var g2   = new List<string> { "OS (1)", "DSA", "LA", "COA", "WP", "PS (2)", "DBS", "DC", "CG", "SEPI" , "Elective (1)"};
        var g3CS = new List<string> { "SE (1)", "CNS", "AAD", "LP", "AL", "AI (1)", "DSP", "PLD", "OS (2)", "CS_Elective (1)" };
        var g3IS = new List<string> { "IT_2", "CN", "SALD", "DBS (2)", "RM", "IIS", "EBIS", "NP", "SDI", "IS_Elective (1)" };
        var g3IT = new List<string> { "IT_CN", "SE_2", "IT_DBS (2)", "Electronics", "RM_2", "SC", "IP", "NP_2", "HMI", "IT_Elective (1)" };
        var g4CS = new List<string> { "NLP", "DS", "AI (2)", "STCS", "CS_Elective (2)", "CS_GP (1)", "KBS", "CC", "CALT", "CS_Elective (3)", "CS_Elective (4)", "CS_GP (2)" };
        var g4IS = new List<string> { "IS_Multimedia", "IS_MC", "DDBS", "PM", "IS_Elective (2)", "IS_GP (1)", "IS_CS", "IS_DM", "GIS", "STIS", "Elective (3)", "GP (2)" };
        var g4IT = new List<string> { "IT_Multimedia", "IT_MC", "PR", "MI", "IT_Elective (2)", "IT_GP (1)", "IT_CS", "IT_DM", "MA", "STIS", "Elective (3)", "GP (2)" };

        string lower = subjectName.ToLower().Trim();

        if (g1.Any(s   => lower.Contains(s.ToLower()))) return (0, 1);
        if (g2.Any(s   => lower.Contains(s.ToLower()))) return (0, 2);
        if (g3CS.Any(s => lower.Contains(s.ToLower()))) return (5, 3);
        if (g3IS.Any(s => lower.Contains(s.ToLower()))) return (6, 3);
        if (g3IT.Any(s => lower.Contains(s.ToLower()))) return (7, 3);
        if (g4CS.Any(s => lower.Contains(s.ToLower()))) return (5, 4);
        if (g4IS.Any(s => lower.Contains(s.ToLower()))) return (6, 4);
        if (g4IT.Any(s => lower.Contains(s.ToLower()))) return (7, 4);

        return (0, 0);
        // 5 = CS, 6 = IS, 7 = IT
    }
}