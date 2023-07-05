using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models;

public class Student
{
    public Student(int id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
    }

    public Student()
    {
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
}