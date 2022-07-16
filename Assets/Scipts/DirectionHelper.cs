using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public static class DirectionHelper
    {
        public static Vector2 GetVectorFromDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Vector2(-1, 0);
                case Direction.Right:
                    return new Vector2(1, 0);
                case Direction.Up:
                    return new Vector2(0, 1);
                case Direction.Down:
                    return new Vector2(0, -1);
                default:
                    return new Vector2(0, 0);
            }
        }

        public static DieFace TurnDice(DieFace face, Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    face.RotateLeft();
                    return face;
                case Direction.Right:
                    face.RotateRight();
                    return face;
                case Direction.Up:
                    face.RotateForward();
                    return face;
                case Direction.Down:
                    face.RotateBackward();
                    return face;
                default:
                    return face;
            }
        }
    }
}
