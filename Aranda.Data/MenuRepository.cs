using Aranda.Models;
using System;

namespace Aranda.Data
{
    public class MenuRepository : RepositoryGeneric<Menu>, IMenuRepository, IDisposable
    {
        public MenuRepository()
        { }
    }

    public interface IMenuRepository : IRepositoryGeneric<Menu>, IDisposable
    { }
}