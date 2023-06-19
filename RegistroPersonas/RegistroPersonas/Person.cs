using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroPersonas
{
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string email { get; set; }

        public Person() { }

        public Person(int id, string name, int age, string email)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.email = email;
        }

        public void ShowPerson()
        {
            Console.WriteLine($"Id:{this.id}, Nombre: {this.name}, Edad: {this.age}, Email: {this.email}");
        }

    }
}
