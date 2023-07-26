﻿using NicesleepingShop.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace NicesleepingShop.Service.Dtos.Users;

public class UserUpdateDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public bool PhoneNumberConfirmed { get; set; }

    public string Address { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;

    public RoleIdentity RoleType { get; set; }

}
