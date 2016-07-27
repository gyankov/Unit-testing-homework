using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAndCourses
{
   public  class School
    {

        private ICollection<Student> students;
        private ICollection<Course> courses;


        public School()
        {
            this.students = new List<Student>();
            this.courses = new List<Course>();
        }


        public ICollection<Student> Students
        {
            get
            {
                return new List<Student>(this.students);
            }
        }

        public ICollection<Course> Courses
        {
            get
            {
                return new List<Course>(this.courses);
            }
        }

        public void AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException();
            }

            if (this.students.Any(x=> x.Id == student.Id))
            {
                throw new InvalidOperationException("There is already a student with this id.");
            }

            this.students.Add(student);
        }

        public void AddCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException();
            }

            this.courses.Add(course);
        }

    }
}
