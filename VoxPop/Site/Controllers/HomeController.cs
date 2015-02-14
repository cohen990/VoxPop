namespace Site.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Services;
    using Storage;
    using Storage.Models;
    using ViewModels;

    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;

        public HomeController()
        {
            _blogService = new BlogService();
        }

        // GET: Blog
        public ActionResult Index()
        {
            var blogs = _blogService.GetAll();

            return View(blogs);
        }

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
            var blogs = _blogService.Get(rowKey, partitionKey);

            return View(blogs);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BlogViewModel blog, HttpPostedFileBase image, params string[] pollOptions)
        {
            blog.PollOptions = pollOptions.ToList();

            await _blogService.CreateAsync(blog, image.InputStream);

            return RedirectToAction("Index");
        }

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