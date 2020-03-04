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
    /// Classe contente le operazioni di CRUD per lavorare con le Concleghe
    /// </summary>
    public class ConclegheDBServices : DBOperations
    {
        #region COSTRUTTORE

        /// <summary>
        /// Indicazione di quale entita si sta prendendo in considerazione
        /// </summary>
        public ConclegheDBServices()
        {
            // DB services per le leghe
            base.currentDBEntity = Constants.DBLabEntities.ConcLeghe;
        }

        #endregion



        #region PROTECTED METHODS

        protected override LabEntities MapCurrentEntity(NpgsqlDataReader currentReader)
        {
            ConcLegaDB currentConcLega = new ConcLegaDB();


            try
            {
                currentConcLega.GrateId = currentReader.GetInt32(0);

                currentConcLega.Elemento = currentReader.GetString(1);

                currentConcLega.concMin = currentReader.GetDouble(2);

                currentConcLega.concMax = currentReader.GetDouble(3);

                currentConcLega.derogaMin = currentReader.GetDouble(4);

                currentConcLega.derogaMax = currentReader.GetDouble(5);

                currentConcLega.obiettivo = currentReader.GetDouble(6);
            }
            catch(Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMILETTURAENTITA, base.currentDBEntity);
                currentException += "\n";
                currentException += e.Message;
            }

            return currentConcLega;
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

        protected override void InsertSetDB(LabEntities currentEntity)
        {
            throw new NotImplementedException();
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
        /// Permette di ottenere tutte le concentrazioni leghe che sono presenti 
        /// all'interno dell'origine
        /// </summary>
        /// <returns></returns>
        public List<ConcLegaDB> GetAllConcLeghe()
        {
            return GetSetDB(QueryStrings.GetAllConcleghe_Query).Cast<ConcLegaDB>().ToList();
        }

        #endregion
    }
}
