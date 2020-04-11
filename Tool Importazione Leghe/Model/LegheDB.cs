using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Questa classe mi modella l'entità database contenuta nel database di origine
    /// </summary>
    public class LegheDB : LabEntities 
    {
        /// <summary>
        /// GradeId Lega
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// Nome Lega
        /// </summary>
        public string Nome { get; set; }


        /// <summary>
        /// Descrizione Lega
        /// </summary>
        public string Descrizione { get; set; }


        /// <summary>
        /// CategoriaId Lega
        /// </summary>
        public int CategoriaId { get; set; }


        /// <summary>
        /// Normativa Lega
        /// </summary>
        public string Normativa { get; set; }


        /// <summary>
        /// Trattamento Lega
        /// </summary>
        public string Trattamento { get; set; }


        /// <summary>
        /// IdNormativa Lega
        /// </summary>
        public int IdNormativa { get; set; }


        /// <summary>
        /// ID base di riferimento
        /// </summary>
        public int IDBase { get; set; }


        /// <summary>
        /// Materiale 
        /// </summary>
        public string MatNum { get; set; }

    }
}
