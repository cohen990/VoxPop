using System.Web.Mvc;

namespace Site.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Helpers;
    using Microsoft.AspNet.Identity;
    using Models;
    using Services;

    public class StoriesController : Controller
    {
        private readonly IBlogService _blogService;

        public StoriesController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            var blogs = _blogService.GetAllBlogs();

            return View(blogs);
        }

        public ActionResult AuthorStories(string authorIdentifier)
        {
            var blogs = _blogService.GetAuthorBlogs(authorIdentifier);

            return View(blogs);
        }

        [Route("Stories/{authorIdentifier}/{articleIdentifier}")]
        public async Task<ActionResult> Stories(string articleIdentifier, string authorIdentifier)
        {
            var blog = await _blogService.GetBlog(articleIdentifier, authorIdentifier);

            return View(blog);
        }

        [Route("Stories/Edit/{authorIdentifier}/{articleIdentifier}")]
        [Authorize]
        public async Task<ActionResult> Edit(string articleIdentifier, string authorIdentifier)
        {
            if (ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey) != authorIdentifier)
            {
                return View("Error");
            }

            BlogModel blog = await _blogService.GetBlog(articleIdentifier, authorIdentifier);

            return View(blog);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(BlogModel blog, HttpPostedFileBase image, params string[] pollOptions)
        {
            var authorName = ClaimsService.GetAuthenticatedUsersFullName();
            var authorIdentifier = ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey);

            blog.PollOptions = pollOptions.ToList();

            await _blogService.CreateBlogAsync(blog, image, authorName, authorIdentifier);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Vote(
            string pollItemKey,
            string blogPostPartitionKey,
            string blogPostRowKey,
            string returnUrl = null)
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(BlogModel updatedBlog)
        {
            if (ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey) != updatedBlog.AuthorIdentifier)
            {
                return View("Error");
            }

            _blogService.UpdateBlog(updatedBlog);

            // TODO: shitty hack - remove
            var targetUri = string.Format("~/Stories/{0}/{1}", updatedBlog.AuthorIdentifier, updatedBlog.BlogIdentifier);

            return Redirect(targetUri);
        }
    }
}