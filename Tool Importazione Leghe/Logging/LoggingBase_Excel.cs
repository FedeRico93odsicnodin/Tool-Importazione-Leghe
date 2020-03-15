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
