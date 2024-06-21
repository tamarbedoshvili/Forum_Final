//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;

//namespace Final.Entities
//{
//    public class Comment
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }
//        public int PostID { get; set; }
//        public string Content { get; set; }
//        public DateTime CreateDate { get; set; } 
//        [JsonIgnore]

//        public Post Post { get; set; }

//        public User User { get; set; }
//        public int UserId { get; set; }

//    }
//}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int PostID { get; set; }
        //[JsonIgnore]
        public Post Post { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
