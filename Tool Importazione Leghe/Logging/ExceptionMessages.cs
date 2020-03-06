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


        #region EXCEPTIONS MESSAGES EXCEL

        /// <summary>
        /// Indicazione che non è stato trovato all'interno delle celle una porzione che constraddistingua la tipologia di foglio che viene passata in input
        /// </summary>
        public const string NONHOTROVATOINFORMAZIONEIDENTIFICATORETABELLA = "non ho trovato nessuna informazione per distinguere sulla tipologia per il seguente foglio Excel {0}";


        /// <summary>
        /// Indicazione di stop nella lettura dell'header per incompletezza nella lettura dei marker, oltre alla tipologia di foglio specificata viene anche inserito 
        /// il titolo dell'header che ha causato l'eccezione e il numero di riga e colonna per la quale questa eccezione si è verificata
        /// </summary>
        public const string NONHOTROVATOINFORMAZIONECOMPLETADIHEADER = "non ho trovato informazione completa per l'header per la tipologia di foglio {0} (header '{1}', riga {2}, colonna {3}";


        /// <summary>
        /// Indicazione di non aver trovato nessuna informazione utile per una certa tipologia di foglio che viene specificata in input
        /// </summary>
        public const string NONHOTROVATOINFORMAZIONEUTILEPERFOGLIO = "non ho trovato nessuna informazione per la seguente tipologia di foglio Excel {0}";

        #endregion
    }
}
