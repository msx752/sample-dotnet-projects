using System;

namespace SampleProject.Core.Interfaces.DbContexts
{
    public interface IUserEntity : IBaseEntity
    {
        Guid Id { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}