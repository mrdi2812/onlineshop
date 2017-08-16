namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRowCommentVote : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("DeleteRowFromCommentVote",
              p => new
              {
                  userId = p.String(),
                  commentId = p.Int()
              }
              ,
              @"
                delete from CommentVotes where UserId= @userId and CommentId=@commentId"
              );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.DeleteRowFromCommentVote");
        }
    }
}
