using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ERA
{
    /// <summary>
    /// Lokalno spremanje najboljih rezultata
    /// </summary>
    [Serializable]
    public class Score
    {
        /// <summary>
        /// Vrijeme prolaska staze
        /// </summary>
        public float RaceTime;
        /// <summary>
        /// Datum i vrijeme utrke
        /// </summary>
        public DateTime RunDate;
        /// <summary>
        /// Mapa utrke
        /// </summary>
        public Map Map;
        /// <summary>
        /// Postavke i igrač koji se korišteni za navedenu stazu.
        /// </summary>
        public PlayerSettings PlayerSettings;
    }
}
