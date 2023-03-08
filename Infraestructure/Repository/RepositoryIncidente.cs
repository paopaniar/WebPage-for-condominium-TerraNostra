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
    public class RepositoryIncidente : IRepositoryIncidencias
    {
        public IEnumerable<incidente> GetIncidente()
        {
            IEnumerable<incidente> incidente = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener todas las ordenes incluyendo el cliente y el usuario
                    incidente = ctx.incidente.Include("usuario1").ToList();

                }
                return incidente;

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
                throw new Exception(mensaje);
            }
        }

        public incidente GetIncidenteoById(int id)
        {
            incidente oIncidente = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oIncidente = ctx.incidente.
                        Where(l => l.id == id).
                        Include("usuario1").
                        FirstOrDefault();

                }
                return oIncidente;
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

		public incidente Save(incidente incidente)
		{
            int retorno = 0;
            incidente oIncidente = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oIncidente = GetIncidenteoById((int)incidente.id);
                IRepositoryIncidencias _ReporitoryIndicencia = new RepositoryIncidente();

                if (oIncidente == null)
                {

                    //Insertar
                    //Logica para agregar las categorias al libro
                    
                    //Insertar Libro
                    ctx.incidente.Add(incidente);
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
                    ctx.incidente.Add(incidente);
                    ctx.Entry(incidente).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                  
                   
                   
                }
            }

            if (retorno >= 0)
                oIncidente = GetIncidenteoById((int)incidente.id);

            return oIncidente;
        }

		//public incidente Save(incidente incidente, string[] selectedUsuarios)
		//{
		//    int retorno = 0;
		//    incidente oIncidente = null;

		//    using (MyContext ctx = new MyContext())
		//    {
		//        ctx.Configuration.LazyLoadingEnabled = false;
		//        oIncidente = GetIncidenteoById((int)incidente.id);
		//        IRepositoryUsuario _RepositoryUsuarios = new RepositoryUsuario();

		//        if (oIncidente == null)
		//        {

		//            //Insertar
		//            //Logica para agregar las categorias al libro
		//            if (selectedUsuarios != null)
		//            {

		//                incidente.Usuarios= new List<usuario>();
		//                foreach (var usuario in selectedUsuarios)
		//                {
		//                    var usuarioToAdd = _RepositoryUsuarios.GetUsuarioByID(int.Parse(usuario));
		//                    ctx.usuario.Attach(usuarioToAdd); //sin esto, EF intentará crear una categoría
		//                    incidente.Usuarios.Add(usuarioToAdd);// asociar a la categoría existente con el libro


		//                }
		//            }
		//            //Insertar Libro
		//            ctx.incidente.Add(incidente);
		//            //SaveChanges
		//            //guarda todos los cambios realizados en el contexto de la base de datos.
		//            retorno = ctx.SaveChanges();
		//            //retorna número de filas afectadas
		//        }
		//        else
		//        {
		//            //Registradas: 1,2,3
		//            //Actualizar: 1,3,4

		//            //Actualizar incidente
		//            ctx.incidente.Add(incidente);
		//            ctx.Entry(incidente).State = EntityState.Modified;
		//            retorno = ctx.SaveChanges();

		//            //Logica para actualizar Categorias
		//            var selectedUsuariosID = new HashSet<string>(selectedUsuarios);
		//            if (selectedUsuarios != null)
		//            {
		//                ctx.Entry(incidente).Collection(p => p.Usuarios).Load();
		//                var newIncidenteForUsuario = ctx.usuario
		//                 .Where(x => selectedUsuariosID.Contains(x.identificacion.ToString())).ToList();
		//                incidente.Usuarios = newIncidenteForUsuario;

		//                ctx.Entry(incidente).State = EntityState.Modified;
		//                retorno = ctx.SaveChanges();
		//            }
		//        }
		//    }

		//    if (retorno >= 0)
		//        oIncidente = GetIncidenteoById((int)incidente.id);

		//    return oIncidente;
		//} 
	}
}
