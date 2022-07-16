using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    public interface ITile
    {
        void ApplyTileEffect(int x, int y, int value);
    }
}
