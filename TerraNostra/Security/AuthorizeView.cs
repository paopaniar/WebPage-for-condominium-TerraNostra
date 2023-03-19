using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TerraNostra.Enum;

namespace TerraNostra.Security
{
    public class AuthorizeView
    {
        public static bool IsUserInRole(string[] nombreRoles)
        {
            IEnumerable<Roles> allowedroles = nombreRoles.
                Select(a => (Roles)System.Enum.Parse(typeof(Roles), a));
            bool authorize = false;
            var oUsuario = (usuario)HttpContext.Current.Session["User"];
            if (oUsuario != null)
            {
                foreach (var rol in allowedroles)
                {
                    if ((int)rol == oUsuario.rolId)
                        return true;
                }
            }
            return authorize;
        }
    }
}