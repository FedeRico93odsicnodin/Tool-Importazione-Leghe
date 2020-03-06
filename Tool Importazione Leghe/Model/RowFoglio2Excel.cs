using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Permette di leggere le informazioni per il secondo tipo di foglio excel
    /// questo foglio contiene il seguente set di informazioni:
    /// - Lega (Header primario) --> questa deve essere confrontata con l'eventuale presenza con la lista in memoria 
    /// - Criterio --> deve corrispondere ad una qualche informazione già inserita per l'elemento e la sua dimensione deve essere a prescindere al massimo di 2 posizioni
    /// - Min --> indica il minimo valore da inserire per la lettura e l'elemento corrente - viene anche inserito come derogaMin (se specificato)
    /// - Max --> indica il massimo valore da inserire per la lettura e l'elemento corrente - viene anche inserito come derogaMax (se specificato)
    /// - Appross --> verrà inserito come obiettivo nella tabella di destinazione
    /// 
    /// </summary>
    public class RowFoglio2Excel
    {
        /// <summary>
        /// Corrispondenza con l'elemento
        /// </summary>
        public string Criterio { get; set; }


        /// <summary>
        /// Corrispondenza con concMin (derogaMin)
        /// </summary>
        public double Min { get; set; }

        
        /// <summary>
        /// Corrispondenza con concMax (derogaMax)
        /// </summary>
        public double Max { get; set; }


        /// <summary>
        /// Corrispondenza con l'obiettivo
        /// </summary>
        public double Appross { get; set; }
    }
}
