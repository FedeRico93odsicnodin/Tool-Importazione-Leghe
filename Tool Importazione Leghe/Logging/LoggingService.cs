using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Classe che permette di loggare all'interno di uno specifico file di log
    /// tutta la procedura di import corrente
    /// </summary>
    public class LoggingService
    {
        #region MESSAGGI 

        /// <summary>
        /// Identificatore messaggio proveniente dal servizio di log corrente 
        /// </summary>
        private static string loggingIdentifier = " LOGGING: ";

        /// <summary>
        /// Messaggio da mostrare nel caso in cui la folder di log non esista e deve essere creata 
        /// </summary>
        private static string messaggioAssenzaCartella = "la cartella corrente relativa al log non esite, la sto ricreando";


        /// <summary>
        /// Messagio da mostrare quando la cartella è stata creata e il log inserito al suo interno
        /// </summary>
        private static string messaggioCreazioneCartella = "ho appena ricreato la cartella per il log corrente e ho inserito questo al suo interno";


        /// <summary>
        /// Messaggio di lettura corretta il file di configurazioni e che per avviare la procedura va premuto un tasto
        /// </summary>
        private static string messaggioPartenzaImportazione = "file di configurazioni corretto, premere un tasto per avviare la procedura di import";


        /// <summary>
        /// Messaggio di load delle liste di partenza di import, tra queste liste di partenza è anche presente quella relativa agli elementi correnti
        /// </summary>
        private static string messaggioLoadInizialeListeDiPartenza = "sto facendo il load delle informazioni iniziali di import";

        #endregion


        #region ATTRIBUTI PRIATI

        /// <summary>
        /// Sevizio corrente per la gestione del log Database
        /// </summary>
        private LoggingBase_Database _loggingServiceDatabase;


        /// <summary>
        /// Servizio corrente per la gestione del log Excel
        /// </summary>
        private LoggingBase_Excel _loggingServiceExcel;


        /// <summary>
        /// Servizio corrente per la gestione del log XML
        /// </summary>
        private LoggingBase_XML _loggingServiceXML;


        /// <summary>
        /// Servizio corrente per la gestione del log delle configurazioni
        /// </summary>
        private LoggingBase_Configurations _loggingBaseConfigurations;


        /// <summary>
        /// Servizio corrente per la gestione del log delle attività di import
        /// </summary>
        private LoggingBase_ImportActivity _loggingBaseImportActivity;


        /// <summary>
        /// Servizio corrente per il log per gli altri servizi diversi e di contorno rispetto all'imprtazione
        /// </summary>
        private LoggingBase_Others _loggingOthers;


        /// <summary>
        /// Questa stringa contiene tutte le righe da scrivere all'interno del log prima che sia stato inserito 
        /// all'interno delle configurazioni
        /// </summary>
        private static List<string> _docLines;

        #endregion


        #region COSTRUTTORE 

        /// <summary>
        /// In base alla modalita del tool passata ho l'istanziazione dei diversi log che vengono messi a disposizione per l'import corrente 
        /// </summary>
        /// <param name="currentModalitaTool"></param>
        public LoggingService() 
        {
            // inizializzazione dei logs di partenza
            InitializeLoggers();

            // inizializzazione della lista temporanea nella quale verranno inserite tutte le righe da poi scrivere all'interno del log
            _docLines = new List<string>();
        }

        #endregion


        #region METODI PUBBLICI AND GETTERS
        
        


        /// <summary>
        /// Servizio di log generale: passato il path del log e che cosa loggare 
        /// viene inserita la riga nel documento
        /// Questo è valido sono qualora abbia letto nelle configurazioni la stringa relativa al file di log corrente
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="logPath"></param>
        internal static void LogInADocument(string lines, string logPath)
        {
            if(Constants.HoLettoDocPath)
            {
                // indicazione di eventuale inizializzazione per la folder 
                InitializeLogFolder();

                System.IO.StreamWriter file = new System.IO.StreamWriter(logPath, true);
                file.WriteLine(lines);

                file.Close();
            }
            // inserisco le righe momentaneamente nella lista
            else
                _docLines.Add(lines);
        }


        /// <summary>
        /// Permette di fare passare un po di tempo all'interno della console
        /// </summary>
        internal static void GetSomeTimeOnConsole()
        {

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }

            Console.WriteLine("\n");
        }


        /// <summary>
        /// Getters del servizio di log per il database
        /// </summary>
        public LoggingBase_Database GetLoggerDatabase
        {
            get
            {
                return _loggingServiceDatabase;
            }
        }


        /// <summary>
        /// Getters del servizio di log per il file excel
        /// </summary>
        public LoggingBase_Excel GetLoggerExcel
        {
            get
            {
                return _loggingServiceExcel;
            }
        }


        /// <summary>
        /// Getter per il servizio di log del file XML
        /// </summary>
        public LoggingBase_XML GetLoggerXML
        {
            get
            {
                return _loggingServiceXML;
            }
        }


        /// <summary>
        /// Getter per il servizio di log delle configurazioni
        /// </summary>
        public LoggingBase_Configurations GetLoggerConfiguration
        {
            get
            {
                return _loggingBaseConfigurations;
            }
        }


        /// <summary>
        /// Getter per il servizio di log dell'attività di import
        /// </summary>
        public LoggingBase_ImportActivity GetLoggerImportActivity
        {
            get
            {
                return _loggingBaseImportActivity;
            }
        }


        /// <summary>
        /// Mi permette di aggiornare il log file per tutti i servizi di log che lo necessitino a partire 
        /// dal nuovo path dato dall'utente (e che deve essere già inserito all'interno delle configurazioni
        /// </summary>
        public void RefreshLogFile()
        {
            this._loggingBaseConfigurations.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
            this._loggingServiceDatabase.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
            this._loggingServiceExcel.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
            this._loggingServiceXML.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
            this._loggingBaseImportActivity.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
            this._loggingOthers.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
        }


        /// <summary>
        /// Permette di ottenere le righe fino adesso inserite in memoria e che devono essere inserite anche 
        /// nel log per proseguire
        /// </summary>
        public List<string> GetLoggerCurrentLines
        {
            get
            {
                return _docLines;
            }
        }

        #endregion


        #region METODI PRIVATI

        /// <summary>
        /// Permette di inizializzare l'istanza del logger in base al fatto che debbano essere per la console o per la wpf application
        /// </summary>
        private void InitializeLoggers()
        {
            if (Constants.CurrentModalitàTool == Constants.CurrentModalitaTool.isConsoleAppication)
            {
                // logger modalita console per database
                _loggingServiceDatabase = new Logging_Console_Database(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingServiceExcel = new Logging_Console_Excel(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingServiceXML = new Logging_Console_XML(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingBaseConfigurations = new Logging_Console_Configurations(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingBaseImportActivity = new Logging_Console_ImportActivity(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingOthers = new Logging_Console_Others(Constants.LoggerFolder + Constants.LoggerProcedure);


            }
            else if (Constants.CurrentModalitàTool == Constants.CurrentModalitaTool.isWPFApplication)
            {
                // logger modalita UI per database
                _loggingServiceDatabase = new Logging_UI_Database(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingServiceExcel = new Logging_UI_Excel(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingServiceXML = new Logging_UI_XML(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingBaseConfigurations = new Logging_UI_Configurations(Constants.LoggerFolder + Constants.LoggerProcedure);
                // TODO: inserimento dell'eventuale implementazione della classe per gli altri servizi
            }
        }


        /// <summary>
        /// Permette di creare la cartella di log corrente nel caso in cui non esista 
        /// </summary>
        public static void InitializeLogFolder()
        {
            if (Directory.Exists(Constants.LoggerFolder))
                return;

            if(!Directory.Exists(Constants.LoggerFolder))
            {

                // segnalazione di assenza per la folder di log
                LogConsoleAssenzaCartella();

                Directory.CreateDirectory(Constants.LoggerFolder);

                if (Constants.CurrentModalitàTool == Constants.CurrentModalitaTool.isConsoleAppication)
                    LogConsoleMessageCreazioneFolder();
                else
                    LogUIMessageCreazioneFolder();
            }
        }


        /// <summary>
        /// Segnalazioe di creazione della cartella di log per il caso console
        /// </summary>
        private static void LogConsoleAssenzaCartella()
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();

            currentMessage = currentMessage + loggingIdentifier + messaggioAssenzaCartella;

            LogInADocument(currentMessage, Constants.LoggerFolder + Constants.LoggerProcedure);

            Console.WriteLine(currentMessage);
            
        }


        /// <summary>
        /// Segnalazione di creazione della cartella di log per il caso UI
        /// </summary>
        private static void LogUICreazioneCartella()
        {
            // TODO: implementazione grafica se necessario
        }


        /// <summary>
        /// Mi permette di loggare a console la creazione per la folder corrente 
        /// </summary>
        private static void LogConsoleMessageCreazioneFolder()
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();

            currentMessage = currentMessage + loggingIdentifier + messaggioCreazioneCartella;

            Console.WriteLine(currentMessage);

            LogInADocument(currentMessage, Constants.LoggerFolder + Constants.LoggerProcedure);

            LoggingService.GetSomeTimeOnConsole();
        }


        /// <summary>
        /// Mi permette di loggare a UI la creazione per la folder corrente 
        /// </summary>
        private static void LogUIMessageCreazioneFolder()
        {
            // TODO: implementazione grafica se necessario
        }

        #endregion


        #region METODI MAIN

        /// <summary>
        /// Segnalazione di lettura corretta di tutte le configurazioni a partire dal main e quindi 
        /// che per continuare deve essere premuto un qualsiasi tasto
        /// </summary>
        public void HoLettoConfigurazioniPremereUnTastoPerContinuare()
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();

            currentMessage = currentMessage + loggingIdentifier + messaggioPartenzaImportazione;

            Console.WriteLine(currentMessage);

            LogInADocument(currentMessage, Constants.LoggerFolder + Constants.LoggerProcedure);
            
        }


        /// <summary>
        /// Segnalazione di stare leggendo le liste di partenza dal database, queste liste sono 
        /// indispensabili per l'esecuzione di tutta la procedura di import successiva
        /// </summary>
        public void StoLeggendoListeInizialiElementi()
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();

            currentMessage += loggingIdentifier;
            currentMessage += messaggioLoadInizialeListeDiPartenza;

            Console.WriteLine(currentMessage);

            LogInADocument(currentMessage, Constants.LoggerFolder + Constants.LoggerProcedure);
        }

        #endregion
    }
}
