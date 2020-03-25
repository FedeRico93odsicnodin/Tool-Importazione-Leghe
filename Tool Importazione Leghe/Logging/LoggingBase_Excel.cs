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
        

        /// <summary>
        /// Messaggio di apertura corretta per il file excel letto dalle configurazioni
        /// </summary>
        protected string _aperturaFileExcelSuccesso = "il file excel '{0}' è stato aperto correttamente";


        /// <summary>
        /// Messaggio di segnalazione di aver trovato un determinato foglio excel per il file in apertura corrente
        /// </summary>
        protected string _hoTrovatoSeguenteFoglioExcel = "ho trovato il foglio excel '{0}' per il file '{1}'";


        /// <summary>
        /// Messaggio di segnalazione di nessun marker trovato per il foglio excel del nome che viene passato in input
        /// </summary>
        protected string _nonHoTrovatoMarkerPerIlFoglioExcel = "non ho trovato nessuna informazione di marker per il folgio excel '{0}'";


        /// <summary>
        /// Messaggio di segnalazione di non aver trovato nessuna informazione utile corrispondente per un determinato marker all'interno del foglio excel
        /// </summary>
        protected string _nonHoTrovatoInformazionePerIlSeguenteMarker = "non ho trovato una corrispondenza per il seguente marker: '{0}', col {1}, row {2}";


        /// <summary>
        /// Messaggio segnalazione di aver trovato tutti i marker per un determinato foglio excel che viene identificato per una certa lettura
        /// </summary>
        protected string _hoTrovatoTuttiMarker = "ho trovato tutti i marker per il seguente foglio '{0}', identificato come '{1}'";


        /// <summary>
        /// Segnalazione di aver trovato del contenuto utile per il foglio excel riconosciuto di una certa tipologia
        /// </summary>
        protected string _hoTrovatoContenutoPerIlFoglio = "ho trovato contenuto in col = {0}, row = {1} per il foglio '{2}' riconosciuto come '{3}', la lettura del contenuto comincerà da qui";


        /// <summary>
        /// Messaggio di indicazione che benche il foglio sia stato riconosciuto effettivamente come foglio di informazione di leghe 
        /// non si è trovata nessuna informazione utile per questo foglio, quindi viene inserito come foglio a contenuto nullo
        /// </summary>
        protected string _nonHoTrovatoInformazioniUtiliDiLega = "non ho trovato nessuna informazione per il foglio '{0}' riconosciuto come '{1}', il foglio è sconosciuto";


        /// <summary>
        /// Messaggio di riconoscimento di un determinato foglio excel contenuto nel file come portatore di informazioni di un certo tipo
        /// </summary>
        protected string _hoRiconosciutoFoglioExcelCome = "ho riconosciuto il seguente foglio excel '{0}' come '{1}'";

        #region RICONOSCIMENTO QUADRANTI CONCENTRAZIONI

        /// <summary>
        /// Messaggio relativo a nessun riconscimento del nome del materiale per il qualdrante delle concentrazioni
        /// </summary>
        protected string _nonHoTrovatoInformazionePerIlTitoloMateriale = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI col = {0}, row = {1}: non ho trovato nessuna informazione per il titolo del materiale";


        /// <summary>
        /// Messaggio relativo al riconoscimento del nome per il materiale per il quadrante delle concentrazioni
        /// </summary>
        protected string _hoTrovatoInformazionePerIlTitoloMatariale = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI col = {0}, row = {1}: ho trovato informazione valida per il titolo del materiale";


        /// <summary>
        /// Messaggio relativo alla segnalazione di aver trovato la giusta corrispondenza di header per le concentrazioni e per il quadrante corrente
        /// </summary>
        protected string _hoTrovatoHeaderConcentrationsQuadranteCorrente = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI col = {0}, row = {1}: ho trovato inforazione valida di header per le concentrazioni";


        /// <summary>
        /// Messaggio relativo alla segnalazione di non aver trovato la giusta corrispondenza di header per le concentrazioni e per il quadrante corrente
        /// </summary>
        protected string _nonHoTrovatoHeaderConcentrationsQuadranteCorrente = "RICONOSCIMENTO QUADRANTE CONCENTRAZIONI col = {0}, row = {1}: non ho trovato informazione valida di header per le concentrazioni";


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


        /// <summary>
        /// Messsaggio di già trovata proprietà letta per le informazioni generali all'interno del foglio excel
        /// </summary>
        protected string _hoGiaTrovatoLaProprietaHeaderInfoCorrente = "RICONOSCIMENTO QUADRANTE INFORMAZIONI: ho già trovato la proprietà '{0}', questa non viene inserita nella lettura";


        /// <summary>
        /// Messaggio relativo al fatto che la proprieta in lettura corrente non appartiene alle definizioni date per le informazioni obbligatorie nella lettura delle informazioni di lega 
        /// </summary>
        protected string _informazioneNonContenutaTraLeDefinizioniInformazioniGenerali = "RICONOSCIMENTO QUADRANTE INFORMAZIONI: la proprieta '{0}' non appartiene alle definizioni date per le informazioni obbligatorie";
        
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


        /// <summary>
        /// Non ho trovato nessuna informazione di marker per il foglio excel passato in input
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public abstract void NonHoTrovatoNessunaInformazioneDiMarker(string currentFoglioExcel);


        /// <summary>
        /// Indicazione di non aver trovato informazione utile per un determinato marker
        /// </summary>
        /// <param name="currentMarker"></param>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public abstract void NonHoTrovatoInformazionePerIlSeguenteMarker(string currentMarker, int currentCol, int currentRow);


        /// <summary>
        /// Indicazione di aver trovato tutti i marker, il determinato foglio excel è stato riconosciuto come 
        /// contenente informazioni per una certa categoria tra leghe e concentrazioni
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="currentTipologia"></param>
        public abstract void HoTrovatoTuttiIMarker(string currentFoglioExcel, Constants.TipologiaFoglioExcel currentTipologia);


        /// <summary>
        /// Indicazione di aver trovato del contenuto per il determinato foglio excel in lettura corrente
        /// la lettura effettiva del contenuto avverrà dall'indice indicato
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="currentTipologia"></param>
        public abstract void SegnalazioneTrovatoContenutoUtile(string currentFoglioExcel, Constants.TipologiaFoglioExcel currentTipologia, int currentCol, int currentRow);



        /// <summary>
        /// Indicazione che non si è trovata nessuna informazione utile per un determinato foglio excel riconosciuto come 
        /// un certo contenitore per dati su lega / concentrazioni
        /// </summary>
        /// <param name="currentFoglio"></param>
        /// <param name="currentTipologia"></param>
        public abstract void SegnalazioneFoglioContenutoNullo(string currentFoglio, Constants.TipologiaFoglioExcel currentTipologia);


        /// <summary>
        /// Indicazione che il foglio excel è stato riconosciuto come una certa tipologia
        /// </summary>
        /// <param name="currentFoglio"></param>
        /// <param name="currentTipologia"></param>
        public abstract void HoRiconosciutoSeguenteFoglioCome(string currentFoglio, Constants.TipologiaFoglioExcel currentTipologia);


        #region RICONOSCIMENTO QUADRANTE CONCENTRAZIONI

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
