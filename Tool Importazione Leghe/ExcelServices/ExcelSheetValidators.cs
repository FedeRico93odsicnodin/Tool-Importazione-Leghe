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
    /// In questa classe sono contenuti tutti i metodi di supporto per la validazione della sintassi excel rispetto alle 2 tipologie di foglio
    /// </summary>
    internal static class ExcelSheetValidators
    {
        /// <summary>
        /// Peremtte di verificare che le celle di headers incontrate all'interno l'analisi dello STEP 1 per il foglio excel di informazioni generali di lega 
        /// siano tutte allineate rispetto alla stessa riga
        /// </summary>
        /// <param name="currentInfoHeaders"></param>
        /// <returns></returns>
        internal static bool CheckAllineamentoHeadersForGeneralInfo(List<HeadersInfoLega_Excel> currentInfoHeaders)
        {
            int currentRowInfo = currentInfoHeaders.FirstOrDefault().Starting_Row;

            foreach(HeadersInfoLega_Excel currentHeaderGeneralInfo in currentInfoHeaders)
            {
                if (currentHeaderGeneralInfo.Starting_Row != currentRowInfo)
                    return false;
            }

            return true;
        }


        /// <summary>
        /// Permette di verificare che: 
        /// 1) il titolo sia inserito prima della riga di header
        /// 2) il titolo e il primo header siano allineati sulla stessa colonna 
        /// 3) la riga delle concentrazioni è maggiore di quella degli header
        /// 4) la riga di fine lettura delle concentrazioni è maggiore di quella di inizio lettura 
        /// 
        /// se qualcuna di queste proprieta non è rispettata allora deve essere gestita opportunamente l'eccezione con il messaggio di errore corretto
        /// </summary>
        /// <param name="currentConcQuadrant"></param>
        /// <returns></returns>
        internal static bool CheckAllineamentoHeadersForCurrentConcQuadrant(ExcelConcQuadrant currentConcQuadrant)
        {
            // check 1: la colonna di titolo viene prima di quella degli header
            if (currentConcQuadrant.Title_Col != currentConcQuadrant.Head_Col)
                throw new Exception(ExceptionMessages.CONCENTRATIONSQUADRANT_COLONNEHEADERTITLEDISALLINEATE);

            // check 2: la riga di headers è maggiore rispetto a quella del titolo
            if (currentConcQuadrant.Head_Row <= currentConcQuadrant.Title_Row)
                throw new Exception(ExceptionMessages.CONCENTRATIONSQUADRANT_RIGATITLEPRIMADIRIGAHEADER);

            // check 3: la riga di inizio lettura concentrazioni è maggiore rispetto a quella di header
            if (currentConcQuadrant.Conc_Row_Start <= currentConcQuadrant.Head_Row)
                throw new Exception(ExceptionMessages.CONCENTRATIONSQUADRANT_RIGACONCENTRATIONSPRIMADIHEADER);

            // check 4: la riga di fine lettura per le concentrazioni è maggiore rispetto a quella di inizio lettura 
            if (currentConcQuadrant.Conc_Row_End < currentConcQuadrant.Conc_Row_Start)
                throw new Exception(ExceptionMessages.CONCENTRATIONSQUADRANT_RIGAFINELETTURACONCMINORE);

            return true;
        }



        /// <summary>
        /// Controllo che per l'informazione recuperata per la riga di excel ci siano le proprieta indispensabili 
        /// che possano permette anche la persistenza durante la scrittura su una destinazione per la riga corrente 
        /// STEP1: questo metodo di validazione è molto importante perché se cosi non fosse la riga viene segnalata come invalida 
        /// e non passando il primo controllo viene scartata a priori
        /// </summary>
        /// <param name="readInfo"></param>
        /// <returns></returns>
        internal static List<string> CurrentNullPropertiesRowInfoLega(RowFoglioExcel readInfo)
        {
            List<string> currentNullPropertiesPerRiga = new List<string>();

            List<string> allPossibleProperties = ExcelMarkers.GetAllColumnHeadersForGeneralInfoSheet().ToList();

            foreach(string property in allPossibleProperties)
            {
                if (readInfo.GetValue(property) == String.Empty)
                    currentNullPropertiesPerRiga.Add(property);
            }

            return currentNullPropertiesPerRiga;
        }
    }
}
