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
        private string loggingIdentifier = "LOGGING: ";

        /// <summary>
        /// Messaggio da mostrare nel caso in cui la folder di log non esista e deve essere creata 
        /// </summary>
        private string messaggoCreazioneCartellaLog = "la cartella corrente relativa al log non esite, la sto ricreando";


        /// <summary>
        /// Messagio da mostrare quando la cartella è stata creata e il log inserito al suo interno
        /// </summary>
        private string messaggioCreazioneCartella = "ho appena ricreato la cartella per il log corrente e ho inserito questo al suo interno";
        
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
        }

        #endregion


        #region METODI PUBBLICI AND GETTERS
        
        /// <summary>
        /// Servizio di log generale: passato il path del log e che cosa loggare 
        /// viene inserita la riga nel documento
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="logPath"></param>
        internal static void LogInADocument(string lines, string logPath)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(logPath, true);
            file.WriteLine(lines);

            file.Close();
        }


        /// <summary>
        /// Permette di fare passare un po di tempo all'interno della console
        /// </summary>
        internal static void GetSomeTimeOnConsole()
        {

            for (int i = 0; i < 2; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }

            Console.WriteLine("\n\n");
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
        /// Mi permette di aggiornare il log file per tutti i servizi di log che lo necessitino a partire 
        /// dal nuovo path dato dall'utente (e che deve essere già inserito all'interno delle configurazioni
        /// </summary>
        public void RefreshLogFile()
        {
            this._loggingBaseConfigurations.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
            this._loggingServiceDatabase.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
            this._loggingServiceExcel.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
            this._loggingServiceXML.LoggerFile = Constants.LoggerFolder + Constants.LoggerProcedure;
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

            }
            else if (Constants.CurrentModalitàTool == Constants.CurrentModalitaTool.isWPFApplication)
            {
                // logger modalita UI per database
                _loggingServiceDatabase = new Logging_UI_Database(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingServiceExcel = new Logging_UI_Excel(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingServiceXML = new Logging_UI_XML(Constants.LoggerFolder + Constants.LoggerProcedure);
                _loggingBaseConfigurations = new Logging_UI_Configurations(Constants.LoggerFolder + Constants.LoggerProcedure);
            }
        }


        /// <summary>
        /// Permette di creare la cartella di log corrente nel caso in cui non esista 
        /// </summary>
        private void InitializeLogFolder()
        {
            if(!Directory.Exists(Constants.LoggerFolder))
            {

            }
        }


        /// <summary>
        /// Mi permette di loggare a console la creazione per la folder corrente 
        /// </summary>
        private void LogConsoleMessageCreazioneFolder()
        {

        }


        /// <summary>
        /// Mi permette di loggare a UI la creazione per la folder corrente 
        /// </summary>
        private void LogUIMessageCreazioneFolder()
        {

        }
        
        #endregion
    }
}
