using Aranda.Data;
using Aranda.Models;
using Aranda.Services.ModelView;
using Aranda.Services.Request;
using Aranda.Services.Responses;
using Aranda.Tools;
using Aranda.Tools.String;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Aranda.Services
{
    public class UserAppServices : BaseServices, IUserAppServices
    {
        private readonly IUserAppRepository _userAppRepository;

        public UserAppServices(IUserAppRepository userAppRepository, IMapper mapper) : base(mapper)
        {
            _userAppRepository = userAppRepository ?? throw new ArgumentNullException(nameof(userAppRepository));
        }

        public UserAppResponseList GetUserAppAll()
        {
            var response = new UserAppResponseList();
            try
            {
                var query = _userAppRepository.GetUserAppsAll();
                var data = Mapper.Map<List<UserAppModelView>>(query);
                response.Ok(data);
            }
            catch (Exception ex)
            {
                Logger.ErrorFatal(ex);
                response.Error(ex);
            }
            return response;
        }

        public UserAppResponse GetUserFromById(int id)
        {
            var response = new UserAppResponse();
            try
            {
                var query = _userAppRepository.GetUserFromId(id);

                if (query != null)
                {
                    var data = Mapper.Map<UserAppModelView>(query);
                    response.Ok(data);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFatal(ex);
                response.Error(ex);
            }

            return response;
        }

        public UserAppResponseList GetUserAppFromByFullName(string fullName)
        {
            var response = new UserAppResponseList();
            try
            {
                var query = _userAppRepository.GetUserAppsFromByFullName(fullName);
                var data = Mapper.Map<List<UserAppModelView>>(query);
                response.Ok(data);
            }
            catch (Exception ex)
            {
                Logger.ErrorFatal(ex);
                response.Error(ex);
            }
            return response;
        }

        public UserAppResponseList GetUserAppFromByRolId(int? rolId)
        {
            var response = new UserAppResponseList();
            try
            {
                var query = _userAppRepository.GetUserAppsFromByRol(rolId);
                var data = Mapper.Map<List<UserAppModelView>>(query);
                response.Ok(data);
            }
            catch (Exception ex)
            {
                Logger.ErrorFatal(ex);
                response.Error(ex);
            }
            return response;
        }

        public UserAppResponse SaveUserApp(UserAppModelView request)
        {
            var response = new UserAppResponse();

            try
            {
                if (!string.IsNullOrWhiteSpace(request.Pw))
                {
                    if (request.Pw != request.ConfirmPw)
                    {
                        response.Error("Las contraseñas no son iguales");
                        return response;
                    }
                }

                if (!string.IsNullOrWhiteSpace(request.Pw))
                    request.Pw = request.Pw.Base64Encode();

                var model = Mapper.Map<UserApp>(request);

                if (_userAppRepository.Count("WHERE UserId = @id", new { id = model.UserId }) > 0)
                {
                    if (_userAppRepository.Count("WHERE UserName = @name AND UserId <> @id", new
                    {
                        name = model.UserName,
                        id = model.UserId
                    }) > 0)
                    {
                        response.Error($"Ya existe un usuario con el user Name {model.UserName}");
                        return response;
                    }

                    var userBd = _userAppRepository.GetFindId(model.UserId);
                    userBd.Address = model.Address;
                    userBd.Phone = model.Phone;

                    userBd.RolId = model.RolId;
                    userBd.Email = model.Email;
                    userBd.LastName = model.LastName;
                    userBd.FirstName = model.FirstName;
                    userBd.Birthday = model.Birthday;
                    userBd.UserName = model.UserName;

                    _userAppRepository.Update(userBd);

                    response.Ok(Mapper.Map<UserAppModelView>(userBd));
                }
                else
                {
                    if (_userAppRepository.Count("WHERE UserName = @name", new
                    {
                        name = model.UserName
                    }) > 0)
                    {
                        response.Error($"Ya existe un usuario con el user Name {model.UserName}");
                        return response;
                    }

                    var id = _userAppRepository.Insert<int>(model);
                    model.UserId = id;

                    response.Ok(Mapper.Map<UserAppModelView>(model));
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFatal(ex);
                response.Error(ex);
            }
            return response;
        }

        public UserAppResponse Login(UserLogin login)
        {
            var response = new UserAppResponse();

            try
            {
                var user = _userAppRepository.GetUserFromByUserName(login.UserName);
                if (user == null)
                {
                    response.Error("No existe el usuario");
                    return response;
                }

                if (login.Password.Base64Encode() != user.Pw)
                {
                    response.Error("contraseña incorrecta");
                    return response;
                }

                response.Ok(Mapper.Map<UserAppModelView>(user));
            }
            catch (Exception ex)
            {
                Logger.ErrorFatal(ex);
                response.Error(ex);
            }

            return response;
        }

        public UserAppResponse DeleteUser(int userId) 
        {
            var response = new UserAppResponse();
            try
            {
                var user = _userAppRepository.GetFindId(userId);

                if (user == null) 
                {
                    response.Error($"No existe el usuario por id {userId}");
                    return response;
                }

                var model = Mapper.Map<UserAppModelView>(user);
                _userAppRepository.Delete(user);

                response.Ok(model);
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
                    if (_userAppRepository != null)
                        _userAppRepository.Dispose();
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

    public interface IUserAppServices : IDisposable
    {
        UserAppResponseList GetUserAppAll();

        UserAppResponseList GetUserAppFromByFullName(string fullName);

        UserAppResponseList GetUserAppFromByRolId(int? rolId);

        UserAppResponse GetUserFromById(int id);

        UserAppResponse SaveUserApp(UserAppModelView request);

        UserAppResponse Login(UserLogin login);

        UserAppResponse DeleteUser(int userId);
    }
}