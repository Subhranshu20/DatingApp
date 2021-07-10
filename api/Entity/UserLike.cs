namespace api.Entity
{
    public class UserLike
    {
        public ApiUser SourceUser { get; set; }
        public int SourceUserID{get;set;}
        public ApiUser LikeUser { get; set; }
        public int LikeUserID{get;set;}

    }
}