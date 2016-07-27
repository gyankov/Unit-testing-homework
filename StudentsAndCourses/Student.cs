using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAndCourses
{
    public class Student
    {
        private string name;
        private string id;

        public Student(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }

                this.name = value;
            } 
        }

        public string Id
        {
            get
            {
                return this.id;
            }
            private set
            {
                var allAreDigits = true;

                foreach (var digit in value)
                {
                    if (!char.IsDigit(digit))
                    {
                        allAreDigits = false;
                    }
                }

                if (!allAreDigits)
                {
                    throw new ArgumentException();
                }

                if (value.Length<0 || value.Length>5)
                {
                    throw new ArgumentException();
                }

                this.id = value;
            }
        }
            
    }
}
