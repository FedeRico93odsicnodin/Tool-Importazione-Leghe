using System.Collections.Generic;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto che mi mappa le proprietà principale del foglio excel corrente per poterne 
    /// poi fare il riconoscimento e la lettura successiva
    /// </summary>
    public class ExcelSheetWithUtilInfo
    {
        #region COSTRUTTORE

        /// <summary>
        /// By default le informazioni relative alla lettura / validazione e possibile inserimento all'interno della sorgente 
        /// vengono settate a false per il foglio corrente 
        /// </summary>
        public ExcelSheetWithUtilInfo()
        {
            LetturaInformazioniCorretto = false;
            ValidazioneInformazioniCorretto = false;
            PersistenzaInformazioniPossibile = false;
        }

        #endregion
        

        #region INFORMAZIONI DI CARATTERE GENERALE FOGLIO

        /// <summary>
        /// Nome per il foglio excel in analisi corrente
        /// </summary>
        public string SheetName { get; set; }


        /// <summary>
        /// Indicazione del file corrente nel quale si trova il foglio excel
        /// </summary>
        public string ExcelFile { get; set; }

        
        /// <summary>
        /// Ottenimento della posizione per il foglio excel corrente
        /// </summary>
        public int PositionInExcelFile { get; set; }


        /// <summary>
        /// Ottenimento della tipologia riconosciuta per il foglio excel corrente 
        /// </summary>
        public Constants.TipologiaFoglioExcel TipologiaRiconosciuta { get; set; }
        
        #endregion


        #region HEADERS E QUADRANTI PER IL FOGLIO EXCEL

        /// <summary>
        /// Questa proprieta è valida se il foglio excel corrente viene letto come foglio excel di carattere 
        /// generale per la lega corrente, le informazioni vengono quindi valorizzate successivamente 
        /// </summary>
        public List<HeadersInfoLega_Excel> GeneralInfo_Lega { get; set; }


        /// <summary>
        /// Quadranti di concentrazioni iniziali per il foglio correntemente in analisi.
        /// Questa informazione viene valorizzata solamente nel caso in cui il foglio sia effettivamente riconosciuto
        /// come un foglio contenente informazioni di concentrazioni per determinati materiali
        /// </summary>
        public List<ExcelConcQuadrant> Concentrations_Quadrants { get; set; }

        #endregion


        #region INFORMAZIONI LETTE EFFETTIVAMETE SU FOGLIO 

        /// <summary>
        /// Tiene traccia di tutti i valori letti per la persistenza delle informazioni di lega al primo step
        /// tiene traccia di tutti i valori che vengono validati per la persistenza corretta al secondo step
        /// </summary>
        public List<LegaInfoObject> InfoLegheFromThisExcel { get; set; }


        /// <summary>
        /// Tiene traccia dei valori che sono stati letti analizzando i quadranti delle concentrazioni al primo step 
        /// Tiene traccia di tutti i valori che vengono validati per la persistenza database al secondo step 
        /// </summary>
        public List<MaterialConcentrationsObject> InfoConcentrationsFromThisExcel { get; set; }

        #endregion


        #region PASSAGGIO VALIDAZIONI 

        /// <summary>
        /// Mi dice se ho completato correttamente la lettura delle informazioni per il foglio excel corrente 
        /// sia che si tratti di un foglio di informazioni di lega sia che si tratti di un foglio relativo alle concentrazioni
        /// </summary>
        public bool LetturaInformazioniCorretto { get; set; }


        /// <summary>
        /// Mi dice se ho completato correttamente la validazione per le informazioni contenute all'interno del foglio excel corrente 
        /// sia che si tratti di un foglio per le informazioni di lega sia di uno per le concentrazioni
        /// </summary>
        public bool ValidazioneInformazioniCorretto { get; set; }


        /// <summary>
        /// Mi dice in base ai valori precedenti se sarà possibile la persistenza delle informazioni per il foglio excel corrente 
        /// in base all'analisi relativa alle informazioni acquisite e validate agli steps precedenti e a che cosa è gia presente 
        /// di utile all'interno della destinazione 
        /// </summary>
        public bool PersistenzaInformazioniPossibile { get; set; }

        #endregion
    }
}
