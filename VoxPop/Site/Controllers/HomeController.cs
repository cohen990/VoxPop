namespace Site.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Models;
    using Services;
    using Storage.Models;

    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: Blog
        public ActionResult Index()
        {
            var blogs = _blogService.GetAllBlogs();

            return View(blogs);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Story(string rowKey, string partitionKey)
        {
            var blogs = _blogService.GetBlog(rowKey, partitionKey);

            return View(blogs);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(BlogViewModel blog, HttpPostedFileBase image, params string[] pollOptions)
        {
            blog.PollOptions = pollOptions.ToList();

            await _blogService.CreateBlogAsync(blog, image);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Vote(
            string pollItemKey,
            string blogPostPartitionKey,
            string blogPostRowKey,
            string returnUrl  = null)
        {
            var model = new VoteModel
            {
                PollItemKey = pollItemKey,
                BlogPostPartitionKey = blogPostPartitionKey,
                BlogPostRowKey = blogPostRowKey
            };

            _blogService.Vote(model);

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}