namespace Site.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models;
    using Services;

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

        [Route("Story/{partitionKey}/{rowKey}")]
        public async Task<ActionResult> Story(string rowKey, string partitionKey)
        {
            var blog = await _blogService.GetBlog(rowKey, partitionKey);

            return View(blog);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(BlogModel blog, HttpPostedFileBase image, params string[] pollOptions)
        {
            var userName = User.Identity.GetUserName();

            var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();

            var author = await userManager.GetFullName(userName);

            blog.PollOptions = pollOptions.ToList();

            await _blogService.CreateBlogAsync(blog, image, author);

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
                BlogPostRowKey = blogPostRowKey,
                UserId = User.Identity.GetUserId()
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