using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// OGGETTO per la mappatura di partenza delle righe trovate per il primo foglio excel
    /// questa riga si appoggia sulle tabelle relative a 
    /// Normative, Leghe, Categorie_Leghe e Basi (indirettamente, facendo un check su FK, se non presente è da inserire
    /// </summary>
    public class RowFoglio1Excel
    {
        /// <summary>
        /// Mappatura della colonna normativa corrispondente --> DA CONFRONTARE CON QUESTO PARAMETRO
        /// </summary>
        public string NormativaCorrispondente { get; set; }

        
        /// <summary>
        /// Indica il nome della lega corrispondente per questo caso --> DA CONFRONTARE CON QUESTO PARAMETRO
        /// </summary>
        public string NomeLegaCorrispondente { get; set; }


        /// <summary>
        /// Indica un set di categorie leghe che viene direttamente recuperato dal documento di partenza --> DA CONFRONTARE CON QUESTO PARAMETRO
        /// </summary>
        public List<string> CategorieLegheCorrispondenti { get; set; }

        
    }
}
