﻿using System;

namespace Domain.DTO;

public class LoginDTO
{
    public string email { get; set; } = null!;
    public string password { get; set; } = null!;
}

