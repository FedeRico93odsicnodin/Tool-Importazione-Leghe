﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// OGGETTO DI TRANSIZIONE per le proprieta lette rispetto a un quadrante di concentrazioni 
    /// contiene tutti gli elementi per poter individuare la sorgente e tutti gli elementi che poi verranno configurati per 
    /// la rispettiva scrittura all'interno della destinazione 
    /// </summary>
    public class MaterialConcentrationsObject
    {

        #region COSTRUTTORE 

        /// <summary>
        /// By default le proprieta relative al recupero alla validazione e alla possibile persistenza sono a false 
        /// per l'oggetto corrente 
        /// </summary>
        public MaterialConcentrationsObject()
        {
            Step1_Recupero = false;
            Step2_Validazione_SameSheet = false;
            Step3_Persistenza = false;
        }

        #endregion

        /// <summary>
        /// Quadrante di riferimento sul quale vengono lette le informazioni inerenti 
        /// il materiale corrente con le relative concentrazioni
        /// </summary>
        public ExcelConcQuadrant ExcelQuadrantReference { get; set; }


        /// <summary>
        /// Nome del materiale corrispondente 
        /// </summary>
        public string MaterialName { get; set; }


        /// <summary>
        /// Nome per la lega corrispondente sul quale viene preso il materiale 
        /// </summary>
        public string AlloyName { get; set; }


        /// <summary>
        /// Tutte le righe lette per il quadrante di concentrazioni corrente
        /// </summary>
        public List<RowFoglioExcel> ReadConcentrationsRows { get; set; }


        /// <summary>
        /// Impostazione delle rispettive righe di concentrazione per la valorizzazione 
        /// vera e propria per un database di origine / di destinazione delle informazioni
        /// </summary>
        public List<ConcLegaDB> ConcentrationsDB { get; set; }


        #region STEPS

        /// <summary>
        /// Indica se l'informazione corrente per il quadrante da inserire è stata recuperata correttamente 
        /// </summary>
        public bool Step1_Recupero { get; set; }


        /// <summary>
        /// Indica se l'informazione corrente per il foglio è stata validata correttamente in base alle informazioni contenute nello stesso foglio 
        /// </summary>
        public bool Step2_Validazione_SameSheet { get; set; }
        

        /// <summary>
        /// Indica se l'informazione corrente può essere persistita in base alle informazioni di lega contenute in questo foglio o gia presenti 
        /// all'interno della sorgente (che quindi validano in se il set di concentrazioni che si sta inserendo)
        /// </summary>
        public bool Step3_Persistenza { get; set; }

        #endregion
    }
}
