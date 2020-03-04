using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.DatabaseServices
{
    /// <summary>
    /// Contratto rispetto alle diverse operazioni che si possono eseguire a livello di entità all'interno del database 
    /// passato in input
    /// </summary>
    public abstract class DBOperations
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Indica in quale classe e quindi per quale entità database vengono poi implementati 
        /// i metodi sottostanti
        /// </summary>
        protected Constants.DBLabEntities currentDBEntity;

        #endregion


        #region PROTECTED DB OPERATIONS

        /// <summary>
        /// Mappatura dell'entita database corrente, questa viene implementata diversamente in base 
        /// all'entità corrente
        /// Questa implementazione riguarda la mappatura di una singola entità dal database verso la memoria
        /// </summary>
        /// <param name="currentReader"></param>
        /// <returns></returns>
        protected abstract LabEntities MapCurrentEntity(NpgsqlDataReader currentReader);


        /// <summary>
        /// Permette di ottenere un certo set di elementi database in base alla classe che la implementa 
        /// e rispetto alla query passata
        /// </summary>
        /// <param name="currentQuery"></param>
        /// <returns></returns>
        protected abstract List<LabEntities> GetSetDB(string currentQuery);


        /// <summary>
        /// Permette di inserire una nuova entità database in base alla classe nella quale viene implementato 
        /// e rispetto alla query passata</summary>
        /// <param name="currentEntity"></param>
        protected abstract void InsertSetDB(LabEntities currentEntity);


        /// <summary>
        /// Permette di fare l'update di una nuova entità database in base alla classe nella quale viene implementato 
        /// e rispetto alla query passata
        /// </summary>
        /// <param name="currentEntity"></param>
        protected abstract void UpdateSetDB(LabEntities currentEntity);


        /// <summary>
        /// Permette di cancellare una entità esistente in base alla classe nella quale viene implementato
        /// e rispetto alla query passata
        /// </summary>
        /// <param name="currentQuery"></param>
        protected abstract void DeleteSetDB(int currentID);

        #endregion
    }
}
