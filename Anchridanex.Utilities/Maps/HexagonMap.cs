namespace Anchridanex.Utilities.Maps
{
    public class HexagonMap<T> : MapBase<T> where T : IMapCell
    {
        public HexagonMap(int width, int height) : base(width, height)
        {
        }

        protected override List<MapAdjacentDirection> GetAdjacentDirections()
        {
            return new List<MapAdjacentDirection>()
            {
                MapAdjacentDirection.Above, MapAdjacentDirection.Below, 
                MapAdjacentDirection.AboveLeft, MapAdjacentDirection.AboveRight,
                MapAdjacentDirection.BelowLeft, MapAdjacentDirection.BelowRight
            };
        }

        public override MapVector2? GetAdjacentCoordinate(MapVector2 position, MapAdjacentDirection direction)
        {
            MapVector2? retVal = null;

            // Above middle
            if (position.Y > 1 && direction == MapAdjacentDirection.Above)
                retVal = new MapVector2(position.X, position.Y - 1);

            // Below middle
            if (position.Y < Height && direction == MapAdjacentDirection.Below)
                retVal = new MapVector2(position.X, position.Y + 1);

            if (position.X % 2 == 0)
            {
                // If position.X is an even number

                // Above left
                if (position.X > 1 && position.Y > 1 && direction == MapAdjacentDirection.AboveLeft)
                    retVal = new MapVector2(position.X - 1, position.Y - 1);

                // Above right
                if (position.X < Width && position.Y > 1 && direction == MapAdjacentDirection.AboveRight)
                    retVal = new MapVector2(position.X + 1, position.Y - 1);

                // Below left
                if (position.X > 1 && direction == MapAdjacentDirection.BelowLeft)
                    retVal = new MapVector2(position.X - 1, position.Y);

                // Below right
                if (position.X < Width && direction == MapAdjacentDirection.BelowRight)
                    retVal = new MapVector2(position.X + 1, position.Y);
            }
            else
            {
                // If position.X is an odd number

                // Above left
                if (position.X > 1 && direction == MapAdjacentDirection.AboveLeft)
                    retVal = new MapVector2(position.X - 1, position.Y);

                // Above right
                if (position.X < Width && direction == MapAdjacentDirection.AboveRight)
                    retVal = new MapVector2(position.X + 1, position.Y);

                // Below left
                if (position.X > 1 && position.Y < Height && direction == MapAdjacentDirection.BelowLeft)
                    retVal = new MapVector2(position.X - 1, position.Y + 1);

                // Below right
                if (position.X < Width && position.Y < Height && direction == MapAdjacentDirection.BelowRight)
                    retVal = new MapVector2(position.X + 1, position.Y + 1);
            }

            return retVal;
        }
    }
}
