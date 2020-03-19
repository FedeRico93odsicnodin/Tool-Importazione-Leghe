using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// In questa classe sono inseriti tutti i log relativi ai servizi di contorno rispetto all'import vero e proprio 
    /// </summary>
    public abstract class LoggingBase_Others
    {
        #region FILE DI LOG 

        /// <summary>
        /// File nel quale avverrà il log di tutti i servizi messi a disposizione con questo servizio di log
        /// </summary>
        protected string LogFile;

        #endregion

        
        #region STARTING SERVICES

        /// <summary>
        /// Segnalazione che tutti gli elementi sono stati letti a partire dal database di partenza 
        /// </summary>
        protected string _segnalazioneLetturaElementiFromDB = "STARTING SERVICES: ho appena letto tutti gli elementi dal database di partenza";


        /// <summary>
        /// Segnalazione lettura di tutti gli elementi dal database iniziale
        /// </summary>
        public abstract void StartingServicesLOG_HoAppenaLettoTuttiGliElementiFromDB();

        #endregion



        #region SETTERS

        /// <summary>
        /// Mi permette di aggiornare il file di log con il valore corrente
        /// </summary>
        public string LoggerFile
        {
            set
            {
                LogFile = value;
            }
        }

        #endregion


    }
}
