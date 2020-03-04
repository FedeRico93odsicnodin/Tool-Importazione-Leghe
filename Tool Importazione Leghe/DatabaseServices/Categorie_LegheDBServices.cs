using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.DatabaseServices
{
    /// <summary>
    /// Classe contenente le operazioni CRUD da eseguire sulla tabella relativa alle 
    /// Categorie_Leghe
    /// </summary>
    public class Categorie_LegheDBServices : DBOperations
    {

        #region COSTRUTTORE

        /// <summary>
        /// Indicazione di quale entita si sta prendendo in considerazione
        /// </summary>
        public Categorie_LegheDBServices()
        {
            // DB services per le leghe
            base.currentDBEntity = Constants.DBLabEntities.Categorie_Leghe;
        }

        #endregion



        #region PROTECTED METHODS

        /// <summary>
        /// Mappatura della singola categoria per la lega corrente 
        /// </summary>
        /// <param name="currentReader"></param>
        /// <returns></returns>
        protected override LabEntities MapCurrentEntity(NpgsqlDataReader currentReader)
        {
            Categorie_LegheDB currentCategoriaLega = new Categorie_LegheDB();


            try
            {
                currentCategoriaLega.ID = currentReader.GetInt32(0);

                currentCategoriaLega.Categoria = currentReader.GetString(1);

                currentCategoriaLega.IDBase = (int)currentReader.GetDouble(2);
            }
            catch(Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMILETTURAENTITA, base.currentDBEntity);
                currentException += "\n";
                currentException += e.Message;
            }

            return currentCategoriaLega;
        }

        protected override List<LabEntities> GetSetDB(string currentQuery)
        {
            List<LabEntities> currentLegheDB = new List<LabEntities>();



            try
            {
                // richiamo il servizio dei db services per l'ottenimento del reader
                using (NpgsqlDataReader currentReaderLeghe = DBServices.GetCurretSetDB(currentQuery, base.currentDBEntity))
                {

                    if (currentReaderLeghe == null)
                        throw new Exception(String.Format(ExceptionMessages.PROBLEMIDIESECUZIONEREADER, base.currentDBEntity));

                    while (currentReaderLeghe.Read())
                        currentLegheDB.Add(MapCurrentEntity(currentReaderLeghe));
                }

            }
            catch (Exception e)
            {
                string currentException = e.Message;
            }

            return currentLegheDB.Cast<LabEntities>().ToList();
        }


        /// <summary>
        /// Permette di inserire una nuova categoria lega all'interno della tabella di riferimento
        /// per convenzione prima vengono popolati i parametri poi il richiamo del servizio
        /// con la connection string effettiva
        /// </summary>
        /// <param name="currentEntity"></param>
        protected override void InsertSetDB(LabEntities currentEntity)
        {
            try
            {

                // cast oggetto generico LabEntities in una entità di tipo lega
                Categorie_LegheDB currentCategoriaLega = (Categorie_LegheDB)currentEntity;

                // istanza del comando
                NpgsqlCommand currentInsertCommand = new NpgsqlCommand(QueryStrings.InsertNewLega_Query);

                // aggiunta dei parametri
                currentInsertCommand.Parameters.AddWithValue("ID", currentCategoriaLega.ID);
                currentInsertCommand.Parameters.AddWithValue("Categoria", currentCategoriaLega.Categoria);
                currentInsertCommand.Parameters.AddWithValue("IDBase", currentCategoriaLega.IDBase);

                // richiamo il servizio con la connessione vera e propria
                DBServices.InsertNewValue(currentInsertCommand, base.currentDBEntity);
            }
            catch (Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMACASTOGGETTODB, base.currentDBEntity);
                currentException += "\n";
                currentException += e.Message;
            }
        }

        protected override void UpdateSetDB(LabEntities currentEntity)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteSetDB(int currentID)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region METODI PUBBLICI 

        /// <summary>
        /// Esposizione ottenimento di tutte le concentrazioni leghe 
        /// rispetto al database di partenza
        /// </summary>
        /// <returns></returns>
        public List<Categorie_LegheDB> GetAllCategorieLeghe()
        {
            return GetSetDB(QueryStrings.GetAllConcleghe_Query).Cast<Categorie_LegheDB>().ToList();
        }

        #endregion
    }
}
