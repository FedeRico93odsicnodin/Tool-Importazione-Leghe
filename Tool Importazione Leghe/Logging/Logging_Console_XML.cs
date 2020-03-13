using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio di logging console per la lettura del file xml
    /// </summary>
    class Logging_Console_XML : LoggingBase_XML
    {
        #region COSTRUTTORE

        /// <summary>
        /// Inizializzazione della stringa di log per il file nel quale vengono salvate 
        /// tutte le operazioni XML
        /// </summary>
        /// <param name="currentLogPath"></param>
        public Logging_Console_XML(string currentLogPath)
        {
            base._currentLogFile = currentLogPath;
        }

        #endregion
    }
}
