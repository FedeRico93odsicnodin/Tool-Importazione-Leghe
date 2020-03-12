using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Qui dentro saranno contenuti tutti i messaggi e le operazioni di log rispetto alla console
    /// e alle operazioni che vengono effettuate all'interno del database configurato
    /// La ridefinizione die metodi di partenza sarà funzione della modalità in cui si avvia il tool 
    /// in questo caso WPF application
    /// </summary>
    public class Logging_UI_Database : LoggingBase_Database
    {
        #region COSTRUTTORE 

        /// <summary>
        /// Attribuzione del path nel quale verranno salvate tutte le operazioni 
        /// che sono compiute sul database
        /// </summary>
        /// <param name="currentLogPath"></param>
        public Logging_UI_Database(string currentLogPath)
        {
            base._currentLogDatabase = currentLogPath;
        }

        public override void SegnalazioneEccezione(string currentException)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
