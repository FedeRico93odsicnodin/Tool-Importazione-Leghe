using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio complessivo di logging per quanto riguarda le operazioni che si svolgono all'interno del foglio excel
    /// </summary>
    public abstract class LoggingBase_Excel
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Gestione della variabile contenente il path per il log corrente database
        /// </summary>
        protected string _currentLogExcel;

        #endregion
        
    }
}
