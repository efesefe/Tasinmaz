namespace tasinmaz_V3.Models
{
    public class ListParameters
    {
        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        public string searchKey { get; set; } = "";
        // private int _pageSize = 5;
        // public int PageSize
        // {
        //     get{
        //         return _pageSize;
        //     }
        //     set
        //     {
        //         _pageSize = (value > maxPageSize) ? maxPageSize : value;
        //     }
        // }
        
    }
}