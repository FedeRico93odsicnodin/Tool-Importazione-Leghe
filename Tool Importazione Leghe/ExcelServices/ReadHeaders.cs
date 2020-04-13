using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.ExcelServices
{
    /// <summary>
    /// Questa classe mi permette di leggere gli headers relativi ai diversi fogli excel 
    /// grazie a questo metodo riesco a distinguere la presenza di un determinato header per un certo foglio 
    /// e quindi capire come procedere nella lettura e per quale caso siamo (se il tipo di sheet 1 o il tipo di sheet 2)
    /// </summary>
    public class ReadHeaders
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Tiene traccia dell'indice di colonna corrente per l'eventuale gestione dell'eccezione
        /// </summary>
        private int _tracciaCurrentCol = 1;


        /// <summary>
        /// Tiene traccia dell'indice di riga corrente per l'eventuale gestione dell'eccezione
        /// </summary>
        private int _tracciaCurrentRow = 1;


        /// <summary>
        /// Tiene traccia del marker corrente per l'eventuale gestione dell'eccezione
        /// </summary>
        private string _currentMarker = String.Empty;


        /// <summary>
        /// Indica il limite sul numero di righe che posso leggere per trovare l'informazione relativa all'header
        /// </summary>
        private const int LIMITROW = 5;


        /// <summary>
        /// indica il limite sul numero di colonne che posso leggere per trovare l'informazione relativa all'header
        /// </summary>
        private const int LIMITCOL = 5;


        /// <summary>
        /// Indica il limite sul numero di righe che posso leggere a partire dall'individuazione dell'header per trovare il primo valore 
        /// utile per l'informazione sul foglio excel corrente
        /// </summary>
        private const int LIMITINFOROW = 10;


        /// <summary>
        /// Inizializzazione con la dimensione di riga massima per il foglio excel corrente
        /// </summary>
        private int MaxExcelSheetPos_row = 0;


        /// <summary>
        /// Inizializzazione con la dimensione di colonna massima per il foglio excle corrente
        /// </summary>
        private int MaxExcelSheetPos_col = 0;


        /// <summary>
        /// Permette di mappare tutte le informazioni relative alle posizione dalle quali iniziare a leggere i diversi oggetti 
        /// per le concentrazioni
        /// </summary>
        private List<ExcelConcQuadrant> _currentPositionsConcentrations;


        /// <summary>
        /// Indice che mi dice quale sarà il limite nella lettura delle concentrazioni rispetto alla colonna 
        /// se spostandomi in orizzontale sulle colonne non trovo piu quadranti utili dopo questo limite allora fermo l'iterazione
        /// </summary>
        private const int LIMITCOL_LETTURACONCENTRAZIONI = 15;


        /// <summary>
        /// Indice che mi dice quale sarà il limite nella lettura delle concentraizoni rispetto alla riga
        /// se spostandomi in verticale sulle righe non trovo piu quadranti utili dopo questo limite allora fermo l'iterazione
        /// </summary>
        private const int LIMITROW_LETTURACONCENTRAZIONI = 15;


        /// <summary>
        /// Questa lista contiene tutti gli headers necessari al ricoscimento del quadrante di header per le diverse concentrazioni
        /// del materiale in questione
        /// </summary>
        private List<string> _concentrationsMandatoryInfo;


        /// <summary>
        /// Constante che mi dice quale deve essere la spaziatura massima tra gli elementi contenuti all'interno di un quadrante delle concentrazioni
        /// e per quanto riguarda la linea (le colonne devono essere invece tutte attaccate)
        /// </summary>
        private const int LIMITBETWEENCONCENTRATIONSROWS = 5;


        /// <summary>
        /// Questa lista contiene tutti gli headers che sono obbligatori quando si legge le informazioni di carattere generale per una determinata lega
        /// se anche solo una di queste proprietà manca all'interno del foglio excel questo non viene riconosciuto come contenitore delle informazioni generali 
        /// per la lega in lettura
        /// </summary>
        private List<string> _mandatoryHeadersForGeneralInfo;


        /// <summary>
        /// Questa lista contiene tutte le proprietà addizionali che è possibile comunque leggere all'interno degli header per le informazioni a carattere generale 
        /// su una certa lega 
        /// </summary>
        private List<string> _additionalHeadersForGeneralInfo;

        
        /// <summary>
        /// Porprieta addizionali per la lettura degli headers relativi alle concentrazioni
        /// </summary>
        public List<string> _concentrationsAdditionalInfo;


        /// <summary>
        /// Permette di dare una enumerazione crescente a tutti i quadranti che vengono via via letti all'interno di un foglio per le concentrazioni
        /// </summary>
        private int _currentSheetQuadrantsEnumerator = 1;
        
        #endregion


        #region COSTRUTTORE

        /// <summary>
        /// Inizializzazione della lista di header per le concentrazioni
        /// </summary>
        public ReadHeaders()
        {
            // inizializzazione degli headers per le informazioni a carattere generale obbligatorie individuabili su 
            // un foglio excel per le informazioni generali di lega 
            _mandatoryHeadersForGeneralInfo = ExcelMarkers.GetAllColumnHeadersForGeneralInfoSheet();

            // inizializzazione della lista con tutte le proprieta addizionali inerenti una determinata lega 
            _additionalHeadersForGeneralInfo = ExcelMarkers.GetAdditionalPropertiesGeneralInfoSheet();


            // inizializzazione della lsita contenente gli headers per le concentrazioni in lettura 
            _concentrationsMandatoryInfo = ExcelMarkers.GetAllMandatoryPropertiesForConcentrations();

            // inizializzazione della lista contenente gli headers addizionali per la lettura delle concentrazioni
            _concentrationsAdditionalInfo = ExcelMarkers.GetAllColumnAdditionalHeadersForConcentrations();


            
        }

        #endregion


        #region LETTURA E RICONOSCIMENTO FOGLIO COME FOGLIO CON INFORMAZIONI DI CARATTERE GENERALE PER LA LEGA 
        
        /// <summary>
        /// Remake del metodo per la lettura corretta delle informazioni contenute all'interno del foglio delle informazioni primarie di lega
        /// Viene passato in input il foglio in analisi corrente e se corrisponde alla lettura delle informazioni relative alla lega corrente viene restituito vero
        /// e la lista degli headers da cui iniziare effettivamente la lettura 
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="generalInfoHeaders"></param>
        /// <returns></returns>
        internal bool ReadInformation_GeneralInfoLega(ref ExcelWorksheet currentExcelSheet, out List<HeadersInfoLega_Excel> generalInfoHeaders)
        {
            // azzeramento dei parametri relativi alla lettura della colonna e della riga correnti
            _tracciaCurrentCol = 1;
            _tracciaCurrentRow = 1;


            // inizializzazione delle 2 liste per la lettura degli headers correnti
            List<HeadersInfoLega_Excel> readMandatoryInfo = new List<HeadersInfoLega_Excel>();
            List<HeadersInfoLega_Excel> readAdditionalInfo = new List<HeadersInfoLega_Excel>();
            
            // indicazione di lettura complessiva di tutte le proprieta 
            bool hoLettoTutteLeProprieta = false;


            do
            {
                do
                {
                    // iterazione su riga finche non trovo una informazione utile alla lettura effettiva 
                    if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value == null)
                    {
                        _tracciaCurrentRow++;
                        continue;
                    }
                    
                    
                    // inizio la lettura delle colonne 
                    do
                    {
                        // iterazione su colonne finche non trovo una informazione utile alla lettura effettiva 
                        if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value == null)
                        {
                            _tracciaCurrentCol++;
                            continue;
                        }

                        // trovo una proprieta obbligatoria
                        if (RecognizeMandatoryInfoPropertyPresence(readMandatoryInfo, currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value.ToString(), out readMandatoryInfo))
                        {
                            // segnalazione di trovata proprieta obbligatoria per il foglio excel corrente 
                            //ServiceLocator.GetLoggingService.GetLoggerExcel.TrovataInformazioneObbligatoriaLetturaInformazioniGenerali(currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value.ToString(), _tracciaCurrentRow, _tracciaCurrentCol);

                            _tracciaCurrentCol++;

                            continue;
                        }

                        // trovo una proprieta opzionale
                        if(RecognizeAdditionalInfoPropertiesPresence(readAdditionalInfo, currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value.ToString(), out readAdditionalInfo))
                        {
                            // segnalazione di trovata proprieta addizionale per il foglio excel corrente 
                            //ServiceLocator.GetLoggingService.GetLoggerExcel.TrovataInformazioneAddizionaleLetturaInformazioniGenerali(currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value.ToString(), _tracciaCurrentRow, _tracciaCurrentCol);

                            _tracciaCurrentCol++;

                            continue;
                        }
                        else 
                            _tracciaCurrentCol++;

                    }
                    // mi fermo se non rispetto correttamente i vincoli imposti sullo spazio
                    while (_tracciaCurrentCol <= currentExcelSheet.Dimension.End.Column);

                    // indico che ho finito con il tentativo di lettura per le proprieta correnti - nel caso in cui la lista delle proprieta obbligatorie sia piena
                    if(readMandatoryInfo.Count() > 0)
                        hoLettoTutteLeProprieta = true;
                    


                    // se sono riuscito a leggere tutte le proprieta posso uscire dal ciclo
                    if (hoLettoTutteLeProprieta)
                        break;


                    // se sono arrivato qui è perché devo incrementare indice di riga
                    _tracciaCurrentRow++;
                }
                while (_tracciaCurrentRow <= currentExcelSheet.Dimension.End.Row);


                // se leggo tutte le proprieta esco dal ciclo
                if (hoLettoTutteLeProprieta)
                    break;

                // se sono arrivato fino a qui è perché devo incrementare indice di colonna 
                _tracciaCurrentRow = 1;
                _tracciaCurrentCol++;

            }
            while (_tracciaCurrentCol <= currentExcelSheet.Dimension.End.Column);

            // segnalazione di fine lettura per tutte le informazioni di carattere generale sulla lega corrente 
            //ServiceLocator.GetLoggingService.GetLoggerExcel.FineProcessamentoGeneralInfoPerFoglioExcel(currentExcelSheet.Name);

            
            // ho letto almeno tutte le proprieta obbligatorie
            if(readMandatoryInfo.Count() == _mandatoryHeadersForGeneralInfo.Count())
            {
                // se trovo un valore maggiore di 0 per le proprieta opzionali eventualmente lette allora unisco le 2 liste e torno true
                if (readAdditionalInfo.Count() > 0)
                    generalInfoHeaders = readMandatoryInfo.Union(readAdditionalInfo).ToList();
                else
                    generalInfoHeaders = readMandatoryInfo;

                return true;
            }
            
            // non ho trovato tutte le proprieta obbligatorie per il foglio corrispondente 
            generalInfoHeaders = null;

            return false;
        }


        /// <summary>
        /// Lettura di un certo foglio excel sul quale si riconoscono le posizioni utili per la lettura delle concentrazioni associate
        /// ai diversi materiali, il risultato è avere la validità del foglio come di un foglio relativo alle concentrazioni e avere 
        /// un set di posizioni dalle quali iniziare a leggerle
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentTipologiaFoglio"></param>
        /// <param name="detectedMaterials"></param>
        /// <returns></returns>
        internal bool ReadHeaders_Concentrazioni(ref ExcelWorksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaFoglio, out List<ExcelConcQuadrant> detectedMaterials)
        {
            // reset attributi di lettura corrente
            _tracciaCurrentCol = 1;
            _tracciaCurrentRow = 1;
            _currentMarker = String.Empty;

            // inizializzazione con le posizioni di fine lettura per l'indice di riga e quello di colonna 
            MaxExcelSheetPos_row = currentExcelSheet.Dimension.End.Row;
            MaxExcelSheetPos_col = currentExcelSheet.Dimension.End.Column;

            // reset della lista sulla quale si andranno a inserire le eventuali posizioni utili trovate per la lettura delle concentrazioni
            detectedMaterials = new List<ExcelConcQuadrant>();
            _currentPositionsConcentrations = new List<ExcelConcQuadrant>();


            // reset enumeratore quadranti concentrazioni
            _currentSheetQuadrantsEnumerator = 1;
            

            // vado a riconoscere la prima posizione utile per la lettura delle concentrazioni
            RecognizeConcentrationsPosition(ref currentExcelSheet, Utils.Constants.TipologiaFoglioExcel.Informazioni_Concentrazione);
            
            

            if (_currentPositionsConcentrations == null)
            {
                ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoNessunQuadranteConcentrazioniPerFoglio(currentExcelSheet.Name);
                return false;
            }

            detectedMaterials = _currentPositionsConcentrations;

            return true;
            
        }
        
        #endregion

        


        /// <summary>
        /// Permette il riconoscimento di una proprieta obbligatoria per quanto riguarda il foglio contenente le informazioni 
        /// generali per la lega corrente. Se l'elemento corrente è riconosciuto come presente nella lista relativa a tutte le proprietà obbligatorie
        /// e non era già stato letto precendentemente allora viene ritornato true altrimenti viene ritornato false
        /// </summary>
        /// <param name="currentRecognizedProperties"></param>
        /// <param name="currentProperty"></param>
        /// <param name="newRecognizedProperties"></param>
        /// <returns></returns>
        private bool RecognizeMandatoryInfoPropertyPresence(List<HeadersInfoLega_Excel> currentRecognizedProperties, string currentProperty, out List<HeadersInfoLega_Excel> newRecognizedProperties)
        {


            if (currentRecognizedProperties.Where(x => x.NomeProprietà == currentProperty).ToList().Count() > 0)
            {
                // ho gia letto questa informazione per gli headers correnti
                //ServiceLocator.GetLoggingService.GetLoggerExcel.HoGiaTrovatoInformazioneACarattereGenerale(currentProperty);

                newRecognizedProperties = currentRecognizedProperties;

                return false;
            }


            if(!_mandatoryHeadersForGeneralInfo.Contains(currentProperty.ToUpper()))
            {
                // l'informazione non è contenuta nelle definizioni delle proprieta obbligatorie per la lettura delle informazioni generali per la lega corrente 
                //ServiceLocator.GetLoggingService.GetLoggerExcel.InformazioneGeneraleNonContenutaNelleDefinizioniObbligatorie(currentProperty);

                newRecognizedProperties = currentRecognizedProperties;

                return false;
            }


            // se passo tutte le altre condizioni significa che la proprietà rispetta i vincoli dati di informazioni generali obbligatorie
            // quindi l'aggiungo a tutte le proprieta lette per la lega corrente 

            // inizializzazione per l'elemento corrente
            HeadersInfoLega_Excel readProperty = new HeadersInfoLega_Excel()
            {
                NomeProprietà = currentProperty,
                Starting_Col = _tracciaCurrentCol,
                Starting_Row = _tracciaCurrentRow,
            };

            currentRecognizedProperties.Add(readProperty);

            newRecognizedProperties = currentRecognizedProperties;
            
            return true;
                
        }


        /// <summary>
        /// Permette di riconoscere la presenza di eventuali altre proprietà addizionali contenute nel foglio corrente e relativo alle informazioni generali
        /// per la lega in analisi. Se anche per questa lista di proprietà non c'è un vero e proprio riconoscimento effettivo allora il foglio non 
        /// puo essere riconosciuto come contenitore di informazioni generali per la determinata lega 
        /// </summary>
        /// <param name="currentRecognizedProperties"></param>
        /// <param name="currentProperty"></param>
        /// <param name="newRecognizedProperties"></param>
        /// <returns></returns>
        private bool RecognizeAdditionalInfoPropertiesPresence(List<HeadersInfoLega_Excel> currentRecognizedProperties, string currentProperty, out List<HeadersInfoLega_Excel> newRecognizedProperties)
        {
            // TODO: implementazione della dinamica di riconscimento delle proprieta opzionali per il caso di lettura delle informazioni generali in lettura per il foglio corrente
            newRecognizedProperties = currentRecognizedProperties;


            if(currentRecognizedProperties.Where(x => x.NomeProprietà == currentProperty).ToList().Count() > 0)
            {
                // ho gia letto questa informazione per gli headers correnti
                ServiceLocator.GetLoggingService.GetLoggerExcel.HoGiaTrovatoInformazioneACarattereGenerale(currentProperty);

                newRecognizedProperties = currentRecognizedProperties;

                return false;
            }


            if(!_additionalHeadersForGeneralInfo.Contains(currentProperty.ToUpper()))
            {
                // l'informazione non è contenuta nelle definizioni delle proprieta addizionali per la lettura delle informazioni generali per la lega corrente 
                //ServiceLocator.GetLoggingService.GetLoggerExcel.InformazioneGeneraleNonContenutaNelleDefinizioniAddizionali(currentProperty);

                newRecognizedProperties = currentRecognizedProperties;

                return false;
            }

            // se passo tutte le altre condizioni significa che la proprietà rispetta i vincoli dati di informazioni generali obbligatorie
            // quindi l'aggiungo a tutte le proprieta lette per la lega corrente 

            // inizializzazione per l'elemento corrente
            HeadersInfoLega_Excel readProperty = new HeadersInfoLega_Excel()
            {
                NomeProprietà = currentProperty,
                Starting_Col = _tracciaCurrentCol,
                Starting_Row = _tracciaCurrentRow,
            };

            currentRecognizedProperties.Add(readProperty);

            newRecognizedProperties = currentRecognizedProperties;

            return true;
        }



        #region METODI PER IL RICONOSCIMENTO DEL FOGLIO COME FOGLIO CONTENENTE CONCENTRAZIONI PER DETERMINATI MATERIALI

        /// <summary>
        /// Ritorna le coordinate per una determinata posizione di lettura delle diverse concentrazioni per una determinata lega
        /// all'interno del foglio corrente
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentTipologiaFoglio"></param>
        /// <returns></returns>
        private void RecognizeConcentrationsPosition(ref ExcelWorksheet currentExcelSheet, Utils.Constants.TipologiaFoglioExcel currentTipologiaFoglio)
        {
            // recupero degli header per la certa tipologia di foglio excel
            List<string> currentHeaderFoglio = new List<string>();

            _currentPositionsConcentrations = null;


            if (currentTipologiaFoglio == Utils.Constants.TipologiaFoglioExcel.Informazioni_Concentrazione)
                currentHeaderFoglio = ExcelMarkers.GetAllMandatoryPropertiesForConcentrations();

            // non ho trovato nessuna informazione utile di header per il foglio corrente
            if (currentHeaderFoglio.Count() == 0)
            {
                //ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoNessunaInformazioneDiMarker(currentExcelSheet.Name);
                return;
            }


            // iterazione a partire dalla prima riga 
            do
            {
                // reset indice di riga per ripartire al conteggio
                _tracciaCurrentRow = 1;

                // indicazione di lettura di almeno un materiale per la riga corrente 
                bool hoLettoMateriale = false;

                do
                {
                    // passo alla riga successiva se non ho ancora incontrato informazioni utili
                    if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value == null)
                    {
                        _tracciaCurrentRow++;
                        continue;
                    }

                    // se non mi trovo piu nei limit allora rompo il ciclo
                    if (!CheckLimitRowCurrentIteration(_tracciaCurrentCol, _tracciaCurrentRow))
                    {
                        _tracciaCurrentRow++;
                        break;
                    }

                    ExcelConcQuadrant currentReadInfoConcentration;

                    bool isValid = FillMaterialConcentrationInfo(ref currentExcelSheet, out currentReadInfoConcentration);

                    // ho trovato una informazione
                    if (isValid)
                    {
                        // do enumerazione progressiva per il quadrante delle concentrazioni che sto leggendo 
                        currentReadInfoConcentration.EnumerationQuadrant = _currentSheetQuadrantsEnumerator;

                        _currentSheetQuadrantsEnumerator++;

                        // indico di aver almeno letto un materiale per l'iterazione sulla riga corrente 
                        hoLettoMateriale = true;

                        // eventuale inizializzazione della lista dei quadranti per le concentrazioni correnti
                        if (_currentPositionsConcentrations == null)
                            _currentPositionsConcentrations = new List<ExcelConcQuadrant>();

                        _currentPositionsConcentrations.Add(currentReadInfoConcentration);

                        //ServiceLocator.GetLoggingService.GetLoggerExcel.InserimentoQuadranteLetturaConcentrazioniPerFoglio(currentExcelSheet.Name);

                    }
                    else
                        _tracciaCurrentRow++;

                }
                while (_tracciaCurrentRow <= currentExcelSheet.Dimension.End.Row);


                // ricalcolo posizione index per iterazione su colonne successive
                _tracciaCurrentCol = RicalcolaPosizioneColonna(ref currentExcelSheet, hoLettoMateriale);

            }
            while (_tracciaCurrentCol <= currentExcelSheet.Dimension.End.Column);

        }


        /// <summary>
        /// Mi permette di ricalcolare la posizione della nuova colonna quando si completa il riconoscimento dei quadranti 
        /// "in verticale"
        /// Se non ho letto materiale sulla riga corrente allora ritorno semplicemente l'indice di colonna incrementato di una posizione 
        /// per l'eventuale lettura successiva 
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="readMaterialOnCurrentRow"></param>
        /// <returns></returns>
        private int RicalcolaPosizioneColonna(ref ExcelWorksheet currentExcelSheet, bool readMaterialOnCurrentRow)
        {
            // se la lista dei quadranti in lettura è vuota, allora ritorno semplicemente l'indice di colonna incrementato di una posizione
            // per passare eventualmente alla lettura successiva 
            if (_currentPositionsConcentrations == null)
                return _tracciaCurrentCol + 1;
            

            // calcolo del massimo indice di colonna sulle ultime letture fatte per i materiali
            int newColIndex = _currentPositionsConcentrations.Select(x => x.Get_Max_Col_Quadrante).Max() + 1;

            // confronto l'indice di colonna con la traccia per la colonna corrente, se ho un valore minore per la traccia 
            // allora imposto la traccia all'indice massimo di lettura per la colonna sull'ultima iterazione utile 
            if (_tracciaCurrentCol <= newColIndex)
                _tracciaCurrentCol = newColIndex;

            // contronto l'ultima lettura letta fatta sulla riga: se non ho letto nessun quadrante di concentrazioni
            // per tutte le righe sulle quali ho iterato per la colonna corrente, allora incremento l'indice di traccia di una posizione
            if (!readMaterialOnCurrentRow)
                _tracciaCurrentCol++;

            return _tracciaCurrentCol;
        }


        /// <summary>
        /// Mi permette di capire se sono passati i limiti rispetto alla lettura sulla colonna corrente 
        /// delle diverse concentrazioni che dovrei riscontrare alle righe successive
        /// </summary>
        /// <param name="currentColIndex"></param>
        /// <param name="currentRowIndex"></param>
        /// <returns></returns>
        private bool CheckLimitRowCurrentIteration(int currentColIndex, int currentRowIndex)
        {
            // non ho ancora inserito nessun elemento nella lista e ho superato i limiti di riga
            if (_currentPositionsConcentrations == null && _tracciaCurrentRow < MaxExcelSheetPos_row)
                return true;

            // non ho ancora nessun elemento nella lista ma ho superato i limiti di lettura di riga 
            if (_currentPositionsConcentrations == null)
                return false;

            
            // trovo l'indice di riga massimo letto per l'ultimo elemento su questa colonna 
            int currentMaxRow = _currentPositionsConcentrations.Where(x => x.Title_Col <= currentColIndex).Select(x => x.Conc_Row_End).Max();

            if (currentRowIndex > MaxExcelSheetPos_row)
                return false;

            return true;
        }


        /// <summary>
        /// Permette di fare un fill di tutte le informazioni relative al materiale corrente a partire dall'indice di 
        /// riga e colonna riscontrati, viene restituito l'oggetto e ci si muovera in verticale rispetto
        /// alla lettura che ne viene fatta
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentQuadrantConcentrations"></param>
        /// <returns></returns>
        private bool FillMaterialConcentrationInfo(ref ExcelWorksheet currentExcelSheet, out ExcelConcQuadrant currentQuadrantConcentrations)
        {
            // indicazione su aver trovato o meno tutti gli elementi
            bool hoTrovatoNome = false;
            bool hoTrovatoHeader = false;
            bool hoTrovatoConcentrazioni = false;
            

            // inizializzazione oggetto contenente gli indici
            currentQuadrantConcentrations = null;

            // Valori primari per current row e current col: questi valori mi servono nel caso in cui non trovo nessun valore e devo reimpostarli
            int startingCurrentRow = _tracciaCurrentRow;

            // valore di partenza del titolo
            int startingRowTitle = 0;

            // valore di partenza riga headers
            int startingRowHeaders = 0;

            // valore di partenza riga concentrazioni
            int startingRowConcentrations = 0;


            #region VERIFICA NOME



            if (currentExcelSheet.Cells[_tracciaCurrentRow, _tracciaCurrentCol].Value == null)
            {
                //ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoInformazioniPerTitoloMateriale(_tracciaCurrentCol, _tracciaCurrentRow);

                _tracciaCurrentRow = startingCurrentRow;
                return false;
            }
                
            // attribuzione degli indici di title
            else
            {
                hoTrovatoNome = true;
                startingRowTitle = _tracciaCurrentRow;

                //ServiceLocator.GetLoggingService.GetLoggerExcel.HoTrovatoInformazioniPerTitoloDelMateriale(_tracciaCurrentCol, _tracciaCurrentRow);
                
            }

            #endregion


            // questa informazione mi serve per stabilire quale sia il limite sia per il quadrante di header che per quello delle concentrazioni
            int colonnaFineLetturaHeader = 0;


            #region VERIFICA HEADER

            if (hoTrovatoNome)
            {

                do
                {
                    // incremento rispetto al title corrente    
                    _tracciaCurrentRow++;

                    hoTrovatoHeader = CheckHeadersConcentrations(ref currentExcelSheet, _tracciaCurrentCol, _tracciaCurrentRow, out colonnaFineLetturaHeader);

                    if (hoTrovatoHeader)
                    {
                        startingRowHeaders = _tracciaCurrentRow;

                        //ServiceLocator.GetLoggingService.GetLoggerExcel.HoTrovatoInformazioniHeaderPerQuadranteCorrente(_tracciaCurrentCol, _tracciaCurrentRow);
                        break;
                    }

                }
                // mi fermo nel caso non abbia trovato nessuna posizione valida
                while (_tracciaCurrentRow <= startingRowTitle + LIMITBETWEENCONCENTRATIONSROWS);

                // segnalazione di non aver trovato informazioni header per il quadrante corrente
                if (!hoTrovatoHeader)
                {
                    //ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoInformazioniHeaderPerQuadranteCorrente(_tracciaCurrentCol, _tracciaCurrentRow);
                    _tracciaCurrentRow = startingCurrentRow;
                    return false;
                }
                    

            }

            #endregion


            #region VERIFICA CONCENTRAZIONI

            int concentrationsEnding = 0;


            if (hoTrovatoNome && hoTrovatoHeader)
            {
                
                int numElementi = 0;


                do
                {
                    // incremento rispetto al title corrente    
                    _tracciaCurrentRow++;

                    hoTrovatoConcentrazioni = CalculateLastRowConcentrationsValue(ref currentExcelSheet, _tracciaCurrentCol, _tracciaCurrentRow, out concentrationsEnding, out numElementi);

                    if(hoTrovatoConcentrazioni)
                    {
                        startingRowConcentrations = _tracciaCurrentRow;

                        //ServiceLocator.GetLoggingService.GetLoggerExcel.HoTrovatoConcentrazioniPerIlQuadranteCorrente(numElementi);

                        break;
                    }

                }
                while (_tracciaCurrentRow <= startingRowHeaders + LIMITBETWEENCONCENTRATIONSROWS);


                if(!hoTrovatoConcentrazioni)
                {
                    //if (numElementi > Utils.Constants.CurrentListElementi.Count)
                    //    ServiceLocator.GetLoggingService.GetLoggerExcel.HoTrovatoConcentrazioniPerUnNumeroMaggioreDiElementi();
                    //else
                    //    ServiceLocator.GetLoggingService.GetLoggerExcel.NonHoTrovatoConcentrazioniPerIlQuadranteCorrente();

                    _tracciaCurrentRow = startingCurrentRow;
                    return false;

                }

            }

            #endregion


            #region CHECK VARIABILI

            // ritorno true SOLO se ho letto tutte le informazioni correnti
            if(hoTrovatoHeader && hoTrovatoNome && hoTrovatoConcentrazioni)
            {
                currentQuadrantConcentrations = new ExcelConcQuadrant();

                // valorizzazione per il quadrante 
                // titolo
                currentQuadrantConcentrations.Title_Col = _tracciaCurrentCol;
                currentQuadrantConcentrations.Title_Row = startingRowTitle;

                // header
                currentQuadrantConcentrations.Head_Col = _tracciaCurrentCol;
                currentQuadrantConcentrations.Head_Row = startingRowHeaders;

                // concentrazioni
                currentQuadrantConcentrations.Conc_Row_Start = startingRowConcentrations;
                currentQuadrantConcentrations.Conc_Row_End = concentrationsEnding;

                // impostazione delle informazioni per la traccia di ripresa lettura corrente 
                _tracciaCurrentRow = concentrationsEnding + 1;

                // solo per questo caso esco positivamente trovando un quadrante
                return true;
            }

            #endregion

            _tracciaCurrentRow = startingCurrentRow;
            return false;
        }


        /// <summary>
        /// Permette di capire se una certa linea in lettura contiene tutte le instestazioni relative all'header per 
        /// le concentrazioni in lettura corrente per il quadrante
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentColIndex"></param>
        /// <param name="currentRowIndex"></param>
        /// <param name="nextColIndex"></param>
        /// <returns></returns>
        private bool CheckHeadersConcentrations(ref ExcelWorksheet currentExcelSheet, int currentColIndex, int currentRowIndex, out int nextColIndex)
        {
            nextColIndex = 0;

            // indice relativo a quante proprieta obbligatorie sono state lette fino adesso iterando sulla colonna corrente 
            int mandatoryPropertiesCount = 0;

            do
            {
                // se non trovo nessun valore ritorno direttamente 
                if (currentExcelSheet.Cells[currentRowIndex, currentColIndex].Value == null)
                    break;

                string currentProperty = currentExcelSheet.Cells[currentRowIndex, currentColIndex].Value.ToString();

                if (_concentrationsMandatoryInfo.Contains(currentProperty))
                {
                    mandatoryPropertiesCount++;
                    
                }

                currentColIndex++;
            }
            while ((currentColIndex <= currentExcelSheet.Dimension.End.Column || currentExcelSheet.Cells[currentRowIndex, currentColIndex].Value != null) || mandatoryPropertiesCount < _concentrationsMandatoryInfo.Count);
            
            if(_concentrationsMandatoryInfo.Count == mandatoryPropertiesCount)
            {
                // indice di colonna di fine lettura header
                nextColIndex = currentColIndex - 1;

                return true;
            }

            return false;

        }


        /// <summary>
        /// Mi permette di trovare l'ultima cella leggendo la prima colonna degli elementi per la quale ci si ritrova
        /// ad avere ancora contenuto nullo (nei limiti dettati da row index)
        /// </summary>
        /// <param name="currentExcelSheet"></param>
        /// <param name="currentColIndex"></param>
        /// <param name="currentRowIndex"></param>
        /// <param name="nextRowIndex"></param>
        /// <param name="numElementi"></param>
        /// <returns></returns>
        private bool CalculateLastRowConcentrationsValue(ref ExcelWorksheet currentExcelSheet, int currentColIndex, int currentRowIndex, out int nextRowIndex, out int numElementi)
        {
            nextRowIndex = currentRowIndex;
            numElementi = 0;

            // iterazione sugli elementi che potrebbero esserci all'interno del quadrante
            int elementsIterations = 0;

            do
            {
                if (currentExcelSheet.Cells[currentRowIndex, currentColIndex].Value == null)
                {
                    // non trovo nessun elemento da leggere per i materiale corrente
                    if (elementsIterations == 0)
                        return false;

                    nextRowIndex = currentRowIndex - 1;
                    numElementi = elementsIterations;
                    return true;
                }


                // leggo ogni volta un elemento se la cella contiene un valore 
                if (currentExcelSheet.Cells[currentRowIndex, currentColIndex].Value != null)
                {
                    if(Utils.Constants.CurrentListElementi.Select(x => x.Symbol).Contains(currentExcelSheet.Cells[currentRowIndex, currentColIndex].Value.ToString()))
                    {
                        // incremento indice di riga ad ogni iterazione oltre che l'elemento corrispondente
                        currentRowIndex++;
                        elementsIterations++;
                        continue;
                    }
                    // verifico che la cella successiva non contenga lo stesso valore 
                    else if(!currentExcelSheet.Cells[currentRowIndex, currentColIndex + 1].Merge == true)
                    {
                        // incremento indice di riga ad ogni iterazione oltre che l'elemento corrispondente
                        currentRowIndex++;
                        elementsIterations++;
                        continue;
                    }

                    // stesso controllo fatto su, nel caso in cui in una cella non trovo piu contenuto conforme alla definizione di un elemento
                    // finisco l'iterazione cosi
                    if (elementsIterations > 0)
                    {
                        nextRowIndex = currentRowIndex - 1;
                        numElementi = elementsIterations;
                        return true;
                    }
                    
                    return false;
                }


            }
            // itero finche non arrivo al numero massimo di elementi per cui è possibile la lettura
            while (elementsIterations <= Utils.Constants.CurrentListElementi.Count);

            numElementi = currentColIndex;
            nextRowIndex = currentRowIndex;

            return false;
        }

        #endregion
        
    }
}
