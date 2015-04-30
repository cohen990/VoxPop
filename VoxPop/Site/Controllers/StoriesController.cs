using System.Web.Mvc;

namespace Site.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Models;
    using Ninject.Infrastructure.Language;
    using Services;
    using System;
    using System.Collections.Generic;
    using Site.Storage.Models;

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

        [ChildActionOnly]
        public ActionResult _CommentsBox(string blogRowKey, string blogPartitionKey)
        {
            var comments = _blogService.GetAllComments(blogRowKey);

            ViewBag.BlogID = blogRowKey;

            ViewBag.BlogAuthID = blogPartitionKey;

            return View(comments);
        }

        public ActionResult AuthorStories(string Auth)
        {
            var blogs = _blogService.GetAuthorBlogs(Auth);

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
            string returnUrl = null)
        {
            var model = new VoteModel
            {
                PollItemKey = pollItemKey,
                BlogPostPartitionKey = blogPostPartitionKey,
                BlogPostRowKey = blogPostRowKey,
                UserId = User.Identity.GetUserId(),
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
        public async Task<ActionResult> StoriesComment(string articleIdentifier, string authorIdentifier, string replyId)
        {
            BlogModel blog = await _blogService.GetBlog(articleIdentifier, authorIdentifier);

            return View(blog);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Comment(
            string pollItemKey,
            string blogPostPartitionKey,
            string blogPostRowKey,

            string commentPollItemKey,
            int commentPollItemIndex,
            string userComment,
            string commentBlogPostPartitionKey,
            string commentBlogPostRowKey,
            string commentId,
            string repliedTo,
            string repliedToUN,

            string returnUrl = null)
        {
            var model2 = new VoteModel
            {
                PollItemKey = pollItemKey,
                BlogPostPartitionKey = blogPostPartitionKey,
                BlogPostRowKey = blogPostRowKey,
                UserId = User.Identity.GetUserId()
            };

            if (userComment == "")
            {
                _blogService.Vote(model2);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");

            }

            if (commentId == "")
            {
                var model = new CommentModel
                {
                    PollItemKey = commentPollItemKey,
                    PollItemIndex = commentPollItemIndex,
                    VotersComment = userComment,
                    BlogPostPartitionKey = commentBlogPostPartitionKey,
                    BlogPostRowKey = commentBlogPostRowKey,
                    UserId = ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey),
                    CommenterName = ClaimsService.GetAuthenticatedUsersFullName(),
                    CommentTimestamp = DateTime.Now.ToString("d:MM:yyy HH:mm:ss.fffff", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                    CommentIdentifier = Guid.NewGuid().ToString("N").Substring(0, 6),
                    ReplyYayOrNay = false,
                    RepliedTo = "",
                    RepliedToUN = "",
                    CommentPic = "BOHICA"
                };

                _blogService.Vote(model2);

                await _blogService.Comment(model);
            }
            else
            {
                var model = new CommentModel
                {
                    PollItemKey = commentPollItemKey,
                    PollItemIndex = commentPollItemIndex,
                    VotersComment = userComment,
                    BlogPostPartitionKey = commentBlogPostPartitionKey,
                    BlogPostRowKey = commentBlogPostRowKey,
                    UserId = ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey),
                    CommenterName = ClaimsService.GetAuthenticatedUsersFullName(),
                    CommentTimestamp = DateTime.Now.ToString("d:MM:yyy HH:mm:ss.fffff", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                    CommentIdentifier = commentId,
                    ReplyYayOrNay = true,
                    RepliedTo = repliedTo,
                    RepliedToUN = repliedToUN,
                    CommentPic = "BOHICA"
                };

                _blogService.Vote(model2);

                await _blogService.Comment(model);
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public static string GetPrettyDate(DateTimeOffset d)
        {
            // 1.
            // Get time span elapsed since the date.
            TimeSpan s = DateTimeOffset.Now.Subtract(d);

            // 2.
            // Get total number of days elapsed.
            int dayDiff = (int)s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            int secDiff = (int)s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }

            // 5.
            // Handle same-day times.
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1min ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0}mins ago",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1hr ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0}hrs ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return "yesterday at " + d.ToString("t");
            }
            if (dayDiff > 1)
            {
                var dt = d.ToString("R");

                return dt.Remove(dt.Length - 3);
            }

            //if (dayDiff < 7)
            //{
            //    return string.Format("{0} days ago",
            //    dayDiff);
            //}
            //if (dayDiff < 31)
            //{
            //    return string.Format("{0} weeks ago",
            //    Math.Ceiling((double)dayDiff / 7));
            //}
            return null;
        }
    }
}