using OnlineShop.Common.ViewModels;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Service
{
    public interface ICommentService
    {
        Comment Add(Comment commnet);
        void Update(Comment commnet);
        Comment Delete(int id);
        void Save();
        Comment GetById(int id);
        IEnumerable<Comment> GetListCommentByPostId(int postId);
        IEnumerable<ApplicationUser> GetUserByPostId(int postId);
        IEnumerable<CommentVote> GetListCommentVoteById(int commentId);
        IEnumerable<ApplicationUser> GetUserById(string userId);
        Comment GetMaxId();
        IEnumerable<ApplicationUser> GetUserById();
        int Count(int postId);
    }
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        private IUnitOfWork _unitOfWork;
        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            this._commentRepository = commentRepository;
            this._unitOfWork = unitOfWork;
        }
        public Comment Add(Comment commnet)
        {
            return _commentRepository.Add(commnet);
        }

        public int Count(int postId)
        {
            var query = _commentRepository.GetAll().Where(x => x.PostId == postId);
            int total = query.Count();
            return total;
        }

        public Comment Delete(int id)
        {
            return _commentRepository.Delete(id);
        }

        public Comment GetById(int id)
        {
            return _commentRepository.GetSingleById(id);
        }

        public IEnumerable<Comment> GetListCommentByPostId(int postId)
        {
            return _commentRepository.GetMulti(x=>x.PostId==postId);
        }

        public IEnumerable<CommentVote> GetListCommentVoteById(int commentId)
        {
            return _commentRepository.getListVote(commentId);
        }

        public Comment GetMaxId()
        {
           int max = _commentRepository.GetAll().Max(x => x.ID);
           var comment = _commentRepository.GetSingleById(max);
           return comment;
        }

        public IEnumerable<ApplicationUser> GetUserById()
        {
            int max = _commentRepository.GetAll().Max(x => x.ID);
            var comment = _commentRepository.GetSingleById(max);
            IEnumerable<ApplicationUser> user = _commentRepository.GetUserById(comment.UserId);
            return user;
        }

        public IEnumerable<ApplicationUser> GetUserById(string userId)
        {
            return _commentRepository.GetUserById(userId);
        }

        public IEnumerable<ApplicationUser> GetUserByPostId(int postId)
        {
            return _commentRepository.GetListUserByPostId(postId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Comment commnet)
        {
            _commentRepository.Update(commnet);
        }
    }
}
