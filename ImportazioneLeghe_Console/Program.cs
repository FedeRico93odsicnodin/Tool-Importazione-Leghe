using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe;
using Tool_Importazione_Leghe.Utils;

namespace ImportazioneLeghe_Console
{
    /// <summary>
    /// Tutte le funzionalità del tool ma senza l'interfaccia grafica 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 1- inizializzazione del timer
            ServiceLocator.GetConfigurations.StartTimerOnProcedure();

            // 2- lettura delle configurazioni correnti
            ServiceLocator.GetConfigurations.ReadConfigFile();

            if(Constants.HoLettoTutteLeConfigurazioni)
            {
                ServiceLocator.GetLoggingService.HoLettoConfigurazioniPremereUnTastoPerContinuare();
                Console.ReadKey();

                // 3- load liste iniziali
                CaricaConfigurazioniIniziali();

                // 4- avviamento import corrente
                ImportActivity currentActivity = new ImportActivity(Constants.CurrentTipologiaImport);
                currentActivity.Do_Import();
            }


        }


        /// <summary>
        /// Permette di leggere tutte le configurazioni di lista iniziale contenute nel database corrente 
        /// e che consentono la validazione - inserimento corretto di alcuni elementi
        /// </summary>
        private static void CaricaConfigurazioniIniziali()
        {
            // caricamento della lista di tutti gli elementi presenti
            ServiceLocator.GetStartingLoad_Activity.LoadElements();
        }
    }
}
