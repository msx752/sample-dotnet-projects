namespace Samp.Identity.API.Models.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }
        public string Email { get; set; }
    }
}