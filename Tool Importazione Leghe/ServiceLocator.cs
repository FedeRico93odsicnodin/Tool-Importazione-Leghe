using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.DatabaseServices;
using Tool_Importazione_Leghe.ExcelServices;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe
{
    /// <summary>
    /// Allocazione di tutti i servizi utilizzati per l'importazione corrente 
    /// </summary>
    public static class ServiceLocator
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Servizi database sulle diverse tabelle coinvolte nelle operazioni CRUD
        /// per l'import stabilito dal programma
        /// </summary>
        private static DBServices _currentDBServices;


        /// <summary>
        /// Servizi excel sulle diverse tab che sono coinvolte nella possibile lettura 
        /// scrittura per il file di excel corrente
        /// </summary>
        private static XlsServices _currentExcelServices;


        /// <summary>
        /// Servizi relativi ai messaggi di segnalazione per tutte le componenti che intervengono all'interno 
        /// del programma in questione
        /// </summary>
        private static LoggingService _currentLoggingService;


        /// <summary>
        /// Servizi relativi alle configurazioni che verranno adottate dal programma in questione
        /// </summary>
        private static Configurations _currentConfigurations;


        /// <summary>
        /// Servizio di attività iniziale correnti - queste attività iniziali vanno a popolare le diverse liste 
        /// che poi risulteranno utili a tutta la procedura di import per le diverse validazioni e le diverse operazioni svolte
        /// </summary>
        private static StartingLoad_Activity _currentStartingLoadActivity;

        #endregion


        #region GETTERS SERVIZI

        /// <summary>
        /// Getter per i servizi database disponibili
        /// </summary>
        public static DBServices GetDBServices
        {
            get
            {
                if (_currentDBServices == null)
                    _currentDBServices = new DBServices();

                return _currentDBServices;
            }
        }


        /// <summary>
        /// Getter per i servizi excel disponibili
        /// </summary>
        public static XlsServices GetExcelServices
        {
            get
            {
                if (_currentExcelServices == null)
                    _currentExcelServices = new XlsServices();

                return _currentExcelServices;
            }
        }


        /// <summary>
        /// Getter per i servizi di logging 
        /// </summary>
        public static LoggingService GetLoggingService
        {
            get
            {
                if (_currentLoggingService == null)
                    _currentLoggingService = new LoggingService();


                return _currentLoggingService;
            }

        }


        /// <summary>
        /// Getters per le configurazioni correnti adottate all'interno del programma 
        /// (e lette dall'opportuno file di configurazione)
        /// </summary>
        public static Configurations GetConfigurations
        {
            get
            {
                if (_currentConfigurations == null)
                    _currentConfigurations = new Configurations();

                return _currentConfigurations;
            }
        }


        /// <summary>
        /// Getter per le impostazioni di inizio import da eseguire prima dell'import vero e proprio
        /// </summary>
        public static StartingLoad_Activity GetStartingLoad_Activity
        {
            get
            {
                if (_currentStartingLoadActivity == null)
                    _currentStartingLoadActivity = new StartingLoad_Activity();


                return _currentStartingLoadActivity;
            }
        }

        #endregion
    }
}
