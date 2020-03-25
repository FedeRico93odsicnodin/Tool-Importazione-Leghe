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
        /// Header che mi da indicazione della riga di colonna alla quale siamo arrivati
        /// </summary>
        public const string ROWNUMBER = "#";

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


        /// <summary>
        /// Permette di ottenre la lista completa di markers con la quale distinguo il primo foglio excel
        /// relativo alle informazioni generali
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllColumnHeadersForGeneralInfoSheet()
        {
            List<string> currentInfo = new List<string>();

            // inserimenti di tutte le informazioni utili per poter distinguere il primo foglio excel
            currentInfo.Add(ROWNUMBER);
            currentInfo.Add(MATERIALE_CELL);
            currentInfo.Add(NORMATIVA_CELL);
            currentInfo.Add(PAESEPRODUTTORE_CELL);
            currentInfo.Add(TIPO_CELL);

            return currentInfo;
        }

        #endregion


        #region PARAMETRI AUSILIARI CHE E' POSSIBILE AVERE NEL FOGLIO DELLE INFORMAZIONI

        /// <summary>
        /// Stringa relativa alla designazione alternativa che è possible riscontrare come parametro opzionale 
        /// all'interno dell'excel
        /// </summary>
        public const string DESIGNAZIONE_ALTERNATIVA = "Designazione Alternativa";


        /// <summary>
        /// Permette di ottenere la lista di tutte le proprietà opzionali che è possibile riscontrare all'interno
        /// del foglio excel riguardante le informazioni generali di lega
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAdditionalPropertiesGeneralInfoSheet()
        {
            List<string> currentAdditionalProperties = new List<string>();

            currentAdditionalProperties.Add(DESIGNAZIONE_ALTERNATIVA);

            return currentAdditionalProperties;
        }

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


        /// <summary>
        /// Permette di ottenere la lista completa di markers per la distinzione del secondo foglio excel
        /// relativo alle concentrazioni per un determinato materiale che deve comunque essere individuato 
        /// prima all'interno del foglio excel
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllColumnHeadersForConcentrationsInfoSheet()
        {
            List<string> currentInfo = new List<string>();

            currentInfo.Add(CRITERI_CELL);
            currentInfo.Add(MIN_CELL);
            currentInfo.Add(MAX_CELL);
            currentInfo.Add(APPROSSIMAZIONE_CELL);
            currentInfo.Add(COMMENTO_CELL);

            return currentInfo;
        }

        #endregion

    }
}
