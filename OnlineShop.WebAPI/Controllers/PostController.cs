using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineShop.Common;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.App_Start;
using OnlineShop.WebAPI.Infrastructure.Core;
using OnlineShop.WebAPI.Infrastructure.Extensions;
using OnlineShop.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.WebAPI.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        IPostService _postService;
        IPostCategoryService _postCategoryService;
        ICommentService _commentService;
        ICommentVoteService _commentVoteService;
        private ApplicationUserManager _userManager;
        public PostController(IPostService postService, IPostCategoryService postCategoryService, ICommentService commentService, ICommentVoteService commentVoteService, ApplicationUserManager userManager)
        {
            _postService = postService;
            _postCategoryService = postCategoryService;
            _commentService = commentService;
            _userManager = userManager;
            _commentVoteService = commentVoteService;
        }
        public ActionResult Category(int id, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.GetAllByCategoryPaging(id, page, pageSize, out totalRow);
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var category = _postCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<PostCategory, PostCategoryViewModel>(category);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }
        public ActionResult Detail(int postId)
        {          
            int top = int.Parse(ConfigHelper.GetByKey("Top"));
            var postModel = _postService.GetById(postId);
            if (postModel != null)
            {
                postModel.ViewCount += 1;
                _postService.Update(postModel);
                _postService.SaveChanges();
            }          
            var viewModel = Mapper.Map<Post, PostViewModel>(postModel);
            var relatedPost = _postService.GetReatePosts(postId, top);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(relatedPost);
            ViewBag.PostId = postId;
            int total = _commentService.Count(postId);          
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var userNameId = User.Identity.GetUserName();
                ViewBag.UserId = userId;
                ViewBag.UserNameId = userNameId;
                ViewBag.TotalCount = total;
            }
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_postService.GetListTagByPostID(postId));
            return View(viewModel);
        }
        public JsonResult GetAll(int postId)
        {
            var users = _commentService.GetUserByPostId(postId);
            var userVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(users);
            var comments = _commentService.GetListCommentByPostId(postId);
            foreach (var item in comments)
            {
                var vote = _commentService.GetListCommentVoteById(item.ID);
                var voteVm = Mapper.Map<IEnumerable<CommentVote>, IEnumerable<CommentVoteViewModel>>(vote);
            }
            var model = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
            if (model != null)
            {
                return Json(new
                {
                    data = model,
                    status = true
                },JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false
            },JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Insert(CommentViewModel commentObj)
        {
            commentObj.CreateBy = User.Identity.GetUserName();
            commentObj.CreateDate = DateTime.Now;
            commentObj.ModifiedDate = DateTime.Now;
            commentObj.ReplyCount = 0;
            commentObj.UserId = User.Identity.GetUserId();
            commentObj.Status = true;
            if (ModelState.IsValid)
            {
                var comment = new Comment();
                comment.UpdateComment(commentObj);
                _commentService.Add(comment);
                _commentService.Save();
                var model = _commentService.GetMaxId();
                var user = _commentService.GetUserById(model.UserId);
                var userVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(user);
                var commentVm = Mapper.Map<Comment, CommentViewModel>(model);
                commentObj.AppUser = commentVm.AppUser;
                commentObj.ID = commentVm.ID;
            }

            return Json(new
                {
                    data=commentObj,
                    status = true
                }, JsonRequestBehavior.AllowGet);                          
          
        }
        [HttpPost]
        public JsonResult Update(CommentViewModel commentObj)
        {
            var comment = _commentService.GetById(commentObj.ID);
            comment.Content = commentObj.Content;
            comment.ModifiedDate = DateTime.Now;
            _commentService.Update(comment);
            _commentService.Save();
            var user = _commentService.GetUserById(comment.UserId);
            var userVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(user);
            var comments = _commentService.GetListCommentByPostId(comment.PostId);
            foreach (var item in comments)
            {
                var vote = _commentService.GetListCommentVoteById(item.ID);
                var voteVm = Mapper.Map<IEnumerable<CommentVote>, IEnumerable<CommentVoteViewModel>>(vote);
            }
            var commentVm = Mapper.Map<Comment, CommentViewModel>(comment);
            commentObj.CreateBy = commentVm.CreateBy;
            commentObj.CreateDate = commentVm.CreateDate;
            commentObj.ModifiedDate = DateTime.Now;
            commentObj.ReplyCount = commentVm.ReplyCount;
            commentObj.UserId = commentVm.UserId;
            commentObj.Status = commentVm.Status;
            commentObj.ParentId = commentVm.ParentId;
            commentObj.PostId = commentVm.PostId;
            commentObj.AppUser = commentVm.AppUser;
            commentObj.CommentVotes = commentVm.CommentVotes;
            return Json(new
            {
                data=commentObj
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Vote(int commentId,string currentUserId)
        {
            int checkvote = _commentVoteService.CheckVote(currentUserId, commentId);
            var vote = new CommentVote();
            vote.CommentId = commentId;
            vote.UserId = User.Identity.GetUserId();
            var listVote = _commentVoteService.GetAll();
            if (checkvote == 0)
            {
                _commentVoteService.Add(vote);
                _commentVoteService.Save();

                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _commentVoteService.Delete(currentUserId, commentId);
                _commentVoteService.Save();
            }
            return Json(new
            {
                status = false
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult CheckVote(string currentUserId,int commentId)
        {
            int total = _commentVoteService.CheckVote(currentUserId, commentId);
            if (total > 0)
            {
                return Json(new
                {
                    data=total,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                data = total,
                status = false
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult Detele(int id)
        {
            if (ModelState.IsValid)
            {
                _commentService.Delete(id);
                _commentService.Save();
            }
            return Json("");
        }

    }
}
