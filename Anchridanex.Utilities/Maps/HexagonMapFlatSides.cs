namespace Anchridanex.Utilities.Maps
{
    public class HexagonMapFlatSides<T> : MapBase<T> where T : IMapCell
    {
        public HexagonMapFlatSides(int width, int height) : base(width, height)
        {
        }

        protected override List<MapAdjacentDirection> GetAdjacentDirections()
        {
            return new List<MapAdjacentDirection>()
            {
                MapAdjacentDirection.AboveLeft, MapAdjacentDirection.AboveRight,
                MapAdjacentDirection.Left, MapAdjacentDirection.Right,
                MapAdjacentDirection.BelowLeft, MapAdjacentDirection.BelowRight
            };
        }

        public override MapVector2? GetAdjacentCoordinate(MapVector2 position, MapAdjacentDirection direction)
        {
            MapVector2? retVal = null;

            // Left
            if (position.X > 1 && direction == MapAdjacentDirection.Left)
                retVal = new MapVector2(position.X - 1, position.Y);

            // Right
            if (position.X < Width && direction == MapAdjacentDirection.Left)
                retVal = new MapVector2(position.X - 1, position.Y);

            if (position.Y % 2 == 0)
            {
                // If position.Y is an even number

                // Above left
                if (position.Y > 1 && direction == MapAdjacentDirection.AboveLeft)
                    retVal = new MapVector2(position.X, position.Y - 1);

                // Above right
                if (position.X < Width && position.Y > 1 && direction == MapAdjacentDirection.AboveRight)
                    retVal = new MapVector2(position.X + 1, position.Y - 1);

                // Below left
                if (position.Y < Height && direction == MapAdjacentDirection.BelowLeft)
                    retVal = new MapVector2(position.X, position.Y + 1);

                // Below right
                if (position.X < Width && position.Y < Height && direction == MapAdjacentDirection.BelowRight)
                    retVal = new MapVector2(position.X + 1, position.Y + 1);
            }
            else
            {
                // If position.Y is an odd number

                // Above left
                if (position.X > 1 && position.Y > 1 && direction == MapAdjacentDirection.AboveLeft)
                    retVal = new MapVector2(position.X - 1, position.Y - 1);

                // Above right
                if (position.Y > 1 && direction == MapAdjacentDirection.AboveRight)
                    retVal = new MapVector2(position.X, position.Y - 1);

                // Below left
                if (position.X > 1 && position.Y < Height && direction == MapAdjacentDirection.BelowLeft)
                    retVal = new MapVector2(position.X - 1, position.Y + 1);

                // Below right
                if (position.Y < Height && direction == MapAdjacentDirection.BelowRight)
                    retVal = new MapVector2(position.X, position.Y + 1);
            }


            return retVal;
        }


    }
}
