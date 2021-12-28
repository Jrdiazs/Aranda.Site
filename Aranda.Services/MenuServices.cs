using Aranda.Data;
using Aranda.Services.Responses;
using Aranda.Tools;
using AutoMapper;
using System;

namespace Aranda.Services
{
    public class MenuServices : BaseServices, IMenuServices
    {
        private readonly IMenuRepository _menuRepository;

        public MenuServices(IMenuRepository menuRepository, IMapper mapper) : base(mapper)
        {
            _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository));
        }

       
    }

    public interface IMenuServices
    {
    }
}