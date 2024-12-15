namespace Anchridanex.Utilities.Maps
{
    public class RectangleMap<T> : MapBase<T> where T : IMapCell
    {
        public RectangleMap(int width, int height) : base(width, height)
        {
        }

        protected override List<MapAdjacentDirection> GetAdjacentDirections()
        {
            return new List<MapAdjacentDirection>()
            {
                MapAdjacentDirection.Above, MapAdjacentDirection.Below, MapAdjacentDirection.Left, MapAdjacentDirection.Right
            };
        }

        public override MapVector2? GetAdjacentCoordinate(MapVector2 position, MapAdjacentDirection direction)
        {
            MapVector2? retVal = null;

            // Above
            if (position.Y > 1 && direction == MapAdjacentDirection.Above)
                retVal = new MapVector2(position.X, position.Y - 1);

            // Below
            if (position.Y < Height && direction == MapAdjacentDirection.Below)
                retVal = new MapVector2(position.X, position.Y + 1);

            // Left
            if (position.Y > 1 && direction == MapAdjacentDirection.Left)
                retVal = new MapVector2(position.X - 1, position.Y);

            // Right
            if (position.Y < Width && direction == MapAdjacentDirection.Right)
                retVal = new MapVector2(position.X + 1, position.Y);

            return retVal;
        }
    }
}
