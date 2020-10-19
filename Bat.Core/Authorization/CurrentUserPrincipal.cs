using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;

namespace Bat.Core
{
    public class CurrentUserPrincipal : ICurrentUserPrincipal
    {
        public object UserId { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Picture { get; set; }

        public object CustomField { get; set; }

        public List<string> Roles { get; set; }

        public IIdentity Identity { get; private set; }

        public List<UserAction> UserActionList { get; set; }

        public void SetIdentity(string username) => Identity = new GenericIdentity(username);

        public bool IsInRole(string role) => Roles.Any(x => x == role);

        public bool? IsAuthorized(string controller, string action)
        {
            if (UserActionList == null || UserActionList.Count() == 0) return null;

            return UserActionList.Any(x => x.Controller == controller && x.Action == action);
        }

    }

}