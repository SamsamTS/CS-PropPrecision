using ICities;

using System;

using ColossalFramework;
using ColossalFramework.UI;

namespace PropPrecision
{
    public class ModInfo : IUserMod
    {
        public string Name
        {
            get { return "Prop Precision " + version; }
        }

        public string Description
        {
            get { return "More precision for props"; }
        }

        public const string version = "1.0.1";
    }
}
