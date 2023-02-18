using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class ServiceUsuario : IServiceUsuario
	{
        public Autor GetAutorByID(int id)
        {
            IRepositoryAutor repository = new RepositoryAutor();
            return repository.GetAutorByID(id);
        }

        public IEnumerable<Autor> GetAutors()
        {
            IRepositoryAutor repository = new RepositoryAutor();
            return repository.GetAutors();
        }

    }
}
