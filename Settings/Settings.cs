using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.Settings
{
    public struct Settings
    {
        public KeyboardOptions KeyboardOptions { get; set; }

        public Settings()
        {
            KeyboardOptions = KeyboardOptions.MovementOnAWSD;
        }
    }
}
