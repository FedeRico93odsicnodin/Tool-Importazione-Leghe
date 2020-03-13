using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.ModifiedElements;

namespace Tool_Importazione_Leghe.Utils
{
    /// <summary>
    /// In questa classe devono essere inserite tutte le configurazioni in lettura da un file di configurazione apposito
    /// e l'indicazione del tempo utilizzata per markare le diverse righe in log corrente
    /// </summary>
    public class Configurations
    {
        #region COSTANTI CHE DEVONO ESSERE PRESE DAL DOCUMENTO

        /// <summary>
        /// Indicazione sulla stringa di connessione che mi serve per leggere / inserire nel database
        /// </summary>
        private object[] DBCONNECTION_NPG = { "DBCONNECTION_NPG:", false };


        /// <summary>
        /// Indicazione sul documento excel di origine sul quale andare a leggere / inserire
        /// </summary>
        private object[] EXCELDOCUMENT = { "EXCELDOCUMENT:", false };


        /// <summary>
        /// Indicazione del documento xml di origine sul quale andare a leggere / inserire
        /// </summary>
        private object[] XMLDOCUMENT = { "XMLDOCUMENT:", false };


        /// <summary>
        /// Eventuale secondo database da usare come destinazione 
        /// </summary>
        private object[] DBCONNECTION_NPG_DESTINATION = { "DBCONNECTION_NPG_DESTINATION:", false };


        /// <summary>
        /// Eventuale secondo foglio excel da utilizzare come destinazione 
        /// </summary>
        private object[] EXCELDOCUMENT_DESTINATION = { "EXCELDOCUMENT_DESTINATION:", false };


        /// <summary>
        /// Eventuale secondo foglio xml da utizzare come destinazione
        /// </summary>
        private object[] XMLDOCUMENT_DESTINATION = { "XMLDOCUMENT_DESTINATION:", false };


        /// <summary>
        /// Indicazione della tipologia di import che bisogna seguire 
        /// </summary>
        private object[] TIPOLOGIA_IMPORT = { "TIPOLOGIA_IMPORT:", false };


        /// <summary>
        /// Permette di capire in che modalità si sta lanciando il tool, se in console o window application
        /// </summary>
        private object[] CURRENTMODALITATOOL = { "CURRENTMODALITATOOL:", false };


        /// <summary>
        /// Indicazione sul quale sarà il path per il file di log finale 
        /// </summary>
        private object[] PATHLOGFILE = { "PATHLOGFILE:", false };


        /// <summary>
        /// In questa lista saranno contenuti tutti gli oggetti di configurazione utile all'avviamento 
        /// dell'import
        /// </summary>
        private List<object[]> _letturaConfig;


        /// <summary>
        /// Mappatura del tempo trascorso durante tutta la fase di import 
        /// </summary>
        private ExtendedStopWatch _currentTimerOnProcedure;


        /// <summary>
        /// Mi permette di capire in base al nuovo percorso eventualmente letto per il file di log 
        /// se questo deve essere spostato rispetto alla destinazione precedente 
        /// </summary>
        private bool _devoSpostareFileLog;

        #endregion


        #region COSTRUTTORE - INIZIALIZZAZIONE DELLA LISTA DI CONFIGURAZIONI E DEL TEMPO CHE STA TRASCORRENDO

        /// <summary>
        /// Inizializzazione dei parametri di import
        /// </summary>
        public Configurations()
        {
            _letturaConfig = new List<object[]>();

            _letturaConfig.Add(this.DBCONNECTION_NPG);
            _letturaConfig.Add(this.DBCONNECTION_NPG_DESTINATION);

            _letturaConfig.Add(this.EXCELDOCUMENT);
            _letturaConfig.Add(this.EXCELDOCUMENT_DESTINATION);

            _letturaConfig.Add(this.XMLDOCUMENT);
            _letturaConfig.Add(this.XMLDOCUMENT_DESTINATION);

            _letturaConfig.Add(this.CURRENTMODALITATOOL);
            _letturaConfig.Add(this.TIPOLOGIA_IMPORT);


            _currentTimerOnProcedure = new ExtendedStopWatch();
        }

        #endregion


        #region METODO DI LETTURA DELLE IMPOSTAZIONI

        /// <summary>
        /// Mi permette di fare partire il timer prima dell'inizio della procedura effettiva di import per tenere traccia 
        /// del relativo tempo di inizio / fine della procedura nel suo complesso
        /// </summary>
        public void StartTimerOnProcedure()
        {
            _currentTimerOnProcedure.Start();
            
            // indico di avere appena fatto partire il tempo sulla procedura 
            ServiceLocator.GetLoggingService.GetLoggerConfiguration.HoAppenaInizializzatoTimerSuProcedura();

        }


