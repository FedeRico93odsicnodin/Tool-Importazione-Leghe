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


        /// <summary>
        /// Implementazione a console della mancanza dei marker per l'individuazione del tipo per il foglio excel correntemente in lettura
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public override void NonHoTrovatoNessunaInformazioneDiMarker(string currentFoglioExcel)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._nonHoTrovatoMarkerPerIlFoglioExcel, currentFoglioExcel);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console di non aver trovato nessuna informazione utile per il riconoscimento di un determinato 
        /// header di colonna 
        /// </summary>
        /// <param name="currentMarker"></param>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public override void NonHoTrovatoInformazionePerIlSeguenteMarker(string currentMarker, int currentCol, int currentRow)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._nonHoTrovatoInformazionePerIlSeguenteMarker, currentMarker, currentCol, currentRow);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console di aver trovato tutti i marker, il foglio excel è stato correttamente identificato 
        /// per la lettura di certe informazioni tra leghe e concentrazioni
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="currentTipologia"></param>
        public override void HoTrovatoTuttiIMarker(string currentFoglioExcel, Constants.TipologiaFoglioExcel currentTipologia)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoTrovatoTuttiMarker, currentFoglioExcel, currentTipologia);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console non aver trovato informazioni utili per il foglio in analisi corrente
        /// </summary>
        /// <param name="currentFoglio"></param>
        /// <param name="currentTipologia"></param>
        public override void SegnalazioneFoglioContenutoNullo(string currentFoglio, Constants.TipologiaFoglioExcel currentTipologia)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._nonHoTrovatoInformazioniUtiliDiLega, currentFoglio, currentTipologia.ToString());

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Display a console dell'informazione utile trovata per il determinato foglio excel corrente
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        /// <param name="currentTipologia"></param>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public override void SegnalazioneTrovatoContenutoUtile(string currentFoglioExcel, Constants.TipologiaFoglioExcel currentTipologia, int currentCol, int currentRow)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoTrovatoContenutoPerIlFoglio, currentCol, currentRow, currentFoglioExcel, currentTipologia.ToString());

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console e nel log che il foglio excel è stato riconosciuto come di una certa tipologia
        /// </summary>
        /// <param name="currentFoglio"></param>
        /// <param name="currentTipologia"></param>
        public override void HoRiconosciutoSeguenteFoglioCome(string currentFoglio, Constants.TipologiaFoglioExcel currentTipologia)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoRiconosciutoFoglioExcelCome, currentFoglio, currentTipologia.ToString());

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console di avere trovato informazione relativa al name per la lega correntemente in analisi nella lettura delle concentrazioni
        /// </summary>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public override void HoTrovatoInformazioniPerTitoloDelMateriale(int currentCol, int currentRow)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoTrovatoInformazionePerIlTitoloMatariale, currentCol, currentRow);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console di non avere trovato informazione relativa al name per la lega correntemente in analisi nella lettura delle concentrazioni
        /// </summary>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public override void NonHoTrovatoInformazioniPerTitoloMateriale(int currentCol, int currentRow)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._nonHoTrovatoInformazionePerIlTitoloMateriale, currentCol, currentRow);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console e nel log di aver trovato le giuste informazioni di header concentrazioni per il quadrante corrente
        /// </summary>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public override void HoTrovatoInformazioniHeaderPerQuadranteCorrente(int currentCol, int currentRow)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoTrovatoHeaderConcentrationsQuadranteCorrente, currentCol, currentRow);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console e nel log di non aver trovato le giuste informazioni di header concentrazioni per il quadrante corrente
        /// </summary>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        public override void NonHoTrovatoInformazioniHeaderPerQuadranteCorrente(int currentCol, int currentRow)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._nonHoTrovatoHeaderConcentrationsQuadranteCorrente, currentCol, currentRow);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console di aver trovato concentrazioni per il quadrante corrente
        /// </summary>
        /// <param name="numElementi"></param>
        public override void HoTrovatoConcentrazioniPerIlQuadranteCorrente(int numElementi)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoTrovatoConcentrazioniPerQuadranteCorrente, numElementi);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console di non aver trovato concentrazioni per il quadrante corrente
        /// </summary>
        public override void NonHoTrovatoConcentrazioniPerIlQuadranteCorrente()
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._nonHoTrovatoConcentrazioniPerQuadranteCorrente);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console di aver trovato un numero di elementi maggiori rispetto a tutti quelli disponibili
        /// </summary>
        public override void HoTrovatoConcentrazioniPerUnNumeroMaggioreDiElementi()
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoTrovatoConcentrazioniPerNumElementiMaggiore);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione in console dell'individuazione di un determinato quadrante di lettura concentrazioni per un materiale 
        /// e per il foglio excel che viene passato in input
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public override void InserimentoQuadranteLetturaConcentrazioniPerFoglio(string currentFoglioExcel)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoAppenaInseritoUnQuadranteDiLettura, currentFoglioExcel);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione che non si è trovato nessun quadrante di lettura per il foglio passato in input che quindi non viene considerato 
        /// come un foglio di concentrazioni materiali
        /// </summary>
        /// <param name="currentFoglioExcel"></param>
        public override void NonHoTrovatoNessunQuadranteConcentrazioniPerFoglio(string currentFoglioExcel)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._nonHoTrovatoNessunQuadranteDiLettura, currentFoglioExcel);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console di aver gia trovato una informazione a carattere generale per la lettura degli headers per il foglio excel 
        /// correntemente in analisi
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        public override void HoGiaTrovatoInformazioneACarattereGenerale(string currentProprietaLettura)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._hoGiaTrovatoLaProprietaHeaderInfoCorrente, currentProprietaLettura);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console che l'informazione letta per una determinata proprieta non corrisponde a quelle di carattere generale per la lettura delle proprieta 
        /// obbligatorie per una determinata lega 
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        public override void InformazioneGeneraleNonContenutaNelleDefinizioniObbligatorie(string currentProprietaLettura)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._informazioneNonContenutaTraLeDefinizioniInformazioniGenerali, currentProprietaLettura);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }

        #endregion


    }
}
