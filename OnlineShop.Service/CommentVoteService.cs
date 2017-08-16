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
    public interface ICommentVoteService
    {
        CommentVote Add(CommentVote commentVote);
        void Update(CommentVote commentVote);
        void Save();
        void Delete(string curentuserId, int commentId);
        IEnumerable<CommentVote> GetAll();
        int CheckVote(string userId, int commentId);
    }
    public class CommentVoteService : ICommentVoteService
    {
        private ICommentVoteRepository _commentVoteRepository;
        private IUnitOfWork _unitOfWork;
        public CommentVoteService(ICommentVoteRepository commentVoteRepository, IUnitOfWork unitOfWork)
        {
            this._commentVoteRepository = commentVoteRepository;
            this._unitOfWork = unitOfWork;
        }
        public CommentVote Add(CommentVote commentVote)
        {
            return _commentVoteRepository.Add(commentVote);
        }

        public int CheckVote(string userId, int commentId)
        {
            return _commentVoteRepository.Check(userId, commentId);
        }

        public void Delete(string curentuserId, int commentId)
        {
            _commentVoteRepository.DeleteCommentVote(curentuserId,commentId);
        }

        public IEnumerable<CommentVote> GetAll()
        {
            return _commentVoteRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(CommentVote commentVote)
        {
            _commentVoteRepository.Update(commentVote);
        }
    }
}
