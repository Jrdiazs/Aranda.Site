using Aranda.Data;
using Aranda.Services.ModelView;
using Aranda.Services.Responses;
using Aranda.Tools;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Aranda.Services
{
    public class RolServices : BaseServices, IRolServices
    {
        private readonly IRolRepository _rolRepository;

        public RolServices(IRolRepository rolRepository, IMapper mapper) : base(mapper)
        {
            _rolRepository = rolRepository ?? throw new ArgumentNullException(nameof(rolRepository));
        }

        public RolResponseList GetRolesAll()
        {
            var response = new RolResponseList();
            try
            {
                var query = _rolRepository.GetAll();

                var data = Mapper.Map<List<RolModelView>>(query);
                response.Ok(data);
            }
            catch (Exception ex)
            {
                Logger.ErrorFatal(ex);
                response.Error(ex);
            }

            return response;
        }

        #region [Dispose]

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (_rolRepository != null)
                        _rolRepository.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UserAppServices()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion [Dispose]
    }

    public interface IRolServices : IDisposable
    {
        RolResponseList GetRolesAll();
    }
}