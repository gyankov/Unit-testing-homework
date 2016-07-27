using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StudentsAndCourses;

namespace StudentsAndCoursesTests
{
    [TestFixture]
    public class SchoolTest
    {

        [Test]
        public void SettingNullForNameShouldThrowException()
        {  
            Assert.Throws<ArgumentException>(() => new Student(null, "11111"));
        }

        [Test]
        public void SettingEmptyNameShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Student("", "11111"));
        }

        [Test]
        public void SettingNonDigitCharInTheIdShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Student("gosho", "gosho"));
        }

        [Test]
        public void SettingIdWithInvalidLengthShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Student("gosho", "123456"));
        }

        [Test]
        public void NameWithValidValueShouldBeSet()
        {
            var pesho = new Student("pesho", "12345");
            var expected = "pesho";

            Assert.True(pesho.Name==expected);
        }

        [Test]
        public void IdWithValidValueSchouldBeSet()
        {
            var pesho = new Student("pesho", "12345");
            var expected = "12345";

            Assert.True(pesho.Id == expected);
        }


        [Test]
        public void AddingNullStudentToCoursShouldThrowException()
        {
            var course = new Course();

            Assert.Throws<ArgumentNullException>(() => course.AddStudent(null));
        }

        [Test]
        public void AddingMoreThan30StudentsInTheSameCourseShouldThrowException()
        {
            var course = new Course();
            var student = new Student("gosho", "12345");


            for (int i = 0; i < 30; i++)
            {
                course.AddStudent(student);
            }

            Assert.Throws<ArgumentException>(() => course.AddStudent(student));

        }

        public void AddingValidStudentShouldWorkProperly()
        {
            var course = new Course();
            var gosho = new Student("gosho", "12345");

            course.AddStudent(gosho);

            Assert.True(course.Students.Contains(gosho));
        }

        [Test]
        public void RemovingNullStudentFromCourse()
        {
            var course = new Course();

            Assert.Throws<ArgumentNullException>(() => course.RemoveStudent(null));
        }

        [Test]
        public void RemovingStudentThatIsNotInTheCourse()
        {
            var course = new Course();
            var student = new Student("gosho", "11111");

            Assert.Throws<InvalidOperationException>(() => course.RemoveStudent(student));
        }

        [Test]
        public void RemovingExistingStudentFromCourseShouldWorkProperly()
        {
            var course = new Course();
            var student = new Student("gosho", "12345");
            course.AddStudent(student);

            course.RemoveStudent(student);

            Assert.False(course.Students.Contains(student));
        }

        [Test]
        public void AddingNullStudentToSchoolShoutThrowException()
        {
            var school = new School();

            Assert.Throws<ArgumentNullException>(() => school.AddStudent(null));
        }

        [Test]
        public void AddingStudentWithOccupiedIdShouldThrowException()
        {
            var school = new School();
            var gosho = new Student("gosho", "12345");
            var pesho = new Student("pesho", "12345");

            school.AddStudent(gosho);

            Assert.Throws<InvalidOperationException>(() => school.AddStudent(pesho));
        }

        
        [Test]
        public void AddingStudentsWithUniqueIdShouldWorkProperly()
        {
            var school = new School();
            var gosho = new Student("gosho", "12345");
            var pesho = new Student("pesho", "12346");

            school.AddStudent(gosho);
            school.AddStudent(pesho);
            var expectedResult = 2;

            Assert.True(school.Students.Count == expectedResult);

        }

        [Test]
        public void AddingNullCourseInSchoolShouldThrowException()
        {
            var school = new School();

            Assert.Throws<ArgumentNullException>(() => school.AddCourse(null));
        }

        [Test]
        public void AddingValidCourseShouldWorkProperly()
        {
            var school = new School();
            var course = new Course();

            school.AddCourse(course);
            var expectedCountOfCourses = 1;


            Assert.True(school.Courses.Count == expectedCountOfCourses);
        }
    }
}
