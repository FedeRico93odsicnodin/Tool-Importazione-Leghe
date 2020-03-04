using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.DatabaseServices;

namespace Tool_Importazione_Leghe
{
    /// <summary>
    /// Allocazione di tutti i servizi utilizzati per l'importazione corrente 
    /// </summary>
    public static class ServiceLocator
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Servizi database sulle diverse tabelle coinvolte nelle operazioni CRUD
        /// per l'import stabilito dal programma
        /// </summary>
        private static DBServices _currentDBServices;

        #endregion


        #region GETTERS SERVIZI

        /// <summary>
        /// Getter per i servizi database disponibili
        /// </summary>
        public static DBServices GetDBServices
        {
            get
            {
                if (_currentDBServices == null)
                    _currentDBServices = new DBServices();

                return _currentDBServices;
            }


            
        }

        #endregion
    }
}
