using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto di supporto alla lettura dell'header delle concentrazioni per individuare dove
    /// si trovano i diversi elementi utili a distinguere il materiale e le diverse concentrazioni
    /// ad esso associato
    /// </summary>
    public class ExcelConcQuadrant
    {
        /// <summary>
        /// Mappatura index colonna per il titolo associato al materiale corrente
        /// </summary>
        public int TitlePos_X { get; set; }


        /// <summary>
        /// Mappatura index riga per il titolo associato al materiale corrente
        /// </summary>
        public int TitlePos_Y { get; set; }


        /// <summary>
        /// Mappatura index di riga per l'header concentrazioni per il materiale corrente
        /// </summary>
        public int HeaderPos_X { get; set; }


        /// <summary>
        /// Mappatura index di colonna per l'header concentrazioni per il materiale corrente
        /// </summary>
        public int HeaderPos_Y { get; set; }


        /// <summary>
        /// Mappatura index di riga da cui partire a leggere le concentrazioni per il materiale corrente
        /// </summary>
        public int StartConc_X { get; set; }


        /// <summary>
        /// Mappatura index di colonna da cui partire a leggere le concentrazioni per il materiale corrente
        /// </summary>
        public int StartConc_Y { get; set; }


        /// <summary>
        /// Mappatura index di riga su cui finire di leggere le concentrazioni per il materiale corrente
        /// </summary>
        public int EndConc_X { get; set; }


        /// <summary>
        /// Mappatura index di colonna su cui finire di leggere le concentrazioni per il materiale corrente
        /// </summary>
        public int EndConc_Y { get; set; }
    }
}
