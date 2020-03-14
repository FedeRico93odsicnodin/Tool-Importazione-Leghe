using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio di logging per le configurazioni, questo servizio 
    /// esegue il display di cio che viene letto per le configurazioni 
    /// all'interno dell'interfaccia grafica
    /// </summary>
    public class Logging_UI_Configurations : LoggingBase_Configurations
    {
        #region COSTRUTTORE

        /// <summary>
        /// Inizializzazione del percorso nel quale verrà loggata l'intera procedura
        /// </summary>
        /// <param name="currentLogFile"></param>
        public Logging_UI_Configurations(string currentLogFile)
        {
            base._currentLogFile = currentLogFile;
        }

        public override void HoAppenaInizializzatoTimerSuProcedura()
        {
            throw new NotImplementedException();
        }

        public override void HoAppenaStoppatoTimerSuProcedura()
        {
            throw new NotImplementedException();
        }

        #endregion


        public override void LetturaCorrettaConfigurazione(string currentConfigurazione)
        {
            throw new NotImplementedException();
        }

        public override void LetturaCorrettaDiTutteLeConfigurazioni()
        {
            throw new NotImplementedException();
        }

        public override void LetturaScorrettaConfigurazione(string currentConfigurazione)
        {
            throw new NotImplementedException();
        }

        public override void StoPerVedereSeTutteLeConfigurazioniSonoCorrette()
        {
            throw new NotImplementedException();
        }
    }
}
