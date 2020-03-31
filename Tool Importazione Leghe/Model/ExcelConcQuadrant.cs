using Tool_Importazione_Leghe.ExcelServices;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto di supporto alla lettura dell'header delle concentrazioni per individuare dove
    /// si trovano i diversi elementi utili a distinguere il materiale e le diverse concentrazioni
    /// ad esso associato
    /// </summary>
    public class ExcelConcQuadrant
    {
        #region ENUMERAZIONE QUADRANTE 

        /// <summary>
        /// Mi serve per identificare progressivamente i quadranti individuati
        /// </summary>
        public int EnumerationQuadrant { get; set; }

        #endregion


        #region IDENTIFICATORI PER IL TITOLO MATERIALE

        /// <summary>
        /// Idenficatore per la riga del titolo materiale all'interno del quadrante excel
        /// </summary>
        public int Title_Row { get; set; }


        /// <summary>
        /// Identificatore per la colonna del titolo materiale all'interno del quadrante excel
        /// </summary>
        public int Title_Col { get; set; }

        #endregion


        #region IDENTIFICATORI HEADERS

        /// <summary>
        /// Identificatore per la riga dell'header materiale all'interno del quadrante excel
        /// </summary>
        public int Head_Row { get; set; }
        

        /// <summary>
        /// Identificazione della colonna di partenza di header
        /// NB: tutte le colonne relative all'header sono identificate partendo da questo valore
        /// </summary>
        public int Head_Col{ get; set; }
        
        #endregion


        #region DELIMITATORI ROWS CONCENTRAZIONI

        /// <summary>
        /// Identificazione della riga di partenza dal quale individuare le concentrazioni per il materiale corrente
        /// </summary>
        public int Conc_Row_Start { get; set; }


        /// <summary>
        /// Indentificazione della riga di fine per il quale viene individuata la fine di lettura delle concentrazioni per il materiale corrente
        /// </summary>
        public int Conc_Row_End { get; set; }

        #endregion


        #region CALCOLO DEI PARAMETRI INDISPENSABILI AL RICONOSCIMENTO OGGETTO EXCEL CORRENTE

        /// <summary>
        /// Calcolo in automatico la posizione massima per la nuova colonna in base agli headers sui quali viene 
        /// fatta la lettura corrente
        /// </summary>
        public int Get_Max_Col_Quadrante
        {
            get
            {
                return this.Head_Col + (ExcelMarkers.GetAllColumnHeadersForConcentrationsInfoSheet().Count - 1);
            }
        }

        #endregion

    }
}
