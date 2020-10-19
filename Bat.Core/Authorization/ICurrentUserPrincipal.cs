using System.Security.Principal;
using System.Collections.Generic;

namespace Bat.Core
{
    public interface ICurrentUserPrincipal : IPrincipal
    {
        object UserId { get; set; }
        string FullName { get; set; }
        string UserName { get; set; }
        string Picture { get; set; }
        object CustomField { get; set; }
        List<string> Roles { get; set; }
        List<UserAction> UserActionList { get; set; }
        void SetIdentity(string username);
        bool? IsAuthorized(string controller, string action);
    }
}
