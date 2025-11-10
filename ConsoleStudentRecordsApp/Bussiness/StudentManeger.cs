using ConsoleStudentRecordsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStudentRecordsApp.Bussiness
{
    public class StudentManeger
    {
        private string filePath = "students.csv";

        private List<Student> students;
        public StudentManeger(List<Student>? existingStudents = null)
        {
            students = existingStudents ?? new List<Student>();
        }
        public List<Student>? GetAllStudents()
        {
            return (students.Count>0)?  students : null;
        }

        public void SaveToFile()
        {
            List<string> lines = new List<string>();

            // Add Heading (Optional)
            lines.Add("Id,Name,Age,Grade,City");

            foreach (var s in students)
            {
                string line = $"{s.Id},{s.Name},{s.Age},{s.Grade},{s.City}";
                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
        }
        public void LoadFromFile()
        {
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);

            students.Clear(); // clear old list

            // Skip heading line
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');

                Student s = new Student
                (
                    id: int.Parse(parts[0]),
                    name: parts[1],
                    age: int.Parse(parts[2]),
                    grade: parts[3],
                    city: parts[4]
                );

                students.Add(s);
            }
        }

        public void AddStudent(Student student,StudentManeger manager)
        {
            Console.Clear();
            if (student != null)
            {
                if(students.Find(s => s.Id == student.Id) == null)
                {
                    students.Add(student);
                    Console.WriteLine("\n\t----Student Added Succesfully----\n");
                    manager.SaveToFile();
                }
                else
                {
                    Console.WriteLine($"\n----Student with id : {student.Id} already exist try with another id----\n");
                }

            }
            else
            {
                Console.WriteLine("Invalid/Missing Student Data");
            }
        }

        public Student? GetStudentById() {
            Console.WriteLine("\n---------Student By Id---------\n");
            Console.Write("Enter Student Id : ");
            int idToSearch = Convert.ToInt32(Console.ReadLine());
            return students.Where(s => s.Id == idToSearch).FirstOrDefault() ?? null;
        }

        public Student? UpdateStudent(Student student) {
            Student oldStudent = students.Where(s => s.Id == student.Id).FirstOrDefault() ?? null;

            if (oldStudent != null) {
                oldStudent.Name = student.Name;
                oldStudent.Age = student.Age;
                oldStudent.City = student.City;
                oldStudent.Grade = student.Grade;
            }

            return oldStudent;
        }

        public void DeleteStudent(int id,StudentManeger maneger) { 
            Student? student = students.FirstOrDefault(s => s.Id == id);
            Console.Clear();

            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine($"Student with Id : {id} is Delete Successfully\n");
                maneger.SaveToFile();
            }
            else
            {
                Console.WriteLine($"Student with this Id :  {id} is not found.\n");
            }
        }
    }
}