        /// <summary>
        /// Mi permette di ottenere il tempo corrente passato per la procedura, questo tempo mi serve per loggare correttamente le cose 
        /// a console e nella procedura complessiva
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetCurrentProcedureTime()
        {
            return _currentTimerOnProcedure.Elapsed;
        }


        /// <summary>
        /// Mi permette di stoppare il timer corrente al termine della procedura, questo metodo verrà richiamato 
        /// unicamente alla fine di tutta la procedura, gli eventuali tempi sommati da start e stop import precedenti vengono utilizzati come somma 
        /// </summary>
        public void StopTimerOnProcedure()
        {
            _currentTimerOnProcedure.Stop();

            // indico di avere appena fermato il tempo sulla procedura 
            ServiceLocator.GetLoggingService.GetLoggerConfiguration.HoAppenaStoppatoTimerSuProcedura();


        }


        /// <summary>
        /// Lettura delle impostazioni di config e set delle costanti per l'import corrente 
        /// </summary>
        public void ReadConfigFile()
        {
            // controllo esistenza del file
            if (!File.Exists(Constants.CurrentFileConfig))
                throw new Exception(ExceptionMessages.NONHOTROVATOFILECONFIGURAZIONI);


            string currentLetturaConfig = "";
            StreamReader fileConfig = new StreamReader(Constants.CurrentFileConfig);

            while(fileConfig.EndOfStream == false)
            {
                currentLetturaConfig = fileConfig.ReadLine();

                if (currentLetturaConfig.StartsWith("--") || currentLetturaConfig == "")
                    continue;

                // CONFIGURAZIONE 1: database di origine
                if(currentLetturaConfig.Contains((string)this.DBCONNECTION_NPG[0]))
                {
                    string readConnectionString_origin = currentLetturaConfig.Substring(this.DBCONNECTION_NPG[0].ToString().Length);

                    readConnectionString_origin = readConnectionString_origin.Trim();

                    Constants.NPGConnectionString = readConnectionString_origin;

                    // log della lettura corretta per la configurazione corrente 
                    ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.DBCONNECTION_NPG[0].ToString());

                    // ho letto la confirazione corrente per il database di origine 
                    _letturaConfig.Where(x => x[0] == this.DBCONNECTION_NPG[0]).FirstOrDefault()[1] = true;
                }
                // CONFIGURAZIONE 2: database di destinazione
                else if(currentLetturaConfig.Contains((string)this.DBCONNECTION_NPG_DESTINATION[0]))
                {
                    string readConnectionString_destination = currentLetturaConfig.Substring(this.DBCONNECTION_NPG_DESTINATION[0].ToString().Length);

                    readConnectionString_destination = readConnectionString_destination.Trim();
                    
                    Constants.NPGConnectionString_Destination = readConnectionString_destination;

                    // log della lettura corretta per la configurazione corrente 
                    ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.DBCONNECTION_NPG_DESTINATION[0].ToString());

                    // ho letto la configurazione corrente per il database di destinazione
                    _letturaConfig.Where(x => x[0] == this.DBCONNECTION_NPG_DESTINATION[0]).FirstOrDefault()[1] = true;
                }
                // CONFIGURAZIONE 3: excel di origine
                else if(currentLetturaConfig.Contains((string)this.EXCELDOCUMENT[0]))
                {
                    string readExcelPath_origine = currentLetturaConfig.Substring(this.EXCELDOCUMENT[0].ToString().Length);

                    readExcelPath_origine = readExcelPath_origine.Trim();

                    Constants.CurrentFileExcelPath = readExcelPath_origine;

                    // log della lettura corretta per la configurazione corrente 
                    ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.EXCELDOCUMENT[0].ToString());

                    // ho letto la configurazione corrente per l'excel di origine
                    _letturaConfig.Where(x => x[0] == this.EXCELDOCUMENT[0]).FirstOrDefault()[1] = true;


                }
                // CONFIGURAZIONE 4: excel di destinazione
                else if(currentLetturaConfig.Contains((string)this.EXCELDOCUMENT_DESTINATION[0]))
                {
                    string readExcelPath_destination = currentLetturaConfig.Substring(this.EXCELDOCUMENT_DESTINATION[0].ToString().Length);

                    readExcelPath_destination = readExcelPath_destination.Trim();

                    Constants.CurrentFileExcelPath_Destination = readExcelPath_destination;

                    // log della lettura corretta per la configurazione corrente 
                    ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.EXCELDOCUMENT_DESTINATION[0].ToString());

