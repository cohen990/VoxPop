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
            ViewBag.BlogID = blogRowKey;

            ViewBag.BlogAuthID = blogPartitionKey;

            var comments = _blogService.GetAllComments(blogRowKey);

            return View(comments);
        }

        [ChildActionOnly]
        public ActionResult _ResponsesBox(string blogRowKey)
        {
            var responses = _blogService.GetAllResponses(blogRowKey);

            //ViewBag.BlogID = blogRowKey;

            return View(responses);
        }

        [ChildActionOnly]
        public ActionResult _DidYouVote(string blogRowKey)
        {
            var votes = _blogService.GetAllVotes(blogRowKey);

            return View(votes);
        }

        [ChildActionOnly]
        public async Task<ActionResult> _IfIAmAResponseBox(string articleIdentifier, string authorIdentifier)
        {
            var response = await _blogService.GetResponse(articleIdentifier, authorIdentifier);

            return View(response);
        }

        public ActionResult AuthorStories(string AuthUn)
        {
            var blogs = _blogService.GetAuthorBlogs(AuthUn);

            return View(blogs);
        }

        [Route("Stories/{authorIdentifier}/{articleIdentifier}")]
        public async Task<ActionResult> Stories(string articleIdentifier, string authorIdentifier)
        {
            var blog = await _blogService.GetBlog(articleIdentifier, authorIdentifier);

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
            var blogIdentifier = Guid.NewGuid().ToString("N");

            blog.PollOptions = pollOptions.ToList();

            await _blogService.CreateBlogAsync(blog, image, authorName, authorIdentifier, blogIdentifier);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateResponse(string replyeeTitle, string replyee, string replyeeBlogIdentifier, string replyeeIdentifier)
        {
            ViewBag.replyeeTitle = replyeeTitle;
            ViewBag.replyee = replyee;
            ViewBag.replyeeBlogIdentifier = replyeeBlogIdentifier;
            ViewBag.replyeeIdentifier = replyeeIdentifier;

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateResponse(
            BlogModel blog,
            ResponseModel response,
            HttpPostedFileBase image,
            string replyeeTitle,
            string replyee,
            string replyeeBlogIdentifier,
            string replyeeIdentifier,
            params string[] pollOptions)
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
            var sharedBlogIdentifier = Guid.NewGuid().ToString("N");

            blog.PollOptions = pollOptions.ToList();

            await _blogService.CreateBlogAsync(blog, image, authorName, authorIdentifier, sharedBlogIdentifier);

            await _blogService.CreateResponseAsync(
                response,
                image,
                authorName,
                authorIdentifier,
                replyeeTitle,
                replyee,
                replyeeBlogIdentifier,
                replyeeIdentifier,
                sharedBlogIdentifier);

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

        [Route("Stories/Edit/{authorIdentifier}/{articleIdentifier}")]
        [Authorize]
        public async Task<ActionResult> Edit(string articleIdentifier, string authorIdentifier)
        {
            if (ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey) != authorIdentifier)
            {
                return View("Error");
            }

            if (_blogService.GetResponse(articleIdentifier, authorIdentifier) != null)
            {
                BlogModel blog = await _blogService.GetBlog(articleIdentifier, authorIdentifier);
                ResponseModel response = await _blogService.GetResponse(articleIdentifier, authorIdentifier);

                return View(blog);

            }
            else
            {
                BlogModel blog = await _blogService.GetBlog(articleIdentifier, authorIdentifier);

                return View(blog);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(BlogModel updatedBlog, ResponseModel updatedResponse)
        {
            if (ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey) != updatedBlog.AuthorIdentifier)
            {
                return View("Error");
            }

            _blogService.UpdateBlog(updatedBlog);

            if (updatedResponse != null)
            {
                _blogService.UpdateResponse(updatedResponse);
            }

            // TODO: shitty hack - remove
            var targetUri = string.Format("~/Stories/{0}/{1}", updatedBlog.AuthorIdentifier, updatedBlog.BlogIdentifier);

            return Redirect(targetUri);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BlogModel blog, ResponseModel response)
        {
            if (ClaimsService.GetClaim(VoxPopConstants.IdentifierClaimKey) != blog.AuthorIdentifier)
            {
                return View("Error");
            }

            // TODO: shitty hack - remove
            var targetUri = string.Format("~/Stories/AuthorStories?AuthUn={0}", blog.AuthorIdentifier);

            _blogService.DeleteYourBlog(blog);

            if(response != null)
            {
                _blogService.DeleteYourResponse(response);
            }

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

            string returnUrl = null,
            params string[] pollOptions)
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
                    CommentTimestamp = DateTime.Now.ToString("d:MM:yyy HH:mm:ss.ffffff", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                    CommentIdentifier = Guid.NewGuid().ToString("N").Substring(0, 6),
                    ReplyYayOrNay = false,
                    RepliedTo = "",
                    RepliedToUN = "",
                    CommentPic = "BOHICA",
                    PollOptions = pollOptions.ToList()
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
                    CommentTimestamp = DateTime.Now.ToString("d:MM:yyy HH:mm:ss.ffffff", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                    CommentIdentifier = commentId,
                    ReplyYayOrNay = true,
                    RepliedTo = repliedTo,
                    RepliedToUN = repliedToUN,
                    CommentPic = "BOHICA",
                    PollOptions = pollOptions.ToList()
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

        public static string GetPrettyDateQualifier(DateTimeOffset d)
        {
            TimeSpan s = DateTimeOffset.Now.Subtract(d);

            int dayDiff = (int)s.TotalDays;

            if (dayDiff > 1)
            {
                var on = "on";

                return on;
            }
            return null;
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

                return dt.Remove(dt.Length - 7);
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