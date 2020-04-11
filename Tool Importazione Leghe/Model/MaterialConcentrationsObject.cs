using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// OGGETTO DI TRANSIZIONE per le proprieta lette rispetto a un quadrante di concentrazioni 
    /// contiene tutti gli elementi per poter individuare la sorgente e tutti gli elementi che poi verranno configurati per 
    /// la rispettiva scrittura all'interno della destinazione 
    /// </summary>
    public class MaterialConcentrationsObject
    {
        #region ATTRIBUTI PRIVATI - REPORTS DI SEGNALAZIONE 

        /// <summary>
        /// Report di errori riscontrati nella lettura del file excel delle concentrazioni per il materiale corrente 
        /// </summary>
        private List<string> _currentReportErrorsExcel;


        /// <summary>
        /// Report di errori riscontrati nella lettura del file xml delle concentraiozni per il materiale corrente 
        /// </summary>
        private List<string> _currentReportErrorsXML;


        /// <summary>
        /// Report di warnings riscontrati nella lettura del file excel delle concentrazioni per il materiale corrente 
        /// </summary>
        private List<string> _currentReportWarningsExcel;


        /// <summary>
        /// Report di warnings riscontrati nella lettura del file xml delle concentrazioni per il materiale corrente 
        /// </summary>
        private List<string> _currentReportWarningsXML;

        #endregion


        #region COSTRUTTORE 

        /// <summary>
        /// By default le proprieta relative al recupero alla validazione e alla possibile persistenza sono a false 
        /// per l'oggetto corrente 
        /// per l'oggetto viene anche passata la provenienza dell'informazione corrente
        /// </summary>
        /// <param name="origineInfomazione"></param>
        public MaterialConcentrationsObject(Utils.Constants.OriginOfInformation origineInfomazione)
        {
            Step1_Recupero = false;
            Step2_Validazione_SameSheet = false;
            Step3_Persistenza = false;

            _origineInformazione = origineInfomazione;

            // inizializzazione della stringa di report errori excel 
            _currentReportErrorsExcel = new List<string>();

            // inizializzazione della stringa di report errori xml
            _currentReportErrorsXML = new List<string>();

            // inizializzazione della stringa di report warnings excel 
            _currentReportWarningsExcel = new List<string>();

            // inizializzazione della stringa di report warnings xml
            _currentReportWarningsXML = new List<string>();
        }

        #endregion


        #region PROPRIETA RELATIVE AL RIEMPIMENTO PER LE CONCENTRAZIONI CORRENTI

        /// <summary>
        /// Quadrante di riferimento sul quale vengono lette le informazioni inerenti 
        /// il materiale corrente con le relative concentrazioni
        /// </summary>
        public ExcelConcQuadrant ExcelQuadrantReference { get; set; }


        /// <summary>
        /// Nome del materiale corrispondente 
        /// </summary>
        public string MaterialName { get; set; }


        /// <summary>
        /// Nome per la lega corrispondente sul quale viene preso il materiale 
        /// </summary>
        public string AlloyName { get; set; }


        /// <summary>
        /// Tutte le righe lette per il quadrante di concentrazioni corrente
        /// </summary>
        public List<RowFoglioExcel> ReadConcentrationsRows { get; set; }


        /// <summary>
        /// Impostazione delle rispettive righe di concentrazione per la valorizzazione 
        /// vera e propria per un database di origine / di destinazione delle informazioni
        /// </summary>
        public List<ConcLegaDB> ConcentrationsDB { get; set; }


        #region STEPS

        /// <summary>
        /// Indica se l'informazione corrente per il quadrante da inserire è stata recuperata correttamente 
        /// </summary>
        public bool Step1_Recupero { get; set; }


        /// <summary>
        /// Indica se l'informazione corrente per il foglio è stata validata correttamente in base alle informazioni contenute nello stesso foglio 
        /// </summary>
        public bool Step2_Validazione_SameSheet { get; set; }
        

        /// <summary>
        /// Indica se l'informazione corrente può essere persistita in base alle informazioni di lega contenute in questo foglio o gia presenti 
        /// all'interno della sorgente (che quindi validano in se il set di concentrazioni che si sta inserendo)
        /// </summary>
        public bool Step3_Persistenza { get; set; }


        /// <summary>
        /// Provenienza per l'informazione corrente 
        /// </summary>
        private Utils.Constants.OriginOfInformation _origineInformazione;


        /// <summary>
        /// Origine per l'informazione corrente 
        /// </summary>
        public Utils.Constants.OriginOfInformation OrigineInformazione { get { return this._origineInformazione; } }
        
        #endregion


        #region SEGNALAZIONI RELATIVE ALL'EVENTUALE REPORT DI COMPIILAZIONE PER IL SET DI CONCENTRAZIONI CORRENTE 

        /// <summary>
        /// Permette l'accodamento di un messaggio relativamente alla segnalazione di qualcosa che non va nella lettura delle concentrazioni per il materiale corrente 
        /// all'interno del foglio excel 
        /// </summary>
        /// <param name="currentMessage"></param>
        public void InsertNewErroMessageInReport_Excel(string currentMessage)
        {
            _currentReportErrorsExcel.Add(currentMessage);
        }


        /// <summary>
        /// Permette l'accodamento di un messaggio relativamente alla segnalazione di qualcosa che non va nella lettura delle concentrazioni per il materiale corrente 
        /// all'interno del foglio xml
        /// </summary>
        /// <param name="currentMessage"></param>
        public void InsertNewErrorMessageInReport_XML(string currentMessage)
        {
            _currentReportErrorsXML.Add(currentMessage);
        }


        /// <summary>
        /// Segnalazione di un messaggio di warning che non inficia la natura dell'importazione ma che viene comunque segnalato all'utente nel caso si riscontri
        /// qualcosa che potrebbe generare anomalia nell'analisi delle concentrazioni per il materiale e per il foglio excel corrente 
        /// </summary>
        /// <param name="currentMessage"></param>
        public void InsertNewWarningMessageInReport_Excel(string currentMessage)
        {
            _currentReportWarningsExcel.Add(currentMessage);
        }


        /// <summary>
        /// Segnalazione di un messaggio di warning che non inficia la natura dell'importazione ma che viene comunque segnalato all'utente nel caso si riscontri 
        /// qualcosa che potrebbe generare anomalia nell'analis delle concentrazioni per il materiale e per il foglio xml corrente 
        /// </summary>
        /// <param name="currentMessage"></param>
        public void InsertNewWarningMessageInReport_XML(string currentMessage)
        {
            _currentReportWarningsXML.Add(currentMessage);
        }


        /// <summary>
        /// Permette di ottenere il report corrente per gli errori nella lettura dell'excel 
        /// </summary>
        public List<string> GetReportErrorsExcel { get { return this._currentReportErrorsExcel; } }


        /// <summary>
        /// Permette di ottenere il report corrente per gli errori nella lettura del file xml
        /// </summary>
        public List<string> GetReportErrorsXML { get { return this._currentReportErrorsXML; } }


        /// <summary>
        /// Permette di ottenere il report di messaggi di alert per il file excel 
        /// </summary>
        public List<string> GetReportWarningsExcel { get { return this._currentReportWarningsExcel; } }


        /// <summary>
        /// Permette di ottenere il report di messaggi di alert per il file xml
        /// </summary>
        public List<string> GetReportWarningsXML { get { return this._currentReportWarningsXML; } }

        #endregion

        #endregion
    }
}
