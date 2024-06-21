using Final.Enum;

namespace Final.Domain.Dto
{
    public class ChangePostStateDto
    {
        public int PostID { get; set; }

        public EState State { get; set; }
    }
}
