using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.WebAPI.Models
{
    public class CommentVoteViewModel
    {
        public int CommentId { set; get; }
        public string UserId { set; get; }
    }
}