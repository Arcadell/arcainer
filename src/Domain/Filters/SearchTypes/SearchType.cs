namespace Domain.Filters.SearchTypes
{
    public class SearchType
    {
        private SearchType(string value) { Value = value; }

        public string Value { get; private set; }

        public static SearchType Equal { get { return new SearchType("="); } }
        public static SearchType Like { get { return new SearchType("like"); } }

        public override string ToString()
        {
            return Value;
        }
    }
}
