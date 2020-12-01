namespace Methodbrary.System.String
{
    public static class CharExtensions
    {
        public static string Repeat(this char s, int times)
            => new string(s, times);
    }
}
