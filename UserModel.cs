using System.ComponentModel.DataAnnotations;

namespace User_Authentication_API
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter the UserName")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
    }
}
