namespace OnlineShop.Common.ViewModels
{
    public class CommentVoteViewModel
    {
        public int CommentId { set; get; }
        public string UserId { set; get; }

        public virtual CommentViewModel Comment { set; get; }
        public virtual ApplicationUserViewModel AppUser { set; get; }
    }
}