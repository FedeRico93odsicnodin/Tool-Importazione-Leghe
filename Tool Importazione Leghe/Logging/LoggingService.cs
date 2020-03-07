using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Classe che permette di loggare all'interno di uno specifico file di log
    /// tutta la procedura di import corrente
    /// </summary>
    public class LoggingService
    {
        #region ATTRIBUTI PRIATI
        
        /// <summary>
        /// Sevizio corrente per la gestione del log Database
        /// </summary>
        private LoggingBase_Database _loggingServiceDatabase;


        /// <summary>
        /// Servizio corrente per la gestione del log Excel
        /// </summary>
        private LoggingBase_Excel _loggingServiceExcel;


        /// <summary>
        /// Servizio corrente per la gestione del log XML
        /// </summary>
        private LoggingBase_XML _loggingServiceXML;

        #endregion


        #region COSTRUTTORE 

        /// <summary>
        /// In base alla modalita del tool passata ho l'istanziazione dei diversi log che vengono messi a disposizione per l'import corrente 
        /// </summary>
        /// <param name="currentModalitaTool"></param>
        public LoggingService(Constants.CurrentModalitaTool currentModalitaTool) 
        {
        }

        #endregion


        #region METODI PRIVATI

        /// <summary>
        /// Permette di inizializzare l'istanza del logger in base al fatto che debbano essere per la console o per la wpf application
        /// </summary>
        private void InitializeLoggers()
        {
            if (Constants.CurrentModalitàTool == Constants.CurrentModalitaTool.isConsoleAppication)
            {
                // logger modalita console per database
                _loggingServiceDatabase = new Logging_Console_Database(Constants.LoggerFolder + Constants.LoggerDatabase);

            }
            else if (Constants.CurrentModalitàTool == Constants.CurrentModalitaTool.isWPFApplication)
            {
                // logger modalita UI per database
                _loggingServiceDatabase = new Logging_UI_Database(Constants.LoggerFolder + Constants.LoggerDatabase);
            }
        }

        #endregion
    }
}
