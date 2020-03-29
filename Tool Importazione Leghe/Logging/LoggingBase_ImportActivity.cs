using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Qui dentro sono contenute tutte le istanze di log per l'attività di import vera e propria con la quale 
    /// si avvia la procedura considerando una determinata sorgente e una determinata destinazione 
    /// </summary>
    public abstract class LoggingBase_ImportActivity
    {
        #region ATTRIBUTI PRIVATI - MESSAGGI

        /// <summary>
        /// File di log nel quale verranno inserite le entries per la procedura di import corrente
        /// </summary>
        protected string _currentLogFile;


        /// <summary>
        /// Indicazione della procedura corrente per l'importazione
        /// </summary>
        protected string _currentProcedure;


        /// <summary>
        /// Identificatore relativo all'oggetto di import activity, responsabile del coordinamento di tutte le operazioni in esecuzione
        /// </summary>
        protected string importActivityIdentifier = " IMPORT ACTIVITY: ";


        /// <summary>
        /// Separatore di una attività con un'altra
        /// </summary>
        protected string separatorActivity = "****************************************************************************************************";


        /// <summary>
        /// Mi serve per separare i diversi processi in avvenimento corrente e per una particolare procedura  
        /// </summary>
        protected string separatorInternalProcesses = "----------------------------------------------------------------------------------------------------";


        /// <summary>
        /// Messaggio relativo alla procedura di import in avvio corrente
        /// </summary>
        protected string avviamentoDiUnaCertaOperazione = "si sta avviando la seguente procedura di import: {0}";
        

        /// <summary>
        /// Messaggio di inizio analisi primaria del foglio excel utilizzato come sorgente
        /// In questa fase vengono analizzati gli headers per il recupero delle eventuali informazioni legate alla lega a livello generale 
        /// e le informazioni legate alle concentrazioni dei diversi materiali che sono inseriti per la lega 
        /// </summary>
        protected string _inizioStep = "IMPORT ACTIVITY ({0}): STEP {1} '{2}'";


        /// <summary>
        /// Segnalazione di fine step, per avere maggiori informazioni ci si riporta all'apertura del file di log inserito nelle configurazioni
        /// </summary>
        protected string _fineProceduraCorrente = "IMPORT ACTIVITITY ({0}) fine dello STEP {1} '{2}', per avere maggiori informazioni consultare il log inserito nelle configurazioni";
        

        /// <summary>
        /// Permette di formattare la stringa relativa alla procedura di import corrente 
        /// </summary>
        /// <param name="currentProceduraImport"></param>
        /// <returns></returns>
        protected string FormatProceduraImport(string currentProceduraImport)
        {
            currentProceduraImport = currentProceduraImport.Replace("_", " ");
            return currentProceduraImport;
        }
        
        #endregion


        #region METODI PUBBLICI DI UTILIZZO DEL LOG

        /// <summary>
        /// Segnalazione di avviamento di una determinata procedura di import tra quelle possibili 
        /// per la tipologia di import
        /// </summary>
        /// <param name="currentProcedure"></param>
        public abstract void VieneAvviataLaSeguenteProceduraDiImport(string currentProcedure);


        /// <summary>
        /// Permette di ottenere dei separatori nel caso in cui siano avviate più attività di import
        /// </summary>
        public abstract void GetSeparatorActivity();


        /// <summary>
        /// Mi permette di ottenere un separatore per l'attività interna rispetto a una attività primaria
        /// </summary>
        public abstract void GetSeparatorInternalActivity();


        #region MESSAGGISTICA LEGATA ALLO STEP PER L'ATTIVITA CORRENTE - EXCEL -> DATABASE


        /// <summary>
        /// Segnalazione dello step corrente per la procedura di import dal file excel al database di destinazione
        /// </summary>
        /// <param name="currentStepImportExcelToDatabase"></param>
        public abstract void BeginningCurrentStep_ExcelToDatabase(Constants.TipologiaImport_ExcelToDatabase currentStepImportExcelToDatabase);


        /// <summary>
        /// Indicazione di fine di un certo step per la procedura corrente, insieme a questo viene data anche indicazione del log nel quale 
        /// andare a verificare le informazioni relative all'analisi per il documento excel corrente 
        /// </summary>
        /// <param name="currentStepImportExcelToDatabase"></param>
        public abstract void EndingCurrentStep_ExcelToDatabase(Constants.TipologiaImport_ExcelToDatabase currentStepImportExcelToDatabase);


        #endregion

        #endregion


        #region SETTERS

        /// <summary>
        /// Permette di modificare la stringa relativa al file di log nel caso in cui cambiasse
        /// all'interno delle configurazioni
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
