using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
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
        private int _tracciaCurrentCol = 0;


        /// <summary>
        /// Tiene traccia dell'indice di riga corrente per l'eventuale gestione dell'eccezione
        /// </summary>
        private int _tracciaCurrentRow = 0;


        /// <summary>
        /// Tiene traccia del marker corrente per l'eventuale gestione dell'eccezione
        /// </summary>
        private string _currentMarker = String.Empty;


        /// <summary>
        /// Indica il limite sul numero di righe che posso leggere per trovare l'informazione relativa all'header
        /// </summary>
        private const int LIMITROW = 20;


        /// <summary>
        /// indica il limite sul numero di colonne che posso leggere per trovare l'informazione relativa all'header
        /// </summary>
        private const int LIMITCOL = 20;


        /// <summary>
        /// Indica il limite sul numero di righe che posso leggere a partire dall'individuazione dell'header per trovare il primo valore 
        /// utile per l'informazione sul foglio excel corrente
        /// </summary>
        private const int LIMITINFOROW = 10;

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
        internal Utils.Constants.TipologiaFoglioExcel ReadFirstInformation_DatiPrimari(ref Worksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaFoglio, out int firstUtilCol, out int firstUtilRow)
        {

            try
            {
                // vado a leggere l'header per il foglio excel corrente
                bool hoLettoHeader = ReadHeader(ref currentExcelSheet, currentTipologiaFoglio);

                // non ho letto le informazioni utili di header
                if (!hoLettoHeader)
                {

                    // attribuzione del valore di default per la riga e la colonna in uscita
                    firstUtilCol = _tracciaCurrentCol;
                    firstUtilRow = _tracciaCurrentRow;
                    return Utils.Constants.TipologiaFoglioExcel.Unknown;
                }


                // calcolo la prima informazione utile per il foglio excel corrente
                CalculateFirstInformation(ref currentExcelSheet, currentTipologiaFoglio, out _tracciaCurrentCol, out _tracciaCurrentRow);


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


            return Utils.Constants.TipologiaFoglioExcel.Unknown;

            
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
        private bool ReadHeader(ref Worksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaFoglio)
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
                    while(_tracciaCurrentRow < LIMITROW)
                    {

                        // iterazione per la colonna corrente
                        while(_tracciaCurrentCol < LIMITCOL)
                        {
                            if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value.ToString() == _currentMarker)
                            {
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
        private void CalculateFirstInformation(ref Worksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaExcel, out int currentInfoCol, out int currentInfoRow)
        {

            // attribuzione del valore di default per la riga e la colonna in uscita
            currentInfoCol = _tracciaCurrentCol;
            currentInfoRow = _tracciaCurrentRow;

        }

        #endregion

    }
}
