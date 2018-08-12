using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using HTTPClientDemo;

namespace HttpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            bool menu = true;
            while (menu == true)
            {
                Console.WriteLine("Please choose what yu would like to do.");
                Console.WriteLine("{1} Get list of students.");
                Console.WriteLine("{2} Post a Student.");
                int intTemp = Convert.ToInt32(Console.ReadLine());

                switch (intTemp)
                {
                    case 1:
                        getStudents();
                        break;
                    case 2:
                        postStudents();
                        break;
                        
                    case 3:
                      //  putStudents();
                        break;
                    default:
                        //do something for all other values
                        //...
                        break;
                }
            }
            
        }




        public static void getStudents()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59491/api/");
                //HTTP GET
                var responseTask = client.GetAsync("student");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Student[]>();
                    readTask.Wait();

                    var students = readTask.Result;

                    foreach (var student in students)
                    {
                        Console.WriteLine(student.StudentName);
                    }
                }
            }
            Console.WriteLine();
        }
        public static void postStudents()
        {
            using (var client = new HttpClient())
            {

                Console.WriteLine("Please enter student name to post");
                string username = Console.ReadLine();
                var student = new Student() { StudentName = username};
                client.BaseAddress = new Uri("http://localhost:59491/api/");
                var postTask = client.PostAsJsonAsync<Student>("student", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    var insertedStudent = readTask.Result;
                    Console.WriteLine("Student {0} inserted", student.StudentName);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }

            }
        }













    }
}
