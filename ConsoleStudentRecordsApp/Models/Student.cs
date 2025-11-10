using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleStudentRecordsApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age {  get; set; }
        public string Grade { get; set; }
        public string City { get; set; }

        public Student(int id,string name,int age,string grade,string city)
        {
            Id = id;
            Name = name;
            Age = age;
            Grade = grade;
            City = city;
        }

        public void PrintStudent()
        {
            Console.WriteLine($"{this.Id,-10} | {this.Name,-10} | {this.Age,-5} | {this.Grade,-5} | {this.City,-10}\n");
        }

    }
}
