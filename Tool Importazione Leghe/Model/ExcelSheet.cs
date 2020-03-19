using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto che mi mappa le proprietà principale del foglio excel corrente per poterne 
    /// poi fare il riconoscimento e la lettura successiva
    /// </summary>
    public class ExcelSheet
    {
        
        /// <summary>
        /// Nome per il foglio excel in analisi corrente
        /// </summary>
        public string SheetName { get; set; }


        /// <summary>
        /// Indicazione del file corrente nel quale si trova il foglio excel
        /// </summary>
        public string ExcelFile { get; set; }


        /// <summary>
        /// Ottenimento della posizione per il foglio excel corrente
        /// </summary>
        public int PositionInExcelFile { get; set; }


        /// <summary>
        /// Ottenimento della tipologia riconosciuta per il foglio excel corrente 
        /// </summary>
        public Constants.TipologiaFoglioExcel TipologiaRiconosciuta { get; set; }

        
        /// <summary>
        /// Tiene traccia del fatto che il foglio sia stato effettivamente letto o meno
        /// </summary>
        public bool Letto { get; set; }


        /// <summary>
        /// Permette di posizionarsi sulla prima colonna utile per le informazioni da leggere su questo 
        /// foglio excel
        /// </summary>
        public int Info_Col { get; set; }


        /// <summary>
        /// Permette di posizionarsi sulla prima riga utile per le informazioni da leggere su questo 
        /// foglio excel
        /// </summary>
        public int Info_Row { get; set; }


        /// <summary>
        /// Quadranti di concentrazioni iniziali per il foglio correntemente in analisi.
        /// Questa informazione viene valorizzata solamente nel caso in cui il foglio sia effettivamente riconosciuto
        /// come un foglio contenente informazioni di concentrazioni per determinati materiali
        /// </summary>
        public List<ExcelConcQuadrant> Concentrations_Quadrants { get; set; }
    }
}
