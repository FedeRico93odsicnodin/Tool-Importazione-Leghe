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
        public static string PROBLEMIAPERTURAFOGLIOEXCEL = "ho avuto problemi nell'apertura del seguente file excel '{0}'";


        /// <summary>
        /// Segnalazione che il file excel e la variabile di contenimento sono state trovate a null
        /// </summary>
        public static string CONTENUTONULLOVARIABILEEXCEL = "contenuto nullo per la variabile che dovrebbe contenere il file excel, si prega di ricontrollare azioni interne di import";


        /// <summary>
        /// Segnalazione di nessun foglio trovato per la lista contenente tutti i fogli excel mappati sul documento
        /// </summary>
        public static string NESSUNFOGLIOCONTENUTOINEXCEL = "nessun foglio excel trovato, si prega di ricontrollare il contenuto del file e le azioni interne di import";


        /// <summary>
        /// Indicazione di errore nella lettura di un determinato header, durante il riconscimento di una certa tipologia di foglio excel
        /// </summary>
        public static string HOTROVATOECCEZIONELETTURAHEADER = "durante riconoscimento foglio excel '{0}' ho trovato un errore per header: '{1}', col = {2}, row = {3}";
        

        /// <summary>
        /// Indicazione di verifica di una eccezione nel tentativo di lettura del secondo foglio delle concentrazioni
        /// </summary>
        public static string HOTROVATOLASEGUENTEECCEZIONENELLEGGEREINFOFOGLIO2 = "durante la lettura del foglio 2 si è verificata la seguente eccezione {0}";


        #region VALIDAZIONE FOGLIO EXCEL

        /// <summary>
        /// Segnalazione di mancanza di alcuna informazione di header passata in input al validatore per il foglio excel corrente 
        /// </summary>
        public static string LISTAHEADERNULLAOVUOTA = "non posso continuare la lettura del folgio '{0}' perché non è stato valorizzato alcun header";


        /// <summary>
        /// Segnalazione di disallineamento delle celle di headers per il foglio excel corrente 
        /// </summary>
        public static string DISALLINEAMENTOHEADERSNELFOGLIO = "non posso continuare la lettura di '{0}' perché gli headers letti sono disallineati";


        /// <summary>
        /// Segnalazione di disallineamento di colonna per headers e per title per la lettura delle informazioni per il quadrante delle concentrazioni corrente
        /// </summary>
        public static string CONCENTRATIONSQUADRANT_COLONNEHEADERTITLEDISALLINEATE = "le colonne di header e di titolo sono disallineate nel quadrante di concentrazione corrente";


        /// <summary>
        /// Segnalazione di disallineamento di riga per il titolo e l'indice relativo alla posizione dell'header per la lettura delle informazioni correnti legate al quadrante delle concentrazioni
        /// </summary>
        public static string CONCENTRATIONSQUADRANT_RIGATITLEPRIMADIRIGAHEADER = "la riga per il titolo del materiale viene prima della colonna relativa agli header";


        /// <summary>
        /// Segnalazione di disallineamento di riga per la lettura delle concentrazioni correnti rispetto alla riga di header identificativo delle proprieta in lettura 
        /// </summary>
        public static string CONCENTRATIONSQUADRANT_RIGACONCENTRATIONSPRIMADIHEADER = "la riga per l'inizio di lettura delle concentrazioni è mionore o uguale di quella dell'header identificativo";


        /// <summary>
        /// Segnalazione di disallineamento della riga di fine lettura rispetto a quella di inizio lettura per le concentrazioni correnti
        /// </summary>
        public static string CONCENTRATIONSQUADRANT_RIGAFINELETTURACONCMINORE = "la riga di fine lettura per la lettura delle concentrazioni è minore di quella di inizio lettura";


        /// <summary>
        /// Segnalazione di titolo nullo per il materiale in lettura per il quadrante delle concentrazioni correnti, l'analisi deve essere bloccata 
        /// </summary>
        public static string CONCENTRATIONSQUADRANT_TITLEMATERIALNULL = "il titolo del materiale è nullo per il materiale correntemente in analisi";


        /// <summary>
        /// Segnalazione di non aver trovato nessuna concentrazione per il quadrante in lettura corrente 
        /// </summary>
        public static string CONCENTRATIONSQUADRANT_NESSUNACONCENTRAZIONETROVATA = "non ho trovato nessuna concentrazione per il quadrante corrente";


        /// <summary>
        /// Segnalazione di errore inaspettato nella lettura di un quadrante di concentrazioni per un certo foglio excel che viene comunque passato in input
        /// </summary>
        public static string CONCENTRATIONSQUADRANT_ERROREINASPETTATONELLALETTURAQUADRANTE = "errore inaspettato nella lettura del quadrante '{0}' per il foglio excel '{1}'";

        #endregion

        #endregion
    }
}
