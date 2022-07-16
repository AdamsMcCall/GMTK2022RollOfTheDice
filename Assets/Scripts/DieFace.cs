using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    public class DieFace
    {
        public int Value;
        public DieFace Up;
        public DieFace Down;
        public DieFace Left;
        public DieFace Right;
        public DieFace Opposite;

        public DieFace(int value)
        {
            Value = value;
        }

        public void RotateForward()
        {
            var oldValue = Value;
            var oldUp = Up;
            var oldDown = Down;
            var oldLeft = Left;
            var oldRight = Right;
            var oldOpposite = Opposite;

            Value = oldUp.Value;
            Left = new DieFace(oldLeft.Value);
            Right = new DieFace(oldRight.Value);
            Up = new DieFace(oldOpposite.Value);
            Down = new DieFace(oldValue);
            Opposite = new DieFace(oldDown.Value);
        }

        public void RotateBackward()
        {
            var oldValue = Value;
            var oldUp = Up;
            var oldDown = Down;
            var oldLeft = Left;
            var oldRight = Right;
            var oldOpposite = Opposite;

            Value = oldDown.Value;
            Left = new DieFace(oldLeft.Value);
            Right = new DieFace(oldRight.Value);
            Up = new DieFace(oldValue);
            Down = new DieFace(oldOpposite.Value);
            Opposite = new DieFace(oldUp.Value);
        }

        public void RotateLeft()
        {
            var oldValue = Value;
            var oldUp = Up;
            var oldDown = Down;
            var oldLeft = Left;
            var oldRight = Right;
            var oldOpposite = Opposite;

            Value = oldLeft.Value;
            Right = new DieFace(oldValue);
            Left = new DieFace(oldOpposite.Value);
            Up = new DieFace(oldUp.Value);
            Down = new DieFace(oldDown.Value);
            Opposite = new DieFace(oldRight.Value);
        }

        public void RotateRight()
        {
            var oldValue = Value;
            var oldUp = Up;
            var oldDown = Down;
            var oldLeft = Left;
            var oldRight = Right;
            var oldOpposite = Opposite;

            Value = oldRight.Value;
            Left = new DieFace(oldValue);
            Right = new DieFace(oldOpposite.Value);
            Up = new DieFace(oldUp.Value);
            Down = new DieFace(oldDown.Value);
            Opposite = new DieFace(oldLeft.Value);
        }

        public static DieFace GenerateDice()
        {
            var one = new DieFace(1);
            var two = new DieFace(2);
            var three = new DieFace(3);
            var four = new DieFace(4);
            var five = new DieFace(5);
            var six = new DieFace(6);

            four.Down = six;
            four.Up = one;
            four.Left = five;
            four.Right = three;
            four.Opposite = two;

            return four;
        }
    }
}
