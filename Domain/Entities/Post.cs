//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Final.Enum;

//namespace Final.Entities
//{
//    public class Post
//    {

//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public string Content { get; set; }
//        public EState State { get; set; }
//        public EStatus Status { get; set; }
//        public int CreatorId { get; set; }
//        public User Creator { get; set; }
//        public DateTime CreateDate { get; set; } 

//        public ICollection<Comment> Comments { get; set; }
//        [NotMapped]

//        public int CommentAmount { get; set; }
//    }
//}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Final.Enum;

namespace Final.Entities
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }

        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public EState State{ get; set; }
        public EStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<Comment> Comments { get; set; }
        [NotMapped]
        public int CommentAmount { get; set; }
        [NotMapped]

        public string UserName { get; set; }


    }
}
