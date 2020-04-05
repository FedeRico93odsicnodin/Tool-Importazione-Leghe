using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.ExcelServices;

namespace Tool_Importazione_Leghe.Model
{
    /// <summary>
    /// OGGETTO CONTENTENTE le righe in lettura per le leghe correnti dal foglio excel di partenza
    /// questo oggetto è valorizzato con tutti i possibili valori contenuti sia per la lista delle proprieta obbligatorie che per la lista delle proprieta
    /// addizionali in merito all'inserimento di una delle righe corrispondenti
    /// </summary>
    public class RowFoglioExcel
    {
        #region ATTRIBUTI PRIVATI
        
        /// <summary>
        /// Dizionario delle proprietà obbligatorie per la riga corrente 
        /// </summary>
        private Dictionary<string, string> _mandatoryProperties_Info;


        /// <summary>
        /// Dizionario delle proprietà addizionali per la riga corrente 
        /// </summary>
        private Dictionary<string, string> _additionalProperties_Info;


        /// <summary>
        /// Questo valore e preso rispetto ai valori per le proprieta obbligatorie del foglio excel 
        /// e per le informazioni a carattere generale preso direttamente dalle costanti
        /// </summary>
        private List<string> _currentMandatoryProperties;


        /// <summary>
        /// Questo valore è preso rispetto ai valori addizionali, sempre contenuti nelle costanti e 
        /// per le informazioni di carattere generale di lega
        /// </summary>
        private List<string> _currentAdditionalProperties;

        #endregion


        #region COSTRUTTORE: INIZIALIZZAZIONE DELLE LISTE DELLE PROPRIETA OBBLIGATORIE E ADDIZIONALI CON I VALORI PRESENTI NELLE COSTANTI

        /// <summary>
        /// Inizializzazione delle 2 liste con i valori di chiave presenti all'interno delle costanti
        /// </summary>
        public RowFoglioExcel()
        {
            // 1. inizializzazione dei valori per le proprieta obbligatorie
            FillMandatoryProperties_InfoLega();

            // 2. inizializzazione dei valori per le proprieta opzionali
            FillAdditionalProperties_InfoLega();
        }

        #endregion


        #region METODI PRIVATI

        /////////////////////////////////////// INIZIALIZZAZIONE DELLE LISTE PROPRIETA NEL CASO IN CUI STO LEGGENDO INFORMAZIONI GENERALI DI LEGA ///////////////////////////////////////

        /// <summary>
        /// Al momento dell'inizializzazione mi permette di andare a inserire una entry nel dizionario delle proprieta obbligatorie
        /// per ogni prorpieta obbligatoria effettivamente contenuta nella definizione data nelle costanti
        /// </summary>
        private void FillMandatoryProperties_InfoLega()
        {
            _mandatoryProperties_Info = new Dictionary<string, string>();


            _currentMandatoryProperties = ExcelMarkers.GetAllColumnHeadersForGeneralInfoSheet();

            foreach(string currentMandatoryProperty in _currentMandatoryProperties)
            {
                // inizializzazione con il valore a stringa vuota 
                _mandatoryProperties_Info.Add(currentMandatoryProperty, String.Empty);
            }
        }


        /// <summary>
        /// Analogo del metodo creato sopra ma per le proprieta opzionali relativi al materiale corrente 
        /// </summary>
        private void FillAdditionalProperties_InfoLega()
        {
            _additionalProperties_Info = new Dictionary<string, string>();

            _currentAdditionalProperties = ExcelMarkers.GetAdditionalPropertiesGeneralInfoSheet();

            foreach(string currentAdditionalProperty in _currentAdditionalProperties)
            {
                // inizializzazione con il valore a stringa vuota 
                _additionalProperties_Info.Add(currentAdditionalProperty, String.Empty);
            }
        }

        /////////////////////////////////////// INIZIALIZZAZIONE DELLE LISTE PROPRIETA NEL CASO IN CUI STIA LEGGENDO LE CONCENTRAZIONI ///////////////////////////////////////

