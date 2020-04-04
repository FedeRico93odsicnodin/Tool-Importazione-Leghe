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

        public override void FineProcessamentoGeneralInfoPerFoglioExcel(string excelSheetName)
        {
            throw new NotImplementedException();
        }

        public override void HoAppenaFinitoDiLeggereTuttiIValoriGeneralInfoLega(string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void HoGiaTrovatoInformazioneACarattereGenerale(string currentProprietaLettura)
        {
            throw new NotImplementedException();
        }

        public override void HoLettoUnaRigaDiValoriGeneralPerFoglioExcelInRiga(int currentRow, string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void HoRecuperatoInformazioniConcentrazioniPerQuadrante(int enumQuadrante, string currentExcelSheet)
        {
            throw new NotImplementedException();
        }

        public override void HoRiconosciutoIlFoglioComeContenenteConcentrazioniMateriali(string currentExcelSheet)
        {
            throw new NotImplementedException();
        }

        public override void HoRiconosciutoIlFoglioComeContenenteInformazioniGeneraliLega(string currentExcelSheet)
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

        public override void InformazioneGeneraleNonContenutaNelleDefinizioniAddizionali(string currentProprietaLettura)
        {
            throw new NotImplementedException();
        }

        public override void InformazioneGeneraleNonContenutaNelleDefinizioniObbligatorie(string currentProprietaLettura)
        {
            throw new NotImplementedException();
        }

        public override void InformazioniPerFoglioRecuperateCorrettamente(string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void InizioLetturaInformazioniPerFoglioExcelCorrente(string currentFoglioExcel, Constants.TipologiaFoglioExcel currentTipologiaFoglio)
        {
            throw new NotImplementedException();
        }

        public override void InizioProceduraRecuperoInformazioni(string currentFileExcel)
        {
            throw new NotImplementedException();
        }

        public override void InserimentoQuadranteLetturaConcentrazioniPerFoglio(string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoAlcunaInformazionePerIlFoglio(string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoConcentrazioniPerIlQuadranteCorrente()
        {
            throw new NotImplementedException();
        }

        public override void NonHoTrovatoInformazioniGeneraliLegaPerRiga(string currentFoglioExcel, int currentRiga)
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
        

        public override void NonHoTrovatoNessunQuadranteConcentrazioniPerFoglio(string currentFoglioExcel)
        {
            throw new NotImplementedException();
        }

        public override void NonPossoContinuareLetturaQuadranteConcentrazioni(int currentQuadranteEnumerator, string currentExcelSheet)
        {
            throw new NotImplementedException();
        }

        public override void SegnalazioneEccezione(string currentException)
        {
            throw new NotImplementedException();
        }

        public override void TrovataInformazioneAddizionaleLetturaInformazioniGenerali(string currentProprietaLettura, int currentRow, int currentCol)
        {
            throw new NotImplementedException();
        }

        public override void TrovataInformazioneObbligatoriaLetturaInformazioniGenerali(string currentProprietaLettura, int currentRow, int currentCol)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
