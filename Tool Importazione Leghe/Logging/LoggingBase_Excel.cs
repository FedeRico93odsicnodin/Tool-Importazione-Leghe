using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

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
        /// Marker da inserire durante la lettura del file excel per ogni riga che viene loggata 
        /// </summary>
        protected const string excelReaderIdentifier = " EXCEL READER: ";


        /// <summary>
        /// Marker da inserire durante la scrittura del file excel per ogni riga che viene loggata
        /// </summary>
        protected const string exccelWriterIdentifier = " EXCEL WRITER: ";


        /// <summary>
        /// Stringa da formattare per il messaggio relativo al ritrovamento di una informazione di header
        /// </summary>
        protected string hoTrovatoInformazioniPerIlPrimoMarker = "foglio {0} ho trovato le informazioni per '{1}' ({2}) in col {3}, row {4}";
        
        #endregion


        #region ATTRIBUTI PUBBLICI 

        /// <summary>
        /// Permette la segnalazione di una determinata eccezione nata nell'analisi 
        /// delle diverse parti del file excel in questione
        /// </summary>
        /// <param name="currentException"></param>
        public abstract void SegnalazioneEccezione(string currentException);


        /// <summary>
        /// Segnalazione di aver trovato il primo marker utile per il foglio excel corrente nella specifica 
        /// colonna e riga caratterizzante il foglio
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="primoMarker"></param>
        /// <param name="currentTipologiaFoglioExcel"></param>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public abstract void ReadHeaders_HoTrovatoInformazionePerIlPrimoMarker(string currentFoglioExcel, string primoMarker, Constants.TipologiaFoglioExcel currentTipologiaFoglioExcel, int currentCol, int currentRow);


        /// <summary>
        /// Segnalazione di aver trovato tutte le informazioni di lettura per l'header e restituzioen della prima 
        /// posizione utile per la quale iniziare a leggere le informazioni
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public abstract void ReadHeaders_TrovatoTuttiMarkers(string currentFoglioExcel, int currentCol, int currentRow);

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
