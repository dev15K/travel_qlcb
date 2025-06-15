namespace QLCB.Models.ViewModels
{
    public class UserWithRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string CurrentRole { get; set; }
        public List<string> AllRoles { get; set; } = new();
    }
}

