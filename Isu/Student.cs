namespace Isu
{
    public class Student
    {
        private string name;
        private int _id;
        private int nextID = 0;

        private string _groupname;
        public Student(string name, string groupname)
        {
            this.name = name;
            this._groupname = groupname;
            this._id = ++nextID;
        }

        public int GetID()
        {
            return this._id;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetGroup(string groupname)
        {
            _groupname = groupname;
        }

        public string GetGroup()
        {
            return _groupname;
        }
    }
}