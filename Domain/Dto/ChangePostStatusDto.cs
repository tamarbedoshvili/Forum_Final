using Final.Enum;

namespace Final.Domain.Dto
{
    public class ChangePostStatusDto
    {
        public int PostID { get; set; }

        public EStatus Status { get; set; }
    }
}
