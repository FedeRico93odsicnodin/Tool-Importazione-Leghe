using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.ExcelServices
{
    /// <summary>
    /// Questa classe mi permette di leggere gli headers relativi ai diversi fogli excel 
    /// grazie a questo metodo riesco a distinguere la presenza di un determinato header per un certo foglio 
    /// e quindi capire come procedere nella lettura e per quale caso siamo (se il tipo di sheet 1 o il tipo di sheet 2)
    /// </summary>
    public class ReadHeaders
    {
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
        public bool isFoglioDatiPrimari(Microsoft.Office.Interop.Excel._Worksheet currentExcelSheet, out int firstUtilCol, out int firstUtilRow)
        {
            int firstMarkerCol = 0;
            int firstMarkerRow = 0;


            bool hoTrovatoPrimoMarker = TrovaPrimoMarker(currentExcelSheet, ExcelMarkers.ROWNUMBER, out firstMarkerCol, out firstMarkerRow);

            // non ho trovato la sequenza corretta per poter idenficare la riga corrente 
            if (!hoTrovatoPrimoMarker)
            {
                // non ho trovato posizioni utili
                firstUtilCol = 0;
                firstUtilRow = 0;
                return false;
            }

            // non ho trovato posizioni utili
            firstUtilCol = 0;
            firstUtilRow = 0;

            // TODO : finire di implementare questo metodo

            return false;

        }

        #endregion


        #region METODI PRIVATI - DI SUPPORTO AI METODI PRECEDENTI

        /// <summary>
        /// Questo metodo trova il primo marker della serie relativamente a un determinato foglio di excel 
        /// sul quale bisogna capire l'dentità
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentMarker"></param>
        /// <param name="columnNumber"></param>
        /// <param name="rowNumner"></param>
        /// <returns></returns>
        private bool TrovaPrimoMarker(Microsoft.Office.Interop.Excel._Worksheet currentExcelSheet, string currentMarker, out int columnNumber, out int rowNumner)
        {
            int currentRow = 0;
            int currentColumn = 0;

            // indicazione del primo marker che devo incontrare per distinguere questa tipologia di foglio excel
            string marker1 = String.Empty;

            do
            {

                do
                {
                    marker1 = currentExcelSheet.Cells[currentRow, currentColumn].Value;

                    // ho già trovato il marker corrispondente al primo
                    if (marker1 == currentMarker)
                    {
                        columnNumber = currentColumn;
                        rowNumner = currentRow;

                        return true;
                    }
                    else currentColumn++;

                }
                // per convenzione prendo uno spettro di colonne che sia <= 5
                while (currentColumn <= 5);

                currentRow++;
                currentColumn = 0;

            }
            // per convenzione mi fermo quando la linea in lettura corrente è = 10
            while (currentRow <= 10);

            columnNumber = 0;
            rowNumner = 0;

            return false;
        }


        /// <summary>
        /// Permette di capire se trovo esattamente una corrispondenza per l'header relativo ad un determinato foglio excel 
        /// tra quelli possibili per andare a estrapolare le informazioni (eventualmente) sottostanti
        /// Viene per questo passata anche la lista di tutti gli headers di colonna, ricavabili dagli excel markers
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentColIndex"></param>
        /// <param name="currentRowIndex"></param>
        /// <param name="currentListHeaders"></param>
        /// <param name="colIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="markerExc"></param>
        /// <returns></returns>
        //private bool TrovaSequenzaColonneHeaderIndividuazioneFoglioExcel(Microsoft.Office.Interop.Excel._Worksheet currentExcelSheet, int currentColIndex, int currentRowIndex, List<string> currentListHeaders, out int colIndex, out int rowIndex, out string markerExc)
        //{
        //    foreach(string currentHeader in currentListHeaders)
        //    {
        //        // mi trovo nella condizione per la quale non è stato riconosciuto un determinato header per l'iterazione corrente 
        //        if (currentExcelSheet.Cells[currentRowIndex, currentColIndex].Value != currentHeader)
        //        {
        //            colIndex = currentColIndex;
        //            rowIndex = currentRowIndex;
        //            markerExc = currentHeader;
        //            return false;
        //        }

                  // todo: finire di implementare questo metodo
        //    }
        //}

        #endregion

    }
}
