using ConsoleStudentRecordsApp.Bussiness;
using ConsoleStudentRecordsApp.Models;

namespace ConsoleStudentRecordsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentManeger studentManeger = new StudentManeger();
            studentManeger.LoadFromFile();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("-----------Menu------------");
                
                Console.WriteLine("1\tAdd Student");
                Console.WriteLine("2\tSearch Student By Id");
                Console.WriteLine("3\tUpdate Student");
                Console.WriteLine("4\tDelete Student");
                Console.WriteLine("5\tShow All Students");
                Console.WriteLine("6\tExit\n\n");
                
                Console.Write("Select an Option from Above Menu : ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddStudent(studentManeger);
                        break;
                    case 2:
                        SearchStudentById(studentManeger);
                        break;
                    case 3:
                        UpdateStudent(studentManeger);
                        break;
                    case 4:
                        DeleteStudent(studentManeger);
                        break;
                    case 5:
                        ShowAllStudents(studentManeger);
                        break;
                    case 6:
                        studentManeger.SaveToFile();
                        exit = true; 
                        break;
                    default:
                        Console.WriteLine("Enter And Option Between 1 to 6");
                        break;
                }
            }
        }
        private static void AddStudent(StudentManeger manager)
        {
            Console.WriteLine("\nEnter The Following Information to Add a Student.");
            Console.Write("Enter Student Id : ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Student Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter Student Age : ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Student Grade : ");
            string grade = Console.ReadLine();

            Console.Write("Enter Student City : ");
            string city = Console.ReadLine();

            Student student = new Student(id, name, age, grade, city);

            manager.AddStudent(student, manager);

        }
        private static void SearchStudentById(StudentManeger manager)
        {
            Student? student = manager.GetStudentById();

            PrintStudentData(student);
            
        }

        private static void UpdateStudent(StudentManeger manager)
        {
            Student? student = manager.GetStudentById();

            PrintStudentData(student);

            Console.Write($"Enter Name to Update {student.Name} =>  ");
            string name = Console.ReadLine();

            Console.Write($"Enter Name to Update {student.Age} =>  ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Enter Name to Update {student.City} =>  ");
            string city= Console.ReadLine();

            Console.Write($"Enter Name to Update {student.Grade} =>  ");
            string grade = Console.ReadLine();

            student = manager.UpdateStudent(new Student(student.Id, name, age, grade, city));

            if (student != null) { 
                Console.WriteLine("Student Record Updated Successfully");
                PrintStudentData(student,updated:true);
                manager.SaveToFile();

            }
            else
            {
                Console.WriteLine("Error");
            }

        }
        private static void DeleteStudent(StudentManeger manger)
        {
            Console.WriteLine("\n-------Delete Student By Id-------\n\n");

            Console.Write("Enter Student Id : ");
            int id = Convert.ToInt32(Console.ReadLine());

            manger.DeleteStudent(id,manger);
        }
        private static void ShowAllStudents(StudentManeger manager)
        {
            Console.Clear();
            
            List<Student>? students = manager.GetAllStudents();

            if (students!=null) {

                Console.WriteLine("\n----------All Students----------\n");
                AddColumnHeading();

                foreach (Student student in students)
                {
                    student.PrintStudent();
                }

            }
            else
            {
                Console.WriteLine("\nNo Students Are Present in Record.......\n");
            }
        }

        private static void AddColumnHeading()
        {
            Console.WriteLine($"{"ID",-10} | {"Name",-10} | {"Age",-5} | {"Grade",-5} | {"City",-10}");
            Console.WriteLine(new string('-', 60));
        }
        private static void PrintStudentData(Student? student,bool updated = false)
        {
            Console.Clear();

            if (student != null) {
                
                if (updated)
                {
                    Console.WriteLine($"\n------------Updated Record -------------\n");
                }
                else
                {
                    Console.WriteLine($"\n------------Student of ID : {student.Id} -------------\n");
                }

                AddColumnHeading();
                student.PrintStudent();

            }
            else
            {
                Console.WriteLine("\n---------------No Record Found---------------\n");
            }
        }
    }
}
