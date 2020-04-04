using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Elemento di transizione per l'eventuale scrittura delle informazioni relative alle leghe all'interno di una destinazione 
    /// qui dentro sono contenute le 3 rappresentazioni per quanto riguarda l'oggetto da persistere 
    /// </summary>
    public class LegaInfoObject
    {

        #region COSTRUTTORE 

        /// <summary>
        /// By default le proprieta relative al recupero alla validazione e alla possibile persistenza sono a false 
        /// per l'oggetto corrente 
        /// </summary>
        public LegaInfoObject()
        {
            Step1_Recupero = false;
            Step2_Validazione_SameSheet = false;
            Step3_Persistenza = false;
        }

        #endregion

        /// <summary>
        /// Indicazione della eventuale riga excel corrispondente per la lega 
        /// </summary>
        public RowFoglioExcel Lega_ExcelRow { get; set; }


        /// <summary>
        /// corrispondenza della eventuale nornamtiva corrispondente a livello DB
        /// </summary>
        public NormativeDB Lega_NormativaDB { get; set; }


        /// <summary>
        /// corrispondenza con eventuale categoria lega corrispondente a livello DB 
        /// </summary>
        public Categorie_LegheDB Lega_CategoriaLega { get; set; }


        /// <summary>
        /// corrispondenza con la eventuale base corrispodnente 
        /// </summary>
        public BaseDB Lega_BaseCorrispondente { get; set; }


        /// <summary>
        /// oggetto di corrispondenza database per la lega in questione
        /// </summary>
        public LegheDB LegaDBCorrispondente { get; set; }


        #region STEPS

        /// <summary>
        /// Indica se l'informazione corrente per la lega è stata letta correttamente  
        /// </summary>
        public bool Step1_Recupero { get; set; }


        /// <summary>
        /// Indica se l'informazione corrente per il foglio è stata validata correttamente in base alle informazioni contenute nello stesso foglio 
        /// </summary>
        public bool Step2_Validazione_SameSheet { get; set; }


        /// <summary>
        /// Indica se l'informazione corrente può essere persistita in base alle informazioni di lega contenute in questo foglio o gia presenti 
        /// all'interno della sorgente (che quindi validano in se il set di leghe che si sta inserendo)
        /// </summary>
        public bool Step3_Persistenza { get; set; }

        #endregion
    }
}
