using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    lista = ctx.usuario.Include("rol").OrderByDescending(u => u.identificacion).ToList<usuario>().Where(u => u.estado == 1);
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

        public usuario Save(usuario usuario)
        {
            int retorno = 0;
            usuario oUsuario = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oUsuario = GetUsuarioByID((int)usuario.identificacion);

                if (oUsuario == null)
                {
                    ctx.usuario.Add(usuario);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4
                    //Actualizar Libro
                    ctx.usuario.Add(usuario);
                    ctx.Entry(usuario).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                oUsuario = GetUsuarioByID((int)usuario.identificacion);
            return oUsuario;
        }


    }
}
