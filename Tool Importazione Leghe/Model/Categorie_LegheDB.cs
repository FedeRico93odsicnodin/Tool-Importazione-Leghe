using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto per la modellizzazione in memoria di una singola entita del tipo 
    /// Categoria_Leghe
    /// </summary>
    public class Categorie_LegheDB : LabEntities
    {
        /// <summary>
        /// ID categoria lega
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// Categoria categoria lega
        /// </summary>
        public string Categoria { get; set; }


        /// <summary>
        /// ID Base collegata alla categoria lega
        /// </summary>
        public int IDBase { get; set; }
    }
}
