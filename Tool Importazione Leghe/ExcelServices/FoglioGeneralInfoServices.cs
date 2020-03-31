using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Model;

namespace Tool_Importazione_Leghe.ExcelServices
{
    /// <summary>
    /// Qui sono contenute tutti metodi per poter lavorare sul primo foglio excel:
    /// lettura di:
    /// - Normativa
    /// - Categorie Leghe
    /// - 
    /// </summary>
    public class FoglioGeneralInfoServices
    {
        /// <summary>
        /// Permette di ottenere tutto il set di righe (informazioni generali) utili
        /// al fine del riconoscimento di una determinat concentrazione
        /// L'effettivo inserimento 
        /// </summary>
        /// <returns></returns>
        public List<RowFoglioExcel> GetCurrentSheet1Information(Microsoft.Office.Interop.Excel._Worksheet currentSheet)
        {
            // TODO: implementazione del metodo 
            return new List<RowFoglioExcel>();
        }


        /// <summary>
        /// Permette di scrivere l'header iniziale per le posizioni passate in input 
        /// vengono poi restitui gli indici di colonna e riga per cui iniziare a scrivere informazione utile per 
        /// questo foglio excel
        /// </summary>
        /// <param name="currentSheet"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentCol"></param>
        /// <param name="nextRow"></param>
        /// <param name="nextCol"></param>
        public void WriteHeaders(Microsoft.Office.Interop.Excel._Worksheet currentSheet, int currentRow, int currentCol, out int nextRow, out int nextCol)
        {
            nextCol = 0;
            nextRow = 0;
        }


        /// <summary>
        /// Permette di scrivere una riga di informazione generale relativa alle leghe 
        /// inserendo nella cella di riga e colonna passate in input 
        /// viene restituito il prossimo indice di riga e il prossimo indice di colonna nel quale andare a inserire la riga 
        /// successiva
        /// </summary>
        /// <param name="currentSheet"></param>
        /// <param name="currentRow1"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentColumn"></param>
        /// <param name="nextRow"></param>
        /// <param name="nextCol"></param>
        public void WriteCurrentRow1Excel(Microsoft.Office.Interop.Excel._Worksheet currentSheet, RowFoglioExcel currentRow1, int currentRow, int currentColumn, out int nextRow, out int nextCol)
        {
            nextCol = 0;
            nextRow = 0;
        }
    }
}
