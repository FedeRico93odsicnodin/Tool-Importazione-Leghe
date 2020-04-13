using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto per la modellizazzione di un singolo elemento
    /// </summary>
    public class ElementiDB : LabEntities
    {
        /// <summary>
        /// ID elemento
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// Symbol elemento
        /// </summary>
        public string Symbol { get; set; }


        /// <summary>
        /// Nome elemento
        /// </summary>
        public string Nome { get; set; }
    }
}
