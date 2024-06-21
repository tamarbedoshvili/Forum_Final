using Final.Enum;

namespace Final.Domain.Dto
{
    public class ChangeUserRoleDto
    {
        public EUserRole Role { get; set; }
       public string UserId { get; set; }
    }
}
