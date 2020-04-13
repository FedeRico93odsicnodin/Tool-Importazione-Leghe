using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe
{
    /// <summary>
    /// In questa classe sono inseriti tutti gli elementi database da recuperare prima della partenza effettiva del tool 
    /// qualsiasi sia la modalità coinvolta nell'import 
    /// </summary>
    public class StartingLoad_Activity
    {
        /// <summary>
        /// Permette il caricamento effettivo di tutti gli elementi presenti a database nella lista delle costanti
        /// Questo caricamento coinvolge unicamente le stringhe che caratterizzano il singolo elemento per una questione di semplicità
        /// </summary>
        public void LoadElements()
        {
            // recupero della lista di tutti gli elementi all'interno del DB
            List<ElementiDB> _currentElementi = ServiceLocator.GetDBServices.GetElementiDBServices.GetAllElementiDB();
            
            // caricamento della lista appena formata all'interno delle costanti
            Constants.CurrentListElementi = _currentElementi;

            // segnalazione log
            // TODO: inserire la segnalazione per il caso corrente 
        }
    }
}
