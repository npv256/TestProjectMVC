using System;
using System.Linq;
using Domain;
using Repository.Contexts;
using Repository.Repositories;
using System.Web.Security;

namespace Repository.Providers
{
    public class CustomRoleProvider : RoleProvider
    {

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (UserContext db = new UserContext("DefaultConnection"))
            {
                Teacher teacher = db.Teachers.FirstOrDefault(u => u.Login == username);

                if (teacher != null)
                {
                    roles = new string[] { teacher.Role};
                }
                else
                {
                    Student student = db.Students.FirstOrDefault(u => u.Login == username);
                    if (student != null) roles = new string[] {student.Role};
                }

                return roles;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}