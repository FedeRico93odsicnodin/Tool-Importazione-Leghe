using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Model;

namespace Tool_Importazione_Leghe.ExcelServices
{
    /// <summary>
    /// Qui trovo tutti i servizi per andare a scrivere sul secondo foglio excel relativo alle concentrazioni per 
    /// un certo materiale
    /// </summary>
    public class FoglioConcentrazioniServices
    {
        /// <summary>
        /// Permette di ottenere tutte le concentrazioni di un certo materiale che viene specificato in input
        /// </summary>
        /// <param name="currentSheet"></param>
        /// <param name="materialDescription"></param>
        /// <returns></returns>
        public List<RowFoglio2Excel> GetCurrentConcentrations(Microsoft.Office.Interop.Excel._Worksheet currentSheet, string materialDescription)
        {
            return new List<RowFoglio2Excel>();
        }
        

        /// <summary>
        /// Permette la scrittura della prima riga di header relativa al materiale di riferimento sul quale si inizieranno a scrivere poi 
        /// tutte le concentrazioni
        /// </summary>
        /// <param name="currentSheet"></param>
        /// <param name="materialDescription"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentColumn"></param>
        /// <param name="nextRow"></param>
        /// <param name="nextCol"></param>
        public void WriteCurrentMaterialeHeader(Microsoft.Office.Interop.Excel._Worksheet currentSheet, string materialDescription, int currentRow, int currentColumn, out int nextRow, out int nextCol)
        {
            nextRow = 0;
            nextCol = 0;
        }


        /// <summary>
        /// Permette di scrivere le informazioni di header per la distinzione dei valori di concentrazioni inseriti successivamente 
        /// </summary>
        /// <param name="currentSheet"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentColumn"></param>
        /// <param name="nextRow"></param>
        /// <param name="nextCol"></param>
        public void WriteSecondHeaderInfoConcetratins(Microsoft.Office.Interop.Excel._Worksheet currentSheet, int currentRow, int currentColumn, out int nextRow, out int nextCol)
        {
            nextCol = 0;
            nextRow = 0;
        }


        /// <summary>
        /// Permette di scrivere una informazione di riga per le concentrazioni
        /// </summary>
        /// <param name="currentSheet"></param>
        /// <param name="currentRow2"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentColumn"></param>
        /// <param name="nextRow"></param>
        /// <param name="nextCol"></param>
        public void WriteConcentrationValues(Microsoft.Office.Interop.Excel._Worksheet currentSheet, RowFoglio2Excel currentRow2, int currentRow, int currentColumn, out int nextRow, out int nextCol)
        {
            nextCol = 0;
            nextRow = 0;
        }
    }
}
