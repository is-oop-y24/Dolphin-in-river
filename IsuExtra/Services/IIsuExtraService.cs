using System.Collections.Generic;
using Isu;
namespace IsuExtra
{
    public interface IIsuExtraService
    {
        OGNP AddOgnp(MegaFaculty letterMegaFaculty);
        List<Student> StudentsNotJoin(Group group);
        bool CheckContainsOgnp(OGNP ognp);
    }
}