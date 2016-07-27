using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAndCourses
{
    public  class Course
    {

        ICollection<Student> students;

        public Course()
        {
            this.students = new List<Student>();
        }

        public ICollection<Student> Students
        {
            get
            {
                return new List<Student>(this.students);
            }
        }

        public void AddStudent(Student student)
        {
            if (student==null)
            {
                throw new ArgumentNullException();
            }
       
            if (this.students.Count<30)
            {
                this.students.Add(student);
            }
            else
            {
                throw new ArgumentException("The course is full");
            }
        }


        public void RemoveStudent(Student student)
        {

            if (student == null)
            {
                throw new ArgumentNullException();
            }
            if (this.students.Contains(student))
            {
                this.students.Remove(student);
            }
            else
            {
                throw new InvalidOperationException("This student doesn't exist");
            }
        }



    }
}
