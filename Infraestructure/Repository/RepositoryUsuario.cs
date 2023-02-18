using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
	public class RepositoryUsuario : IRepositoryUsuario
	{
        public usuario GetUsuarioByID(int id)
        {
            try
            {
                usuario oAutor = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //*** Sintaxis LINQ Query *** https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/basic-linq-query-operations
                    //var infoAutor = from a in ctx.Autor
                    //                where a.IdAutor == id
                    //                select a;
                    //oAutor = infoAutor.First();
                    //*** Sintaxis LINQ Method *** https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/queries-in-linq-to-entities
                    //* Método Find sin ningún otro método, formato automático
                    oAutor = ctx.usuario.Find(id);
                    //* Con las demás partes de una consulta y se debe dar formato
                    //oAutor = ctx.Autor.Where(x => x.IdAutor == id).First<Autor>();
                    //La expresión lambda es una forma más corta de representar un método anónimo utilizando una sintaxis especial.

                }
                return oAutor;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
        public IEnumerable<usuario> GetUsuario()
        {
            try
            {
                IEnumerable<usuario> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Autor 
                    lista = ctx.usuario.ToList<usuario>();
                    //lista = ctx.Autor.ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

		
	}
}
