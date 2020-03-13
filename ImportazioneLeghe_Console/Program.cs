using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe;

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


        }
    }
}
