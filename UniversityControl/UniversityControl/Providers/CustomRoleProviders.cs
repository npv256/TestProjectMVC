using System;
using System.Linq;
using System.Web.Security;
using UniversityControl.Models;
using System.Data.Entity;
using Domain;
using Service.Interfaces;
using Repository.Contexts;
using Repository.Repositories;

namespace UniversityControl.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly TeacherRepository _teachers;
        private readonly StudentRepository _students;
        public CustomRoleProvider()
        {
            UserContext _db = new UserContext();
            _teachers = new TeacherRepository(_db);
            _students = new StudentRepository(_db);
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles;
            Teacher user = _teachers.GetItemList().First(t => t.Login == username);

            if (user != null && user.Role != null)
            {
                roles = new string[] {user.Role};
            }
            else
            {
                Student student = _students.GetItemList().First(t => t.Login == username);
                roles = new string[] {student.Role};
            }

            return roles;
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