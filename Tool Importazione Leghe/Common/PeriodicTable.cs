using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.Common
{
    public class Element
    {
        public int AtomicNumber { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal AtomicWeight { get; set; }
        public string NameIT {  get; set; }
        //  public string GroupNumber { get; set; }
        //  public string GroupName { get; set; }
        //  public string Period { get; set; }
        //  public string Block { get; set; }
        //  public string CASRegistryID { get; set; }
        //  public string DiscoveryDate { get; set; }
        //  public string DiscovererName { get; set; }

        public Element() { }
        public Element(int atomicNumber, string symbol, string name, decimal atomicWeight)
        {
            AtomicNumber = atomicNumber;
            Symbol = symbol;
            Name = name;
            AtomicWeight = atomicWeight;
        }
    }

    public static class PeriodicTable
    {
        /// <summary>
        /// Lista degli elementi correnti con le definizioni date per la classe precedente 
        /// </summary>
        private static List<Element> _listaElementi; 


        /// <summary>
        /// Lista di tutti gli elementi per l'analisi
        /// </summary>
        public static List<Element> Elements {
            get
            {
                // inizializzazione della lista degli elementi
                if(_listaElementi == null)
                {
                    _listaElementi = new List<Element>();
                    _listaElementi.Add(new Element(1, "H", "Hydrogen", 1.007825M));
                    _listaElementi.Add(new Element(2, "He", "Helium", 4.00260M));
                    _listaElementi.Add(new Element(3, "Li", "Lithium", 6.941M));
                    _listaElementi.Add(new Element(4, "Be", "Beryllium", 9.01218M));
                    _listaElementi.Add(new Element(5, "B", "Boron", 10.81M));
                    _listaElementi.Add(new Element(6, "C", "Carbon", 12.011M));
                    _listaElementi.Add(new Element(7, "N", "Nitrogen", 14.0067M));
                    _listaElementi.Add(new Element(8, "O", "Oxygen", 15.999M));
                    _listaElementi.Add(new Element(9, "F", "Fluorine", 18.99840M));
                    _listaElementi.Add(new Element(10, "Ne", "Neon", 20.179M));
                    _listaElementi.Add(new Element(11, "Na", "Sodium", 22.98977M));
                    _listaElementi.Add(new Element(12, "Mg", "Magnesium", 24.305M));
                    _listaElementi.Add(new Element(13, "Al", "Aluminum", 26.98154M));
                    _listaElementi.Add(new Element(14, "Si", "Silicon", 28.0855M));
                    _listaElementi.Add(new Element(15, "P", "Phosphorus", 0.0M));
                    _listaElementi.Add(new Element(16, "S", "Sulphur", 32.06M));
                    _listaElementi.Add(new Element(17, "Cl", "Chlorine", 35.453M));
                    _listaElementi.Add(new Element(18, "Ar", "Argon", 39.948M));
                    _listaElementi.Add(new Element(19, "K", "Potassium", 39.0983M));
                    _listaElementi.Add(new Element(20, "Ca", "Calcium", 40.08M));
                    _listaElementi.Add(new Element(21, "Sc", "Scandium", 44.9559M));
                    _listaElementi.Add(new Element(22, "Ti", "Titanium", 47.90M));
                    _listaElementi.Add(new Element(23, "V", "Vanadium", 50.9414M));
                    _listaElementi.Add(new Element(24, "Cr", "Chromium", 51.996M));
                    _listaElementi.Add(new Element(25, "Mn", "Manganese", 54.9380M));
                    _listaElementi.Add(new Element(26, "Fe", "Iron", 55.85M));
                    _listaElementi.Add(new Element(27, "Co", "Cobalt", 58.9332M));
                    _listaElementi.Add(new Element(28, "Ni", "Nickel", 58.71M));
                    _listaElementi.Add(new Element(29, "Cu", "Copper", 63.546M));
                    _listaElementi.Add(new Element(30, "Zn", "Zinc", 65.37M));
                    _listaElementi.Add(new Element(31, "Ga", "Gallium", 69.72M));
                    _listaElementi.Add(new Element(32, "Ge", "Germanium", 72.59M));
                    _listaElementi.Add(new Element(33, "As", "Arsenic", 74.9216M));
                    _listaElementi.Add(new Element(34, "Se", "Selenium", 78.96M));
                    _listaElementi.Add(new Element(35, "Br", "Bromine", 79.904M));
                    _listaElementi.Add(new Element(36, "Kr", "Krypton", 83.80M));
                    _listaElementi.Add(new Element(37, "Rb", "Rubidium", 85.4678M));
                    _listaElementi.Add(new Element(38, "Sr", "Strontium", 87.62M));
                    _listaElementi.Add(new Element(39, "Y", "Yttrium", 88.9059M));
                    _listaElementi.Add(new Element(40, "Zr", "Zirconium", 91.22M));
                    _listaElementi.Add(new Element(41, "Nb", "Niobium", 92.91M));
                    _listaElementi.Add(new Element(42, "Mo", "Molybdenum", 95.94M));
                    _listaElementi.Add(new Element(43, "Tc", "Technetium", 99.0M));
                    _listaElementi.Add(new Element(44, "Ru", "Ruthenium", 101.1M));
                    _listaElementi.Add(new Element(45, "Rh", "Rhodium", 102.91M));
                    _listaElementi.Add(new Element(46, "Pd", "Palladium", 106.42M));
                    _listaElementi.Add(new Element(47, "Ag", "Silver", 107.87M));
                    _listaElementi.Add(new Element(48, "Cd", "Cadmium", 112.4M));
                    _listaElementi.Add(new Element(49, "In", "Indium", 114.82M));
                    _listaElementi.Add(new Element(50, "Sn", "Tin", 118.69M));
                    _listaElementi.Add(new Element(51, "Sb", "Antimony", 121.75M));
                    _listaElementi.Add(new Element(52, "Te", "Tellurium", 127.6M));
                    _listaElementi.Add(new Element(53, "I", "Iodine", 126.9045M));
                    _listaElementi.Add(new Element(54, "Xe", "Xenon", 131.29M));
                    _listaElementi.Add(new Element(55, "Cs", "Cesium", 132.9054M));
                    _listaElementi.Add(new Element(56, "Ba", "Barium", 137.33M));
                    _listaElementi.Add(new Element(57, "La", "Lanthanum", 138.91M));
                    _listaElementi.Add(new Element(58, "Ce", "Cerium", 140.12M));
                    _listaElementi.Add(new Element(59, "Pr", "Praseodymium", 140.91M));
                    _listaElementi.Add(new Element(60, "Nd", "Neodymium", 0.0M));
                    _listaElementi.Add(new Element(61, "Pm", "Promethium", 147.0M));
                    _listaElementi.Add(new Element(62, "Sm", "Samarium", 150.35M));
                    _listaElementi.Add(new Element(63, "Eu", "Europium", 167.26M));
                    _listaElementi.Add(new Element(64, "Gd", "Gadolinium", 157.25M));
                    _listaElementi.Add(new Element(65, "Tb", "Terbium", 158.925M));
                    _listaElementi.Add(new Element(66, "Dy", "Dysprosium", 162.50M));
                    _listaElementi.Add(new Element(67, "Ho", "Holmium", 164.9M));
                    _listaElementi.Add(new Element(68, "Er", "Erbium", 167.26M));
                    _listaElementi.Add(new Element(69, "Tm", "Thulium", 168.93M));
                    _listaElementi.Add(new Element(70, "Yb", "Ytterbium", 173.04M));
                    _listaElementi.Add(new Element(71, "Lu", "Lutetium", 174.97M));
                    _listaElementi.Add(new Element(72, "Hf", "Hafnium", 178.49M));
                    _listaElementi.Add(new Element(73, "Ta", "Tantalum", 180.95M));
                    _listaElementi.Add(new Element(74, "W", "Tungsten", 183.85M));
                    _listaElementi.Add(new Element(75, "Re", "Rhenium", 186.23M));
                    _listaElementi.Add(new Element(76, "Os", "Osmium", 190.2M));
                    _listaElementi.Add(new Element(77, "Ir", "Iridium", 192.2M));
                    _listaElementi.Add(new Element(78, "Pt", "Platinum", 195.09M));
                    _listaElementi.Add(new Element(79, "Au", "Gold", 196.9655M));
                    _listaElementi.Add(new Element(80, "Hg", "Mercury", 200.59M));
                    _listaElementi.Add(new Element(81, "Tl", "Thallium", 204.383M));
                    _listaElementi.Add(new Element(82, "Pb", "Lead", 207.2M));
                    _listaElementi.Add(new Element(83, "Bi", "Bismuth", 208.9804M));
                    _listaElementi.Add(new Element(84, "Po", "Polonium", 210.0M));
                    _listaElementi.Add(new Element(85, "At", "Astatine", 210.0M));
                    _listaElementi.Add(new Element(86, "Rn", "Radon", 222.0M));
                    _listaElementi.Add(new Element(87, "Fr", "Francium", 233.0M));
                    _listaElementi.Add(new Element(88, "Ra", "Radium", 226.0254M));
                    _listaElementi.Add(new Element(89, "Ac", "Actinium", 227.0M));
                    _listaElementi.Add(new Element(90, "Th", "Thorium", 232.04M));
                    _listaElementi.Add(new Element(91, "Pa", "Protactinium", 231.0359M));
                    _listaElementi.Add(new Element(92, "U", "Uranium", 238.03M));
                    _listaElementi.Add(new Element(93, "Np", "Neptunium", 237.0M));
                    _listaElementi.Add(new Element(94, "Pu", "Plutonium", 244.0M));
                    _listaElementi.Add(new Element(95, "Am", "Americium", 243.0M));
                    _listaElementi.Add(new Element(96, "Cm", "Curium", 247.0M));
                    _listaElementi.Add(new Element(97, "Bk", "Berkelium", 247.0M));
                    _listaElementi.Add(new Element(98, "Cf", "Californium", 251.0M));
                    _listaElementi.Add(new Element(99, "Es", "Einsteinium", 254.0M));
                    _listaElementi.Add(new Element(100, "Fm", "Fermium", 257.0M));
                    _listaElementi.Add(new Element(101, "Md", "Mendelevium", 258.0M));
                    _listaElementi.Add(new Element(102, "No", "Nobelium", 259.0M));
                    _listaElementi.Add(new Element(103, "Lr", "Lawrencium", 262.0M));
                    _listaElementi.Add(new Element(104, "Rf", "Rutherfordium", 260.9M));
                    _listaElementi.Add(new Element(105, "Db", "Dubnium", 261.9M));
                    _listaElementi.Add(new Element(106, "Sg", "Seaborgium", 262.94M));
                    _listaElementi.Add(new Element(107, "Bh", "Bohrium", 262.0M));
                    _listaElementi.Add(new Element(108, "Hs", "Hassium", 264.8M));
                    _listaElementi.Add(new Element(109, "Mt", "Meitnerium", 265.9M));
                    _listaElementi.Add(new Element(110, "Ds", "Darmstadtium", 261.9M));
                    _listaElementi.Add(new Element(112, "Uub", "Ununbium", 276.8M));
                    _listaElementi.Add(new Element(114, "Uuq", "Ununquadium", 289.0M));
                    _listaElementi.Add(new Element(116, "Uuh", "Ununhexium", 0.0M));

                    
                }

                return _listaElementi;
            }
        }

       
    }
}
