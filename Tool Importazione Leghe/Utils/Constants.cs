using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Utils
{
    /// <summary>
    /// In questa classe sono presenti tutte le costanti di programma 
    /// </summary>
    public static class Constants
    {
        #region CONFIGURAZIONE AMBIENTE

        /// <summary>
        /// Stringa di connessione al database postgres sul quale vengono eseguite le operazioni 
        /// di import
        /// </summary>
        public static string NPGConnectionString = "Server=localhost;Port=6543;User Id=postgres;Password=root;Database=MetalLab300";

        #endregion


        #region DB ENTITIES

        /// <summary>
        /// Con questo enumeratore si mappano tutte le possibili entità database disponibili
        /// per l'import corrente
        /// </summary>
        public enum DBLabEntities
        {
            Leghe = 1,
            Normative = 2,
            Elementi = 3,
            Categorie_Leghe = 4,
            Basi = 5,
            ConcLeghe = 6
        }

        #endregion
    }
}
