using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Questo oggetto mappa le informazioni di headers relative alla particolare lega lette in input
    /// </summary>
    public class HeadersInfoLega_Excel
    {
        /// <summary>
        /// Nome della proprieta letta dal foglio di informazioni generali per la lega 
        /// </summary>
        public string NomeProprietà { get; set; }


        /// <summary>
        /// Informazione di natura generale sulla riga nella quale si trova l'informazione che ha il nome 
        /// attribuito con la proprieta precedente 
        /// </summary>
        public int Starting_Row { get; set; }


        /// <summary>
        /// Informazione di natura generale sulla colonna nella quale si trova l'informazione che ha il nome
        /// attribuito con la proprieta precedente 
        /// </summary>
        public int Starting_Col { get; set; }
    }
}
