namespace NadinSoft.Domain.Common
{
    public class BasePaging<T>
    {
        public BasePaging()
        {
            Page = 1;
            TakeEntity = 10;
            HowManyShowPageAfterAndBefore = 5;
            Entities = new List<T>();
        }

        public int Page { get; set; }

        public int PageCount { get; set; }

        public int AllEntitiesCount { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int TakeEntity { get; set; }

        public int SkipEntity { get; set; }

        public int HowManyShowPageAfterAndBefore { get; set; }

        public List<T> Entities { get; set; }

        public int Counter { get; set; }

        public PagingViewModel GetCurrentPaging()
        {
            return new PagingViewModel
            {
                EndPage = EndPage,
                Page = Page,
                StartPage = StartPage,
                PageCount = PageCount
            };
        }

        public string GetShownEntitiesPagesTitle()
        {
            if (AllEntitiesCount != 0)
            {
                var startItem = 1;
                var endItem = AllEntitiesCount;

                if (EndPage > 1)
                {
                    startItem = (Page - 1) * TakeEntity + 1;
                    endItem = Page * TakeEntity > AllEntitiesCount ? AllEntitiesCount : Page * TakeEntity;
                }

                return $"Display {startItem} To {endItem} From {AllEntitiesCount}";
            }

            return $"0 Item";
        }

        public BasePaging<T> Paging(IQueryable<T> queryable)
        {
            TakeEntity = TakeEntity;

            var allEntitiesCount = queryable.Count();

            var pageCount = 0;
            try
            {
                pageCount = Convert.ToInt32(Math.Ceiling(allEntitiesCount / (double)TakeEntity));
            }
            catch (Exception)
            {

            }

            Page = Page > pageCount ? pageCount : Page;
            if (Page <= 0) Page = 1;

            AllEntitiesCount = allEntitiesCount;
            HowManyShowPageAfterAndBefore = HowManyShowPageAfterAndBefore;
            SkipEntity = (Page - 1) * TakeEntity;
            StartPage = Page - HowManyShowPageAfterAndBefore <= 0 ? 1 : Page - HowManyShowPageAfterAndBefore;
            EndPage = Page + HowManyShowPageAfterAndBefore > pageCount ? pageCount : Page + HowManyShowPageAfterAndBefore;
            PageCount = pageCount;
            Entities = queryable.Skip(SkipEntity).Take(TakeEntity).ToList();
            Counter = (Page - 1) * TakeEntity + 1;
            return this;
        }
    }

    public class PagingViewModel
    {
        public int Page { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int PageCount { get; set; }
    }
}
