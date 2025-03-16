using System.Drawing;

namespace Anchridanex.Utilities.Maps
{
    public struct MapVector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MapVector2() { }
        public MapVector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString();
        }
    }
}