        /// <summary>
        /// Inizializzazione della lista delle proprieta che devo obbligatoriamente leggere per poter inserire correttamente le concentrazioni
        /// </summary>
        private void FillMandatoryProperties_Concentrations()
        {
            // TODO: implementazione delle liste e della lettura "DINAMICA" delle informazioni obbligatorie per le concentrazioni
        }
        

        /// <summary>
        /// Inizializzazione della lista delle proprieta che posso addizionalmente leggere per poter inserire correttamente le concentrazioni
        /// </summary>
        private void FillAdditionalProperties_Concentrations()
        {
            // TODO: refactoring come sopra 
        }

        #endregion


        #region METODI PUBBLICI

        /// <summary>
        /// Permette di ottenere il valore della proprieta passata in input
        /// </summary>
        /// <param name="currentProperty"></param>
        /// <returns></returns>
        public string GetValue(string currentProperty)
        {
            // controllo che la chiave passata in input sia nelle proprieta obbligatorie
            if (_mandatoryProperties_Info.ContainsKey(currentProperty))
                return _mandatoryProperties_Info[currentProperty];

            // controllo che la chiave passata in input sia nelle proprieta opzionali
            if (_additionalProperties_Info.ContainsKey(currentProperty))
                return _additionalProperties_Info[currentProperty];


            // errore: ritorno stringa vuota 
            return String.Empty;
        }


        /// <summary>
        /// Permette di settare il valore della proprieta passati in input
        /// </summary>
        /// <param name="currentProperty"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        public void SetValue(string currentProperty, string currentValue)
        {
            // provo con le proprieta obbligatorie
            if (_mandatoryProperties_Info.ContainsKey(currentProperty))
            {
                _mandatoryProperties_Info[currentProperty] = currentValue;
                return;
            }
                
            // provo con le proprieta opzionali
            if(_additionalProperties_Info.ContainsKey(currentProperty))
            {
                _additionalProperties_Info[currentProperty] = currentValue;
                return;
            }
        }


        /// <summary>
        /// Mi permette di verificare che la determinata proprieta passata in input contiene effettivamente 
        /// ubn valore 
        /// </summary>
        /// <returns></returns>
        public bool ContainsValue(string currentProperty)
        {
            if (_mandatoryProperties_Info.ContainsKey(currentProperty))
                if (_mandatoryProperties_Info[currentProperty] != String.Empty)
                    return true;

            if (_additionalProperties_Info.ContainsKey(currentProperty))
                if (_additionalProperties_Info[currentProperty] != String.Empty)
                    return true;


            return false;
        }


        /// <summary>
        /// Mi dice se l'oggetto corrente è empty, in questo caso posso eliminarlo dalla lista di tutti gli oggetti 
        /// appena inseriti per il foglio excel e le proprieta di headers correnti
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            bool isEmptyMandatory = false;
            bool isEmptyAdditional = false;

            if (_mandatoryProperties_Info.Where(x => x.Value != String.Empty).Select(x => x.Key).ToList().Count() == 0)
                isEmptyMandatory = true;

            if (_additionalProperties_Info.Where(x => x.Value != String.Empty).Select(x => x.Key).ToList().Count() == 0)
                isEmptyAdditional = true;

            return (isEmptyMandatory || isEmptyAdditional);
        }


        /// <summary>
        /// Informazione di riga corrente per il foglio excel
        /// E' sufficiente per il riconoscimento nel foglio excel in quanto le informazioni di colonna potrebbero essere calcolate 
        /// facendo un match per il rispettivo header letto
        /// </summary>
        public int Excel_CurrentRow { get; set; }


        /// <summary>
        /// Indicazione dalla validazione di riga se si tratta di una informazione valida, che puo essere inserita a database da 
        /// una prima analisi per il foglio excel corrente 
        /// </summary>
        public bool IsValidInfo_STEP1 { get; set; }

        #endregion
    }
}
