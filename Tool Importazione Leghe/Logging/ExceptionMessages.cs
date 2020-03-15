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
        #region CONFIGURATIONS

        /// <summary>
        /// Messaggio di errore nella lettura del file di configurazione
        /// </summary>
        public const string PROBLEMIDILETTURACONFIGURAZIONI = "si è verificato un errore nella lettura delle configurazioni, si prega di aggiornare il file";


        /// <summary>
        /// Messaggio relativo a non aver trovato il file di configurazione per il progetto corrente 
        /// </summary>
        public const string NONHOTROVATOFILECONFIGURAZIONI = "non ho trovato il file di configurazioni, si prega di inserirlo nella cartella bin";


        /// <summary>
        /// Messaggio relativo alla lettura scorretta del nuovo percorso nel quale andare a inserire il file di log corrente 
        /// </summary>
        public const string ERRORELETTURACONFIGURAZIONELOGFILE = "ho riscontrato un errore nella lettura del nuovo path per il log corrente \n";

        #endregion

        
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
        /// Indicazione di problema nell'apertura di un determinato foglio excel che era stato inserito nelle configurazioni
        /// </summary>
        public static string PROBLEMIAPERTURAFOGLIOEXCEL = "ho avuto problemi nell'apertura del seguente foglio excel {0}";


        /// <summary>
        /// Indicazione di errore nella lettura di un determinato header, durante il riconscimento di una certa tipologia di foglio excel
        /// </summary>
        public static string HOTROVATOECCEZIONELETTURAHEADER = "durante riconoscimento foglio excel '{0}' ho trovato un errore per header: '{1}', col = {2}, row = {3}";
        

        /// <summary>
        /// Indicazione di verifica di una eccezione nel tentativo di lettura del secondo foglio delle concentrazioni
        /// </summary>
        public static string HOTROVATOLASEGUENTEECCEZIONENELLEGGEREINFOFOGLIO2 = "durante la lettura del foglio 2 si è verificata la seguente eccezione {0}";




        #endregion
    }
}
