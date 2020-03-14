using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Utils
{
    /// <summary>
    /// In questa classe sono inseriti tutti i metodi di utilità generale per lavorare correttamente sui file 
    /// in lettura e scrittura per la fase di import corrente
    /// </summary>
    public static class GeneralUtilities
    {
        /// <summary>
        /// Permette di ottenere il nome del file a partire dal path nel quale questo file 
        /// è stato inserito per l'import corrente
        /// </summary>
        /// <param name="currentFilePath"></param>
        /// <returns></returns>
        public static string GetFileName(string currentFilePath)
        {
            return currentFilePath.Substring(currentFilePath.LastIndexOf("\\") + 1);
        }
    }
}
