using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TerraNostra.Security
{
    public class AutorizeView
    {
        /* public static bool IsUserInRole(string[] nombreRoles)
         {
             IEnumerable<rol> allowedroles = nombreRoles.
                 Select(a => (rol)Enum.Parse(typeof(rol), a));
             bool authorize = false;
             var oUsuario = (usuario)HttpContext.Current.Session["User"];
             if (oUsuario != null)
             {
                 foreach (var r in allowedroles)
                 {
                     if ((int)r == oUsuario.rolId)
                         return true;
                 }
             }
             return authorize;
    }*/
    }
}