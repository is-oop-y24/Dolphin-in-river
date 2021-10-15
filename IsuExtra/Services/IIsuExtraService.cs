using System.Collections.Generic;
using Isu;
namespace IsuExtra
{
    public interface IIsuExtraService
    {
        OGNP AddOgnp(MegaFaculty letterMegaFaculty);
        List<Student> StudentsNotJoin(Group group);
        public bool CheckContainsOgnp(OGNP ognp);
    }
}