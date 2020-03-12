using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.ModifiedElements;

namespace Tool_Importazione_Leghe.Utils
{
    /// <summary>
    /// In questa classe devono essere inserite tutte le configurazioni in lettura da un file di configurazione apposito
    /// e l'indicazione del tempo utilizzata per markare le diverse righe in log corrente
    /// </summary>
    public class Configurations
    {
        #region COSTANTI CHE DEVONO ESSERE PRESE DAL DOCUMENTO

        /// <summary>
        /// Indicazione sulla stringa di connessione che mi serve per leggere / inserire nel database
        /// </summary>
        private object[] DBCONNECTION_NPG = { "DBCONNECTION_NPG:", false };


        /// <summary>
        /// Indicazione sul documento excel di origine sul quale andare a leggere / inserire
        /// </summary>
        private object[] EXCELDOCUMENT = { "EXCELDOCUMENT:", false };


        /// <summary>
        /// Indicazione del documento xml di origine sul quale andare a leggere / inserire
        /// </summary>
        private object[] XMLDOCUMENT = { "XMLDOCUMENT:", false };


        /// <summary>
        /// Eventuale secondo database da usare come destinazione 
        /// </summary>
        private object[] DBCONNECTION_NPG_DESTINATION = { "DBCONNECTION_NPG_DESTINATION:", false };


        /// <summary>
        /// Eventuale secondo foglio excel da utilizzare come destinazione 
        /// </summary>
        private object[] EXCELDOCUMENT_DESTINATION = { "EXCELDOCUMENT_DESTINATION:", false };


        /// <summary>
        /// Eventuale secondo foglio xml da utizzare come destinazione
        /// </summary>
        private object[] XMLDOCUMENT_DESTINATION = { "XMLDOCUMENT_DESTINATION:", false };


        /// <summary>
        /// Indicazione della tipologia di import che bisogna seguire 
        /// </summary>
        private object[] TIPOLOGIA_IMPORT = { "TIPOLOGIA_IMPORT:", false };


        /// <summary>
        /// Permette di capire in che modalità si sta lanciando il tool, se in console o window application
        /// </summary>
        private object[] CURRENTMODALITATOOL = { "CURRENTMODALITATOOL:", false };


        /// <summary>
        /// In questa lista saranno contenuti tutti gli oggetti di configurazione utile all'avviamento 
        /// dell'import
        /// </summary>
        private List<object[]> _letturaConfig;


        /// <summary>
        /// Mappatura del tempo trascorso durante tutta la fase di import 
        /// </summary>
        private ExtendedStopWatch _currentTimerOnProcedure;

        #endregion


        #region COSTRUTTORE - INIZIALIZZAZIONE DELLA LISTA DI CONFIGURAZIONI E DEL TEMPO CHE STA TRASCORRENDO

        /// <summary>
        /// Inizializzazione dei parametri di import
        /// </summary>
        public Configurations()
        {
            _letturaConfig = new List<object[]>();

            _letturaConfig.Add(this.DBCONNECTION_NPG);
            _letturaConfig.Add(this.DBCONNECTION_NPG_DESTINATION);

            _letturaConfig.Add(this.EXCELDOCUMENT);
            _letturaConfig.Add(this.EXCELDOCUMENT_DESTINATION);

            _letturaConfig.Add(this.XMLDOCUMENT);
            _letturaConfig.Add(this.XMLDOCUMENT_DESTINATION);

            _letturaConfig.Add(this.CURRENTMODALITATOOL);
            _letturaConfig.Add(this.TIPOLOGIA_IMPORT);


            _currentTimerOnProcedure = new ExtendedStopWatch();
        }

        #endregion
    }
}
