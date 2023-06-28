using SchuseOnlineShop.Helpers;

namespace SchuseOnlineShop.ViewModels.Blog
{
    public class BlogVM
    {
        public List<Models.Blog> Blogs { get; set; }
        public Models.Blog Blog { get; set; }

        public Paginate<Models.Blog> PaginateDatas { get; set; }

    }
}
