using Aranda.Models;
using System;

namespace Aranda.Data
{
    public class RolRepository : RepositoryGeneric<Rol>, IRolRepository, IDisposable
    {
        public RolRepository()
        { }
    }

    public interface IRolRepository : IRepositoryGeneric<Rol>, IDisposable
    { }
}