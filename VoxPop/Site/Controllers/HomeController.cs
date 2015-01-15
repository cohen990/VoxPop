namespace Site.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Models;
    using Services;

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

        [HttpPost]
        public ActionResult Create(BlogViewModel blog, params string[] pollOptions)
        {
            blog.PollOptions = pollOptions.ToList();

            _blogService.CreateAsync(blog);

            return RedirectToAction("Index");
        }
    }
}