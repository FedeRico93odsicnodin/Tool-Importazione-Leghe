using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto per la modellizazione della singola concentrazione
    /// che è possibile individuare per una lega
    /// </summary>
    public class ConcLegaDB : LabEntities
    {
        /// <summary>
        /// Grade id per la concentrazione corrente
        /// </summary>
        public int GrateId { get; set; }


        /// <summary>
        /// Elemento sul quale viene presa la concentrazione
        /// </summary>
        public string Elemento { get; set; }


        /// <summary>
        /// valore minimo per la concentrazione corrente 
        /// </summary>
        public  double concMin { get; set; }


        /// <summary>
        /// valore massimo per la concentrazione corrente
        /// </summary>
        public double concMax { get; set; }


        /// <summary>
        /// deroga minima per la concentrazione corrente
        /// </summary>
        public double derogaMin { get; set; }


        /// <summary>
        /// deroga massima per la concentrazione corrente
        /// </summary>
        public double derogaMax { get; set; }


        /// <summary>
        /// obiettivo per la concentrazione corrente
        /// </summary>
        public double obiettivo { get; set; }
    }
}
