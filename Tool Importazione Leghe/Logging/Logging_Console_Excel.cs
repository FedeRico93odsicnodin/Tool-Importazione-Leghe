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


        /// <summary>
        /// Implementazione a console della messaggistica relativa alla segnalazione che l'informazione addizionale non si trova all'interno delle definizioni date per gli headers 
        /// che è possible avere in lettura corrente per la lega 
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        public override void InformazioneGeneraleNonContenutaNelleDefinizioniAddizionali(string currentProprietaLettura)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._informazioneNoNContenutaTraLeDefinizioniAddizionaliGenerali, currentProprietaLettura);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Implementazione a console della messaggistica relativa alla lettura di una certa proprieta obbligatoria per le informazioni di carattere generale contenute nel foglio 
        /// excel per una certa lega in lettura 
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentCol"></param>
        public override void TrovataInformazioneObbligatoriaLetturaInformazioniGenerali(string currentProprietaLettura, int currentRow, int currentCol)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._segnalazioneLetturaProprietaObbligatoriaLega, currentRow, currentCol, currentProprietaLettura);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Implementazione a console del messaggio di trovata proprieta addizionale per le informazioni generali in lettura sul foglio e per la lega corrente
        /// </summary>
        /// <param name="currentProprietaLettura"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentCol"></param>
        public override void TrovataInformazioneAddizionaleLetturaInformazioniGenerali(string currentProprietaLettura, int currentRow, int currentCol)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._segnalazioneLetturaProprietaAddizionaleLega, currentRow, currentCol, currentProprietaLettura);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Implementazione a console del messaggio di fine processamento per le informazioni generali del foglio excel che viene passato in input
        /// </summary>
        /// <param name="excelSheetName"></param>
        public override void FineProcessamentoGeneralInfoPerFoglioExcel(string excelSheetName)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._fineProcessamentoGeneralInfoFoglioExcel, excelSheetName);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console riconoscimento del foglio excel come contenitore di informazioni a carattere generale per la determinata lega
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        public override void HoRiconosciutoIlFoglioComeContenenteInformazioniGeneraliLega(string currentExcelSheet)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._readExcel_foglioRiconosciutoComeDiInfoBase, currentExcelSheet);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }


        /// <summary>
        /// Segnalazione a console riconoscimento del foglio excel come contenitore di informazioni per le concentrazioni dei materiali di una determinata lega 
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        public override void HoRiconosciutoIlFoglioComeContenenteConcentrazioniMateriali(string currentExcelSheet)
        {
            string currentMessage = ServiceLocator.GetConfigurations.GetCurrentProcedureTime().ToString();
            currentMessage += FormatModalitaCorrente(XlsServices.CurrentModalitaExcel.EXCELREADER);
            currentMessage += String.Format(base._readExcel_foglioRiconosciutoComeInfoConcentrazioni, currentExcelSheet);

            Console.WriteLine(currentMessage);

            LoggingService.LogInADocument(currentMessage, base._currentLogFile);
        }

        #endregion


    }
}
