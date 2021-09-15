using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        private readonly string _nameGroup;

        private List<Student> students = new List<Student>();

        public Group(string nameGroup)
        {
            _nameGroup = nameGroup;
        }

        public string GetName()
        {
            return _nameGroup;
        }

        public List<Student> GetList()
        {
            return students;
        }

        public void AddStudent(Student student)
        {
            if (students.Count == 20)
            {
                throw new IsuException("Reach Max Student per Group");
            }

            students.Add(student);
        }

        public Student GetStudent(int id, string name)
        {
            foreach (Student student in students)
            {
                if (student.GetID() == id) return student;
                if (student.GetName() == name) return student;
            }

            return null;
        }

        public void Delete(Student student)
        {
            int idForDelete = -1;
            foreach (Student curStudent in students)
            {
                idForDelete++;
                if (curStudent.GetID() == student.GetID())
                {
                    break;
                }
            }

            students.RemoveAt(idForDelete);
        }
    }
}