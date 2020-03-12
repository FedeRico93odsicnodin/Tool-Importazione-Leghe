using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool_Importazione_Leghe.ModifiedElements
{
    /// <summary>
    /// Stopwatch esteso per garantire la funzionalità di recupero nel caso in cui questa procedura
    /// risulterà interrotta
    /// </summary>
    public class ExtendedStopWatch : System.Diagnostics.Stopwatch
    {
        TimeSpan _offset = new TimeSpan();


        public ExtendedStopWatch()
        {
        }


        public ExtendedStopWatch(TimeSpan offset)
        {
            _offset = offset;
        }


        /// <summary>
        /// Inserisco un tempo di partenza per lo stopwatch modificato
        /// </summary>
        /// <param name="offsetElapsedTimeSpan"></param>
        public void SetOffset(TimeSpan offsetElapsedTimeSpan)
        {
            _offset = offsetElapsedTimeSpan;
        }


        public TimeSpan Elapsed
        {
            get { return base.Elapsed + _offset; }
            set { _offset = value; }
        }


        public long ElapsedMillisecond
        {
            get { return base.ElapsedMilliseconds + _offset.Milliseconds; }
        }


        public long ElapsedTicks
        {
            get { return base.ElapsedTicks + _offset.Ticks; }
        }

    }
}
