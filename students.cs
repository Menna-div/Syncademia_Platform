using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace syncademia
{
public class Student {
    public int Id { get; set; }
    public string Name { get; set; }
    public double Gpa { get; set; }
    public string Phone { get; set; }
    public string Major { get; set; }
    public int Year { get; set; }
    public Subjects subjects;

    public Student(int id, string name, double gpa,string phone, string Major, int year) {
        Id = id;
        Name = name;
        Gpa = gpa;
        Phone = phone;
        Major = Major;
        Year = year;
        
    }
    public string subject_Name { get; set; }
    public List<assignment> MyAssignments = new List<assignment>();
    
}

}