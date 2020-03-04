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
    /// In questa classe sono presenti tutte le operazioni CRUD 
    /// per poter lavorare con le leghe contenute all'interno del database di partenza
    /// </summary>
    public class LegheDBServices : DBOperations
    {
        #region COSTRUTTORE

        /// <summary>
        /// Specifica dell'entità database alla quale mi sto riferendo
        /// </summary>
        public LegheDBServices()
        {
            // riferimento alle leghe
            base.currentDBEntity = Constants.DBLabEntities.Leghe;
        }

        #endregion
        

        #region METODI PROTECTED

        /// <summary>
        /// Mappatura singola entità lega
        /// </summary>
        /// <param name="currentReader"></param>
        /// <returns></returns>
        protected override LabEntities MapCurrentEntity(NpgsqlDataReader currentReader)
        {
            LegheDB currentLegaDB = new LegheDB();

            // mappatura paremetri letti correntemente
            try
            {
                currentLegaDB.GradeId = currentReader.GetInt32(0);

                currentLegaDB.Nome = currentReader.GetString(1);

                currentLegaDB.Descrizione = currentReader.GetString(2);

                currentLegaDB.CategoriaId = currentReader.GetInt32(3);

                currentLegaDB.Normativa = currentReader.GetString(4);

                currentLegaDB.Trattamento = currentReader.GetString(5);

                currentLegaDB.IdNormativa = currentReader.GetInt32(6);
            }
            catch (Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMILETTURAENTITA, base.currentDBEntity);
                currentException += "\n";
                currentException += e.Message;
            }


            return currentLegaDB;
        }


        /// <summary>
        /// Permette di ottenere un certo set di leghe presente all'interno del database
        /// per il quale è stata precificata e inizializzata la connessione all'interno dei DBServices
        /// </summary>
        /// <param name="currentQuery"></param>
        /// <returns></returns>
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

            return currentLegheDB;
        }


        /// <summary>
        /// Permette di inserire una nuova entità di tipo lega all'interno del database
        /// l'entita mappata è una singola entita con la query delle query strings
        /// </summary>
        /// <param name="currentEntity"></param>
        protected override void InsertSetDB(LabEntities currentEntity)
        {
            try
            {

                // cast oggetto generico LabEntities in una entità di tipo lega
                LegheDB currentLega = (LegheDB)currentEntity;

                // istanza del comando
                NpgsqlCommand currentInsertCommand = new NpgsqlCommand(QueryStrings.InsertNewLega_Query);

                // aggiunta dei parametri
                currentInsertCommand.Parameters.AddWithValue("GradeId", currentLega.GradeId);
                currentInsertCommand.Parameters.AddWithValue("Nome", currentLega.Nome);
                currentInsertCommand.Parameters.AddWithValue("Descrizione", currentLega.Descrizione);
                currentInsertCommand.Parameters.AddWithValue("CategoriaId", currentLega.CategoriaId);
                currentInsertCommand.Parameters.AddWithValue("Normativa", currentLega.Normativa);
                currentInsertCommand.Parameters.AddWithValue("Trattamento", currentLega.Trattamento);
                currentInsertCommand.Parameters.AddWithValue("IdNormativa", currentLega.IdNormativa);

                // richiamo il servizio con la connessione vera e propria
                DBServices.InsertNewValue(currentInsertCommand, base.currentDBEntity);
            }
            catch(Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMACASTOGGETTODB, base.currentDBEntity);
                currentException += "\n";
                currentException += e.Message;
            }



        }


        protected override void UpdateSetDB(LabEntities currentEntity)
        {
            // TODO: implementazione servizio base
            throw new NotImplementedException();
        }

        protected override void DeleteSetDB(int currentID)
        {
            // TODO: implementazione servizio base
            throw new NotImplementedException();
        }


        #endregion


        #region METODI PUBBLICI

        /// <summary>
        /// Servizio che mi permette di ottenere tutte le leghe all'interno del database di partenza
        /// </summary>
        /// <returns></returns>
        public List<LegheDB> GetAllLeghe()
        {
            return GetSetDB(QueryStrings.GetALLLeghe_Query).Cast<LegheDB>().ToList();
        }

        #endregion
    }
}
