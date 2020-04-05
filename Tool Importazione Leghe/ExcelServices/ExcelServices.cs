using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// Nome per il file excel correntemente aperto
        /// </summary>
        private string _currentExcelName;


        /// <summary>
        /// Indicazione dell'apertura del file excel corrente
        /// </summary>
        private ExcelPackage _currentOpenedExcel;


        /// <summary>
        /// Insieme di tutti i fogli excel presenti nel file excel correntemente aperto
        /// </summary>
        private List<ExcelSheetWithUtilInfo> _currentSheetsExcel;


        /// <summary>
        /// Servizio dove si trovano i metodi per il riconoscimento vero e proprio di un determinato header e quindi 
        /// il riconoscimento di un foglio excel di un certo tipo rispetto a un altro 
        /// </summary>
        private ReadHeaders _currentReadHeadersServices;


        /// <summary>
        /// Servizio di validazione e match delle informazioni per il file excel corrente
        /// </summary>
        private ReadAndValidateExcelInfo _currentInfoExcelValidator;



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

            // servizi di validazione e match informazioni foglio 
            _currentInfoExcelValidator = new ReadAndValidateExcelInfo();
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
        /// Con questo metodo si esegue una prima apertura del file excel di partenza 
        /// </summary>
        public void OpenExcelFile()
        {
            try
            {
                // set licenza corrente
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                _currentExcelName = GeneralUtilities.GetFileName(Utils.Constants.CurrentFileExcelPath);

                // apertura excel corrente
                FileStream currentFileExcel = new FileStream(Utils.Constants.CurrentFileExcelPath,FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                _currentOpenedExcel = new ExcelPackage(currentFileExcel);

                // segnalazione apertura corretta per il file excel corrente
                ServiceLocator.GetLoggingService.GetLoggerExcel.AperturaCorrettaFileExcel(_currentExcelName, CurrentModalitaExcel.EXCELREADER);
                
            }
            catch(Exception e)
            {
                string currentExceptionMsg = String.Format(ExceptionMessages.PROBLEMIAPERTURAFOGLIOEXCEL, _currentExcelName);
                currentExceptionMsg += "\n";
                currentExceptionMsg += e.Message;

                ServiceLocator.GetLoggingService.GetLoggerExcel.SegnalazioneEccezione(currentExceptionMsg);
            }

        }


        /// <summary>
        /// Permette di leggere le informazioni di base per i fogli excel presenti nel documento che 
        /// è stato appena aperto
        /// </summary>
        /// <param name="currentExcelFile"></param>
        public void ReadCurrentSheets(CurrentModalitaExcel CurrentModalita)
        {
            if (_currentOpenedExcel == null)
                throw new Exception(ExceptionMessages.CONTENUTONULLOVARIABILEEXCEL);


            // segnalazione della posizione per il file excel corrente
            int currentSheetPosition = 0;

            // inizializzazione della lista relativa ai fogli excel in lettura corrente
            _currentSheetsExcel = new List<ExcelSheetWithUtilInfo>();

            foreach(ExcelWorksheet currentWorksheet in _currentOpenedExcel.Workbook.Worksheets)
            {
                string currentSheetName = currentWorksheet.Name;

                ServiceLocator.GetLoggingService.GetLoggerExcel.HoTrovatoIlSeguenteFoglioExcel(currentSheetName, _currentExcelName, CurrentModalita);

                ExcelSheetWithUtilInfo currentSheetInfo = new ExcelSheetWithUtilInfo();

                currentSheetInfo.SheetName = currentSheetName;
                currentSheetInfo.ExcelFile = _currentExcelName;
                currentSheetInfo.PositionInExcelFile = currentSheetPosition;
                currentSheetInfo.TipologiaRiconosciuta = Utils.Constants.TipologiaFoglioExcel.Unknown;
                
                _currentSheetsExcel.Add(currentSheetInfo);

                currentSheetPosition++;
            }
        }


        #region STEP 1: READER EXCEL

        /// <summary>
        /// Permette la lettura corretta degli headers e quindi la distinzione di tutti i fogli per i quali 
        /// sono presenti delle informazioni di lega che dovranno poi essere mappate dalle diverse concentrazioni 
        /// contenute nei fogli rimanenti
        /// </summary>
        /// <param name="CurrentModalita"></param>
        public void ReadHeaderLeghe(CurrentModalitaExcel CurrentModalita)
        {
            if (_currentSheetsExcel == null)
                throw new Exception(ExceptionMessages.NESSUNFOGLIOCONTENUTOINEXCEL);

            if (_currentSheetsExcel.Count == 0)
                throw new Exception(ExceptionMessages.NESSUNFOGLIOCONTENUTOINEXCEL);

            foreach(ExcelSheetWithUtilInfo currentExcelSheet in _currentSheetsExcel)
            {
                int currentSheetPos = currentExcelSheet.PositionInExcelFile;
                
                ExcelWorksheet currentFoglio = _currentOpenedExcel.Workbook.Worksheets[currentSheetPos];

                // eventuale informazioni header per la lettura delle informazioni generali di lega 
                List<HeadersInfoLega_Excel> headersInformazioniGeneraliFoglioCorrente = null;
            
                // eventuale informazioni quadranti concentrazioni per la lega corrente 
                List<ExcelConcQuadrant> quadrantiConcentrazioniPerFoglioCorrente = null;
                

                // controllo che il foglio sia di informazioni generali di lega
                if (_currentReadHeadersServices.ReadInformation_GeneralInfoLega(ref currentFoglio, out headersInformazioniGeneraliFoglioCorrente))
                {
                    currentExcelSheet.TipologiaRiconosciuta = Constants.TipologiaFoglioExcel.Informazioni_Lega;

                    ServiceLocator.GetLoggingService.GetLoggerImportActivity.GetSeparatorInternalActivity();

                    // segnalazione a console riconoscimento foglio a carattere generale per la lega 
                    ServiceLocator.GetLoggingService.GetLoggerExcel.HoRiconosciutoIlFoglioComeContenenteInformazioniGeneraliLega(currentFoglio.Name);

                    // inserimento delle informazioni lette per gli headers per la lettura delle informazioni generali per la lega corrente
                    currentExcelSheet.GeneralInfo_Lega = headersInformazioniGeneraliFoglioCorrente;
                }
                // controllo che il foglio corrente non sia un foglio di lettura delle concentrazioni per nomi appartenenti a una certa lega 
                else if(_currentReadHeadersServices.ReadHeaders_Concentrazioni(ref currentFoglio, Constants.TipologiaFoglioExcel.Informazioni_Concentrazione, out quadrantiConcentrazioniPerFoglioCorrente))
                {
                    currentExcelSheet.TipologiaRiconosciuta = Constants.TipologiaFoglioExcel.Informazioni_Concentrazione;

                    ServiceLocator.GetLoggingService.GetLoggerImportActivity.GetSeparatorInternalActivity();

                    // segnalazione a console riconoscimento foglio come foglio delle concentrazioni di materiali per una determinata lega 
                    ServiceLocator.GetLoggingService.GetLoggerExcel.HoRiconosciutoIlFoglioComeContenenteConcentrazioniMateriali(currentFoglio.Name);

                    // inserimento delle informazioni lette per i quadranti delle concentrazioni per la lega corrente 
                    currentExcelSheet.Concentrations_Quadrants = quadrantiConcentrazioniPerFoglioCorrente;
                }


                // separazione delle attività
                ServiceLocator.GetLoggingService.GetLoggerImportActivity.GetSeparatorInternalActivity();

                //LoggingService.GetSomeTimeOnConsole();

            }
        }

        #endregion


        #region STEP 2: ANALISI SINTASSI EXCEL (PRE DATABASE)

        /// <summary>
        /// Permette l'analisi della sintassi del foglio excel per il quale al momento sono stati letti sono i quadranti e gli headers relativi 
        /// alle informazioni di lega e di concentrazioni materiali.
        /// Questa analisi viene fatta prima di eseguire il compare delle informazioni database vere e proprie
        /// </summary>
        /// <param name="currentModalita"></param>
        public void AnalyzeExcelSheetsSyntax(CurrentModalitaExcel currentModalita)
        {
            // lettura delle informazioni per i fogli contenuti nel file excel corrente 
            ReadExcelUtilInformation(currentModalita);
        }


        /// <summary>
        /// Permette di leggere l'effettivo contenuto di tutte le informazioni contenute nel file excel di partenza 
        /// prima della vera e propria validazione delle informazioni contenute al suo interno
        /// </summary>
        /// <param name="currentModalita"></param>
        private void ReadExcelUtilInformation(CurrentModalitaExcel currentModalita)
        {
            if (_currentSheetsExcel == null)
                throw new Exception(ExceptionMessages.NESSUNFOGLIOCONTENUTOINEXCEL);

            if (_currentSheetsExcel.Count == 0)
                throw new Exception(ExceptionMessages.NESSUNFOGLIOCONTENUTOINEXCEL);


            foreach (ExcelSheetWithUtilInfo currentExcelSheet in _currentSheetsExcel)
            {

                int currentSheetPos = currentExcelSheet.PositionInExcelFile;

                // recupero del foglio excel in base alla posizione 
                ExcelWorksheet currentFoglio = _currentOpenedExcel.Workbook.Worksheets[currentSheetPos];

                // lista righe finali per il caso relativo a foglio leghe 
                List<LegaInfoObject> listaInformazioniGeneraliLega = new List<LegaInfoObject>();

                // lista oggetti elementi per il caso in cui stia parlando di un foglio di concentrazioni
                List<MaterialConcentrationsObject> listaInformazioniConcentrazioniMateriali = new List<MaterialConcentrationsObject>();

                if (currentExcelSheet.TipologiaRiconosciuta == Constants.TipologiaFoglioExcel.Informazioni_Lega)
                {
                    // segnalazione di inizio recupero informazioni per il foglio excel corrente
                    ServiceLocator.GetLoggingService.GetLoggerExcel.InizioLetturaInformazioniPerFoglioExcelCorrente(currentFoglio.Name, currentExcelSheet.TipologiaRiconosciuta);

                    // recupero delle informazioni generali per il foglio corrente 
                    bool hoLettoInformazioni = _currentInfoExcelValidator.GetAllGeneralInfoFromExcel(ref currentFoglio, currentExcelSheet.GeneralInfo_Lega, out listaInformazioniGeneraliLega);

                    if (!hoLettoInformazioni)
                        ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoAlcunaInformazionePerIlFoglio(currentFoglio.Name);
                    else
                    {
                        ServiceLocator.GetLoggingService.GetLoggerExcel.InformazioniPerFoglioRecuperateCorrettamente(currentFoglio.Name);

                        // inserimento per il foglio della lista delle concentrazioni letto
                        currentExcelSheet.InfoLegheFromThisExcel = listaInformazioniGeneraliLega;

                        // indico che la lettura delle informazioni è andata a buon fine 
                        currentExcelSheet.LetturaInformazioniCorretto = true;
                    }

                }
                else if (currentExcelSheet.TipologiaRiconosciuta == Constants.TipologiaFoglioExcel.Informazioni_Concentrazione)
                {
                    // segnalazione di inizio recupero informazioni per il foglio excel corrente
                    ServiceLocator.GetLoggingService.GetLoggerExcel.InizioLetturaInformazioniPerFoglioExcelCorrente(currentFoglio.Name, currentExcelSheet.TipologiaRiconosciuta);

                    bool hoLettoInformazioni = _currentInfoExcelValidator.GetAllConcentrationsFromExcel(ref currentFoglio, currentExcelSheet.Concentrations_Quadrants, out listaInformazioniConcentrazioniMateriali);

                    if (!hoLettoInformazioni)
                        ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoAlcunaInformazionePerIlFoglio(currentFoglio.Name);
                    else
                    {
                        ServiceLocator.GetLoggingService.GetLoggerExcel.InformazioniPerFoglioRecuperateCorrettamente(currentFoglio.Name);

                        // inserimento per il foglio della lista delle concentrazioni letto
                        currentExcelSheet.InfoConcentrationsFromThisExcel = listaInformazioniConcentrazioniMateriali;

                        // indico che la lettura delle informazioni è andata a buon fine 
                        currentExcelSheet.LetturaInformazioniCorretto = true;
                    }

                }

            }
        }


        /// <summary>
        /// Permette l'analis di sintassi del foglio excel corrente nei fogli per i quali è stato inserito contenuto
        /// </summary>
        /// <param name="currentModalita"></param>
        private void AnalyzeExcelSyntax(CurrentModalitaExcel currentModalita)
        {

        }


        /// <summary>
        /// Permette l'esecuzione del match delle informazioni contenute nel foglio excel per la successiva (ancora ipotetica)
        /// persistenza all'interno della detinazione 
        /// </summary>
        /// <param name="currentModalita"></param>
        public void MatchInformationForDestination(CurrentModalitaExcel currentModalita)
        {

        }

        #endregion

        #endregion
    }
}
