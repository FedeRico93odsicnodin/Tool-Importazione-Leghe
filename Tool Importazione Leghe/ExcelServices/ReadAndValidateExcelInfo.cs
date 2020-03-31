using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Model;

namespace Tool_Importazione_Leghe.ExcelServices
{
    /// <summary>
    /// STEP 2 per la procedura di lettura del contenuto del foglio excel
    /// In questa classe sono contenute tutte le informazioni per l'analisi della sintassi e delle informazioni che eventualmente verranno persistite 
    /// all'interno del database di destinazione.
    /// </summary>
    public class ReadAndValidateExcelInfo
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Traccia relativa alla riga correntemente in lettura per il foglio excel corrente 
        /// </summary>
        private int _tracciaCurrentRow = 1;


        /// <summary>
        /// Traccia relativa alla colonna correntemente in lettura per il foglio excel corrente 
        /// </summary>
        private int _tracciaCurrentCol = 1;

        #region MARKER CHE POTREBBERO ESSERE INCONTRATI


        #endregion

        #endregion


        #region METODI PER IL RECUPERO DELLE INFORMAZIONI UTILI PER I DIVERSI FOGLI EXCEL (CONCENTRAZIONI E INFORMAZIONI GENERALI DI LEGA)

        /// <summary>
        /// Permette di ottenere la lista con tutte le informazioni per ciascuna delle righe utili per le informazioni generali di lega 
        /// correntemente in lettura dal foglio excel passato in input
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentInfoHeaders"></param>
        /// <param name="currentListRowsInfoLega"></param>
        /// <returns></returns>
        public bool GetAllGeneralInfoFromExcel(ref ExcelWorksheet currentExcelSheet, List<HeadersInfoLega_Excel> currentInfoHeaders, out List<RowFoglioExcel> currentListRowsInfoLega)
        {
            currentListRowsInfoLega = new List<RowFoglioExcel>();

            // primo check: se la lista è = 0 allora non posso continuare lettura 
            if (currentInfoHeaders == null)
                throw new Exception(String.Format(ExceptionMessages.LISTAHEADERNULLAOVUOTA, currentExcelSheet.Name));

            if (currentInfoHeaders.Count == 0)
                throw new Exception(String.Format(ExceptionMessages.LISTAHEADERNULLAOVUOTA, currentExcelSheet.Name));

            // secondo check: se c'è un disallineamento di colonna per le diverse proprieta non posso continuare la lettura 
            if (!ExcelSheetValidators.CheckAllineamentoHeadersForGeneralInfo(currentInfoHeaders))
                throw new Exception(String.Format(ExceptionMessages.DISALLINEAMENTOHEADERSNELFOGLIO, currentExcelSheet.Name));

            _tracciaCurrentRow = currentInfoHeaders.FirstOrDefault().Starting_Row + 1;

            do
            {
                // separazione delle righe in lettura 
                ServiceLocator.GetLoggingService.GetLoggerImportActivity.GetSeparatorActivity();

                if (CheckNullRow(ref currentExcelSheet, currentInfoHeaders, _tracciaCurrentRow))
                {
                    // segnalazione di non aver trovato nessuna informazione per la lettura della riga corrente 
                    ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoInformazioniGeneraliLegaPerRiga(currentExcelSheet.Name, _tracciaCurrentRow);
                    
                    continue;
                }


                // creo il nuovo oggetto di riga per le informazioni generali
                RowFoglioExcel currentRowInfo = new RowFoglioExcel();

                // inserisco la riga corrente come identificatrice dell'oggetto di valori
                currentRowInfo.Excel_CurrentRow = _tracciaCurrentRow;

                // iterazione su indice di colonna 
                foreach (HeadersInfoLega_Excel currentHeader in currentInfoHeaders)
                {
                    // colonna e proprieta in lettura 
                    int currentColHeader = currentHeader.Starting_Col;
                    string currentPropertyValue = String.Empty;

                    // se la proprieta è diversa da null la converto in una stringa
                    if (currentExcelSheet.Cells[_tracciaCurrentRow, currentColHeader].Value != null)
                        currentPropertyValue = currentExcelSheet.Cells[_tracciaCurrentRow, currentColHeader].Value.ToString();

                    // aggiungo il valore all'interno dell'oggetto di valori
                    currentRowInfo.SetValue(currentHeader.NomeProprietà, currentPropertyValue);
                }

                currentListRowsInfoLega.Add(currentRowInfo);

                // segnalazione di inserimento di una riga di informazioni generali per il foglio excel correntemente in lettura 
                ServiceLocator.GetLoggingService.GetLoggerExcel.HoLettoUnaRigaDiValoriGeneralPerFoglioExcelInRiga(_tracciaCurrentRow, currentExcelSheet.Name);
            }
            while (_tracciaCurrentRow <= currentExcelSheet.Dimension.End.Row); 
            
            
            return false;
        }


