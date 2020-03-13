using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio complessivo di logging per quanto riguarda le operazioni che si svolgono all'interno del database
    /// </summary>
    public abstract class LoggingBase_Database
    {
        
        #region ATTRIBUTI PROTECTED

        /// <summary>
        /// Gestione della variabile contenente il path per il log corrente database
        /// </summary>
        protected string _currentLogFile;

        #endregion


        #region ATTRIBUTI PUBBLICI 

        /// <summary>
        /// Permette la segnalazione di una determinata eccezione nata nell'analisi 
        /// delle diverse parti del file excel in questione
        /// </summary>
        /// <param name="currentException"></param>
        public abstract void SegnalazioneEccezione(string currentException);

        #endregion


        #region SETTERS

        /// <summary>
        /// Mi permette di aggiornare il file di log con il valore corrente
        /// </summary>
        public string LoggerFile
        {
            set
            {
                _currentLogFile = value;
            }
        }

        #endregion

    }
}
