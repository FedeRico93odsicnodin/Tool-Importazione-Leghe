using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Utils
{
    /// <summary>
    /// In questa classe sono contenute tutte le stringhe di query utilizzare per eseguire le operazioni di CRUD 
    /// sulle diverse tabelle del database di partenza
    /// 
    /// MODIFICA DI TEST
    /// </summary>
    public static class QueryStrings
    {
        #region LEGHE 

        /// <summary>
        /// Permette la selezione di tutte le leghe disponibili nell'origine
        /// </summary>
        public static string GetALLLeghe_Query = "SELECT \"GradeId\", \"Nome\", \"Descrizione\", \"CategoriaId\", \"Normativa\", \"Trattamento\", \"IdNormativa\"FROM public.\"Leghe\";";


        /// <summary>
        /// Permette l'inserimento di una nuova lega all'interno della relativa tabella
        /// </summary>
        public static string InsertNewLega_Query = "INSERT INTO public.\"Leghe\"" +
                                                  "(" +
                                                        "\"GradeId\", " +
                                                        "\"Nome\", " +
                                                        "\"Descrizione\", " +
                                                        "\"CategoriaId\", " +
                                                        "\"Normativa\", " +
                                                        "\"Trattamento\", " +
                                                        "\"IdNormativa\")" +
                                                    "VALUES" +
                                                    "(" +
                                                        "@GradeId, " +
                                                        "@Nome, " +
                                                        "@Descrizione, " +
                                                        "@CategoriaId, " +
                                                        "@Normativa, " +
                                                        "@Trattamento, " +
                                                        "@IdNormativa" +
                                                     ");";

        #endregion


        #region NORMATIVE

        /// <summary>
        /// Permette di ottenere tutte le normative dall'origine
        /// </summary>
        public static string GetALLNormative_Query = "SELECT \"ID\", \"Normativa\" FROM public.\"Normative\";";


        /// <summary>
        /// Permette di inserire un nuovo valore per la normativa
        /// </summary>
        public static string InsertNewNormativa_Query = "INSERT INTO public.\"Normative\"" +
                                                        "(" +
                                                                "\"ID\", " +
                                                                "\"Normativa\"" +
                                                        ") " +
                                                        "VALUES " +
                                                        "(" +
                                                                "@ID, " +
                                                                "@Normativa" +
                                                         ");";

        #endregion


        #region CATEGORIE LEGHE

        /// <summary>
        /// Permette di ottenere tutte le categorie leghe dall'origine
        /// </summary>
        public static string GetAllCategorieLeghe_Query = "SELECT \"ID\", \"Categoria\", \"IDBase\" FROM public.\"Categorie_Leghe\";";


        /// <summary>
        /// Permette l'inserimento di una nuova categoria di lega all'interno della relativa tabella
        /// </summary>
        public static string InsertNewCategoriaLeghe_Query = "INSERT INTO public.\"Categorie_Leghe\"" +
                                                                                "(" + 
	                                                                                "\"ID\"," +
                                                                                    "\"Categoria\", " +
                                                                                    "\"IDBase\"" +
                                                                                ")" + 
	                                                          "VALUES" +
                                                                                "(" +
                                                                                    "@ID, " +
                                                                                    "@Categoria, " +
                                                                                    "@IDBase" +
                                                                                ")";

        #endregion


        #region BASI

        /// <summary>
        /// Permette di ottenere tutte le basi dall'origine
        /// </summary>
        public static string GetAllBasi_Query = "SELECT \"ID\", \"IDElem\", \"Enabled\", \"OrdineVis\" FROM public.\"Basi\";";


        /// <summary>
        /// Permette di inserire per una nuova base nella relativa tabella
        /// </summary>
        public static string InsertNewBase_Query = "INSERT INTO public.\"Basi\"(" +
                                                    "\"ID\", " +
                                                    "\"IDElem\", " +
                                                    "\"Enabled\", " +
                                                    "\"OrdineVis\"" +
                                                    ")" +
                                                    "VALUES(@ID, " +
                                                            "@IDElem," +
                                                            "@Enabled, " +
                                                            "@OrdineVis" +
                                                    ");";

        #endregion


        #region CONC LEGHE

        /// <summary>
        /// Permette di ottenere tutte le concentrazioni leghe dall'origine
        /// </summary>
        public static string GetAllConcleghe_Query = "SELECT \"GradeId\", \"Elemento\", \"concMin\", \"concMax\", \"derogaMin\", \"derogaMax\", obiettivo FROM public.\"Concleghe\";";


        /// <summary>
        /// Permette di inserire per una nuova conclega all'interno della tabella di origine
        /// </summary>
        public static string InsertNewConcLega_Query = "INSERT INTO public.\"Concleghe\"(" +
                                                                                            "\"GradeId\", " +
                                                                                            "\"Elemento\", " +
                                                                                            "\"concMin\", " +
                                                                                            "\"concMax\", " +
                                                                                            "\"derogaMin\", " +
                                                                                            "\"derogaMax\", " +
                                                                                            "\"obiettivo\")" +
                                                                                    "VALUES" +
                                                                                            "(@GradeId, " +
                                                                                            "@Elemento, " +
                                                                                            "@concMin, " +
                                                                                            "@concMax, " +
                                                                                            "@derogaMin, " +
                                                                                            "@derogaMax, " +
                                                                                            "@obiettivo);";

        #endregion


        #region ELEMENTI

        /// <summary>
        /// Permette di ottenere tutti gli elementi dall'origine
        /// </summary>
        public static string GetAllElementi_Query = "SELECT \"ID\", \"Symbol\" FROM public.\"Elementi\";";


        /// <summary>
        /// Permette di inserire per un nuovo elemento all'interno della stessa tabella 
        /// </summary>
        public static string InsertNewElemento_Query = "INSERT INTO public.\"Elementi\"" +
                                                                                        "(" +
                                                                                            "\"ID\", " +
                                                                                            "\"Symbol\")" +
                                                                                        "VALUES" +
                                                                                        "(@ID, " +
                                                                                        "@Symbol" +
                                                                                        ");";

        #endregion
    }
}
