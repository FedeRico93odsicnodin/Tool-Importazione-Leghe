namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// Oggetto di supporto alla lettura dell'header delle concentrazioni per individuare dove
    /// si trovano i diversi elementi utili a distinguere il materiale e le diverse concentrazioni
    /// ad esso associato
    /// </summary>
    public class ExcelConcQuadrant
    {
        #region POSIZIONI DI TITOLO

        /// <summary>
        /// Mappatura index colonna per il titolo associato al materiale corrente
        /// </summary>
        public int TitlePos_X { get; set; }


        /// <summary>
        /// Mappatura index riga per il titolo associato al materiale corrente
        /// </summary>
        public int TitlePos_Y { get; set; }

        #endregion


        #region POSIZIONE DI HEADER INIZIALE 

        /// <summary>
        /// Mappatura index INIZIALE di colonna per l'header concentrazioni per il materiale corrente
        /// </summary>
        public int HeaderPos_Start_X { get; set; }


        /// <summary>
        /// Mappatura index INIZIALE di riga per l'header concentrazioni per il materiale corrente
        /// </summary>
        public int HeaderPos_Start_Y { get; set; }


        /// <summary>
        /// Mappatura index di riga FINALE per l'header concentrazioni per il materiale corrente
        /// </summary>
        public int HeaderPos_End_X { get; set; }


        /// <summary>
        /// Mappatura index di colonna FINALE per l'header concentrazioni per il materiale corrente
        /// </summary>
        public int HeaderPos_End_Y { get; set; }

        #endregion


        #region ZONA RELATIVA ALLE CONCENTRAZIONI

        /// <summary>
        /// Mappatura indice di riga SINISTRO DI PARTENZA per la tabella relativa alle concentrazioni
        /// </summary>
        public int Conc_Start_Left_X { get; set; }
        

        /// <summary>
        /// Mappatura indice di colonna SINISTRO DI PARTENZA per la tabella relativa alle concentrazioni
        /// </summary>
        public int Conc_Start_Left_Y { get; set; }


        /// <summary>
        /// Mappatura indice di riga DESTRO DI PARTENZA per la tabella relativa alle concentrazioni
        /// </summary>
        public int Conc_Start_Right_X { get; set; }


        /// <summary>
        /// Mappatura indice di colonna DESTRO DI PARTENZA per la tabella relativa alle concentrazioni
        /// </summary>
        public int Conc_Start_Right_Y { get; set; }


        /// <summary>
        /// Mappatura indice di riga DESTRO DI FINE per la tabella relativa alle concentrazioni
        /// </summary>
        public int Conc_End_Left_X { get; set; }


        /// <summary>
        /// Mappatura indice di colonna DESTRO DI FINE per la tabella relativa alle concentrazioni
        /// </summary>
        public int Conc_End_Left_Y { get; set; }


        /// <summary>
        /// Mappatura indice di riga DESTRO DI FINE per la tabella relativa alle concentrazioni
        /// </summary>
        public int Conc_End_Right_X { get; set; }


        /// <summary>
        /// Mappatura indice di riga DESTRO DI FINE per la tabella relativa alle concentrazioni
        /// </summary>
        public int Conc_End_Right_Y { get; set; }

        #endregion
    }
}
