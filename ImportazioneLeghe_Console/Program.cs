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
            // inizializzazione del timer
            ServiceLocator.GetConfigurations.StartTimerOnProcedure();

            // lettura delle configurazioni correnti
            ServiceLocator.GetConfigurations.ReadConfigFile();

            if(Constants.HoLettoTutteLeConfigurazioni)
            {
                ServiceLocator.GetLoggingService.HoLettoConfigurazioniPremereUnTastoPerContinuare();
                Console.ReadKey();

                // avviamento import corrente
                ImportActivity currentActivity = new ImportActivity(Constants.CurrentTipologiaImport);
                currentActivity.Do_Import();
            }


        }
    }
}
