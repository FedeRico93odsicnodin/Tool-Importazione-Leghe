using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio di log per le configurazioni, questo servizio di log 
    /// ha validità per l'applicazione lanciata attraverso console
    /// </summary>
    public class Logging_Console_Configurations : LoggingBase_Configurations
    {

        #region COSTRUTTORE 

        /// <summary>
        /// Attribuzione del nome per il log corrente
        /// </summary>
        /// <param name="currentLogFile"></param>
        public Logging_Console_Configurations(string currentLogFile)
        {
            base._currentLogFile = currentLogFile;
        }
        
        #endregion



        #region IMPLEMENTAZIONE MESSAGGI PER CONSOLE

        /// <summary>
        /// Messaggio di lettura corretta di una certa configurazione in console
        /// </summary>
        /// <param name="currentConfigurazione"></param>
        public override void LetturaCorrettaConfigurazione(string currentConfigurazione)
        {
            string currentMessage = String.Format(base._messaggioLetturaCorrettaConfigurazione, currentConfigurazione);

            currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString() + base._loggerConfigurationsIdentifier + currentMessage;

            // TODO: aggiunta del time preso dalle configurazioni

            Console.WriteLine(currentMessage);

            // log del messaggio iniziale all'interno del log excel
            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Messaggio di lettura scorretta di una certa configurazione in console
        /// </summary>
        /// <param name="currentConfigurazione"></param>
        public override void LetturaScorrettaConfigurazione(string currentConfigurazione)
        {
            string currentMessage = String.Format(base._messaggioErroreLetturaConfigurazione, currentConfigurazione);

            currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString() + base._loggerConfigurationsIdentifier + currentMessage;

            // TODO: aggiunta del time preso dalle configurazioni

            Console.WriteLine(currentMessage);

            // log del messaggio iniziale all'interno del log excel
            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione relativa al fatto che si sta per verificare che tutte le informazioni 
        /// siano valide all'interno del file di configurazione corrente
        /// </summary>
        public override void StoPerVedereSeTutteLeConfigurazioniSonoCorrette()
        {
            string currentMessage = base._messaggioLetturaCorrettaConfigurazione;

            currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString() + base._loggerConfigurationsIdentifier + currentMessage;

            Console.WriteLine(currentMessage);

            // log del messaggio iniziale all'interno del log excel
            LoggingService.LogInADocument(currentMessage, base._currentLogFile);

            LoggingService.GetSomeTimeOnConsole();
        }


        /// <summary>
        /// Segnalazione a console e nel log di avere appena fatto partire il tempo per 
        /// l'import corrente 
        /// </summary>
        public override void HoAppenaInizializzatoTimerSuProcedura()
        {
            string currentMessage = base._messaggioInizioTimer;

            currentMessage = base._loggerConfigurationsIdentifier + currentMessage;

            Console.WriteLine(currentMessage);

            // log del messaggio iniziale all'interno del log excel
            LoggingService.LogInADocument(currentMessage, base._currentLogFile);

            LoggingService.GetSomeTimeOnConsole();
        }


        /// <summary>
        /// Segnalazione a console e nel log di avere appena fatto partire il tempo per 
        /// l'import corrente 
        /// </summary>
        public override void HoAppenaStoppatoTimerSuProcedura()
        {
            string currentMessage = base._messagioStopTimer;

            currentMessage = base._loggerConfigurationsIdentifier + currentMessage;

            Console.WriteLine(currentMessage);

            // log del messaggio iniziale all'interno del log excel
            LoggingService.LogInADocument(currentMessage, base._currentLogFile);

            LoggingService.GetSomeTimeOnConsole();
        }

        #endregion
    }
}
