using Aranda.Models;
using Aranda.Tools.String;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Aranda.Data
{
    /// <summary>
    /// Repositorio generico para las entidades
    /// </summary>
    /// <typeparam name="TModel">modelo entidad base de datos</typeparam>
    public class RepositoryGeneric<TModel> : DataConnections, IDisposable, IRepositoryGeneric<TModel> where TModel : class
    {
        #region [Constructor]

        public RepositoryGeneric() : base()
        {
            SelectDialect();
        }

        public RepositoryGeneric(IDbConnection connections) : base(connections)
        {
            SelectDialect();
        }

        #endregion [Constructor]

        #region [Methods]

        /// <summary>
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="id">prymary key id</param>
        /// <returns>TModel</returns>
        public TModel GetFindId(object id, IDbTransaction transaction = null)
        {
            try
            {
                var data = Connection.Get<TModel>(id, transaction: transaction);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="id">prymary key id</param>
        /// <returns>TModel</returns>
        public async Task<TModel> GetFindIdAsync(object id, IDbTransaction transaction = null)
        {
            try
            {
                var data = await Connection.GetAsync<TModel>(id, transaction: transaction);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los registros del modelo actual
        /// </summary>
        /// <returns>List</returns>
        public List<TModel> GetAll()
        {
            try
            {
                var data = Connection.GetList<TModel>().ToList();
                return data ?? new List<TModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los registros del modelo actual de manera asincrona
        /// </summary>
        /// <returns>List TModel</returns>
        public async Task<List<TModel>> GetAllAsync()
        {
            try
            {
                var data = await Connection.GetListAsync<TModel>();
                return data.ToList() ?? new List<TModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los registros del modelo actual
        /// </summary>
        /// <param name="parameters">parametros del filtro</param>
        /// <param name="transaction">transacción sql</param>
        /// <param name="where">filtro sql</param>
        /// <returns>List TModel</returns>
        public List<TModel> GetList(string where, object parameters, IDbTransaction transaction = null)
        {
            try
            {
                var data = Connection.GetList<TModel>(conditions: where, parameters: parameters, transaction: transaction).ToList();
                return data ?? new List<TModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los registros del modelo actual
        /// </summary>
        /// <param name="parameters">parametros del filtro</param>
        /// <param name="transaction">transacción sql</param>
        /// <param name="where">filtro sql</param>
        /// <returns>List TModel </returns>
        public async Task<List<TModel>> GetListAsync(string where, object parameters, IDbTransaction transaction = null)
        {
            try
            {
                var data = await Connection.GetListAsync<TModel>(conditions: where, parameters: parameters, transaction: transaction);
                return data.ToList() ?? new List<TModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los registros del modelo actual
        /// </summary>
        /// <param name="whereConditions">parametros del filtro</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>List TModel</returns>
        public List<TModel> GetList(object whereConditions, IDbTransaction transaction = null)
        {
            try
            {
                var data = Connection.GetList<TModel>(whereConditions: whereConditions, transaction: transaction).ToList();
                return data ?? new List<TModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los registros del modelo actual
        /// </summary>
        /// <param name="whereConditions">parametros del filtro</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>List TModel</returns>
        public async Task<List<TModel>> GetListAsync(object whereConditions, IDbTransaction transaction = null)
        {
            try
            {
                var data = await Connection.GetListAsync<TModel>(whereConditions: whereConditions, transaction: transaction);
                return data.ToList() ?? new List<TModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserta una nueva entidad de base de datos retornando su id
        /// </summary>
        /// <typeparam name="Tkey">tipo de dato del primary key</typeparam>
        /// <param name="model">modelo a insertar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>primary key identity</returns>
        public Tkey Insert<Tkey>(TModel model, IDbTransaction transaction = null)
        {
            try
            {
                Tkey key = Connection.Insert<Tkey, TModel>(model, transaction: transaction);
                return key;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserta una nueva entidad de base de datos retornando su id
        /// </summary>
        /// <typeparam name="Tkey">tipo de dato del primary key</typeparam>
        /// <param name="model">modelo a insertar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>primary key identity</returns>
        public async Task<Tkey> InsertAsync<Tkey>(TModel model, IDbTransaction transaction = null)
        {
            try
            {
                Tkey key = await Connection.InsertAsync<Tkey, TModel>(model, transaction: transaction);
                return key;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza una entidad de base de datos
        /// </summary>
        /// <param name="model">modelo a modificar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        public TModel Update(TModel model, IDbTransaction transaction = null)
        {
            try
            {
                int update = Connection.Update(model, transaction: transaction);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza una entidad de base de datos
        /// </summary>
        /// <param name="model">modelo a modificar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        public async Task<TModel> UpdateAsync(TModel model, IDbTransaction transaction = null)
        {
            try
            {
                int update = await Connection.UpdateAsync(model, transaction: transaction);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina una entidad de base de datos
        /// </summary>
        /// <param name="model">modelo a modificar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        public TModel Delete(TModel model, IDbTransaction transaction = null)
        {
            try
            {
                int delete = Connection.Delete(model, transaction: transaction);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina una entidad de base de datos
        /// </summary>
        /// <param name="model">modelo a modificar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        public async Task<TModel> DeleteAsync(TModel model, IDbTransaction transaction = null)
        {
            try
            {
                int delete = await Connection.DeleteAsync(model, transaction: transaction);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina una entidad de base de datos por id
        /// </summary>
        /// <param name="model">modelo a modificar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        public int Delete(object id, IDbTransaction transaction = null)
        {
            try
            {
                int delete = Connection.Delete(id, transaction: transaction);
                return delete;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina una entidad de base de datos
        /// </summary>
        /// <param name="id">modelo a modificar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        public async Task<int> DeleteAsync(object id, IDbTransaction transaction = null)
        {
            try
            {
                int delete = await Connection.DeleteAsync(id, transaction: transaction);
                return delete;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la cantidad de registros de una entidad segun el filtro aplicado
        /// </summary>
        /// <param name="conditions">where sql</param>
        /// <param name="parameters">parametros</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>total de registros</returns>
        public int Count(string conditions, object parameters, IDbTransaction transaction = null)
        {
            try
            {
                int count = Connection.RecordCount<TModel>(conditions: conditions, parameters: parameters, transaction: transaction);
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la cantidad de registros de una entidad segun el filtro aplicado
        /// </summary>
        /// <param name="conditions">where sql</param>
        /// <param name="parameters">parametros</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>total de registros</returns>
        public async Task<int> CountAsync(string conditions, object parameters, IDbTransaction transaction = null)
        {
            try
            {
                int count = await Connection.RecordCountAsync<TModel>(conditions: conditions, parameters: parameters, transaction: transaction);
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la cantidad de registros de una entidad segun el filtro aplicado
        /// </summary>
        /// <param name="whereConditions">where sql</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>total de registros</returns>
        public int Count(object whereConditions, IDbTransaction transaction = null)
        {
            try
            {
                int count = Connection.RecordCount<TModel>(whereConditions: whereConditions, transaction: transaction);
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la cantidad de registros de una entidad segun el filtro aplicado
        /// </summary>
        /// <param name="whereConditions">where sql</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>total de registros</returns>
        public async Task<int> CountAsync(object whereConditions, IDbTransaction transaction = null)
        {
            try
            {
                int count = await Connection.RecordCountAsync<TModel>(whereConditions: whereConditions, transaction: transaction);
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene un listado paginado segun el filtro aplicado
        /// </summary>
        /// <param name="filterPage">filter page model</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>PagineModel</returns>
        public PagineModel<TModel> GetPageList(PageModelRequest filterPage, IDbTransaction transaction = null)
        {
            try
            {
                var model = new PagineModel<TModel>();

                var pageModel = Connection.GetListPaged<TModel>(pageNumber: filterPage.PageNumber, rowsPerPage: filterPage.RowsPerPage, conditions: filterPage.Conditions, orderby: filterPage.OrderBy, parameters: filterPage.Parameters, transaction: transaction);
                model.Data = pageModel.ToList() ?? new List<TModel>();
                model.PageNumber = filterPage.PageNumber;
                model.RecordsPerPage = filterPage.RowsPerPage;
                model.TotalRecords = Count(conditions: filterPage.Conditions, parameters: filterPage.Parameters, transaction: transaction);

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene un listado paginado segun el filtro aplicado
        /// </summary>
        /// <param name="filterPage">filter page model</param>
        /// <param name="transaction">transaccion sql</param>
        /// <returns>PagineModel</returns>
        public async Task<PagineModel<TModel>> GetPageListAsync(PageModelRequest filterPage, IDbTransaction transaction = null)
        {
            try
            {
                var model = new PagineModel<TModel>();

                var pageModel = await Connection.GetListPagedAsync<TModel>(pageNumber: filterPage.PageNumber, rowsPerPage: filterPage.RowsPerPage, conditions: filterPage.Conditions, orderby: filterPage.OrderBy, parameters: filterPage.Parameters, transaction: transaction);
                model.Data = pageModel.ToList() ?? new List<TModel>();
                model.PageNumber = filterPage.PageNumber;
                model.RecordsPerPage = filterPage.RowsPerPage;
                model.TotalRecords = await CountAsync(conditions: filterPage.Conditions, parameters: filterPage.Parameters, transaction: transaction);

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Establece el dialecto o tipo de base de datos segun lo establecido en el web config
        /// </summary>
        private void SelectDialect()
        {
            try
            {
                string dialect = "Dialect".ReadKey("0");
                switch (dialect)
                {
                    case "0":
                        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
                        break;

                    case "1":
                        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                        break;

                    case "3":
                        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                        break;

                    default:
                        goto case "0";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion [Methods]
    }

    #region [IRepositoryGeneric]

    /// <summary>
    /// Repositorio generico para las entidades
    /// </summary>
    /// <typeparam name="TModel">modelo entidad base de datos</typeparam>
    public interface IRepositoryGeneric<TModel> : IDisposable where TModel : class
    {
        /// <summary>
        /// Obtiene el total de registros de una entidad segun el filtro aplciado
        /// </summary>
        /// <param name="whereConditions">filtro parametros</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>total de registros</returns>
        int Count(object whereConditions, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene el total de registros de una entidad segun el filtro aplicado
        /// </summary>
        /// <param name="conditions">filtro sql</param>
        /// <param name="parameters">parametros del filtro</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>total de registros</returns>
        int Count(string conditions, object parameters, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene el total de registros de una entidad segun el filtro aplciado
        /// </summary>
        /// <param name="whereConditions">filtro parametros</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>total de registros</returns>
        Task<int> CountAsync(object whereConditions, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene el total de registros de una entidad segun el filtro aplicado
        /// </summary>
        /// <param name="conditions">filtro sql</param>
        /// <param name="parameters">parametros del filtro</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>total de registros</returns>
        Task<int> CountAsync(string conditions, object parameters, IDbTransaction transaction = null);

        /// <summary>
        /// Elimina una entidad de base de datos por id o primary key
        /// </summary>
        /// <param name="id">id object primary key</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        int Delete(object id, IDbTransaction transaction = null);

        /// <summary>
        /// Elimina el objeto de base de datos
        /// </summary>
        /// <param name="model">modelo entidad a eliminar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        TModel Delete(TModel model, IDbTransaction transaction = null);

        /// <summary>
        /// Elimina una entidad de base de datos por id
        /// </summary>
        /// <param name="id">id object primary key</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        Task<int> DeleteAsync(object id, IDbTransaction transaction = null);

        /// <summary>
        /// Elimina el objeto de base de datos
        /// </summary>
        /// <param name="model">modelo entidad a eliminar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>TModel</returns>
        Task<TModel> DeleteAsync(TModel model, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene todos los registros del modelo actual
        /// </summary>
        /// <returns>List </returns>
        List<TModel> GetAll();

        /// <summary>
        /// Obtiene todos los registros del modelo actual
        /// </summary>
        /// <returns>List </returns>
        Task<List<TModel>> GetAllAsync();

        /// <summary>
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="id">prymary key id</param>
        /// <returns>TModel</returns>
        TModel GetFindId(object id, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="id">prymary key id</param>
        /// <returns>TModel</returns>
        Task<TModel> GetFindIdAsync(object id, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene todos los registros del modelo actual segun el filtro aplicado
        /// </summary>
        /// <param name="transaction">transacción sql</param>
        /// <param name="whereConditions">parametros</param>
        /// <returns>List </returns>
        List<TModel> GetList(object whereConditions, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene todos los registros del modelo actual segun el filtro aplicado
        /// </summary>
        /// <param name="parameters">parametros del filtro</param>
        /// <param name="transaction">transacción sql</param>
        /// <param name="where">filtro sql</param>
        /// <returns>List </returns>
        List<TModel> GetList(string where, object parameters, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene un listado de una entidad segun el filtro aplicado
        /// </summary>
        /// <param name="whereConditions">filtro</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>List TModel</returns>
        Task<List<TModel>> GetListAsync(object whereConditions, IDbTransaction transaction = null);

        /// <summary>
        /// Obtiene un listado de una entidad segun el filtro aplicado
        /// </summary>
        /// <param name="where">filtro sql</param>
        /// <param name="parameters">parametros</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>List TModel </returns>
        Task<List<TModel>> GetListAsync(string where, object parameters, IDbTransaction transaction = null);

        /// <summary>
        /// Retorna un listado paginado de una entidad
        /// </summary>
        /// <param name="pageModel">filtro y parametrización de la paginación</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>PagineModel TModel</returns>
        PagineModel<TModel> GetPageList(PageModelRequest pageModel, IDbTransaction transaction = null);

        /// <summary>
        /// Retorna un listado paginado de una entidad
        /// </summary>
        /// <param name="pageModel">filtro y parametrización de la paginación</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>PagineModel TModel</returns>
        Task<PagineModel<TModel>> GetPageListAsync(PageModelRequest pageModel, IDbTransaction transaction = null);

        /// <summary>
        /// Inserta un modelo entidad en la base de datos y retorna su primary key o identity
        /// </summary>
        /// <typeparam name="Tkey">tipo de objeto para el primary key</typeparam>
        /// <param name="model">modelo a insertar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>key primary key</returns>
        Tkey Insert<Tkey>(TModel model, IDbTransaction transaction = null);

        /// <summary>
        /// Inserta un modelo entidad en la base de datos y retorna su primary key o identity
        /// </summary>
        /// <typeparam name="Tkey">tipo de objeto para el primary key</typeparam>
        /// <param name="model">modelo a insertar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>key primary key</returns>
        Task<Tkey> InsertAsync<Tkey>(TModel model, IDbTransaction transaction = null);

        /// <summary>
        /// Actualiza una entidad de base de datos
        /// </summary>
        /// <param name="model">modelo a actualizar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>Tmodel</returns>
        TModel Update(TModel model, IDbTransaction transaction = null);

        /// <summary>
        /// Actualiza una entidad de base de datos
        /// </summary>
        /// <param name="model">modelo a actualizar</param>
        /// <param name="transaction">transacción sql</param>
        /// <returns>Tmodel</returns>
        Task<TModel> UpdateAsync(TModel model, IDbTransaction transaction = null);

        /// <summary>
        ///
        /// </summary>
        IDbConnection Connection { get; set; }
    }

    #endregion [IRepositoryGeneric]
}