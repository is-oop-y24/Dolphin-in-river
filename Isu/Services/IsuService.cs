using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class IsuService : IIsuService
    {
        private List<Group> _groups;
        public IsuService()
        {
            _groups = new List<Group>();
        }

        public Group AddGroup(string name)
        {
            CorrectInput(name);
            var newGroup = new Group(name);
            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (!_groups.Contains(group))
            {
                throw new Exception();
            }

            var newStudent = new Student(name, group.GetName());
            group.AddStudent(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            Student findStudent = null;
            foreach (Group group in _groups)
            {
                if (findStudent != null)
                {
                    break;
                }

                findStudent = group.GetStudent(id, null);
            }

            if (findStudent == null)
            {
                throw new IsuException();
            }

            return findStudent;
        }

        public Student FindStudent(string name)
        {
            Student findStudent = null;
            foreach (Group group in _groups)
            {
                if (findStudent != null)
                {
                    break;
                }

                findStudent = group.GetStudent(-1, name);
            }

            if (findStudent == null)
            {
                Console.WriteLine("Student was not found");
            }

            return findStudent;
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.GetName().Equals(groupName))
                {
                    return group.GetList();
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var resultList = new List<Student>();
            foreach (Group group in _groups)
            {
                if (CharToInt(group) == courseNumber.GetCourseNumber())
                {
                    resultList = resultList.Concat(group.GetList()).ToList();
                }
            }

            return resultList;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.GetName().Equals(groupName))
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var resultList = new List<Group>();
            foreach (Group group in _groups)
            {
                if (CharToInt(group) == courseNumber.GetCourseNumber())
                {
                    resultList.Add(group);
                }
            }

            return resultList;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (!_groups.Contains(newGroup))
            {
                throw new IsuException();
            }

            Group oldGroup = FindGroup(student.GetGroup());
            newGroup.AddStudent(student);
            oldGroup.Delete(student);
            student.SetGroup(newGroup.GetName());
        }

        private bool MyIf(string name)
        {
            return (name[0] != 'm' && name[0] != 'M') || (name[1] != '3') || (name[2] < '0')
                   || (name[2] > '9') || (name[3] < '0') || (name[3] > '9');
        }

        private void CorrectInput(string name)
        {
            if (MyIf(name))
            {
                throw new IsuException();
            }

            foreach (Group group in _groups)
            {
                if (group.GetName().Equals(name))
                {
                    throw new IsuException();
                }
            }
        }

        private int CharToInt(Group @group)
        {
            char courseChar = group.GetName()[2];
            int courseInt = courseChar - '0';
            return courseInt;
        }
    }
}