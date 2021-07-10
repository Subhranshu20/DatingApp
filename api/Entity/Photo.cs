

using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entity
{
    [Table("Photos")]
    public class Photo
    {
        
        public int Id { get; set; }
        public string Url { get; set; }

        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public ApiUser ApiUser{get;set;}
        public int ApiUserId{get;set;}
    }
}