using Final.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Final.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public int PostID { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }

        [NotMapped]
        public string UserName { get; set; }
    }
}
