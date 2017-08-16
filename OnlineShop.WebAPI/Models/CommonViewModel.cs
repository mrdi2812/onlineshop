using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.WebAPI.Models
{
    public class CommonViewModel
    {
        public int ID { set; get; }
        public string ParentId { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
        public string Content { set; get; }
        public string FullName { set; get; }
        public string Avatar { set; get; }
        public string UserName { set; get; }
        public string UserId { set; get; }
        public bool CreateByAdmin { set; get; }
        public bool CreateByUser { set; get; }
        public int UpVoteCount { set; get; }
        public bool UserHasVote { set; get; }

    }
}