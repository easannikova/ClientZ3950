namespace USMarcLibrary.Bib1Attributes
{
    public enum Bib1Attr
    {
        Title = 4,
        ISBN = 7,
        LocNumber = 12,
        Date = 30,
        Author = 1003,
        Subject = 21,
    }

    public static class BibAttributes
    {
        public static string GetAttributes(this Bib1Attr attr)
        {
            return $"@attr 1={(int)attr} ";
        }
    }
}