                    // ho letto la configurazione corrente per l'excel di destinazione
                    _letturaConfig.Where(x => x[0] == this.EXCELDOCUMENT_DESTINATION[0]).FirstOrDefault()[1] = true;
                }
                // CONFIGURAZIONE 5: xml di origine
                else if(currentLetturaConfig.Contains((string)this.XMLDOCUMENT[0]))
                {
                    string readXMLPath_origin = currentLetturaConfig.Substring(this.XMLDOCUMENT[0].ToString().Length);

                    readXMLPath_origin = readXMLPath_origin.Trim();

                    Constants.CurrentFileXMLPath = readXMLPath_origin;

                    // log della lettura corretta per la configurazione corrente 
                    ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.XMLDOCUMENT[0].ToString());

                    // ho letto la configurazione corrente per l'xml di origine
                    _letturaConfig.Where(x => x[0] == this.XMLDOCUMENT[0]).FirstOrDefault()[1] = true;
                }
                // CONFIGURAZIONE 6: xml di destinazione 
                else if(currentLetturaConfig.Contains((string)this.XMLDOCUMENT_DESTINATION[0]))
                {
                    string readXMLPath_destination = currentLetturaConfig.Substring(this.XMLDOCUMENT_DESTINATION[0].ToString().Length);

                    readXMLPath_destination = readXMLPath_destination.Trim();

                    Constants.CurrentFileXMLPath_Destination = readXMLPath_destination;

                    // log della lettura corretta per la configurazione corrente 
                    ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.XMLDOCUMENT_DESTINATION[0].ToString());

                    // ho letto la configurazione per l'xml di destinazione
                    _letturaConfig.Where(x => x[0] == this.XMLDOCUMENT_DESTINATION[0]).FirstOrDefault()[1] = true;
                }
                // CONFIGURAZIONE 7: tipologia di import 
                else if(currentLetturaConfig.Contains((string)this.TIPOLOGIA_IMPORT[0]))
                {
                    string currentTipologiaImport = currentLetturaConfig.Substring(this.TIPOLOGIA_IMPORT[0].ToString().Length);

                    currentTipologiaImport = currentTipologiaImport.Trim();

                    switch(currentTipologiaImport)
                    {
                        case "excel_to_database":
                            {
                                Constants.CurrentTipologiaImport = Constants.TipologiaImport.excel_to_database;

                                // ho letto la configurazione per la tipologia import 
                                _letturaConfig.Where(x => x[0] == this.TIPOLOGIA_IMPORT[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.TIPOLOGIA_IMPORT[0].ToString());

                                break;
                            }
                        case "database_to_excel":
                            {
                                Constants.CurrentTipologiaImport = Constants.TipologiaImport.database_to_excel;

                                // ho letto la configurazione per la tipologia import 
                                _letturaConfig.Where(x => x[0] == this.TIPOLOGIA_IMPORT[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.TIPOLOGIA_IMPORT[0].ToString());

                                break;
                            }
                        case "xml_to_database":
                            {
                                Constants.CurrentTipologiaImport = Constants.TipologiaImport.xml_to_database;

                                // ho letto la configurazione per la tipologia import 
                                _letturaConfig.Where(x => x[0] == this.TIPOLOGIA_IMPORT[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.TIPOLOGIA_IMPORT[0].ToString());

                                break;
                            }
                        case "database_to_xml":
                            {
                                Constants.CurrentTipologiaImport = Constants.TipologiaImport.database_to_xml;

                                // ho letto la configurazione per la tipologia import 
                                _letturaConfig.Where(x => x[0] == this.TIPOLOGIA_IMPORT[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.TIPOLOGIA_IMPORT[0].ToString());

                                break;
                            }
                        case "database_to_database":
                            {
                                Constants.CurrentTipologiaImport = Constants.TipologiaImport.database_to_database;

                                // ho letto la configurazione per la tipologia import 
                                _letturaConfig.Where(x => x[0] == this.TIPOLOGIA_IMPORT[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.TIPOLOGIA_IMPORT[0].ToString());

                                break;
                            }
                        case "excel_to_excel":
                            {
                                Constants.CurrentTipologiaImport = Constants.TipologiaImport.excel_to_excel;

                                // ho letto la configurazione per la tipologia import 
                                _letturaConfig.Where(x => x[0] == this.TIPOLOGIA_IMPORT[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.TIPOLOGIA_IMPORT[0].ToString());

                                break;
                            }
                        case "xml_to_xml":
                            {
                                Constants.CurrentTipologiaImport = Constants.TipologiaImport.xml_to_xml;

                                // ho letto la configurazione per la tipologia import 
                                _letturaConfig.Where(x => x[0] == this.TIPOLOGIA_IMPORT[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.TIPOLOGIA_IMPORT[0].ToString());

                                break;
                            }
                    }
                }
                // CONFIGURAZIONE 8: modalità tool
                else if(currentLetturaConfig.Contains((string)this.CURRENTMODALITATOOL[0]))
                {
                    string currentConfigFile = currentLetturaConfig.Substring(this.CURRENTMODALITATOOL[0].ToString().Length);

                    currentConfigFile = currentConfigFile.Trim();

                    switch(currentConfigFile)
                    {
                        case "console":
                            {
                                Constants.CurrentModalitàTool = Constants.CurrentModalitaTool.isConsoleAppication;

                                // ho letto la configurazione sulla modalita del tool 
                                _letturaConfig.Where(x => x[0] == this.CURRENTMODALITATOOL[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.CURRENTMODALITATOOL[0].ToString());

                                break;
                            }
                        case "window":
                            {
                                Constants.CurrentModalitàTool = Constants.CurrentModalitaTool.isWPFApplication;

                                // ho letto la configurazione sulla modalita del tool 
                                _letturaConfig.Where(x => x[0] == this.CURRENTMODALITATOOL[0]).FirstOrDefault()[1] = true;

                                // log della lettura corretta per la configurazione corrente 
                                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.CURRENTMODALITATOOL[0].ToString());

                                break;
                            }
                    }
                }
                // CONFIGURAZIONE 9: log path per il file di log 
                else if(currentLetturaConfig.Contains((string)this.PATHLOGFILE[0]))
                {
                    string newLogPath = currentLetturaConfig.Substring(this.PATHLOGFILE[0].ToString().Length);

                    newLogPath = newLogPath.Trim();

                    // verifica sul fatto che il path di log sia attualmente diverso rispetto a quello di default
                    if(newLogPath != (Constants.LoggerFolder + Constants.LoggerProcedure))
                    {
                        try
                        {
                            File.Move((Constants.LoggerFolder + Constants.LoggerProcedure), newLogPath);
                            Constants.LoggerFolder = newLogPath;

                            // comunicazione al servizio di log che il path dove continuare a loggare le informazioni è cambiato
                            ServiceLocator.GetLoggingService.RefreshLogFile();

                        }
                        catch(Exception e)
                        {
                            string currentExceptionMessage = ExceptionMessages.ERRORELETTURACONFIGURAZIONELOGFILE + e.Message;

                            throw new Exception(currentExceptionMessage);
                        }
                    }

                    // ho letto la configurazione sulla modalita del tool 
                    _letturaConfig.Where(x => x[0] == this.PATHLOGFILE[0]).FirstOrDefault()[1] = true;

                    // log della lettura corretta per la configurazione corrente 
                    ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaCorrettaConfigurazione(this.PATHLOGFILE[0].ToString());
                }

                
                // dico che ho finito con la lettura ora inizio con la verifica delle informazioni lette
                ServiceLocator.GetLoggingService.GetLoggerConfiguration.StoPerVedereSeTutteLeConfigurazioniSonoCorrette();


                // check validità sulle configurazioni appena lette
                CheckFileValidity();
            }
        }


        /// <summary>
        /// Mi viene sollevata una eccezione nel caso in cui non è stato letto correttamente tutto il file di configurazione corrente 
        /// </summary>
        private void CheckFileValidity()
        {
            // vado a ricercare tutti gli elementi per i quali non sono state ritrovate delle configurazioni valide
            List<object[]> currentNotFilledProperties = _letturaConfig.Where(x => (bool)x[1] == false).ToList();
            
            foreach (object[] currentNotFilledProperty in currentNotFilledProperties)
            {
                // log di lettura scorretta di una configurazione
                ServiceLocator.GetLoggingService.GetLoggerConfiguration.LetturaScorrettaConfigurazione(currentNotFilledProperties[0].ToString());
            }


            // raise exception
            if (currentNotFilledProperties.Count > 0)
                throw new Exception(ExceptionMessages.PROBLEMIDILETTURACONFIGURAZIONI);

        }


        #endregion
    }
}
