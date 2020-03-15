using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.ExcelServices
{
    /// <summary>
    /// Questa classe mi permette di leggere gli headers relativi ai diversi fogli excel 
    /// grazie a questo metodo riesco a distinguere la presenza di un determinato header per un certo foglio 
    /// e quindi capire come procedere nella lettura e per quale caso siamo (se il tipo di sheet 1 o il tipo di sheet 2)
    /// </summary>
    public class ReadHeaders
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Tiene traccia dell'indice di colonna corrente per l'eventuale gestione dell'eccezione
        /// </summary>
        private int _tracciaCurrentCol = 1;


        /// <summary>
        /// Tiene traccia dell'indice di riga corrente per l'eventuale gestione dell'eccezione
        /// </summary>
        private int _tracciaCurrentRow = 1;


        /// <summary>
        /// Tiene traccia del marker corrente per l'eventuale gestione dell'eccezione
        /// </summary>
        private string _currentMarker = String.Empty;


        /// <summary>
        /// Indica il limite sul numero di righe che posso leggere per trovare l'informazione relativa all'header
        /// </summary>
        private const int LIMITROW = 5;


        /// <summary>
        /// indica il limite sul numero di colonne che posso leggere per trovare l'informazione relativa all'header
        /// </summary>
        private const int LIMITCOL = 5;


        /// <summary>
        /// Indica il limite sul numero di righe che posso leggere a partire dall'individuazione dell'header per trovare il primo valore 
        /// utile per l'informazione sul foglio excel corrente
        /// </summary>
        private const int LIMITINFOROW = 10;


        /// <summary>
        /// Indice che viene settato con la posizione di colonna per il primo marker per la restituzione corretta delle prime informazioni 
        /// utili relative al foglio corrente e da cui partire con la lettura effettiva dei dati
        /// </summary>
        private int _posizioneColonnaPrimoMarker = 0;


        /// <summary>
        /// Permette di mappare tutte le informazioni relative alle posizione dalle quali iniziare a leggere i diversi oggetti 
        /// per le concentrazioni
        /// </summary>
        private List<ExcelConcQuadrant> _currentPositionsConcentrations;


        /// <summary>
        /// Indicazione del massimo indice per la colonna sul foglio excel corrente
        /// </summary>
        private int MaxExcelSheetPos_col = 0;


        /// <summary>
        /// Indicazione del massimo indice di riga sul foglio excel corrente
        /// </summary>
        private int MaxExcelSheetPos_row = 0;


        /// <summary>
        /// Indice che mi dice quale sarà il limite nella lettura delle concentrazioni rispetto alla colonna 
        /// se spostandomi in orizzontale sulle colonne non trovo piu quadranti utili dopo questo limite allora fermo l'iterazione
        /// </summary>
        private const int LIMITCOL_LETTURACONCENTRAZIONI = 15;


        /// <summary>
        /// Indice che mi dice quale sarà il limite nella lettura delle concentraizoni rispetto alla riga
        /// se spostandomi in verticale sulle righe non trovo piu quadranti utili dopo questo limite allora fermo l'iterazione
        /// </summary>
        private const int LIMITROW_LETTURACONCENTRAZIONI = 15;

        #endregion 



        #region METODI PUBBLICI - MI DICONO QUALE SIA IL FOGLIO EXCEL CORRNTE

        /// <summary>
        /// Trova l'eventuale header per la prima tipologia di foglio excel riguardante tutte le informazioni di base per poter
        /// individuare la concentrazione corrente
        /// Una volta trovato l'header corrispondente qualora ci fosse, vengono anche restituiti gli indici di colonna e di riga per 
        /// la prima posizione di riga dalla quale andare a ricavare le informazioni da inserire per le diverse tabelle coinvolte 
        /// da questo foglio
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="firstUtilCol"></param>
        /// <param name="firstUtilRow"></param>
        /// <returns></returns>
        internal bool ReadFirstInformation_DatiPrimari(ref ExcelWorksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaFoglio, out int firstUtilCol, out int firstUtilRow)
        {
            // reset attributi di lettura corrente
            _tracciaCurrentCol = 1;
            _tracciaCurrentRow = 1;
            _currentMarker = String.Empty;
            
            try
            {
                // vado a leggere l'header per il foglio excel corrente
                bool hoLettoHeader = ReadHeader_DatiLega(ref currentExcelSheet, currentTipologiaFoglio);

                // non ho letto le informazioni utili di header
                if (!hoLettoHeader)
                {

                    // attribuzione del valore di default per la riga e la colonna in uscita
                    firstUtilCol = _tracciaCurrentCol;
                    firstUtilRow = _tracciaCurrentRow;
                    return false;
                }
                
                // calcolo la prima informazione utile per il foglio excel corrente
                bool esisteContenuto = CalculateFirstInformation(ref currentExcelSheet, currentTipologiaFoglio, out _tracciaCurrentCol, out _tracciaCurrentRow);

                if(!esisteContenuto)
                {
                    // attribuzione del valore di default per la riga e la colonna in uscita
                    firstUtilCol = _tracciaCurrentCol;
                    firstUtilRow = _tracciaCurrentRow;
                    return false;
                }

                // l'unico modo per riconoscere il foglio nella modalita corrente è avere headers e informazioni
                if(hoLettoHeader && esisteContenuto)
                {
                    // attribuzione del valore di default per la riga e la colonna in uscita
                    firstUtilCol = _tracciaCurrentCol;
                    firstUtilRow = _tracciaCurrentRow;
                    return true;
                }
            }
            catch (Exception e)
            {
                string currentExceptionMessage = String.Format(ExceptionMessages.HOTROVATOECCEZIONELETTURAHEADER, currentExcelSheet.Name, _currentMarker, _tracciaCurrentCol, _tracciaCurrentRow);
                currentExceptionMessage += "\n" + e.Message;
                throw new Exception(currentExceptionMessage);
            }

            // attribuzione del valore di default per la riga e la colonna in uscita
            firstUtilCol = _tracciaCurrentCol;
            firstUtilRow = _tracciaCurrentRow;


            return false;

            
        }


        /// <summary>
        /// Lettura di un certo foglio excel sul quale si riconoscono le posizioni utili per la lettura delle concentrazioni associate
        /// ai diversi materiali, il risultato è avere la validità del foglio come di un foglio relativo alle concentrazioni e avere 
        /// un set di posizioni dalle quali iniziare a leggerle
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentTipologiaFoglio"></param>
        /// <param name="detectedMaterials"></param>
        /// <returns></returns>
        internal bool ReadHeaders_Concentrazioni(ref ExcelWorksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaFoglio, out List<ExcelConcQuadrant> detectedMaterials)
        {
            // reset attributi di lettura corrente
            _tracciaCurrentCol = 1;
            _tracciaCurrentRow = 1;
            _currentMarker = String.Empty;

            // reset della lista sulla quale si andranno a inserire le eventuali posizioni utili trovate per la lettura delle concentrazioni
            detectedMaterials = new List<ExcelConcQuadrant>();

            // recupero dei limiti di riga-colonna per il foglio excel corrente
            MaxExcelSheetPos_col = currentExcelSheet.Dimension.End.Column;
            MaxExcelSheetPos_row = currentExcelSheet.Dimension.End.Row;



            return false;
        }
        
        #endregion


        #region METODI PUBBLICI DI RICONOSCIMENTO HEADER E LETTURA PRIMA INFORMAZIONE
        
        /// <summary>
        /// Permette di leggere un determinato header all'interno del foglio excel
        /// con una certa convenzione adottata sul limite di riga e di colonna per l'esecuzione della lettura
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentTipologiaFoglio"></param>
        /// <returns></returns>
        private bool ReadHeader_DatiLega(ref ExcelWorksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaFoglio)
        {
            // recupero degli header per la certa tipologia di foglio excel
            List<string> currentHeaderFoglio = new List<string>();


            if (currentTipologiaFoglio == Utils.Constants.TipologiaFoglioExcel.Informazioni_Lega)
                currentHeaderFoglio = ExcelMarkers.GetAllColumnHeadersForGeneralInfoSheet();

            // non ho trovato nessuna informazione utile di header per il foglio corrente
            if (currentHeaderFoglio.Count() == 0)
            {
                ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoNessunaInformazioneDiMarker(currentExcelSheet.Name);
                return false;
            }

            // indicazione sul fatto che trovo la prima informazione di colonna 
            bool trovato = false;
            

            foreach(string currentMarkerHeader in currentHeaderFoglio)
            {
                // attribuisco il marker per l'analisi corrente
                _currentMarker = currentMarkerHeader;

                // individuazione di riga per il riconoscimento dell'header corrente 
                if(_currentMarker == currentHeaderFoglio[0])
                {
                    // iterazione per la riga corrente 
                    while(_tracciaCurrentRow <= LIMITROW)
                    {

                        // iterazione per la colonna corrente
                        while(_tracciaCurrentCol <= LIMITCOL)
                        {
                            // cella nulla, continuo nella ricerca
                            if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value == null)
                            {
                                _tracciaCurrentCol++;
                                continue;
                            }
                                

                            if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value.ToString() == _currentMarker)
                            {
                                _posizioneColonnaPrimoMarker = _tracciaCurrentCol;
                                trovato = true;
                                break;
                            }
                            else
                                _tracciaCurrentCol++;
                                
                        }

                        if (trovato)
                            break;
                        else
                        {
                            _tracciaCurrentCol = 1;
                            _tracciaCurrentRow++;
                            continue;
                        }
                            
                    }
                }
                else
                {
                    // se non trovo nessuna informazione per il primo header allora ritorno false
                    if(!trovato)
                    {
                        ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoInformazionePerIlSeguenteMarker(_currentMarker, _tracciaCurrentCol, _tracciaCurrentRow);
                        return false;
                    }
                    else
                    {
                        // incrementazione del marker successivo su colonna 
                        _tracciaCurrentCol++;
                        _currentMarker = currentMarkerHeader;

                        // cella nulla
                        if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value == null)
                        {
                            ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoInformazionePerIlSeguenteMarker(_currentMarker, _tracciaCurrentCol, _tracciaCurrentRow);
                            return false;
                        }


                        // riconoscimento di tutte le altre colonne successive alla prima - se non ritrovo lo stesso marker ritorno false
                        if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value.ToString() != _currentMarker)
                        {
                            ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoInformazionePerIlSeguenteMarker(_currentMarker, _tracciaCurrentCol, _tracciaCurrentRow);
                            return false;
                        }

                    }
                }
            }

            ServiceLocator.GetLoggingService.GetLoggerExcel.HoTrovatoTuttiIMarker(currentExcelSheet.Name, currentTipologiaFoglio);

            return true;
        }


        /// <summary>
        /// Una volta individuata la tipologia per il foglio excel corrente individuo la prima informazione utile per poter 
        /// poi leggere i dati contenuti nel foglio
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name=""></param>
        /// <param name="currentInfoCol"></param>
        /// <param name="currentInfoRow"></param>
        private bool CalculateFirstInformation(ref ExcelWorksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaExcel, out int currentInfoCol, out int currentInfoRow)
        {

            // impostazione della colonna sulla quale inizio a leggere le prime informazioni utili per il foglio corrente
            _tracciaCurrentCol = _posizioneColonnaPrimoMarker;

            // itero sull'indice di riga finche non trovo un valore utile da cui iniziare la lettura delle informazioni per gli steps successivi
            do
            {
                _tracciaCurrentRow++;

                if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value == null)
                    continue;

                currentInfoCol = _tracciaCurrentCol;
                currentInfoRow = _tracciaCurrentRow;

                ServiceLocator.GetLoggingService.GetLoggerExcel.SegnalazioneTrovatoContenutoUtile(currentExcelSheet.Name, currentTipologiaExcel, _tracciaCurrentCol, _tracciaCurrentRow);

                return true;
            }
            while (_tracciaCurrentRow <= LIMITINFOROW);


            ServiceLocator.GetLoggingService.GetLoggerExcel.SegnalazioneFoglioContenutoNullo(currentExcelSheet.Name, currentTipologiaExcel);

            currentInfoCol = 0;
            currentInfoRow = 0;

            return false;
        }


        /// <summary>
        /// Ritorna le coordinate per una determinata posizione di lettura delle diverse concentrazioni per una determinata lega
        /// all'interno del foglio corrente
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentTipologiaFoglio"></param>
        /// <returns></returns>
        private bool RecognizeConcentrationsPosition(ref ExcelWorksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaFoglio)
        {
            // recupero degli header per la certa tipologia di foglio excel
            List<string> currentHeaderFoglio = new List<string>();


            if (currentTipologiaFoglio == Utils.Constants.TipologiaFoglioExcel.Informazioni_Concentrazione)
                currentHeaderFoglio = ExcelMarkers.GetAllColumnHeadersForConcentrationsInfoSheet();

            // non ho trovato nessuna informazione utile di header per il foglio corrente
            if (currentHeaderFoglio.Count() == 0)
            {
                ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoNessunaInformazioneDiMarker(currentExcelSheet.Name);
                return false;
            }


            // iterazione a partire dalla prima riga 
            do
            {

                do
                {
                    // passo alla riga successiva se non ho ancora incontrato informazioni utili
                    if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value == null)
                    {
                        _tracciaCurrentRow++;
                        continue;
                    }

                    // se non mi trovo piu nei limit allora rompo il ciclo
                    if (!CheckLimitRowCurrentIteration(_tracciaCurrentCol, _tracciaCurrentRow))
                        break;


                    // ho trovato una informazione
                }
                while (_tracciaCurrentRow <= MaxExcelSheetPos_row);


                // ricalcolo posizione index per iterazione su colonne successive
                _tracciaCurrentCol = RicalcolaPosizioneColonna(ref currentExcelSheet);

            }
            while (_tracciaCurrentCol <= MaxExcelSheetPos_col);


            if (_currentPositionsConcentrations.Count == 0)
                return false;

            return true;
        }


        /// <summary>
        /// Mi permette di ricalcolare la posizione della nuova colonna quando si completa il riconoscimento dei quadranti 
        /// "in verticale"
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <returns></returns>
        private int RicalcolaPosizioneColonna(ref ExcelWorksheet currentExcelSheet)
        {
            // se non è presente nessun elemento nella lista dei possibili elementi incontrati allora sposto l'indice di colonna di una sola posizione
            if (_currentPositionsConcentrations.Count == 0)
                return _tracciaCurrentCol++;

            // calcolo del massimo indice di colonna 
            int newColIndex = _currentPositionsConcentrations.Select(x => x.EndConc_X).Max();

            return newColIndex++;
        }


        /// <summary>
        /// Mi permette di capire se sono passati i limiti rispetto alla lettura sulla colonna corrente 
        /// delle diverse concentrazioni che dovrei riscontrare alle righe successive
        /// </summary>
        /// <param name="currentColIndex"></param>
        /// <param name="currentRowIndex"></param>
        /// <returns></returns>
        private bool CheckLimitRowCurrentIteration(int currentColIndex, int currentRowIndex)
        {
            // non ho ancora inserito nessun elemento nella lista e ho superato i limiti di riga
            if (_currentPositionsConcentrations.Count == 0 && _tracciaCurrentRow == LIMITROW_LETTURACONCENTRAZIONI)
                return false;

            // trovo l'indice di riga massimo letto per l'ultimo elemento su questa colonna 
            int currentMaxRow = _currentPositionsConcentrations.Where(x => x.StartConc_X == currentColIndex).Select(x => x.EndConc_Y).Max();

            if (currentRowIndex > currentMaxRow + LIMITROW_LETTURACONCENTRAZIONI)
                return false;

            return true;
        }


        /// <summary>
        /// Permette di fare un fill di tutte le informazioni relative al materiale corrente a partire dall'indice di 
        /// riga e colonna riscontrati, viene restituito l'oggetto e ci si muovera in verticale rispetto
        /// alla lettura che ne viene fatta
        /// </summary>
        /// <param name="currentColIndex"></param>
        /// <param name="currentRowIndex"></param>
        /// <returns></returns>
        private ExcelConcQuadrant FillMaterialConcentrationInfo(int currentColIndex, int currentRowIndex)
        {
            ExcelConcQuadrant currentQuadrantConcentrations = new ExcelConcQuadrant();


            return currentQuadrantConcentrations;
        }

        #endregion

    }
}
