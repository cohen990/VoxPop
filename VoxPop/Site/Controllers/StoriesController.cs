﻿using System.Web.Mvc;

namespace Site.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Models;
    using Ninject.Infrastructure.Language;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BlogModel blog, HttpPostedFileBase image, params string[] pollOptions)
        {
            pollOptions = pollOptions.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            if (!pollOptions.Any() || pollOptions.All(string.IsNullOrWhiteSpace))
            {
                ModelState.AddModelError("pollOptions", "Poll options are required");
            }
            if (pollOptions.Distinct().Count() != pollOptions.Count())
            {
                ModelState.AddModelError("pollOptionsDuplicated", "Poll options must be unique");
            }
            if (image == null)
            {
                ModelState.AddModelError("image", "You must provide an image");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var authorName = ClaimsService.GetAuthenticatedUsersFullName();
            var authorIdentifier = ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey);

            blog.PollOptions = pollOptions.ToList();

            await _blogService.CreateBlogAsync(blog, image, authorName, authorIdentifier);

            return RedirectToAction("Index");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(
            string pollItemKey,
            string blogPostPartitionKey,
            string blogPostRowKey,
            string voterComment,
            string returnUrl = null)
        {
            var model = new VoteModel
            {
                PollItemKey = pollItemKey,
                BlogPostPartitionKey = blogPostPartitionKey,
                BlogPostRowKey = blogPostRowKey,
                UserId = User.Identity.GetUserId(),
                VoterComment = voterComment
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

        [Route("Stories/Comment/{authorIdentifier}/{articleIdentifier}")]
        [Authorize]
        public async Task<ActionResult> Comment(string articleIdentifier, string authorIdentifier)
        {
            BlogModel blog = await _blogService.GetBlog(articleIdentifier, authorIdentifier);

            return View(blog);
        }
    }
}