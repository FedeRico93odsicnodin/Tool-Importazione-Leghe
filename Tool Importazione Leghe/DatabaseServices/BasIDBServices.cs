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
    /// Classe contente le operazioni di CRUD per lavorare sulle Basi
    /// </summary>
    public class BasIDBServices : DBOperations
    {
        #region COSTRUTTORE

        /// <summary>
        /// Indicazione di quale entita si sta prendendo in considerazione
        /// </summary>
        public BasIDBServices()
        {
            // DB services per le leghe
            base.currentDBEntity = Constants.DBLabEntities.Basi;
        }

        #endregion



        #region PROTECTED METHODS

        protected override LabEntities MapCurrentEntity(NpgsqlDataReader currentReader)
        {
            BaseDB currentBase = new BaseDB();


            try
            {
                currentBase.ID = currentReader.GetInt32(0);

                currentBase.IDElem = currentReader.GetInt32(1);

                currentBase.Enabled = currentReader.GetBoolean(2);

                currentBase.OrdineVis = currentReader.GetInt32(3);
            }
            catch(Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMILETTURAENTITA, base.currentDBEntity);
                currentException += "\n";
                currentException += e.Message;
            }

            return currentBase;
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
        /// Esposizione del recupero di tutte le basi a partire dall'origine
        /// </summary>
        /// <returns></returns>
        public List<BaseDB> GetAllBasiDB()
        {
            return GetSetDB(QueryStrings.GetAllBasi_Query).Cast<BaseDB>().ToList();
        }

        #endregion
    }
}
