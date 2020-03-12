using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

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


        #region MESSAGES

        /// <summary>
        /// Scrittura del messaggio di ritrovamento delle informazioni per il primo header letto per il documento corrente
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="primoMarker"></param>
        /// <param name="currentTipologiaFoglioExcel"></param>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public override void ReadHeaders_HoTrovatoInformazionePerIlPrimoMarker(string currentFoglioExcel, string primoMarker, Constants.TipologiaFoglioExcel currentTipologiaFoglioExcel, int currentCol, int currentRow)
        {
            string currentMessage = String.Format(base.hoTrovatoInformazioniPerIlPrimoMarker, currentFoglioExcel, primoMarker, currentTipologiaFoglioExcel.ToString(), currentCol, currentRow);
            Console.WriteLine(currentMessage);

            // log del messaggio iniziale all'interno del log excel
            LoggingService.LogInADocument(currentMessage, base._currentLogExcel);

        }

        public override void ReadHeaders_TrovatoTuttiMarkers(string currentFoglioExcel, int currentCol, int currentRow)
        {
            throw new NotImplementedException();
        }

        public override void SegnalazioneEccezione(string currentException)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
