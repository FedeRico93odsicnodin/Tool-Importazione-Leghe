using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto che mappa la singola normativa come oggetto che viene inserito nel database
    /// utilizzato per la configurazione corrente
    /// </summary>
    public class NormativeDB : LabEntities
    {
        /// <summary>
        /// ID per la normativa corrente
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// Normativa (nome colonna per l'omonima tabella)
        /// </summary>
        public string Normativa { get; set; }
    }
}
