namespace AoC.Console.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<string> WithoutNullOrWhiteSpace(this IEnumerable<string> enumerable)
            => enumerable.Where(x => !string.IsNullOrWhiteSpace(x));
    }
}
