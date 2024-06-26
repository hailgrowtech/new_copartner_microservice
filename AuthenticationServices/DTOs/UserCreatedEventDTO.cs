﻿namespace AuthenticationService.DTOs;

public class UserCreatedEventDTO
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string? Mobile { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

