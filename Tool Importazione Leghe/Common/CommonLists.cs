using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Common
{
    /// <summary>
    /// Contiene le liste con tutti gli elementi che vengono prelevati per l'import sia dall'origine che dalla destinazione 
    /// per l'esecuzione di eventuali match di consistenza 
    /// </summary>
    public static class CommonLists
    {
        #region ATTRIBUTI PRIVATI - LISTE DI FUSIONE
        
        /// <summary>
        /// Lista ottenuta dalla fusione degli oggetti relativi alla destinazione e alla sorgente dell'import e inerenti 
        /// le definizioni date per le diverse tabelle (Normative, Leghe, Categorie_Leghe, Basi)
        /// </summary>
        private static List<LegaInfoObject> _fusedListInfoLeghe;


        /// <summary>
        /// Lista proveniente dalla fusione delle informazioni relative alle concentrazioni per i diversi materiali
        /// </summary>
        private static List<MaterialConcentrationsObject> _fusedListInfoConcentrations;

        #endregion


        #region POPOLAZIONE DELLE DIVERSE LISTE COMUNI 
        
        /// <summary>
        /// Permette di andare a inserire la nuova lega in lettura nel set di tutte le leghe in lettura per l'import corrente 
        /// Questo metodo viene richiamato principalmente dalle tipologie di import relative all'import da excel a database o da xml a database
        /// viene controllata la validità delle informazioni rispetto alle condizioni delle informazioni già presente nella destinazione 
        /// e marcate le informazioni in lettura in un certo modo, viene anche offerta la segnalazione del controllo effettuato 
        /// </summary>
        /// <param name="currentReadLega"></param>
        /// <param name="tipologiaImport"></param>
        public static void AddNewLegaInfoObject(LegaInfoObject currentReadLega, Constants.TipologiaImport tipologiaImport)
        {
            // validazione sulle liste rispetto alle quali andare a fare il riconoscimento
            if (NormativeDB == null)
                throw new Exception(ExceptionMessages.NONHOTROVATOINFORMAZIONINORMATIVE);

            if (LegheDB == null)
                throw new Exception(ExceptionMessages.NONHOTROVATOINFORMAZIONILEGHE);

            if (CategorieLegheDB == null)
                throw new Exception(ExceptionMessages.NONHOTROVATOINFORMAZIONICATEGORIELEGHE);

            if (BasiDB == null)
                throw new Exception(ExceptionMessages.NONHOTROVATOINFORMAZIONIBASI);


        }



        #endregion


        #region CHECK CONSISTENCY INFORMAZIONE 

        /// <summary>
        /// Esecuzione check di validità per le informazioni di lega:
        /// CASO 1: Lega -> ASSENTE / Normativa -> ASSENTE : devo inserire (sia lega che normativa)
        /// CASO 2: Lega -> PRESENTE / Normativa -> ASSENTE : devo inserire (ma ottenere conferma... potrei inserire la lega ma con una normativa diversa ...)
        /// CASO 3: Lega -> PRESENTE / Normativa -> PRESENTE : non devo eseguire nessun inserimento 
        /// CASO 4: Lega -> ASSENTE / Normativa -> PRESENTE : da inserire con l'ID della normativa di cui trovo la presenza
        /// 
        /// Questi casi sono distinguere sulla tipologia di import excel / xml
        /// </summary>
        /// <param name="currentReadLega"></param>
        /// <param name="tipologiaImport"></param>
        private static void CheckLega_Normativa_Match(LegaInfoObject currentReadLega, Constants.TipologiaImport tipologiaImport)
        {
            if(tipologiaImport == Constants.TipologiaImport.excel_to_database)
            {
                CheckLega_Normativa_ExcelOrigin(currentReadLega);
            }
            else if(tipologiaImport == Constants.TipologiaImport.xml_to_database)
            {
                CheckLega_Normativa_XMLOrigin(currentReadLega);
            }
        }


        /// <summary>
        /// Procedura descritta nel metodo precedente avviata nel caso in cui sia in presenza di una origine di tipo excel
        /// </summary>
        /// <param name="currentReadLega"></param>
        private static void CheckLega_Normativa_ExcelOrigin(LegaInfoObject currentReadLega)
        {

            // recupero della stringa relativa al nome della lega corrispondente 
            string infoNomeLegaFromExcel = currentReadLega.Lega_ExcelRow.GetValue("MATERIALE");

            // recupero della stringa relativa al nome della normativa lega corrispondente 
            string infoNormativaFromExcel = currentReadLega.Lega_ExcelRow.GetValue("TIPO");
            
            // verifica della presenza della Lega 
            LegheDB correspondingLega = LegheDB.Where(x => x.Nome == infoNomeLegaFromExcel).ToList().FirstOrDefault();
            NormativeDB correspondingNormativa = NormativeDB.Where(x => x.Normativa == infoNormativaFromExcel).ToList().FirstOrDefault();
            

            // non è presente ne l'informazione per la lega ne quella per la normativa, dovrò compiere l'inserimento di entrambe le righe 
            if(correspondingLega == null && correspondingNormativa == null)
            {
                // TODO : inserire messaggistica 
            }
            // inserisco solo l'informazione per la normativa 
            else if(correspondingLega != null && correspondingNormativa == null)
            {
                // TODO : inserire messaggistica
            }
            else if(correspondingLega != null && correspondingNormativa != null)
            {
                // TODO : inserire messaggistica 
            }
            else if(correspondingLega == null && correspondingNormativa != null)
            {
                // TODO : inserire messaggistica 
            }    

        }


        /// <summary>
        /// Procedura descritta nel metodo precedente avviata nel caso in cui sia in presenza di una origine di tipo xml
        /// </summary>
        /// <param name="currentReadLega"></param>
        private static void CheckLega_Normativa_XMLOrigin(LegaInfoObject currentReadLega)
        {

        }

        #endregion


        #region PROPRIETA PUBBLICHE (PROPRIETA RELATIVE AGLI OGGETTI DB) 

        /// <summary>
        /// Lista di tutte le normative presenti nel database in uso
        /// </summary>
        public static List<NormativeDB> NormativeDB { get; set; }


        /// <summary>
        /// Lista di tutte le leghe presenti nel database in uso
        /// </summary>
        public static List<LegheDB> LegheDB { get; set; }


        /// <summary>
        /// Lista di tutte le categorie leghe nel database in uso
        /// </summary>
        public static List<Categorie_LegheDB> CategorieLegheDB { get; set; }


        /// <summary>
        /// Lista di tutte le basi nel database in uso
        /// </summary>
        public static List<BaseDB> BasiDB { get; set; }

        #endregion
    }
}
