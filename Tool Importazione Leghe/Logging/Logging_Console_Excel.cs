using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.ExcelServices;
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
            base._currentLogFile = currentLogPath;
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
            LoggingService.LogInADocument(currentMessage, base._currentLogFile);

        }
        

        public override void ReadHeaders_TrovatoTuttiMarkers(string currentFoglioExcel, int currentCol, int currentRow)
        {
            throw new NotImplementedException();
        }

        public override void SegnalazioneEccezione(string currentException)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Implementazione e visualizzazione in console della messaggistica relativa all'apertura corretta per il file excel corrente 
        /// viene anche passata la modalità di apertura, in modo da riconoscere se il file excel si sta leggendo o scrivendo per il caso
        /// </summary>
        /// <param name="currentFileExcel"></param>
        /// <param name="modalitaCorrente"></param>
        public override void AperturaCorrettaFileExcel(string currentFileExcel, XlsServices.CurrentModalitaExcel modalitaCorrente)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(modalitaCorrente);
            currentMessage += String.Format(base._aperturaFileExcelSuccesso, currentFileExcel);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Implementazione e visualizzazione in console della messaggistica relativa alla lettura di un determinato foglio excel all'interno del documento corrente
        /// viene anche passata la modalita da formattare e il file in apertura corrente
        /// </summary>
        /// <param name="currentFoglioExcelName"></param>
        /// <param name="currentFileExcel"></param>
        /// <param name="modalitaCorrente"></param>
        public override void HoTrovatoIlSeguenteFoglioExcel(string currentFoglioExcelName, string currentFileExcel, XlsServices.CurrentModalitaExcel modalitaCorrente)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(modalitaCorrente);
            currentMessage += String.Format(base._hoTrovatoSeguenteFoglioExcel, currentFoglioExcelName, currentFileExcel);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }

        #endregion


    }
}
