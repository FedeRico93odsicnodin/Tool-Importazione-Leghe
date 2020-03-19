using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.ExcelServices;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe.Logging
{
    /// <summary>
    /// Servizio di logging per il foglio database e rispetto alla wpf application vera e propria
    /// </summary>
    public class Logging_UI_Excel : LoggingBase_Excel
    {

        #region COSTRUTTORE 

        /// <summary>
        /// Inizializzazione della stringa indicante la collocazione del log
        /// relativo alle operazioni excel
        /// </summary>
        /// <param name="currentLogPath"></param>
        public Logging_UI_Excel(string currentLogPath)
        {
            base._currentLogFile = currentLogPath;
        }
        
        public override void AperturaCorrettaFileExcel(string currentFileExcel, XlsServices.CurrentModalitaExcel modalitaCorrente)
        {
            throw new NotImplementedException();
        }

        public override void HoRiconosciutoSeguenteFoglioCome(string currentFoglio, Constants.TipologiaFoglioExcel currentTipologia)
        {
            throw new NotImplementedException();
        }

        public override void HoTrovatoConcentrazioniPerIlQuadranteCorrente(int numElementi)
        {
            throw new NotImplementedException();
        }

        public override void HoTrovatoConcentrazioniPerUnNumeroMaggioreDiElementi()
        {
            throw new NotImplementedException();
        }

        public override void HoTrovatoIlSeguenteFoglioExcel(string currentFoglioExcelName, string currentFileExcel, XlsServices.CurrentModalitaExcel modalitaCorrente)
        {
            throw new NotImplementedException();
        }

        public override void HoTrovatoInformazioniHeaderPerQuadranteCorrente(int currentCol, int currentRow)
        {
            throw new NotImplementedException();
        }

        public override void HoTrovatoInformazioniPerTitoloDelMateriale(int currentCol, int currentRow)
        {
            throw new NotImplementedException();
        }

        public override void HoTrovatoTuttiIMarker(string currentFoglioExcel, Constants.TipologiaFoglioExcel currentTipologia)
        {
            throw new NotImplementedException();
        }

        public override void InserimentoQuadranteLetturaConcentrazioniPerFoglio(string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoConcentrazioniPerIlQuadranteCorrente()
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoInformazionePerIlSeguenteMarker(string currentMarker, int currentCol, int currentRow)
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoInformazioniHeaderPerQuadranteCorrente(int currentCol, int currentRow)
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoInformazioniPerTitoloMateriale(int currentCol, int currentRow)
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoNessunaInformazioneDiMarker(string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoNessunQuadranteConcentrazioniPerFoglio(string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void SegnalazioneEccezione(string currentException)
        {
            throw new NotImplementedException();
        }

        public override void SegnalazioneFoglioContenutoNullo(string currentFoglio, Constants.TipologiaFoglioExcel currentTipologia)
        {
            throw new NotImplementedException();
        }

        public override void SegnalazioneTrovatoContenutoUtile(string currentFoglioExcel, Constants.TipologiaFoglioExcel currentTipologia, int currentCol, int currentRow)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
