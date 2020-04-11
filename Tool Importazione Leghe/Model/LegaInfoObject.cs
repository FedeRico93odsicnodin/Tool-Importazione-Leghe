using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Elemento di transizione per l'eventuale scrittura delle informazioni relative alle leghe all'interno di una destinazione 
    /// qui dentro sono contenute le 3 rappresentazioni per quanto riguarda l'oggetto da persistere 
    /// </summary>
    public class LegaInfoObject
    {
        #region ATTRIBUTI PRIVATI - CREAZIONE REPORTS INSERIMENTO 

        /// <summary>
        /// Report relativo a messaggio di errore da inserire per la lega corrente all'interno del report di errori excel 
        /// </summary>
        private List<string> _currentReportErrorsExcel;


        /// <summary>
        /// Report relativo a messaggio di errore da inserire per la lega corrente all'interno del report di errori xml
        /// </summary>
        private List<string> _currentReportErrorsXML;


        /// <summary>
        /// Report relativo a messaggio di warning da inserire per la lega corrente all'interno del report di warnings excel
        /// </summary>
        private List<string> _currentReportWarningsExcel;


        /// <summary>
        /// Report relativo a messaggio di warning da inserire per la lega corrente all'interno del report di warnings xml
        /// </summary>
        private List<string> _currentReportWarningsXML;


        /// <summary>
        /// Provenienza per l'informazione corrente 
        /// </summary>
        private Utils.Constants.OriginOfInformation _origineInformazione;

        #endregion


        #region COSTRUTTORE 

        /// <summary>
        /// By default le proprieta relative al recupero alla validazione e alla possibile persistenza sono a false 
        /// per l'oggetto corrente 
        /// per l'oggetto viene anche passata la provenienza dell'informazione corrente 
        /// </summary>
        /// <param name="origineInfomazione"></param>
        public LegaInfoObject(Utils.Constants.OriginOfInformation origineInfomazione)
        {
            Step1_Recupero = false;

            _origineInformazione = origineInfomazione;

            // inizializzazione della lista dei possibili errori Excel 
            _currentReportErrorsExcel = new List<string>();

            // inizializzazione della lsita dei possibili errori XML 
            _currentReportErrorsXML = new List<string>();

            // inizializzazione della lista dei possibili warnings excel
            _currentReportWarningsExcel = new List<string>();

            // inizializzazione della lista dei possibili warnings XML 
            _currentReportWarningsXML = new List<string>();
        }

        #endregion

        /// <summary>
        /// Indicazione della eventuale riga excel corrispondente per la lega 
        /// </summary>
        public RowFoglioExcel Lega_ExcelRow { get; set; }


        /// <summary>
        /// corrispondenza della eventuale nornamtiva corrispondente a livello DB
        /// </summary>
        public NormativeDB Lega_NormativaDB { get; set; }


        /// <summary>
        /// corrispondenza con eventuale categoria lega corrispondente a livello DB 
        /// </summary>
        public Categorie_LegheDB Lega_CategoriaLega { get; set; }


        /// <summary>
        /// corrispondenza con la eventuale base corrispodnente 
        /// </summary>
        public BaseDB Lega_BaseCorrispondente { get; set; }


        /// <summary>
        /// oggetto di corrispondenza database per la lega in questione
        /// </summary>
        public LegheDB LegaDBCorrispondente { get; set; }


        #region STEPS

        /// <summary>
        /// Indica se l'informazione corrente per la lega è stata letta correttamente  
        /// </summary>
        public bool Step1_Recupero { get; set; }

        
        /// <summary>
        /// Indicazione della provenienza dell'informazione corrente ai fini dell'import
        /// </summary>
        public Utils.Constants.OriginOfInformation OrigineInformazione { get; set; }


        /// <summary>
        /// Permette l'inserimento di un nuovo messaggio di errore all'interno del report degli errori nella lettura della lega corrente 
        /// dal file excel 
        /// </summary>
        /// <param name="currentMessage"></param>
        public void InsertNewMessage_ReportExcelError(string currentMessage)
        {
            this._currentReportErrorsExcel.Add(currentMessage);
        }


        /// <summary>
        /// Permette l'inserimento di un nuovo messaggio di errore all'interno del report degli errori nella lettura della lega corrente 
        /// dal file xml
        /// </summary>
        /// <param name="currentMessage"></param>
        public void InsertNewMessage_ReportXMLError(string currentMessage)
        {
            this._currentReportErrorsXML.Add(currentMessage);
        }


        /// <summary>
        /// Permette l'inserimento di un nuovo messaggio di warning all'interno del report dei messaggi warning nella lettura della lega corrente 
        /// dal file excel 
        /// </summary>
        /// <param name="currentMessage"></param>
        public void InsertNewMessage_ReportExcelWarnings(string currentMessage)
        {
            this._currentReportWarningsExcel.Add(currentMessage);
        }


        /// <summary>
        /// Permette l'inserimento di un nuovo messaggio di warning all'interno del report dei messaggi di warning nella lettura della lega corrente 
        /// dal file xml
        /// </summary>
        /// <param name="currentMessage"></param>
        public void InsertNewMessage_ReportXMLWarnings(string currentMessage)
        {
            this._currentReportWarningsXML.Add(currentMessage);
        }


        /// <summary>
        /// Ottenimento del report di errori per la lega corrente e la lettura del file excel 
        /// </summary>
        public List<string> GetReportErrorExcel { get { return this._currentReportErrorsExcel; } }


        /// <summary>
        /// Ottenimento del report di errori per la lega corrente e la lettura del file xml
        /// </summary>
        public List<string> GetReportErrorXML { get { return this._currentReportErrorsXML; } }


        /// <summary>
        /// Ottenimento del report di warnings per la lega corrente e la lettura del file excel
        /// </summary>
        public List<string> GetReportWarningExcel { get { return this._currentReportWarningsExcel; } }


        /// <summary>
        /// Ottenimento del report di warnings per la lega corrente e la lettura del file xml
        /// </summary>
        public List<string> GetReportWarningsXML { get { return this._currentReportWarningsXML; } }

        #endregion
    }
}
