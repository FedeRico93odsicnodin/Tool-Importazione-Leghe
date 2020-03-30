using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// OGGETTO DI TRANSIZIONE per le proprieta lette rispetto a un quadrante di concentrazioni 
    /// contiene tutti gli elementi per poter individuare la sorgente e tutti gli elementi che poi verranno configurati per 
    /// la rispettiva scrittura all'interno della destinazione 
    /// </summary>
    public class MaterialConcentrationsObject
    {
        /// <summary>
        /// Quadrante di riferimento sul quale vengono lette le informazioni inerenti 
        /// il materiale corrente con le relative concentrazioni
        /// </summary>
        public ExcelConcQuadrant ExcelQuadrantReference { get; set; }


        /// <summary>
        /// Nome del materiale corrispondente 
        /// </summary>
        public string MaterialName { get; set; }


        /// <summary>
        /// Nome per la lega corrispondente sul quale viene preso il materiale 
        /// </summary>
        public string AlloyName { get; set; }


        /// <summary>
        /// Tutte le righe lette per il quadrante di concentrazioni corrente
        /// </summary>
        public List<RowFoglio2Excel> ReadConcentrationsRows { get; set; }


        /// <summary>
        /// Impostazione delle rispettive righe di concentrazione per la valorizzazione 
        /// vera e propria per un database di origine / di destinazione delle informazioni
        /// </summary>
        public List<ConcLegaDB> ConcentrationsDB { get; set; }


        /// <summary>
        /// Qui dentro è contenuta l'informazione che mi dirà se il materiale corrente 
        /// con le relative concentrazioni ha passato lo step 1 di analisi del foglio excle / xml
        /// </summary>
        public bool IsValid_STEP1 { get; set; }
    }
}
