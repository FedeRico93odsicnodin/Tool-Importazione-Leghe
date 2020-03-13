using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Classe contenente tutte le implementazioni date per il log relativo 
    /// alla classe delle configurazioni
    /// </summary>
    public abstract class LoggingBase_Configurations
    {
        #region FILE DI LOG 

        /// <summary>
        /// File di log nel quale iniziare a inserire i messaggi di display
        /// </summary>
        protected string _currentLogFile;

        #endregion

        
        #region MESSAGGI

        /// <summary>
        /// Indentificatore per il log relativo alle configurazioni
        /// </summary>
        protected string _loggerConfigurationsIdentifier = " CONFIGURATIONS: ";


        /// <summary>
        /// Messaggio relativo alla lettura scorretta di una certa configurazione 
        /// </summary>
        protected string _messaggioErroreLetturaConfigurazione = "la configurazione {0} non è stata letta correttamente";


        /// <summary>
        /// Messaggio relativo alla lettura corretta di una certa configurazione 
        /// </summary>
        protected string _messaggioLetturaCorrettaConfigurazione = "la seguente configurazione {0} è stata letta correttamente";


        /// <summary>
        /// Messaggio relativo all'inizio di verifica sul fatto che tutte le informazioni siano state inserite correttamente 
        /// per il file di configurazione
        /// </summary>
        protected string _messaggioDiInizioVerificaCorrettezzaConfigurazioni = "sto per verificare se tutte le configurazioni sono corrette";


        /// <summary>
        /// Messaggio inizio timer
        /// </summary>
        protected string _messaggioInizioTimer = "ho appena fatto partire il timer su tutta la procedura";


        /// <summary>
        /// Messagio fine timer
        /// </summary>
        protected string _messagioStopTimer = "ho appena fermato il timer su tutta la procedura";

        #endregion


        #region METODI DI DISPLAY MESSAGGI CONFIGURAZIONE

        /// <summary>
        /// Lettura scorretta della configurazione in input 
        /// </summary>
        /// <param name="currentConfigurazione"></param>
        public abstract void LetturaScorrettaConfigurazione(string currentConfigurazione);


        /// <summary>
        /// Lettura corretta della configurazione in input 
        /// </summary>
        /// <param name="currentConfigurazione"></param>
        public abstract void LetturaCorrettaConfigurazione(string currentConfigurazione);


        /// <summary>
        /// Messaggio relativo al controllo che tutte le configurazioni siano state inserite 
        /// correttamente 
        /// </summary>
        public abstract void StoPerVedereSeTutteLeConfigurazioniSonoCorrette();


        /// <summary>
        /// Messaggio di segnalazione di partenza del timer per la procedura corrente 
        /// </summary>
        public abstract void HoAppenaInizializzatoTimerSuProcedura();


        /// <summary>
        /// Messagio di segnalazione di stop del timer per la procedura corrente 
        /// </summary>
        public abstract void HoAppenaStoppatoTimerSuProcedura();

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
