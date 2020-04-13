using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.DatabaseServices
{
    /// <summary>
    /// Qui si trovano tutte istanze relative ai servizi database che verranno utilizzati
    /// dal programma in esecuzione
    /// </summary>
    public class DBServices
    {
        #region ATTRIBUTI PRIVATI
        
        /// <summary>
        /// Stringa di connessione relativa al database PostGres nel quale 
        /// si eseguono le operazioni di import corrente 
        /// </summary>
        private static NpgsqlConnection _currentNPGConnection;


        /// <summary>
        /// Servizi relativi a operazioni di CRUD per la tabella delle leghe
        /// </summary>
        private LegheDBServices _currentLegheDBServices;


        /// <summary>
        /// Servizi relativi a operazioni di CRUD per la tabella delle normative
        /// </summary>
        private NormativeDBServices _currentNormativeDBServices;


        /// <summary>
        /// Servizi relativi a operzioni di CRUD per la tabella delle categorie leghe
        /// </summary>
        private Categorie_LegheDBServices _currentCategorie_LegheDBServices;


        /// <summary>
        /// Servizi relativi a operazioni di CRUD per la tabella delle basi
        /// </summary>
        private BasIDBServices _currentBasiDBServices;


        /// <summary>
        /// Servizi relativi a operazioni di CRUD per la tabella delle concleghe
        /// </summary>
        private ConclegheDBServices _currentConclegheDBServices;


        /// <summary>
        /// Servizi relativi a operazioni di CRUD per la tabella degli elementi
        /// </summary>
        private ElementiDBServices _currentElementiDBServices;
        
        #endregion


        #region COSTRUTTORE

        /// <summary>
        /// inizializzo le istanze relative ai servizi su tutte le tabelle per le quali il tool 
        /// agirà sul database origine e destinazione dei dati di import
        /// </summary>
        public DBServices()
        {
            // ottenimento della connessione al server 
            GetConnectionServer();

            // inizializzazione dei servizi database legati alla singola tabella
            InizializeTablesDBServices();
            
        }

        #endregion


        #region METODI PRIVATI
        
        /// <summary>
        /// Mi permette di ottenere la connessione con il server corrente 
        /// in base al valore configurato per la connection string all'interno della classe
        /// delle constants
        /// </summary>
        private void GetConnectionServer()
        {
            try
            {
                _currentNPGConnection = new NpgsqlConnection(Constants.NPGConnectionString);
                
            }
            catch (Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMIDICONNESSIONEDATABASE, Constants.NPGConnectionString);
                currentException += "\n";
                currentException += e.Message;
            }
        }


        /// <summary>
        /// Inizializzazione dei servizi database legati alla singola tabella 
        /// </summary>
        private void InizializeTablesDBServices()
        {

            // inizializzazione dei servizi per la tabella delle leghe
            _currentLegheDBServices = new LegheDBServices();

            // inizializzazione dei servizi per la tabella delle normative
            _currentNormativeDBServices = new NormativeDBServices();

            // inizializzazione dei servizi per la tabella delle categorie leghe
            _currentCategorie_LegheDBServices = new Categorie_LegheDBServices();

            // inizializzazione dei servizi per la tabella delle basi
            _currentBasiDBServices = new BasIDBServices();

            // inizializzazione dei servizi per la tabella concleghe
            _currentConclegheDBServices = new ConclegheDBServices();

            // inizializzazione dei servizi per la tabella elementi
            _currentElementiDBServices = new ElementiDBServices();

        }

        #endregion


        #region DBSERVICES

        /// <summary>
        /// Servizio di connessione al database sfruttando una unica istanza per questo tipo di chiamata 
        /// questo servizio permette di ottenere un set di valori che fanno parte del model per il database corrente
        /// in base a come si debba avere la mappatura dei valori viene richiamato il servizio di mapping statico
        /// all'interno di ogni classe che identifica i DB services per l'entità in selezione
        /// </summary>
        /// <param name="currentQuery"></param>
        /// <param name="currentEntity"></param>
        /// <returns></returns>
        internal static NpgsqlDataReader GetCurretSetDB(string currentQuery, Constants.DBLabEntities currentEntity)
        {
            
            bool ownershipPassed = false;

            _currentNPGConnection.Open();


            try
            {
                NpgsqlCommand currentCommand = new NpgsqlCommand(currentQuery, _currentNPGConnection);
                NpgsqlDataReader currentDataReader = currentCommand.ExecuteReader(CommandBehavior.CloseConnection);
                ownershipPassed = true;
                return currentDataReader;
                
            }
            catch (Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMIDIESECUZIONEREADER, currentEntity.ToString());
                currentException += "\n";
                currentException += e.Message;

                return null;
            }
            finally
            {
                if (!ownershipPassed)
                    _currentNPGConnection.Dispose();
            }
        }


        /// <summary>
        /// Permette di inserire per un nuovo comando di inserimento che viene passato in input
        /// il parametro in input è già preparato con tutti i parametri di inserimento in base alla query utilizzata 
        /// per l'entita corrente
        /// </summary>
        /// <param name="currentInsertCommand"></param>
        /// <param name="currentEntity"></param>
        public static void InsertUpdateValue(NpgsqlCommand currentInsertCommand, Constants.DBLabEntities currentEntity)
        {
            try
            {
                _currentNPGConnection.Open();

                currentInsertCommand.Connection = _currentNPGConnection;

                currentInsertCommand.Prepare();

                // effettivo inseriemnto per la row attuale
                currentInsertCommand.ExecuteNonQuery();

                _currentNPGConnection.Close();
            }
            catch(Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMIESECUZIONEINSERIMENTO, currentEntity);
                currentException += "\n";
                currentException += e.Message;

            }
        }

        #endregion


        #region GETTERS SERVIZI SULLE DIVERSE TABELLE
        
        /// <summary>
        /// Getter per i servizi sulla tabella leghe
        /// </summary>
        public LegheDBServices GetLegheDBServices
        {
            get
            {
                return _currentLegheDBServices;
            }
        }


        /// <summary>
        /// Getter per i servizi sulla tabella normative
        /// </summary>
        public NormativeDBServices GetNormativeDBServices
        {
            get
            {
                return _currentNormativeDBServices;
            }
        }


        /// <summary>
        /// Getter per i serivizi sulla tabella Categorie_Leghe
        /// </summary>
        public Categorie_LegheDBServices GetCategorieLegheDBServices
        {
            get
            {
                return _currentCategorie_LegheDBServices;
            }
        }


        /// <summary>
        /// Getter per i servizi sulla tabella Basi
        /// </summary>
        public BasIDBServices GetBasiDBServices
        {
            get
            {
                return _currentBasiDBServices;
            }
        }


        /// <summary>
        /// Getter per i servizi sulla tabella ConcLeghe
        /// </summary>
        public ConclegheDBServices GetConclegheDBServices
        {
            get
            {
                return _currentConclegheDBServices;
            }
        }


        /// <summary>
        /// Getter per i serivizi sulla tabella Elementi
        /// </summary>
        public ElementiDBServices GetElementiDBServices
        {
            get
            {
                return _currentElementiDBServices;
            }
        }

        #endregion
    }
}
