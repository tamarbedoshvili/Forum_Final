using Final.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Dto
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }

        public string CreatorId { get; set; }
        public EState State { get; set; }
        public EStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<CommentDto> Comments { get; set; }
        [NotMapped]
        public int CommentAmount { get; set; }
        [NotMapped]

        public string UserName { get; set; }
    }
}
