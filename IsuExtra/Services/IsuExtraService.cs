using System.Collections.Generic;
using Isu;
namespace IsuExtra
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<OGNP> _ognps;

        public IsuExtraService()
        {
            _ognps = new List<OGNP>();
        }

        public OGNP AddOgnp(MegaFaculty letterMegaFaculty)
        {
            var newOGNP = new OGNP(letterMegaFaculty);
            _ognps.Add(newOGNP);
            return newOGNP;
        }

        public List<Student> StudentsNotJoin(Group group)
        {
            List<Student> notJoin = new List<Student>();
            foreach (var student in group.GetList())
            {
                short flag = 0;
                foreach (var ognp in _ognps)
                {
                    if (ognp.ConsistStudent(student))
                    {
                        flag++;
                    }
                }

                if (flag == 0)
                {
                    notJoin.Add(student);
                }
            }

            return notJoin;
        }

        public bool CheckContainsOgnp(OGNP ognp)
        {
            foreach (var item in _ognps)
            {
                if (ognp.GetId() == item.GetId())
                {
                    return true;
                }
            }

            return false;
        }
    }
}