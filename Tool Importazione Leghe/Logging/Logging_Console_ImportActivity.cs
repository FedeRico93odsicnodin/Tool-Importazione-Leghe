using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio di log e messaggistica per l'attività di import e l'applicazione in console
    /// </summary>
    public class Logging_Console_ImportActivity : LoggingBase_ImportActivity
    {
        #region COSTRUTTORE

        /// <summary>
        /// Attribuzione del path di log per il logger corrente
        /// </summary>
        /// <param name="currentLogPath"></param>
        public Logging_Console_ImportActivity(string currentLogPath)
        {
            base._currentLogFile = currentLogPath;
        }

        #endregion


        #region IMPLEMENTAZIONE MESSAGGISTICA

        /// <summary>
        /// Implementazione messaggistica a console relativa all'avviamento di una certa procedura di import
        /// </summary>
        /// <param name="currentProcedure"></param>
        public override void VieneAvviataLaSeguenteProceduraDiImport(string currentProcedure)
        {
            string currentLogMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();

            currentLogMessage += base.importActivityIdentifier + String.Format(base.avviamentoDiUnaCertaOperazione, currentProcedure);

            base._currentProcedure = FormatProceduraImport(currentProcedure);

            Console.WriteLine(currentLogMessage);

            LoggingService.LogInADocument(currentLogMessage, base._currentLogFile);
        }


        /// <summary>
        /// Separatore delle attività correnti CONSOLE
        /// </summary>
        public override void GetSeparatorActivity()
        {
            string currentLogMessage = base.separatorActivity;

            Console.WriteLine(currentLogMessage);

            LoggingService.LogInADocument(currentLogMessage, base._currentLogFile);
        }


        /// <summary>
        /// Separatore internal activity CONSOLE
        /// </summary>
        public override void GetSeparatorInternalActivity()
        {
            string currentLogMessage = base.separatorInternalProcesses;

            Console.WriteLine(currentLogMessage);

            LoggingService.LogInADocument(currentLogMessage, base._currentLogFile);
        }


        #region MESSAGING IMPORT ACTIVITY EXCEL -> DATABASE
        
        /// <summary>
        /// Segnalazione a console dell'inizio dello step corrente per la procedura di import dal file excel al database di destinazione 
        /// </summary>
        /// <param name="currentStepImportExcelToDatabase"></param>
        public override void BeginningCurrentStep_ExcelToDatabase(Constants.TipologiaImport_ExcelToDatabase currentStepImportExcelToDatabase)
        {
            string currentLogMessage = String.Format(base._inizioStep, Constants.GetNameTipologiaImport(Constants.TipologiaImport.excel_to_database.ToString()), (int)currentStepImportExcelToDatabase, Constants.GetCurrentNameStepProcedura(Constants.TipologiaImport.excel_to_database.ToString()));

            Console.WriteLine(currentLogMessage);

            LoggingService.LogInADocument(currentLogMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console per la fine dello step corrente legato alla procedura corrente 
        /// </summary>
        /// <param name="currentStepImportExcelToDatabase"></param>
        public override void EndingCurrentStep_ExcelToDatabase(Constants.TipologiaImport_ExcelToDatabase currentStepImportExcelToDatabase)
        {
            string currentLogMessage = String.Format(base._fineProceduraCorrente, Constants.GetNameTipologiaImport(Constants.TipologiaImport.excel_to_database.ToString()), (int)currentStepImportExcelToDatabase, Constants.GetCurrentNameStepProcedura(Constants.TipologiaImport.excel_to_database.ToString()));

            Console.WriteLine(currentLogMessage);

            LoggingService.LogInADocument(currentLogMessage, base._currentLogFile);
        }

        #endregion

        #endregion
    }
}
