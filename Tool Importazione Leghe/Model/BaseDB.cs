using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto per la modellizzazione della singola base
    /// </summary>
    public class BaseDB : LabEntities
    {
        /// <summary>
        /// ID base
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// IDElem collegato alla base
        /// </summary>
        public int IDElem { get; set; }


        /// <summary>
        /// enabled per la base corrente
        /// </summary>
        public bool Enabled { get; set; }


        /// <summary>
        /// ordine visualizzazione per la base corrente
        /// </summary>
        public int OrdineVis { get; set; }
    }
}
