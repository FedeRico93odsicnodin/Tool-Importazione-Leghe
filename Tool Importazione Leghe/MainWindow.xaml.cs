using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tool_Importazione_Leghe.Model;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // partenza del servizio di recupero di tutte le leghe
            List<LegheDB> currentSetLeghe = ServiceLocator.GetDBServices.GetLegheDBServices.GetAllLeghe();

            // partenza del servizio di recupero di tutte le normative
            List<NormativeDB> currentSetNormative = ServiceLocator.GetDBServices.GetNormativeDBServices.GetAllNormative();

            // partenza del servizio di recupero delle categorie leghe
            List<Categorie_LegheDB> currentSetCategorieLeghe = ServiceLocator.GetDBServices.GetCategorieLegheDBServices.GetAllCategorieLeghe();

            // partenza del servizio di recupero delle basi
            List<BaseDB> _currentBasiDB = ServiceLocator.GetDBServices.GetBasiDBServices.GetAllBasiDB();

            // partenza del servizio di recupero delle concleghe
            List<ConcLegaDB> _currentConcLega = ServiceLocator.GetDBServices.GetConclegheDBServices.GetAllConcLeghe();

            // partenza del servizio di recupero degli elementi
            List<ElementiDB> _currentElementi = ServiceLocator.GetDBServices.GetElementiDBServices.GetAllElementiDB();
        }
    }
}
