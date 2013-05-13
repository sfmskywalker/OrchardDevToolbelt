using Orchard;
using Orchard.Data;
using Orchard.Roles.Models;
using Orchard.Roles.Services;
using Orchard.Security;
using Orchard.UI.Notify;
using Orchard.Users.Events;

namespace Harvest.OrchardDevToolbelt.Handlers {
    public class UserEventHandler : Component, IUserEventHandler {
        private readonly INotifier _notifier;
        private readonly IRepository<UserRolesPartRecord> _userRolesRepository;
        private readonly IRoleService _roleService;

        public UserEventHandler(INotifier notifier, IRepository<UserRolesPartRecord> userRolesRepository, IRoleService roleService) {
            _notifier = notifier;
            _userRolesRepository = userRolesRepository;
            _roleService = roleService;
        }

        public void Creating(UserContext context) {    
        }

        public void Created(UserContext context) {
            var role = _roleService.GetRoleByName("Harvester");

            if (role == null) {
                _notifier.Warning(T("No role found with the name <strong>Harvester</strong>"));
                return;
            }

            _userRolesRepository.Create(new UserRolesPartRecord { Role = role, UserId = context.User.Id });
            _notifier.Information(T("Thanks for registering! You have been added to the {0} role.", role.Name));
        }

        public void LoggedIn(IUser user) {
            _notifier.Information(T("Welcome, {0}", user.UserName));
        }

        public void LoggedOut(IUser user) {
            _notifier.Information(T("Farewell, {0}", user.UserName));
        }

        public void AccessDenied(IUser user) {
        }

        public void ChangedPassword(IUser user) {
        }

        public void SentChallengeEmail(IUser user) {
        }

        public void ConfirmedEmail(IUser user) {
        }

        public void Approved(IUser user) {
        }
    }
}