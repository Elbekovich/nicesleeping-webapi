﻿using NicesleepingShop.DataAccess.Interfaces.Users;
using NicesleepingShop.DataAccess.Utils;
using NicesleepingShop.Domain.Entities.Users;
using NicesleepingShop.Domain.Exception;
using NicesleepingShop.Service.Common.Helpers;
using NicesleepingShop.Service.Common.Security;
using NicesleepingShop.Service.Dtos.Users;
using NicesleepingShop.Service.Interfaces.Users;

namespace NicesleepingShop.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;


    public UserService(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }
    public async Task<long> CountAsync() => await _userRepository.CountAsync();


    public async Task<long> CreateAsync(UserCreateDto userCreateDto)
    {
        User user = new User()
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            PhoneNumber = userCreateDto.PhoneNumber,
            PhoneNumberConfirmed = userCreateDto.PhoneNumberConfirmed,
            Address = userCreateDto.Address,
            //region bo'lishi kerak
            PasswordHash = userCreateDto.PasswordHash,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime(),
            RoleType = userCreateDto.RoleType,
        };
        var hashres = PasswordHasher.Hash(user.PasswordHash);
        user.Salt = hashres.Salt;
        user.PasswordHash = hashres.PasswordHash;


        var res = await _userRepository.CreateAsync(user);
        return res;

    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) throw new NotFoundException();
        var result = await _userRepository.DeleteAsync(id);
        return result > 0;
    }

    public async Task<IList<User>> GetAllAsync(PaginationParams @params)
    {
        var userss = await _userRepository.GetAllAsync(@params);
        return userss;
    }

    public async Task<bool> UpdateAsync(long id, UserUpdateDto userUpdateDto)
    {
        var user = await _userRepository.GetByIdAsync(@id);
        if (user is null) throw new NotFoundException();
        user.FirstName = userUpdateDto.FirstName;
        user.LastName = userUpdateDto.LastName;
        user.PhoneNumber = userUpdateDto.PhoneNumber;
        user.PhoneNumberConfirmed = userUpdateDto.PhoneNumberConfirmed;
        user.Address = userUpdateDto.Address;
        user.PasswordHash = userUpdateDto.PasswordHash;
        user.Salt = userUpdateDto.Salt;
        user.RoleType = userUpdateDto.RoleType;
        user.UpdatedAt = TimeHelper.GetDateTime();
        var rbResult = await _userRepository.UpdateAsync(id, user);
        return rbResult > 0;
    }
}
