using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common.ViewModels
{
    public class CommentViewModel
    {
        public int ID { set; get; }
        public string Content { set; get; }
        public int ParentId { set; get; }
        public string AttachImage { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public DateTime ModifiedDate { set; get; }
        public string Ping { set; get; }
        public int ReplyCount { set; get; }
        public bool Status { set; get; }
        public string UserId { set; get; }
        public int PostId { set; get; }  
        public virtual ApplicationUserViewModel AppUser { set; get; }
        public IEnumerable<CommentVoteViewModel> CommentVotes { set; get; }   
    }
}
