using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Qui dentro sono contenute tutte le istanze di log per l'attività di import vera e propria con la quale 
    /// si avvia la procedura considerando una determinata sorgente e una determinata destinazione 
    /// </summary>
    public abstract class LoggingBase_ImportActivity
    {
        #region ATTRIBUTI PRIVATI - MESSAGGI

        /// <summary>
        /// File di log nel quale verranno inserite le entries per la procedura di import corrente
        /// </summary>
        protected string _currentLogFile;


        /// <summary>
        /// Indicazione della procedura corrente per l'importazione
        /// </summary>
        protected string _currentProcedure;


        /// <summary>
        /// Identificatore relativo all'oggetto di import activity, responsabile del coordinamento di tutte le operazioni in esecuzione
        /// </summary>
        protected string importActivityIdentifier = " IMPORT ACTIVITY: ";


        /// <summary>
        /// Separatore di una attività con un'altra
        /// </summary>
        protected string separatorActivity = "**************************************************";


        /// <summary>
        /// Mi serve per separare i diversi processi in avvenimento corrente e per una particolare procedura  
        /// </summary>
        protected string separatorInternalProcesses = "--";


        /// <summary>
        /// Messaggio relativo alla procedura di import in avvio corrente
        /// </summary>
        protected string avviamentoDiUnaCertaOperazione = "si sta avviamento la seguente procedura di import: {0}";
        
        #endregion


        #region METODI PUBBLICI DI UTILIZZO DEL LOG

        /// <summary>
        /// Segnalazione di avviamento di una determinata procedura di import tra quelle possibili 
        /// per la tipologia di import
        /// </summary>
        /// <param name="currentProcedure"></param>
        public abstract void VieneAvviataLaSeguenteProceduraDiImport(string currentProcedure);


        /// <summary>
        /// Permette di ottenere dei separatori nel caso in cui siano avviate più attività di import
        /// </summary>
        public abstract void GetSeparatorActivity();


        /// <summary>
        /// Mi permette di ottenere un separatore per l'attività interna rispetto a una attività primaria
        /// </summary>
        public abstract void GetSeparatorInternalActivity();

        #endregion


        #region SETTERS

        /// <summary>
        /// Permette di modificare la stringa relativa al file di log nel caso in cui cambiasse
        /// all'interno delle configurazioni
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
