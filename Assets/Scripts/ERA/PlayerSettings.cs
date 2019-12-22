using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ERA
{
    /// <summary>
    /// Postavke lokalnog igrača
    /// </summary>
    [Serializable]
    public class PlayerSettings
    {
        public string Character;
        public Decimal Volume;
        public ControlsEnum Controls;
        /// <summary>
        /// Google autentifikacija. Klasu User potrebno napraviti
        /// </summary>
        public string User = "Android Demo User";
        
    }
    /// <summary>
    /// Enumeracija za izbor kontrola na androidu. Moguće kontrolirati pomoću swipe-a ili tipka.
    /// </summary>
    [Serializable]
    public enum ControlsEnum
    {
        Swipe,
        Button
    }
}
