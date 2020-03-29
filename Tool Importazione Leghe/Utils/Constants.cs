using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Model;

namespace Tool_Importazione_Leghe.Utils
{
    /// <summary>
    /// In questa classe sono presenti tutte le costanti di programma 
    /// </summary>
    public static class Constants
    {
        #region CONFIGURAZIONE AMBIENTE

        /// <summary>
        /// File corrente dal quale vengono lette tutte le configurazioni relative all'import da eseguire per le leghe 
        /// </summary>
        public const string CurrentFileConfig = "ImportLegheConfig.txt";


        /// <summary>
        /// Indica in quale modalità avviare il tool se avere una semplice console application con dei parametri che vengono preconfigurati 
        /// all'interno di un foglio excel o se si tratta di una WPF application
        /// </summary>
        public enum CurrentModalitaTool
        {
            isConsoleAppication = 1,
            isWPFApplication = 2
        }


        /// <summary>
        /// Permette di mappare in quale modalità il tool viene effettivamente lanciato
        /// </summary>
        public static CurrentModalitaTool CurrentModalitàTool = CurrentModalitaTool.isConsoleAppication;


        /// <summary>
        /// Indicazione su che tipologia di import si sta seguendo
        /// </summary>
        public enum TipologiaImport
        {
            excel_to_database = 1,
            database_to_excel = 2,
            xml_to_database = 3,
            database_to_xml = 4,
            database_to_database = 5,
            excel_to_excel = 6,
            xml_to_xml = 7
        }


        /// <summary>
        /// Formattazione in output per la tipologia in import corrente 
        /// </summary>
        /// <param name="currentTipologiaImport"></param>
        /// <returns></returns>
        public static string GetNameTipologiaImport(string currentTipologiaImport)
        {
            currentTipologiaImport = currentTipologiaImport.ToUpper();

            currentTipologiaImport = currentTipologiaImport.Replace("_to_", " -> ");

            return currentTipologiaImport;
        }


        /// <summary>
        /// Indicazione sulla tipologia di import corrente in base agli altri parametri letti
        /// all'interno del file di configurazioni
        /// </summary>
        public static TipologiaImport CurrentTipologiaImport;


        /// <summary>
        /// Stringa di connessione al database postgres sul quale vengono eseguite le operazioni 
        /// di import (origine)
        /// </summary>
        public static string NPGConnectionString = "Server=localhost;Port=6543;User Id=postgres;Password=root;Database=MetalLab300";


        /// <summary>
        /// Stringa di connessione al database postgres sul quale vengono eseguite le operazioni
        /// di import (destinazione)
        /// </summary>
        public static string NPGConnectionString_Destination = "Server=localhost;Port=6543;User Id=postgres;Password=root;Database=MetalLab300";


        /// <summary>
        /// Stringa percorso nel quale trovo il file excel correntemente in analisi (origine)
        /// </summary>
        public static string CurrentFileExcelPath = "D:\\Projects\\GNR\\Tool Importazione Leghe\\Origin Leghe\\Excel\\Nickel_Alloys.xlsx";


        /// <summary>
        /// Stringa percorso nel quale trovo il file excel correntemente in analisi (destinazione)
        /// </summary>
        public static string CurrentFileExcelPath_Destination = "D:\\Projects\\GNR\\Tool Importazione Leghe\\Origin Leghe\\Excel\\Nickel_Alloys.xlsx";


        /// <summary>
        /// Stringa percorso nel quale trovo il xml correntemente in analisi (origine)
        /// </summary>
        public static string CurrentFileXMLPath = "F:\\Projects\\GNR\\Tool Importazione Leghe\\Origin-Dest Leghe\\XML\\ALL_1.7221-G26CR.xml";


        /// <summary>
        /// Stringa percorso nel quale trovo il xml correntemente in analisi (destinazione)
        /// </summary>
        public static string CurrentFileXMLPath_Destination = "F:\\Projects\\GNR\\Tool Importazione Leghe\\Origin-Dest Leghe\\XML\\ALL_1.7221-G26CR.xml";


        /// <summary>
        /// Stringa indicante dove verranno memorizzati i logs per le diverse procedure 
        /// </summary>
        public static string LoggerFolder = "C:\\Loggers\\";


        /// <summary>
        /// Questo mi dice se ho letto il path nel quale verrà inserito il file di log o meno
        /// prima della lettura salvo tutto in memoria per poi scrivere ...
        /// </summary>
        public static bool HoLettoDocPath = false;


        /// <summary>
        /// Stringa indicante il log sulle diverse operazioni eseguite sul database
        /// </summary>
        public static string LoggerProcedure = "ImportazioneLoggingProcedure.txt";


        /// <summary>
        /// Indicazione sulla lettura di tutte le configurazioni per la procedura corrente 
        /// </summary>
        public static bool HoLettoTutteLeConfigurazioni = false;

        #endregion


        #region DB ENTITIES

        /// <summary>
        /// Con questo enumeratore si mappano tutte le possibili entità database disponibili
        /// per l'import corrente
        /// </summary>
        public enum DBLabEntities
        {
            Leghe = 1,
            Normative = 2,
            Elementi = 3,
            Categorie_Leghe = 4,
            Basi = 5,
            ConcLeghe = 6
        }

        #endregion


        #region EXCEL SHEETS

        /// <summary>
        /// Indicazione sulla tipologia di foglio excel sulla quale si sta iterando attualmente 
        /// se si tratta di un foglio contenente le informazioni generali o 
        /// il set di concentrazioni per un determinato materiale
        /// </summary>
        public enum TipologiaFoglioExcel
        {
            Unknown = 0,
            Informazioni_Lega = 1,
            Informazioni_Concentrazione = 2
        }


        /// <summary>
        /// Permette di capire se, leggendo il foglio excel 2 (corrispondente alle concentrazioni)
        /// sarà fatta anche la lettura per deroga minima / massima
        /// </summary>
        public enum DevoInserireDeroghe
        {
            si = 1,
            no = 2
        }
        
        #endregion


        #region LISTE COMUNI - CARICATE A PRESCINDERE DA DB

        /// <summary>
        /// Corrisponde alla lista di tutti gli elementi che vengono recuperati dal database di origine
        /// nel caso delle diverse importazioni questi elementi vengono sempre checkati a priori per verificarne
        /// poi la validità
        /// </summary>
        public static List<string> CurrentListElementi { get; set; }

        #endregion


        #region STEPS DIVERSE PROCEDURE 

        /// <summary>
        /// Indicazione di tutti gli steps da seguire per la tipologia di import che coinvolge l'importazione da un file excel all'interno di un database di destinazione
        /// </summary>
        public enum TipologiaImport_ExcelToDatabase
        {
            ANALISI_MARKERS_EXCEL = 1,
            ANALISI_VALIDITA_INFORMAZIONI_EXCEL = 2,
            ANALISI_VALIDITA_RISPETTO_DATABASE = 3
        }


        /// <summary>
        /// ritorna il nome per lo step della procedura correntemente passata in input come stringa 
        /// </summary>
        /// <param name="stepProcedura"></param>
        /// <returns></returns>
        public static string GetCurrentNameStepProcedura(string stepProcedura)
        {
            stepProcedura = stepProcedura.Replace("_", " ");

            return stepProcedura;
        }
        
        #endregion
    }
}