        /// <summary>
        /// Permette di recuperare il concenuto di un quadrante excel partendo dalle informazioni di perimetro del quadrante. Queste informazioni vengono restituite sottoforma di 
        /// oggetto di transizione sul quale verrà eseguita una analisi di validita delle informazioni recuperate sia dal punto di vista sintattico (quindi proprio rispetto al foglio excel)
        /// sia rispetto alla persistenza a database
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentContrationsQuadrants"></param>
        /// <param name="concentrationsObjects"></param>
        /// <returns></returns>
        public bool GetAllConcentrationsFromExcel(ref ExcelWorksheet currentExcelSheet, List<ExcelConcQuadrant> currentContrationsQuadrants, out List<MaterialConcentrationsObject> concentrationsObjects)
        {
            concentrationsObjects = new List<MaterialConcentrationsObject>();

            // primo check: se la lista è = 0 allora non posso continuare lettura 
            if (currentContrationsQuadrants == null)
                throw new Exception(String.Format(ExceptionMessages.LISTAHEADERNULLAOVUOTA, currentExcelSheet.Name));

            if (currentContrationsQuadrants.Count == 0)
                throw new Exception(String.Format(ExceptionMessages.LISTAHEADERNULLAOVUOTA, currentExcelSheet.Name));


            foreach(ExcelConcQuadrant currentConcQuadrant in currentContrationsQuadrants)
            {

                // mi dice se il quadrante corrente ha passato tutte le validazioni o meno
                bool letturaCorretta = true;


                try
                {
                    // prima validazione: relativa alla formattazione del quadrante - tutte le validazioni sul contenuto sono riportate allo step successivo
                    if(ExcelSheetValidators.CheckAllineamentoHeadersForCurrentConcQuadrant(currentConcQuadrant))
                    {
                        string materialIdentificationTitle = String.Empty;

                        if (currentExcelSheet.Cells[currentConcQuadrant.Title_Row, currentConcQuadrant.Title_Col].Value != null)
                            materialIdentificationTitle = currentExcelSheet.Cells[currentConcQuadrant.Title_Row, currentConcQuadrant.Title_Col].Value.ToString();
                        else
                            throw new Exception(ExceptionMessages.CONCENTRATIONSQUADRANT_TITLEMATERIALNULL);

                        // inizio con riempire le righe per le concentrazioni in lettura corrente 
                        List<RowFoglio2Excel> currentLetturaConcentrazioni = FillConcentrationsRow(ref currentExcelSheet, currentConcQuadrant);

                        // se non trovo alcuna informazione di lista segnalo l'eccezione (almento una informazione dovrà essere contenuta per il quadrante
                        if (currentLetturaConcentrazioni.Count == 0)
                            throw new Exception(ExceptionMessages.CONCENTRATIONSQUADRANT_NESSUNACONCENTRAZIONETROVATA);

                    }
                }
                // cattura dell'eccezione eventualmente generata dalla validazione su quadrante corrente 
                catch(Exception e)
                {
                    ServiceLocator.GetLoggingService.GetLoggerExcel.NonPossoContinuareLetturaQuadranteConcentrazioni(currentConcQuadrant.EnumerationQuadrant, currentExcelSheet.Name);
                    ServiceLocator.GetLoggingService.GetLoggerExcel.SegnalazioneEccezione(e.Message);

                    letturaCorretta = false;
                }
            }


            return false;
        }


        /// <summary>
        /// Permette di verificare se l'informazione di riga corrente contiene tutti i valori nulli.
        /// Se cosi fosse non è necessaria l'iterazione per il recupero delle informazioni generali di lega
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentInfoHeaders"></param>
        /// <param name="currentRowIndex"></param>
        /// <returns></returns>
        private bool CheckNullRow(ref ExcelWorksheet currentExcelSheet, List<HeadersInfoLega_Excel> currentInfoHeaders, int currentRowIndex)
        {
            bool nonHoTrovatoInfo = true;

            foreach(HeadersInfoLega_Excel currentHeaderInfo in currentInfoHeaders)
            {
                if (currentExcelSheet.Cells[currentRowIndex, currentHeaderInfo.Starting_Col].Value != null)
                    nonHoTrovatoInfo = false;
            }

            return nonHoTrovatoInfo;
        }


        /// <summary>
        /// Permette la lettura per le concentrazioni e il quadrante corrente 
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentConcQuadrant"></param>
        /// <returns></returns>
        private List<RowFoglio2Excel> FillConcentrationsRow(ref ExcelWorksheet currentExcelSheet, ExcelConcQuadrant currentConcQuadrant)
        {
            List<RowFoglio2Excel> currentReadConcentrations = new List<RowFoglio2Excel>();

            do
            {
                // inizializzazione dell'indice di colonna corrente 
                _tracciaCurrentCol = currentConcQuadrant.Head_Col;

                do
                {
                    // inizializzazioen dell'indice di riga corrente 
                    _tracciaCurrentRow = currentConcQuadrant.Conc_Row_Start;


                }
                while (_tracciaCurrentRow <= currentConcQuadrant.Conc_Row_End);

            }
            while (_tracciaCurrentCol <= currentConcQuadrant.Get_Max_Col_Quadrante);

            


            return currentReadConcentrations;
        }
        
        #endregion
    }
}
