namespace AnnouncementsApi.Wrappers
{
    public class Pagination<T>
    {
        public int? Page { get; set; }
        public int? TotalPage { get; set; }
        public string PageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
        public string NextPageUrl { get; set; }
        public T Data { get; set; }

        public Pagination() { }

    }
}