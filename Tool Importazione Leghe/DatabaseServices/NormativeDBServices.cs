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
    /// Classe che contiene tutte le operazioni CRUD che è possibile eseguire
    /// rispetto alle normative. I servizi sulle normative in particolare saranno:
    /// 
    /// - andare a recuperare tutte le normative (che saranno visualizzabili in una lista nell'interfaccia nel caso in cui si voglia eseguire export da db delle leghe di una particolare normativa)
    /// - inserimento di una certa normativa (se non compresa nella lista delle normative che viene recuperata di default con il caso precedente)
    /// </summary>
    public class NormativeDBServices : DBOperations
    {

        #region COSTRUTTORE

        /// <summary>
        /// Attribuzione istanza enumeratore corrente
        /// </summary>
        public NormativeDBServices()
        {
            base.currentDBEntity = Constants.DBLabEntities.Normative;
        }

        #endregion


        #region PROTECTED METHODS

        protected override LabEntities MapCurrentEntity(NpgsqlDataReader currentReader)
        {
            NormativeDB currentNormativa = new NormativeDB();

            try
            {


                currentNormativa.ID = currentReader.GetInt32(0);

                currentNormativa.Normativa = currentReader.GetString(1);


            }
            catch (Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMILETTURAENTITA, base.currentDBEntity);
                currentException += "\n";
                currentException += e.Message;
            }
            
            return currentNormativa;
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
        /// Permette di inserire una nuova normativa all'interno della tabella di riferimento
        /// per convenzione prima vengono popolati i parametri poi il richiamo del servizio
        /// con la connection string effettiva
        /// </summary>
        /// <param name="currentEntity"></param>
        protected override void InsertSetDB(LabEntities currentEntity)
        {
            try
            {

                // cast oggetto generico LabEntities in una entità di tipo lega
                NormativeDB currentNormativa = (NormativeDB)currentEntity;



                // istanza del comando
                NpgsqlCommand currentInsertCommand = new NpgsqlCommand(QueryStrings.InsertNewLega_Query);

                // aggiunta dei parametri
                currentInsertCommand.Parameters.AddWithValue("ID", currentNormativa.ID);
                currentInsertCommand.Parameters.AddWithValue("Normativa", currentNormativa.Normativa);

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
        /// Permette di ottenere tutte le normative presenti all'interno del database
        /// di origine
        /// </summary>
        /// <returns></returns>
        public List<NormativeDB> GetAllNormative()
        {
            return GetSetDB(QueryStrings.GetALLNormative_Query).Cast<NormativeDB>().ToList();
        }

        #endregion
    }
}
