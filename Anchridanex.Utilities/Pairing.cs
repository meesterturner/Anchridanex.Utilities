namespace Anchridanex.Utilities
{
    public class Pairing<T> where T : class
    {
        public T? First { get; set; }
        public T? Second { get; set; }

        public bool MatchesInExactOrder(T? one, T? two)
        {
            return First == one && Second == two;
        }

        public bool MatchesInEitherOrder(T? one, T? two)
        {
            return (First == one && Second == two) || (First == two && Second == one);
        }

        public bool Contains(T? either)
        {
            return First == either || Second == either;
        }

        public bool DoesNotContain(T? neither)
        {
            return First != neither && Second != neither;
        }
    }
}
