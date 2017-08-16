using OnlineShop.Common.ViewModels;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<ApplicationUser> GetListUserByPostId(int postId);
        IEnumerable<CommentVote> getListVote(int commentId);
        IEnumerable<Comment> GetListCommentByPostId(int postId);
        IEnumerable<ApplicationUser> GetUserById(string userId);
    }
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
           
        }

        public IEnumerable<Comment> GetListCommentByPostId(int postId)
        {
            var query = from c in DbContext.Comments
                        join cv in DbContext.CommentVotes
                        on c.ID equals cv.CommentId
                        where c.PostId == postId
                        select c;
            return query;
        }

        public IEnumerable<ApplicationUser> GetListUserByPostId(int postId)
        {
            var query = from c in DbContext.Comments
                        where c.PostId == postId
                        select c.AppUser;                       
            return query;
        }

        public IEnumerable<CommentVote> getListVote(int commentId)
        {
            var query = from c in DbContext.CommentVotes
                        where c.CommentId == commentId
                        select c;
            return query;
        }

        public IEnumerable<ApplicationUser> GetUserById(string userId)
        {
            var query = from c in DbContext.Comments
                        where c.UserId == userId
                        select c.AppUser;
            return query;
        }
    }
}
