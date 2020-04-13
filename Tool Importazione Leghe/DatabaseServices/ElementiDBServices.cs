using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Common;
using Tool_Importazione_Leghe.Logging;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.DatabaseServices
{
    /// <summary>
    /// Classe contenente le operazioni di CRUD per lavorare con gli elementi
    /// </summary>
    public class ElementiDBServices : DBOperations
    {
        #region COSTRUTTORE

        /// <summary>
        /// Indicazione di quale entita si sta prendendo in considerazione
        /// </summary>
        public ElementiDBServices()
        {
            // DB services per le leghe
            base.currentDBEntity = Constants.DBLabEntities.Elementi;
        }

        #endregion



        #region PROTECTED METHODS

        protected override LabEntities MapCurrentEntity(NpgsqlDataReader currentReader)
        {
            ElementiDB currentElemento = new ElementiDB();

            try
            {
                currentElemento.ID = currentReader.GetInt32(0);

                currentElemento.Symbol = currentReader.GetString(1);

                if(currentReader.GetValue(2) != null)
                    currentElemento.Nome = currentReader.GetString(2);
            }
            catch(Exception e)
            {
                string currentException = String.Format(ExceptionMessages.PROBLEMILETTURAENTITA, base.currentDBEntity);
                currentException += "\n";
                currentException += e.Message;
            }

            return currentElemento;
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


        /// <summary>
        /// Update relativo possibile della lista del nome attribuito all'elemento
        /// </summary>
        /// <param name="currentEntity"></param>
        protected override void UpdateSetDB(LabEntities currentEntity)
        {

            ElementiDB currentElemento = (ElementiDB)currentEntity;

            // inserimento del valore per il nome dell'elemento corrente nel caso in cui questo sia empty
            if(currentElemento.Nome == null)
            {


                string nomeElemento = PeriodicTable.Elements.Where(x => x.Symbol == currentElemento.Symbol).FirstOrDefault().Name;

                currentElemento.Nome = nomeElemento;
                
                // istanza del comando
                NpgsqlCommand currentInsertCommand = new NpgsqlCommand(
                    String.Format(QueryStrings.UpdateNomeElemento_Query, currentElemento.Nome, currentElemento.Symbol)
                    );

                // richiamo il servizio con la connessione vera e propria
                DBServices.InsertUpdateValue(currentInsertCommand, base.currentDBEntity);
            }


        }

        protected override void DeleteSetDB(int currentID)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region METODI PUBBLICI 

        /// <summary>
        /// Permette di ottenere tutti gli elementi presenti nell'origine di partenza
        /// </summary>
        /// <returns></returns>
        public List<ElementiDB> GetAllElementiDB()
        {
            return GetSetDB(QueryStrings.GetAllElementi_Query).Cast<ElementiDB>().ToList();
        }

        
        /// <summary>
        /// Permette di andare a eseguire un check rispetto al nome per l'elemento corrente
        /// nel caso in cui il nome non sia stato inserito, questo viene recuperato dalla lista della tavola periodica con 
        /// corrisponenza per il simbolo dell'elemento e viene eseguito l'update per il campo in questione
        /// </summary>
        /// <param name="listaElementiDB"></param>
        public void CheckElementiDB(List<ElementiDB> listaElementiDB)
        {
            foreach (ElementiDB currentElemento in listaElementiDB)
                UpdateSetDB(currentElemento);
        }

        #endregion
    }
}
