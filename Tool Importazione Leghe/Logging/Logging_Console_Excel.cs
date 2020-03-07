using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio di logging per il foglio database e rispetto alla console application di supporto
    /// </summary>
    class Logging_Console_Excel : LoggingBase_Excel
    {

        #region COSTRUTTORE 

        /// <summary>
        /// Inizializzazione della stringa indicante la collocazione del log
        /// relativo alle operazioni excel
        /// </summary>
        /// <param name="currentLogPath"></param>
        public Logging_Console_Excel(string currentLogPath)
        {
            base._currentLogExcel = currentLogPath;
        }

        #endregion


    }
}
