using Aranda.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Aranda.Data
{
    public class RolMenuRepository : RepositoryGeneric<RolMenu>, IRolMenuRepository, IDisposable
    {
        public RolMenuRepository()
        { }

        public List<RolMenu> GetMenusFromByRolId(int rolId)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine(@"SELECT rm.RolMenuId,");
                sb.AppendLine(@"rm.RolId,");
                sb.AppendLine(@"rm.MenuId,");
                sb.AppendLine(@"part = NULL,");
                sb.AppendLine(@"m.MenuId,");
                sb.AppendLine(@"m.MenuName,");
                sb.AppendLine(@"m.MenuUrl,");
                sb.AppendLine(@"m.MenuParentId,");
                sb.AppendLine(@"m.OrderMenu,");
                sb.AppendLine(@"m.MenuAction,");
                sb.AppendLine(@"m.MenuController,");
                sb.AppendLine(@"m.[Active],");
                sb.AppendLine(@"part = NULL,");
                sb.AppendLine(@"MenuParent.MenuId,");
                sb.AppendLine(@"MenuParent.MenuName,");
                sb.AppendLine(@"MenuParent.MenuUrl,");
                sb.AppendLine(@"MenuParent.MenuParentId,");
                sb.AppendLine(@"MenuParent.OrderMenu,");
                sb.AppendLine(@"MenuParent.[Active],");
                sb.AppendLine(@"MenuParent.MenuAction,");
                sb.AppendLine(@"MenuParent.MenuController");
                sb.AppendLine(@"FROM RolMenu AS rm");
                sb.AppendLine(@"JOIN Menu AS m");
                sb.AppendLine(@"ON m.MenuId = rm.MenuId");
                sb.AppendLine(@"LEFT JOIN Menu AS MenuParent");
                sb.AppendLine(@"ON MenuParent.MenuId = m.MenuParentId");
                sb.AppendLine(@"WHERE rm.RolId = @rol");

                var query = Connection.Query<RolMenu, Menu, Menu, RolMenu>(sql: sb.ToString(), 
                    map: (rm, m, MenuParent) => { rm.Menu = m; m.MenuParent = MenuParent; return rm; }, 
                    splitOn: "part", 
                    param: new {  rol = rolId },
                    commandType: CommandType.Text).ToList();

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public interface IRolMenuRepository : IRepositoryGeneric<RolMenu>, IDisposable
    {
        List<RolMenu> GetMenusFromByRolId(int rolId);
    }
}