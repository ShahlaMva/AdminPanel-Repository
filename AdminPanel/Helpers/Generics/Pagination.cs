namespace AdminPanel.Helpers.Generics
{
    public class Pagination<T>
    {
        public T Datas { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public Pagination(T datas, int totalPage, int currentPage)
        {
            Datas = datas;
            TotalPage = totalPage;
            CurrentPage = currentPage;

        }

        public bool HasNext {
            get
            {
                return CurrentPage < TotalPage;
            }
        }
          
         public bool HasPrevios
        {
            get
            {
                return CurrentPage > 1;
            }
        }   

    }
}
