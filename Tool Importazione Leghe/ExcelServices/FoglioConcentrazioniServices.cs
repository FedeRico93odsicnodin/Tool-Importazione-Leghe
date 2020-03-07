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
    /// Qui trovo tutti i servizi per andare a scrivere sul secondo foglio excel relativo alle concentrazioni per 
    /// un certo materiale
    /// </summary>
    public class FoglioConcentrazioniServices
    {

        #region METODI PUBBLICI 

        /// <summary>
        /// Permette di ottenere tutte le concentrazioni di un certo materiale che viene specificato in input
        /// </summary>
        /// <param name="currentSheet"></param>
        /// <param name="materialDescription"></param>
        /// <returns></returns>
        public List<RowFoglio2Excel> GetCurrentConcentrations(Microsoft.Office.Interop.Excel._Worksheet currentSheet, int initialRow, int initialCol, string materialDescription)
        {

            do
            {

            }
            while (true);


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

        #endregion


        #region METODI PRIVATI

        /// <summary>
        /// Permette la mappatura di una singola riga per le concentrazioni
        /// isValid è un valore che mi dice se continuare o meno con l'iterazione per riga
        /// </summary>
        /// <param name="currentSheet"></param>
        /// <param name="initialRow"></param>
        /// <param name="initialCol"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        private RowFoglio2Excel MapSingleConcentrationRow(Microsoft.Office.Interop.Excel._Worksheet currentSheet, int initialRow, int initialCol, out bool isValid)
        {
            // informazioni che conterranno i valori recuperati e convertiti
            double currentMinNum = 0;
            double currentMaxNum = 0;
            double currentApproxNum = 0;

            // oggetto che verrà inserito come riga
            RowFoglio2Excel currentRowConcentrazioni = new RowFoglio2Excel();

            // stringa di log per il recupero corrente
            string currentLogMsg = "";


            try
            {
                #region RECUPERO E VALIDAZIONE INIZIALE DI TUTTI I PARAMETRI DI RIGA 

                // informazione per l'elemento corrente
                string currentEl = currentSheet.Cells[initialRow, initialCol].Value;

                // non posso proseguire o sono arrivato alla fine della lettura se non leggo nessuna informazione per l'elemento 
                // (informazione obbligatoria)
                if (currentEl == null || currentEl == "-") 
                    isValid = false;

                // informazione per il valore minimo 
                string currentMin = currentSheet.Cells[initialRow, initialCol + 1].Value;

                // al momento inserisco il minimo corrente = 0
                if (currentMin == null || currentMin == "-")
                    currentMin = "0";

                // informazione per il valore massimo 
                string currentMax = currentSheet.Cells[initialRow, initialCol + 2].Value;
                
                // al momento inserisco il massimo corrente = 0
                if (currentMax == null || currentMax == "-")
                    currentMax = "0";

                // informazione per l'approssimazione
                string currentApprox = currentSheet.Cells[initialRow, initialCol + 3].Value;

                // al momento inserisco l'approssimazione corrente = 0
                if (currentApprox == null || currentApprox == "-")
                    currentApprox = "0";

                // informazione relativa al commento 
                string currentCommento = currentSheet.Cells[initialRow, initialCol + 4].Value;

                // al momento inserisco una stringa vuoto su commento inesistente
                if (currentCommento == null || currentCommento == "-")
                    currentCommento = String.Empty;

                #endregion


                #region CONVERSIONE NEI DIVERSI FORMATI DELLE INFORMAZIONI TIRATE SU COME STRINGHE

                // conversione per il minimo valore
                double.TryParse(currentMin, out currentMinNum);

                // conversione per il massimo valore
                double.TryParse(currentMax, out currentMaxNum);

                // conversione per il valore approssimazione
                double.TryParse(currentApprox, out currentApproxNum);

                // vedo se l'elemento rispetta la definizione
                isValid = CheckElemento(currentEl);

                #endregion

                #region POPOLAZIONE OGGETTO
                
                currentRowConcentrazioni.Criterio = currentEl;

                currentRowConcentrazioni.Min = currentMinNum;

                currentRowConcentrazioni.Max = currentMaxNum;

                currentRowConcentrazioni.Appross = currentApproxNum;
                
                #endregion

            }
            catch (Exception e)
            {
                string currentExceptionMsg = String.Format(ExceptionMessages.HOTROVATOLASEGUENTEECCEZIONENELLEGGEREINFOFOGLIO2, e.Message);
                isValid = false;
            }

            return currentRowConcentrazioni;
        }


        /// <summary>
        /// Permette di capire se l'elemento coincide con quelli già presenti a database o meno
        /// nel caso in cui non sia cosi viene restituita una eccezione
        /// </summary>
        /// <param name="currentEl"></param>
        /// <returns></returns>
        private bool CheckElemento(string currentEl)
        {
            // ritorno direttamente il valore contenuto nella lista comune
            return Constants.CurrentListElementi.Contains(currentEl);
        }

        #endregion
    }
}
