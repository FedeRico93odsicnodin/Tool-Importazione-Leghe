using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            base._currentProcedure = currentProcedure;

            Console.WriteLine(currentLogMessage);

            LoggingService.LogInADocument(currentLogMessage, base._currentLogFile);
        }


        /// <summary>
        /// Separatore delle attività correnti
        /// </summary>
        public override void GetSeparatorActivity()
        {
            string currentLogMessage = "\n" + base.separatorActivity + "\n";

            Console.WriteLine(currentLogMessage);

            LoggingService.LogInADocument(currentLogMessage, base._currentLogFile);
        }


        #endregion
    }
}
