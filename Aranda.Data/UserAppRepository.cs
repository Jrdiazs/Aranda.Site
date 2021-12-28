using Aranda.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Aranda.Data
{
    public class UserAppRepository : RepositoryGeneric<UserApp>, IUserAppRepository, IDisposable
    {
        public UserAppRepository()
        { }

        public List<UserApp> GetUserApps(int? id = null, int? rolId = null, string fullName = "", string userName="", bool showPw = false)
        {
            try
            {
                var query = Connection.Query<UserApp, Rol, UserApp>(sql: "ArandaDB_SP_UserApp_Search",
                    map: (ua, r) => { ua.Role = r; return ua; }, splitOn: "split", commandType: CommandType.StoredProcedure,
                    param: new { UserId = id, RolId = rolId, FullName = fullName, UserName = userName }).ToList();

                if (query.Any() && !showPw)
                    query.ForEach(x => x.Pw = string.Empty);

                return query ?? new List<UserApp>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserApp> GetUserAppsAll()
        {
            try
            {
                var query = GetUserApps();
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserApp> GetUserAppsFromByRol(int? rolId)
        {
            try
            {
                var query = GetUserApps(rolId: rolId);
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserApp> GetUserAppsFromByFullName(string fullName)
        {
            try
            {
                var query = GetUserApps(fullName: fullName);
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserApp GetUserFromId(int id)
        {
            try
            {
                var query = GetUserApps(id: id);
                return query.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserApp GetUserFromByUserName(string userName)
        {
            try
            {
                var query = GetUserApps(userName: userName,showPw: true);
                return query.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public interface IUserAppRepository : IRepositoryGeneric<UserApp>, IDisposable
    {
        List<UserApp> GetUserApps(int? id = null, int? rolId = null, string fullName = "", string userName = "", bool showPw = false);

        UserApp GetUserFromId(int id);

        List<UserApp> GetUserAppsFromByRol(int? rolId);

        List<UserApp> GetUserAppsAll();

        List<UserApp> GetUserAppsFromByFullName(string fullName);

        UserApp GetUserFromByUserName(string userName);
    }
}