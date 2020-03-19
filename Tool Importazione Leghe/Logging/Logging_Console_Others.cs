using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Implementazione a console del servizio di log creato per gli altri servizi diversi dall'import corrente
    /// </summary>
    public class Logging_Console_Others : LoggingBase_Others
    {

        #region COSTRUTTORE

        /// <summary>
        /// Inizializzazione del file di log dove loggare per questo servizio
        /// </summary>
        /// <param name="currentLogFile"></param>
        public Logging_Console_Others(string currentLogFile)
        {
            base.LogFile = currentLogFile;
        }

        #endregion



        /// <summary>
        /// Segnalazione a console della lettura di tutti gli elementi dal database di origine
        /// </summary>
        public override void StartingServicesLOG_HoAppenaLettoTuttiGliElementiFromDB()
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += String.Format(base._segnalazioneLetturaElementiFromDB);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base.LogFile);
        }


    }
}
