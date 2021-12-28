using Aranda.Tools.String;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Aranda.Data
{
    #region [IDataConnections]

    /// <summary>
    /// Clase de conexion
    /// </summary>
    public interface IDataConnections : IDisposable
    {
        /// <summary>
        /// Conexión actual de la base de datos
        /// </summary>
        IDbConnection Connection { get; set; }

        /// <summary>
        /// Cierra la conexion actual
        /// </summary>
        void Close();

        /// <summary>
        /// Obtiene una transaccion de la conexión actual
        /// </summary>
        /// <param name="isolationLevel">IsolationLevel</param>
        /// <returns>IDbTransaction</returns>
        IDbTransaction GetDbTransaction(IsolationLevel? isolationLevel);

        /// <summary>
        /// Abre la conexion actual
        /// </summary>
        void Open();
    }

    #endregion [IDataConnections]

    /// <summary>
    /// Clase de conexion
    /// </summary>
    public abstract class DataConnections : IDataConnections, IDisposable
    {
        #region [Constructor]

        /// <summary>
        /// Instancia la clase de conexion con la conexion por default
        /// </summary>
        public DataConnections()
        {
            string strConnections = "DefaultConnection".ReadConnections();
            Connection = new SqlConnection(strConnections);
        }

        /// <summary>
        /// Instancia una conexion a partir de otra ya generada
        /// </summary>
        /// <param name="connection">otra conexion establecida</param>
        public DataConnections(IDbConnection connection)
        {
            Connection = connection;
        }

        #endregion [Constructor]

        #region [Properties]

        /// <summary>
        /// Conexión actual de la base de datos
        /// </summary>
        public IDbConnection Connection { get; set; }

        #endregion [Properties]

        #region [Methods]

        /// <summary>
        /// Cierra la conexion actual
        /// </summary>
        public void Close()
        {
            try
            {
                if (Connection != null && Connection.State != ConnectionState.Closed)
                    Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene una transaccion de la conexión actual
        /// </summary>
        /// <param name="isolationLevel">IsolationLevel</param>
        /// <returns>IDbTransaction</returns>
        public IDbTransaction GetDbTransaction(IsolationLevel? isolationLevel)
        {
            try
            {
                Open();

                if (isolationLevel.HasValue)
                    return Connection.BeginTransaction(isolationLevel.Value);
                else
                    return Connection.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Abre la conexion actual
        /// </summary>
        public void Open()
        {
            try
            {
                if (Connection != null && Connection.State != ConnectionState.Open)
                    Connection.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion [Methods]

        #region [Dispose]

        private bool disposedValue = false; // To detect redundant calls

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Close();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DataConnections()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        #endregion [Dispose]
    }
}