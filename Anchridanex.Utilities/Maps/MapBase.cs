#nullable enable

namespace Anchridanex.Utilities.Maps
{
    public abstract class MapBase<T> where T : IMapCell
    {
        public int Width { get; init; }
        public int Height { get; init; }

        public MapBase(int width, int height)
        {
            Width = width;
            Height = height;
        }

        private Dictionary<MapVector2, T> cells = new();

        public void AddCell(T c)
        {
            if (cells.ContainsKey(c.Position))
                cells[c.Position] = c;
            else
                cells.Add(c.Position, c); 
        }

        public T? GetCell(MapVector2 position)
        {
            if (position.X < 1 || position.X > Width || position.Y < 1 || position.Y > Height)
                throw new ArgumentException("Requested cell outside of width or height boundary");

            if (cells.ContainsKey(position))
                return cells[position];
            else
                return default(T);
        }

        protected abstract List<MapAdjacentDirection> GetAdjacentDirections();
        public abstract MapVector2? GetAdjacentCoordinate(MapVector2 position, MapAdjacentDirection direction);

        public T? GetAdjacentCell(T cell, MapAdjacentDirection direction)
        {
            return GetAdjacentCell(cell.Position, direction);
        }

        public T? GetAdjacentCell(MapVector2 position, MapAdjacentDirection direction)
        {
            T? retVal = default(T);
            MapVector2? adjCoord = GetAdjacentCoordinate(position, direction);

            if (adjCoord != null)
                retVal = GetCell((MapVector2)adjCoord);

            return retVal;
        }

        public List<MapVector2> GetAdjacentCoordinates(MapVector2 position)
        {
            if (position.X < 1 || position.X > Width || position.Y < 1 || position.Y > Height)
                throw new ArgumentException("Requested cell outside of width or height boundary");

            List<MapVector2> retVal = new();
            List<MapAdjacentDirection> dirs = GetAdjacentDirections();

            foreach (MapAdjacentDirection d in dirs)
            {
                MapVector2? testCoord = GetAdjacentCoordinate(position, d);
                if (testCoord != null)
                    retVal.Add((MapVector2)testCoord);
            }

            return retVal;
        }

        public List<T> GetAdjacentCells(T cell)
        {
            List<T> retVal = new List<T>();
            List<MapVector2> adjCoords = GetAdjacentCoordinates(cell.Position);

            foreach (MapVector2 ac in adjCoords)
            {
                T? c = GetCell(ac);
                if(c != null)
                    retVal.Add(c);
            }

            return retVal;
        }

        public List<T> GetAllNonNullCells()
        {
            List<T> retVal = new List<T>();
            for(int x = 1; x <= Width; x++)
            {
                for(int y = 1; y <= Height; y++)
                {
                    T? cell = GetCell(new MapVector2(x, y));
                    if (cell != null)
                        retVal.Add(cell);
                }
            }
                
            return retVal;
        }
    }
}
