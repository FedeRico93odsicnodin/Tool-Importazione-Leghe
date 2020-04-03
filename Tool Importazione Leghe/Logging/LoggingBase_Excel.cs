using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;
using static Tool_Importazione_Leghe.ExcelServices.XlsServices;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio complessivo di logging per quanto riguarda le operazioni che si svolgono all'interno del foglio excel
    /// </summary>
    public abstract class LoggingBase_Excel
    {
        #region ATTRIBUTI PRIVATI - MESSAGGI
        
        /// <summary>
        /// Gestione della variabile contenente il path per il log corrente database
        /// </summary>
        protected string _currentLogFile;

        #region APERTURA FOGLIO EXCEL CORRENTE 

        /// <summary>
        /// Messaggio di apertura corretta per il file excel letto dalle configurazioni
        /// </summary>
        protected string _aperturaFileExcelSuccesso = "APERTURA FOGLIO EXCEL il file excel '{0}' è stato aperto correttamente";


        /// <summary>
        /// Messaggio di segnalazione di aver trovato un determinato foglio excel per il file in apertura corrente
        /// </summary>
        protected string _hoTrovatoSeguenteFoglioExcel = "APERTURA FOGLIO EXCEL ho trovato il foglio excel '{0}' per il file '{1}'";

        #endregion


        #region RICONOSCIMENTO HEADER PER LE INFORMAZIONI GENERALI SULLA LEGA IN LETTURA 

        /// <summary>
        /// Messsaggio di già trovata proprietà letta per le informazioni generali all'interno del foglio excel
        /// </summary>
        protected string _hoGiaTrovatoLaProprietaHeaderInfoCorrente = "RICONOSCIMENTO QUADRANTE INFORMAZIONI: ho già trovato la proprietà '{0}', questa non viene inserita nella lettura";


        /// <summary>
        /// Messaggio relativo al fatto che la proprieta in lettura corrente non appartiene alle definizioni date per le informazioni obbligatorie nella lettura delle informazioni di lega 
        /// </summary>
        protected string _informazioneNonContenutaTraLeDefinizioniInformazioniGenerali = "RICONOSCIMENTO QUADRANTE INFORMAZIONI: la proprieta '{0}' non appartiene alle definizioni date per le informazioni obbligatorie";


        /// <summary>
        /// Messaggio relativo alla segnalazione che l'informazione corrente non appartiene alla definizione data per le informazioni a carattere addizionale che è possibile leggere per la lega 
        /// </summary>
        protected string _informazioneNoNContenutaTraLeDefinizioniAddizionaliGenerali = "RICONOSCIMENTO QUADRANTE INFORMAZIONI: la proprieta '{0}' non appartiene alle definizioni date per le informazioni addizionali";


        /// <summary>
        /// Messaggio relativo alla lettura dell'informazione obbligatoria per le informazioni di lega in lettura corrente 
        /// </summary>
        protected string _segnalazioneLetturaProprietaObbligatoriaLega = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI: row {0} col {1}: ho appena letto la seguente proprieta a carattere obbligatorio '{2}'";


        /// <summary>
        /// Messaggio relativo alla lettura dell'informazione addizionale per le informazioni di lega in lettura corrente 
        /// </summary>
        protected string _segnalazioneLetturaProprietaAddizionaleLega = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI: row {0} col {1}: ho appena letto la seguente proprieta a carattere opzionale '{2}'";


        /// <summary>
        /// Messaggio relativo a fine process per il foglio excel corrente e in merito alla lettura  di tutti gli headers per il riconoscimento delle informazioni a carattere generale
        /// </summary>
        protected string _fineProcessamentoGeneralInfoFoglioExcel = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI: ho appena finito di processare il seguente foglio excel '{0}'";

        #endregion


        #region RICONOSCIMENTO QUADRANTI CONCENTRAZIONI

        /// <summary>
        /// Messaggio relativo a nessun riconscimento del nome del materiale per il qualdrante delle concentrazioni
        /// </summary>
        protected string _nonHoTrovatoInformazionePerIlTitoloMateriale = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI row = {1}, col = {0}: non ho trovato nessuna informazione per il titolo del materiale";


        /// <summary>
        /// Messaggio relativo al riconoscimento del nome per il materiale per il quadrante delle concentrazioni
        /// </summary>
        protected string _hoTrovatoInformazionePerIlTitoloMatariale = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI row = {1}, col = {0}: ho trovato informazione valida per il titolo del materiale";


        /// <summary>
        /// Messaggio relativo alla segnalazione di aver trovato la giusta corrispondenza di header per le concentrazioni e per il quadrante corrente
        /// </summary>
        protected string _hoTrovatoHeaderConcentrationsQuadranteCorrente = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI row = {1}, col = {0}: ho trovato inforazione valida di header per le concentrazioni";


        /// <summary>
        /// Messaggio relativo alla segnalazione di non aver trovato la giusta corrispondenza di header per le concentrazioni e per il quadrante corrente
        /// </summary>
        protected string _nonHoTrovatoHeaderConcentrationsQuadranteCorrente = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI row = {1}, col = {0}: non ho trovato informazione valida di header per le concentrazioni";


        /// <summary>
        /// Messaggio di segnalazione di avvenuto riconoscimento corretto della lettura del quadrante di tutte le concentrazioni disponibili per il materiale corrente 
        /// </summary>
        protected string _hoTrovatoConcentrazioniPerQuadranteCorrente = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI: ho trovato le concentrazioni per il quadrante corrente, dovrò leggere {0} elemnti";


        /// <summary>
        /// Messaggio di segnalazione di riconoscimento scorretto del quadrante dove dovrebbero essere presenti le concentrazioni disponibili per il materiale corrente
        /// </summary>
        protected string _nonHoTrovatoConcentrazioniPerQuadranteCorrente = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI: non ho trovato concentrazioni per il quadrante corrente";


        /// <summary>
        /// Caso eccezionale: lo letto concentrazioni per elementi in un numero maggiore rispetto a tutti quelli consentiti
        /// </summary>
        protected string _hoTrovatoConcentrazioniPerNumElementiMaggiore = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI: ho trovato concentrazioni per un numero maggiore di elementi possibili per il quadrante corrente";


        /// <summary>
        /// Messaggio di segnalazione di inserimento di un quadrante di lettura concentrazioni per un certo materiale e per il foglio passato in input 
        /// </summary>
        protected string _hoAppenaInseritoUnQuadranteDiLettura = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI: ho appena inserito un quadrante da leggere per il foglio '{0}'";


        /// <summary>
        /// Messaggio di segnalazione che non è stato trovato nessun quadrante di lettura per il foglio passato in input
        /// </summary>
        protected string _nonHoTrovatoNessunQuadranteDiLettura = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI: non ho trovato nessun quadrante di lettura concentrazioni per il foglio '{0}'";

        #endregion


        #region RICONOSCIMENTO DEI FOGLI EXCEL PER INFORMAZIONI DI BASE O CONCENTRAZIONI

        /// <summary>
        /// Messaggio di segnalazione individuazione del foglio excel corrente come di informazioni di base per la determinata lega 
        /// </summary>
        protected string _readExcel_foglioRiconosciutoComeDiInfoBase = "\n****\nLETTURA EXCEL: il foglio '{0}' contiene delle informazioni di base per le leghe";


        /// <summary>
        /// Messaggio di segnalazione individuazione del foglio excel corrente come di informazioni relative alle concentrazioni materiali di una determinata lega 
        /// </summary>
        protected string _readExcel_foglioRiconosciutoComeInfoConcentrazioni = "\n****\nLETTURA EXCEL: il foglio '{0}' contiene delle informazioni relative alle concentrazioni per i materiali";

        #endregion


        #region LETTURA + VALIDAZIONE INFORMAZIONI ALL'INTERNO DEL FOGLIO DELLE INFORMAZIONI GENERALI DI LEGA 

        /// <summary>
        /// Messaggio di segnalazione di lettura di una riga di valori per il numero di riga passato in input e per il determinato foglio excel del quale viene passato il nome
        /// </summary>
        protected string _stoAggiungendoInfoRigaGeneralInRow = "LETTURA INFORMAZIONI GENERALI LEGA: foglio '{0}' sto leggendo una riga di valori per la riga {1}";


        /// <summary>
        /// Messaggio di segnalazione di non aver trovato nessuna informazione generale nella lettura del foglio e per la riga passata in input
        /// </summary>
        protected string _nonHoTrovatoInformazioniGeneraliPerRow = "LETTURA INFORMAZIONI GENERALI LEGA: foglio '{0}' non ho trovato informazioni per la riga {1}";


        /// <summary>
        /// Messaggio di segnalazione di fine lettura di tutti i valori per gli header di carattere generale per la lega correntemente in lettura dal foglio excel
        /// </summary>
        protected string _hoAppenaFinitoLetturaValoriInformazioniGenerali = "EXCEL SERVICES: ho appena finito la lettura di tutti i valori di lega contenuti per il foglio '{0}'";
        
        #endregion


        #region LETTURA + VALIDAZIONE INFORMAZIONI ALL'INTERNO DEL FOGLIO DELLE CONCENTRAZIONI MATERIALI DI UNA CERTA LEGA 
        
        /// <summary>
        /// Messaggio di segnalazione che il quadrante corrente non ha passato una certa validazione e quindi non posso continuarne la lettura delle informazioni contenute 
        /// al suo interno
        /// </summary>
        protected string _internalExceptionReadConcQuadrants = "LETTURA CONCENTRAZIONI QUADRANTE: non posso leggere il quadrante {0} per il foglio '{1}'\n";


        /// <summary>
        /// Messaggio di segnalazione di  aver recuperato correttamente tutte le informazioni sulle concentrazioni per un certo quadrante di cui viene passata l'enumerazione e
        /// contenuto in un determinato foglio excel in lettura corrente 
        /// </summary>
        protected string _riconoscimentoInformazioniValidePerQuadrante = "LETTURA CONCENTRAZIONI QUADRANTE: ho recuperato le informazioni per il quadrante {0} per il foglio excel '{1}'";

        #endregion


        #region STEPS DI LETTURA INFORMAZIONI DA EXCEL 

        /// <summary>
        /// Messaggio di indicazione che inizia lo step di lettura delle informazioni contenute all'interno di un determinato foglio di cui viene passato nome e tipologia 
        /// </summary>
        protected string _inizioLetturaInformazioniFoglioCorrente = "EXCEL SERVICES: inizio della lettura delle informazioni per il foglio '{0}' (tipologia '{1}')";


        /// <summary>
        /// Messaggio di indicazione che le informazioni per il foglio correntemente in analisi sono state recuperate correttamente 
        /// </summary>
        protected string _updateInfoCorrettaPerFoglio = "EXCEL SERVICES: le informazioni per il folgio '{0}' sono state recuperate correttamente";


        /// <summary>
        /// Indicazione che non è stata trovata nessuna informazione inserita per il foglio in analisi
        /// </summary>
        protected string _nonHoTrovatoAlcunaInformazionePerFoglio = "EXCEL SERVICES: non ho trovato nessuna informazione durante la lettura di questo foglio '{0}'";
        
        #endregion

        #endregion


        #region METODI DI CLASSE

        /// <summary>
        /// Formattazione della modalita corrente con il quale si sta loggando le operazioni
        /// </summary>
        /// <param name="currentModalita"></param>
        /// <returns></returns>
        protected string FormatModalitaCorrente(CurrentModalitaExcel currentModalita)
        {
            return " " + currentModalita.ToString() + ": ";
        }

        #endregion


        #region METODI PUBBLICI 

        #region APERTURA FOGLIO EXCEL --> READ EXCEL DI BASE

        /// <summary>
        /// Permette la segnalazione di una determinata eccezione nata nell'analisi 
        /// delle diverse parti del file excel in questione
        /// </summary>
        /// <param name="currentException"></param>
        public abstract void SegnalazioneEccezione(string currentException);


        /// <summary>
        /// Segnalazione di apertura corretta per il file excel passato in input
        /// </summary>
        /// <param name="currentFileExcel"></param>
        /// <param name="modalitaCorrente"></param>
        public abstract void AperturaCorrettaFileExcel(string currentFileExcel, CurrentModalitaExcel modalitaCorrente);


        /// <summary>
        /// Segnalazione di aver trovato un certo foglio excel per il file in apertura corrente e secondo la determinata modalita attuale
        /// </summary>
        /// <param name="currentFoglioExcelName"></param>
        /// <param name="currentFileExcel"></param>
        /// <param name="modalitaCorrente"></param>
        public abstract void HoTrovatoIlSeguenteFoglioExcel(string currentFoglioExcelName, string currentFileExcel, CurrentModalitaExcel modalitaCorrente);

        #endregion


        #region STEP 1: RICONOSCIMENTO HEADER PER INFORMAZIONI GENERALI LEGA --> READ HEADERS


        /// <summary>
        /// Indicazione sul fatto di aver gia trovato una proprieta di carattere generale per il foglio sulle informazioni di lega in lettura
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        public abstract void HoGiaTrovatoInformazioneACarattereGenerale(string currentProprietaLettura);


        /// <summary>
        /// Indicazione che la proprieta in lettura corrente non appartiene alle definizioni date come obbligatorie per la lettura delle informazioni 
        /// generali di lega 
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        public abstract void InformazioneGeneraleNonContenutaNelleDefinizioniObbligatorie(string currentProprietaLettura);


        /// <summary>
        /// Dichiarazione del log per la segnalazione che l'informazione addizionale non si trova nelle definizioni date per tutte le informazioni 
        /// addizionali e generali per le leghe correnti
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        public abstract void InformazioneGeneraleNonContenutaNelleDefinizioniAddizionali(string currentProprietaLettura);


        /// <summary>
        /// Dichiarazione del log per la segnalazione che l'informazione obbligatoria a carattere generale è stata correttamente letta a partire dal
        /// foglio excel corrente 
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentCol"></param>
        public abstract void TrovataInformazioneObbligatoriaLetturaInformazioniGenerali(string currentProprietaLettura, int currentRow, int currentCol);


        /// <summary>
        /// Dichiarazione del log per la segnalazione di lettura corretta per una proprieta addizionale a carattere generale per la lega letta 
        /// all'interno del foglio excel corrente 
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentCol"></param>
        public abstract void TrovataInformazioneAddizionaleLetturaInformazioniGenerali(string currentProprietaLettura, int currentRow, int currentCol);


        /// <summary>
        /// Dichiarazione implementazione log in merito alla fine della procedura per il processamento degli headers delle informazioni 
        /// a carattere generale per il foglio excel corrente 
        /// </summary>
        /// <param name="excelSheetName"></param>
        public abstract void FineProcessamentoGeneralInfoPerFoglioExcel(string excelSheetName);

        #endregion


        #region STEP 1: RICONOSCIMENTO QUADRANTE CONCENTRAZIONI --> READ HEADERS

        /// <summary>
        /// Indicazione di trovo informazioni per il titolo del materiale corrente - la cella è correttamente mappata
        /// </summary>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public abstract void HoTrovatoInformazioniPerTitoloDelMateriale(int currentCol, int currentRow);


        /// <summary>
        /// Indicazione di non trovo informazioni per il titolo del materiale corrente - la cella non è correttamente mappata
        /// </summary>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public abstract void NonHoTrovatoInformazioniPerTitoloMateriale(int currentCol, int currentRow);


        /// <summary>
        /// Indicazione di aver trovato informazioni di header per il quadrante corrente delle concentrazioni
        /// </summary>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public abstract void HoTrovatoInformazioniHeaderPerQuadranteCorrente(int currentCol, int currentRow);


        /// <summary>
        /// Indicazione di non aver trovato informazioni di header per il quadrante corrente delle concentrazioni
        /// </summary>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public abstract void NonHoTrovatoInformazioniHeaderPerQuadranteCorrente(int currentCol, int currentRow);


        /// <summary>
        /// Indicazione di aver trovato concentrazioni valide per il quadrante corrente con indicazioni sul numero di elementi individuati 
        /// nella lettura
        /// </summary>
        /// <param name="numElementi"></param>
        public abstract void HoTrovatoConcentrazioniPerIlQuadranteCorrente(int numElementi);


        /// <summary>
        /// Indicazione di non aver trovato nessuna concentrazione valida per il quadrante corrente
        /// </summary>
        public abstract void NonHoTrovatoConcentrazioniPerIlQuadranteCorrente();


        /// <summary>
        /// Indicazione del caso eccezionale per il quale si trova un numero maggiore di elementi e quindi di concentrazioni da leggere 
        /// per il materiale corrente
        /// </summary>
        public abstract void HoTrovatoConcentrazioniPerUnNumeroMaggioreDiElementi();


        /// <summary>
        /// Indicazione di trovato quadrante di lettura per il foglio excel passato in input
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public abstract void InserimentoQuadranteLetturaConcentrazioniPerFoglio(string currentFoglioExcel);


        /// <summary>
        /// Indicazione di non aver trovato nessun quadrante di lettura per il foglio passato in input
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public abstract void NonHoTrovatoNessunQuadranteConcentrazioniPerFoglio(string currentFoglioExcel);


        #endregion


        #region FINE RICONOSCIMENTO TIPOLOGIA FOGLIO --> READ EXCEL DI BASE

        /// <summary>
        /// Indicazione riconoscimento del foglio come contenente delle informazioni di carattere generale per la determinata 
        /// lega in lettura corrente
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        public abstract void HoRiconosciutoIlFoglioComeContenenteInformazioniGeneraliLega(string currentExcelSheet);


        /// <summary>
        /// Indicazione riconoscimento del foglio come contenente delle informazioni di concentrazione per i materiali di una certa lega 
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        public abstract void HoRiconosciutoIlFoglioComeContenenteConcentrazioniMateriali(string currentExcelSheet);

        #endregion


        #region MESSAGGISTICA RELATIVA ALLA LETTURA + VALIDAZIONE DELLE INFORMAZIONI GENERALI DI LEGA 

        /// <summary>
        /// Indicazione di una lettura di una riga di infomrazione per il determinato foglio excel e per la riga che vengono passati in input
        /// questa lettura riguarda le informazioni generali per la lega corrente 
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="currentFoglioExcel"></param>
        public abstract void HoLettoUnaRigaDiValoriGeneralPerFoglioExcelInRiga(int currentRow, string currentFoglioExcel);


        /// <summary>
        /// Indicazione di fine lettura di tutti i valori a carattere generale per la lega in lettura dal foglio excel correntemente in analisi
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public abstract void HoAppenaFinitoDiLeggereTuttiIValoriGeneralInfoLega(string currentFoglioExcel);


        /// <summary>
        /// Indazione di non aver trovato nessuna informazione provando a leggere la riga excel per il foglio passati in input
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="currentRiga"></param>
        public abstract void NonHoTrovatoInformazioniGeneraliLegaPerRiga(string currentFoglioExcel, int currentRiga);

        #endregion


        #region MESSAGGISTICA RELATIVA ALLA LETTURA + VALIDAZIONE DELLE INFORMAZIONI CONCENTRAZIONI MATERIALI

        /// <summary>
        /// Indicazione di non poter continuare la lettura per la definizione di un certo quadrante 
        /// </summary>
        /// <param name="currentQuadranteEnumerator"></param>
        /// <param name="currentExcelSheet"></param>
        public abstract void NonPossoContinuareLetturaQuadranteConcentrazioni(int currentQuadranteEnumerator, string currentExcelSheet);


        /// <summary>
        /// Indicazione di aver recuperato correttamente tutte le informazioni per un certo quadrante in lettura per il foglio excel corrente 
        /// </summary>
        /// <param name="enumQuadrante"></param>
        /// <param name="currentExcelSheet"></param>
        public abstract void HoRecuperatoInformazioniConcentrazioniPerQuadrante(int enumQuadrante, string currentExcelSheet);

        #endregion


        #region INIZIO LETTURA VALIDAZIONE E MATCH INFORMAZIONI PER IL FOGLIO EXCEL 

        /// <summary>
        /// Indicazione di inizio lettura informazioni per il foglio excel di cui nome e tipologia sono passati in input
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="currentTipologiaFoglio"></param>
        public abstract void InizioLetturaInformazioniPerFoglioExcelCorrente(string currentFoglioExcel, Constants.TipologiaFoglioExcel currentTipologiaFoglio);


        /// <summary>
        /// Indicazione che sul foglio excel le informazioni sono state recuperate tutte correttamente 
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public abstract void InformazioniPerFoglioRecuperateCorrettamente(string currentFoglioExcel);


        /// <summary>
        /// Indicazione che il foglio è stato letto correttamente ma non è stata trovata alcuna informazione al suo interno
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public abstract void NonHoTrovatoAlcunaInformazionePerIlFoglio(string currentFoglioExcel);

        #endregion

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
