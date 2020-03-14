using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using iHealthAccount.DataAccess;

namespace iHealthAccount.BusinessLogic
{
    public static class  Common
    {
        /// <summary>
        /// Background color for the screen
        /// </summary>
        public static Color BGColor
        {
            get
            {
                return Color.FromArgb(122, 150, 223);
            }
        }

        /// <summary>
        /// User roles supported by system
        /// </summary>
        public enum UserRole
        {
            Admin, Provider, FrontOffice
        }

        /// <summary>
        /// Returns the UserRole for the specified RoleId
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <returns>UserRole</returns>
        public static UserRole GetUserRole(int roleId)
        {
            UserRole userRole = new UserRole();
            string sqlQuery = "SELECT Role from RoleDetails Where RoleId=" + roleId.ToString();
            object role = (new DBHelper()).ExecuteScalar(sqlQuery);
            if (role != null)
            {
                switch (role.ToString().ToUpper())
                {
                    case "ADMIN":
                        userRole = UserRole.Admin;
                        break;
                    case "SUPERVISOR":
                        userRole= UserRole.FrontOffice;
                        break;
                    case "ACCOUNTANT":
                        userRole = UserRole.Provider;
                        break;
                    default :
                        userRole = UserRole.Provider;
                        break;
                }
            }

            return userRole;
        }
    }
}
