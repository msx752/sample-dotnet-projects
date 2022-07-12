using System;

namespace Samp.Core.Interfaces
{
    public interface IUserEntity : BaseEntity
    {
        Guid Id { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}