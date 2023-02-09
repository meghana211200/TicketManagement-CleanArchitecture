using System;

namespace Domain.DTO;

public class UserDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string UserEmail { get; set; } = null!;
    public string UserPassword { get; set; } = null!;
    public string UserRole { get; set; } = null!;
}

