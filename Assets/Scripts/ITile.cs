using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public interface ITile
    {
        void ApplyTileEffect(int x, int y, int value);

        void Initialize(GameObject gameEnv);

        public bool isAccessible { get; }
    }
}
