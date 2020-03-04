using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// In questa classe sono contenuti tutti i messaggi di eccezione 
    /// per l'import corrente
    /// </summary>
    public static class ExceptionMessages
    {
        #region DBSERVICES

        /// <summary>
        /// Messaggio di errore di connessione per il database correntemente in utilizzo
        /// </summary>
        public const string PROBLEMIDICONNESSIONEDATABASE = "si è verificato un problema nel tentativo di connessione al seguente database: {0}";


        /// <summary>
        /// Messaggio di errore di lettura nel database per una determinata tabella
        /// </summary>
        public const string PROBLEMIDIESECUZIONEREADER = "si è verificato un problema nel tentativo di eseguire una lettura alla seguente tabella: {0}";


        /// <summary>
        /// Messaggio di errore di lettura di una certa entità DB 
        /// </summary>
        public const string PROBLEMILETTURAENTITA = "si sono verificati problemi nel leggere l'entità proveniente dal seguente servizio DB {0}";


        /// <summary>
        /// Messaggio di errore per l'inserimento di una nuova entità con indicazione di questa
        /// </summary>
        public const string PROBLEMIESECUZIONEINSERIMENTO = "si sono verificati problemi nell'inserimento di una nuova entità per la seguente tabella {0}";

        #endregion


        #region CAST E ACCESSO ENTITA DATABASE

        /// <summary>
        /// Segnalazione problemi di cast dall'entita generale a quella relativa ad un particolare oggetto
        /// con il quale avviene la modellizzazione delle tabelle db
        /// </summary>
        public const string PROBLEMACASTOGGETTODB = "problemi nel cercare di castare l'entita generale nella seguente entita {0}";
        

        #endregion
    }
}
