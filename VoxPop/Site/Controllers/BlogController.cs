namespace Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Models;
    using Services;

    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController()
        {
            _blogService = new BlogService();
        }

        // GET: Blog
        public async Task<ActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();

            return View(blogs);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogViewModel blog)
        {
            _blogService.CreateAsync(blog);

            return RedirectToAction("Index");
        }
    }
}