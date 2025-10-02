using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkour2D360.StateManagment
{
    public interface IScreenFactory
    {
        GameScreen CreateScreen(Type screenType);
    }
}
