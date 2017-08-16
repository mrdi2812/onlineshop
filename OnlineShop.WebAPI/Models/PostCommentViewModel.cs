using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.WebAPI.Models
{
    public class PostCommentViewModel
    {
        public IEnumerable<CommentViewModel> Comment { set; get; }
        public IEnumerable<CommentVoteViewModel> CommentVote { set; get; }
        public int CountVote { set; get; }
    }
}