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


        /// <summary>
        /// Stringa percorso nel quale trovo il file excel correntemente in analisi
        /// </summary>
        public static string CurrentFileExcelPath = "D:\\Projects\\GNR\\Tool Importazione Leghe\\Origin Leghe\\Excel\\Nickel_Alloys.xlsx";

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


        #region EXCEL SHEETS

        /// <summary>
        /// Indicazione sulla tipologia di foglio excel sulla quale si sta iterando attualmente 
        /// se si tratta di un foglio contenente le informazioni generali o 
        /// il set di concentrazioni per un determinato materiale
        /// </summary>
        public enum TipologiaFoglioExcel
        {
            foglioInformazioniGenerali = 1,
            foglioInformazioniConcentrazioni = 2
        }


        /// <summary>
        /// Permette di capire se, leggendo il foglio excel 2 (corrispondente alle concentrazioni)
        /// sarà fatta anche la lettura per deroga minima / massima
        /// </summary>
        public enum DevoInserireDeroghe
        {
            si = 1,
            no = 2
        }

        #endregion

    }
}
