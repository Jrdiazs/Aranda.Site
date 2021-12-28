using AutoMapper;

namespace Aranda.Services
{
    public class BaseServices
    {
        public IMapper Mapper { get; }

        public BaseServices(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}