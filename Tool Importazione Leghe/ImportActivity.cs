using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool_Importazione_Leghe.Utils;

namespace Tool_Importazione_Leghe
{
    /// <summary>
    /// Oggetto relativo all'attività di import corrente 
    /// nel costruttore è passata la modalità di import, con l'attività do import si da il via all'import vero e proprio
    /// </summary>
    public class ImportActivity
    {
        #region ATTRIBUTI PRIVATI

        /// <summary>
        /// Mappatura sul task da eseguire per l'import corrente
        /// </summary>
        private Constants.TipologiaImport _currentActivity;

        #endregion


        #region COSTRUTTORE 

        /// <summary>
        /// Permette di gestire l'attività di import vera e propria con il passaggio dell'azione da eseguire sui diversi oggetti
        /// </summary>
        /// <param name="currentImport"></param>
        public ImportActivity(Constants.TipologiaImport currentImport)
        {
            _currentActivity = currentImport;
        }

        #endregion


        #region METODI PUBBLICI

        /// <summary>
        /// Inizio dell'attività di import per la procedura corrente
        /// </summary>
        public void Do_Import()
        {
            // separazione contesto attività
            ServiceLocator.GetLoggingService.GetLoggerImportActivity.GetSeparatorActivity();

            switch(_currentActivity)
            {
                case Constants.TipologiaImport.excel_to_database:
                    {
                        // segnalazione avviamento import corrente 
                        ServiceLocator.GetLoggingService.GetLoggerImportActivity.VieneAvviataLaSeguenteProceduraDiImport(_currentActivity.ToString());

                        // avviamento della procedura di importazione da excel a database
                        Do_Import_ExcelToDatabase();
                        break;
                    }
                case Constants.TipologiaImport.database_to_excel:
                    {
                        // segnalazione avviamento import corrente 
                        ServiceLocator.GetLoggingService.GetLoggerImportActivity.VieneAvviataLaSeguenteProceduraDiImport(_currentActivity.ToString());

                        // avviamento della procedura di import da database a excel
                        Do_Import_DatabaseToExcel();
                        break;
                    }
                case Constants.TipologiaImport.xml_to_database:
                    {
                        // segnalazione avviamento import corrente 
                        ServiceLocator.GetLoggingService.GetLoggerImportActivity.VieneAvviataLaSeguenteProceduraDiImport(_currentActivity.ToString());

                        // avviamento della procedura di import da xml a database
                        Do_Import_XmlToDatabase();
                        break;
                    }
                case Constants.TipologiaImport.database_to_xml:
                    {
                        // segnalazione avviamento import corrente 
                        ServiceLocator.GetLoggingService.GetLoggerImportActivity.VieneAvviataLaSeguenteProceduraDiImport(_currentActivity.ToString());

                        // avviamento della procedura di import da database a xml
                        Do_Import_DatabaseToXml();
                        break;
                    }
                case Constants.TipologiaImport.database_to_database:
                    {
                        // segnalazione avviamento import corrente 
                        ServiceLocator.GetLoggingService.GetLoggerImportActivity.VieneAvviataLaSeguenteProceduraDiImport(_currentActivity.ToString());

                        // avviamento della procedura di import da database a database
                        Do_Import_Database_To_Database();
                        break;
                    }
                case Constants.TipologiaImport.excel_to_excel:
                    {
                        // segnalazione avviamento import corrente 
                        ServiceLocator.GetLoggingService.GetLoggerImportActivity.VieneAvviataLaSeguenteProceduraDiImport(_currentActivity.ToString());

                        // avviamento della procedura di import da excel a excel
                        Do_Import_ExcelToExcel();
                        break;
                    }
                case Constants.TipologiaImport.xml_to_xml:
                    {
                        // segnalazione avviamento import corrente 
                        ServiceLocator.GetLoggingService.GetLoggerImportActivity.VieneAvviataLaSeguenteProceduraDiImport(_currentActivity.ToString());

                        // avviamento della procedura di import da xml a xml
                        Do_Import_XmlToXml();
                        break;
                    }
            }

            // separazione contesto attività
            ServiceLocator.GetLoggingService.GetLoggerImportActivity.GetSeparatorActivity();

        }


        /// <summary>
        /// Procedura di import per l'importazione da un file excel a un database
        /// </summary>
        private void Do_Import_ExcelToDatabase()
        {
            Console.WriteLine("mi trovo qui");
        }


        /// <summary>
        /// Procedura di import per l'importazione da database a file excel
        /// </summary>
        private void Do_Import_DatabaseToExcel()
        {

        }


        /// <summary>
        /// Procedura di import per l'importazione da xml a database
        /// </summary>
        private void Do_Import_XmlToDatabase()
        {

        }


        /// <summary>
        /// Procedura di import per l'importazione da database a xml
        /// </summary>
        private void Do_Import_DatabaseToXml()
        {

        }


        /// <summary>
        /// Procedura di import per l'importazione da database a database
        /// </summary>
        private void Do_Import_Database_To_Database()
        {

        }


        /// <summary>
        /// Procedura di improt da excel a excel
        /// </summary>
        private void Do_Import_ExcelToExcel()
        {

        }


        /// <summary>
        /// Procedura di import da xml a xml
        /// </summary>
        private void Do_Import_XmlToXml()
        {

        }
         
        #endregion
    }
}
