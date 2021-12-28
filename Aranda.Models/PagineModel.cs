using System.Collections.Generic;
using System.Linq;

namespace Aranda.Models
{
    /// <summary>
    /// Modelo de paginacion
    /// </summary>
    public class PagineModel<TModel>
    {
        /// <summary>
        /// Listado de objetos
        /// </summary>
        public List<TModel> Data { get; set; }

        /// <summary>
        /// Numero de pagina a obtener
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Registros por pagina
        /// </summary>
        public int RecordsPerPage { get; set; }

        /// <summary>
        /// Total de registros obtenidos
        /// </summary>
        public int TotalData
        { get { return Data == null ? 0 : Data.Count(); } }

        /// <summary>
        /// Total de registros
        /// </summary>
        public long TotalRecords { get; set; }
    }

    /// <summary>
    /// Modelo input para realizar un paginado
    /// </summary>
    public class PageModelRequest
    {
        /// <summary>
        /// Numero de pagina
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Registros por pagina
        /// </summary>
        public int RowsPerPage { get; set; } = 20;

        /// <summary>
        /// Filtro sql
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// Ordenamiento sql
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Parametros
        /// </summary>
        public object Parameters { get; set; }
    }
}