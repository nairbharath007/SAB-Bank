namespace E_Bank.Models
{
    public class PageParameters
    {
        const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;

        public int PageSize
        {
            get { return _pageSize; }

            set
            {
                _pageSize = (value > 0) ? value : 5;
            }
        }
    }
}
