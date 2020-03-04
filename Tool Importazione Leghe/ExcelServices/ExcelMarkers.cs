using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.ExcelServices
{
    /// <summary>
    /// In questa classe vanno inseriti tutti i markers che contraddistinguono un foglio di mappatura per relativamente
    /// 
    /// 1) inserimento in Normative, Leghe, Categorie_Leghe, Basi
    /// 
    /// 2) inserimento in ConcLeghe in base ai valori inseriti precedentemente
    /// </summary>
    public static class ExcelMarkers
    {
        #region FOGLIO NORMATIVE, LEGHE, CATEGORIE_LEGHE, BASI

        /// <summary>
        /// Header di colonna per la riga relativa al materiale di partenza
        /// </summary>
        public const string MATERIALE_CELL = "MATERIALE";


        /// <summary>
        /// Header di colonna per la riga relativa alla normativa di partenza
        /// </summary>
        public const string NORMATIVA_CELL = "NORMATIVA";


        /// <summary>
        /// Header di colonna per la riga relativa al paese produttore di partenza
        /// </summary>
        public const string PAESEPRODUTTORE_CELL = "PAESE / PRODUTTORE";


        /// <summary>
        /// Header di colonna per la riga relativa al tipo di partenza
        /// </summary>
        public const string TIPO_CELL = "TIPO";

        #endregion


        #region FOGLIO CONCENTRAZIONI

        /// <summary>
        /// Header di colonna per la riga relativa ai Criteri (gli elementi)
        /// </summary>
        public const string CRITERI_CELL = "Criteri";


        /// <summary>
        /// Header di colonna per la riga relativa alla concentrazione minima 
        /// </summary>
        public const string MIN_CELL = "Min";


        /// <summary>
        /// Header di colonna per la riga relativa alla concentrazione massima
        /// </summary>
        public const string MAX_CELL = "Max";


        /// <summary>
        /// Header di colonna per la riga relativa all'approssimazione
        /// </summary>
        public const string APPROSSIMAZIONE_CELL = "Appross";


        /// <summary>
        /// Header di colonna per la riga relativa al commento
        /// </summary>
        public const string COMMENTO_CELL = "Commento";

        #endregion

    }
}
