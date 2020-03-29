using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Peremtte di ritornare le celle per le quali non è stato inserito valore nel foglio excel di valori 
        /// per le informazioni generali di lega correnti
        /// </summary>
        /// <param name="readInfo"></param>
        /// <returns></returns>
        internal static List<string> CurrentNullPropertiesRowInfoLega(RowFoglio1Excel readInfo)
        {
            List<string> currentNullPropertiesPerRiga = new List<string>();

            List<string> allPossibleProperties = ExcelMarkers.GetAdditionalPropertiesGeneralInfoSheet().Union(ExcelMarkers.GetAllColumnHeadersForGeneralInfoSheet()).ToList();

            foreach(string property in allPossibleProperties)
            {
                if (readInfo.GetValue(property) == String.Empty)
                    currentNullPropertiesPerRiga.Add(property);
            }

            return currentNullPropertiesPerRiga;
        }
    }
}
