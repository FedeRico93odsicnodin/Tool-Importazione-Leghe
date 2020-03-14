using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.ExcelServices
{
    /// <summary>
    /// Qui si trovano tutti i servizi che mi permettono di leggere da un determinato foglio excel in base
    /// alla mappatura foglio 1 o foglio 2 e in base agli header che incontro
    /// Quindi come procedere a livello di popolamento di liste e successivamente scrittura a database
    /// </summary>
    public class XlsServices
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Processo di apertura per il file excel corrente
        /// </summary>
        private Application _currentApplicationExcel;


        /// <summary>
        /// File excel aperto per l'istanza di importazione corrente
        /// </summary>
        private Workbook _currentFileExcel;


        /// <summary>
        /// Nome per il file excel correntemente aperto
        /// </summary>
        private string _currentExcelName;


        /// <summary>
        /// Insieme di tutti i fogli excel presenti nel file excel correntemente aperto
        /// </summary>
        private List<ExcelSheet> _currentSheetsExcel;


        /// <summary>
        /// Servizio dove si trovano i metodi per il riconoscimento vero e proprio di un determinato header e quindi 
        /// il riconoscimento di un foglio excel di un certo tipo rispetto a un altro 
        /// </summary>
        private ReadHeaders _currentReadHeadersServices;



        #endregion


        #region COSTRUTTORE

        /// <summary>
        /// Current excel services - istanziazione dei diversi servizi per la lettura corrente 
        /// da un determinato foglio excel
        /// </summary>
        public XlsServices()
        {
            // servizi lettura headers e riconoscimento foglio
            _currentReadHeadersServices = new ReadHeaders();
        }

        #endregion


        #region GETTERS SERVIZI
        
        /// <summary>
        /// Getters per la lettura degli headers per il folgio excel corrente e per l'eventuale 
        /// riconscimento tra le 2 tipologie di fogli 
        /// </summary>
        public ReadHeaders GetReadHeadersServices
        {
            get
            {
                return _currentReadHeadersServices;
            }
        }

        #endregion


        #region METODI PUBBLICI
        
        /// <summary>
        /// Mi dice in che modalità sto aprendo il file excel corrente
        /// se per scrivere o per leggere delle informazioni
        /// </summary>
        public enum CurrentModalitaExcel
        {
            EXCELREADER = 1,
            EXCELWRITER = 2
        }


        /// <summary>
        /// Apertura del file excel che viene inserito nelle costanti durante la configurazione
        /// </summary>
        /// <param name="currentExcelPath"></param>
        /// <param name="currentModalitaLettura"></param>
        public void OpenFileExcel(string currentExcelPath, CurrentModalitaExcel currentModalitaLettura)
        {
            try
            {

                _currentApplicationExcel = new Application();

                _currentFileExcel = _currentApplicationExcel.Workbooks.Open(currentExcelPath);

                _currentExcelName = GeneralUtilities.GetFileName(currentExcelPath);

                ServiceLocator.GetLoggingService.GetLoggerExcel.AperturaCorrettaFileExcel(_currentExcelName, currentModalitaLettura);

            }
            catch(Exception e)
            {
                string currentExceptionMsg = String.Format(ExceptionMessages.PROBLEMIAPERTURAFOGLIOEXCEL, currentExcelPath);
                currentExceptionMsg += "\n";
                currentExceptionMsg += e.Message;

                throw new Exception(currentExceptionMsg);
            }
        }


        /// <summary>
        /// Permette il display di tutti i fogli di lavoro contenuti all'interno del file excel aperto
        /// </summary>
        /// <param name="currentModalita"></param>
        public void ReadSheetsExcelFile(CurrentModalitaExcel currentModalita)
        {
            _currentSheetsExcel = new List<ExcelSheet>();

            int indexExcelSheet = 0;

            foreach(Worksheet currentWorksheet in _currentFileExcel.Sheets)
            {
                string sheetName = currentWorksheet.Name;

                ServiceLocator.GetLoggingService.GetLoggerExcel.HoTrovatoIlSeguenteFoglioExcel(sheetName, _currentExcelName, currentModalita);

                ExcelSheet sheetObj = new ExcelSheet();

                sheetObj.SheetName = sheetName;
                sheetObj.SheetName = _currentExcelName;
                sheetObj.TipologiaRiconosciuta = Utils.Constants.TipologiaFoglioExcel.Unknown;
                sheetObj.PositionInExcelFile = indexExcelSheet;
                sheetObj.Letto = false;

                _currentSheetsExcel.Add(sheetObj);

                indexExcelSheet++;
                
            }
        }

        #endregion
    }
}
