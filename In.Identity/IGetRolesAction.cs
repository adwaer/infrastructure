using System.Collections.Generic;

namespace In.Identity
{
    public interface IGetRolesAction<in T>
    {
        IList<string> Execute(T user);
    }
}