using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Entities
{
  public class InfluenceBehaviour
  {
    public readonly bool Up;
    public readonly bool Down;
    public readonly bool Left;
    public readonly bool Right;

        public readonly bool Curse;

    public InfluenceBehaviour(bool up, bool down, bool left, bool right, bool curse)
    {
      Up = up;
      Down = down;
      Left = left;
      Right = right;
            Curse = curse;
    }
  }
}
