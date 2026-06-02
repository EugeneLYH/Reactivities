using System;
using Domain;

namespace Application.Interfaces;

public interface IUserAccessor
{
    string GetUserId();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<User> GetUserAsync();
}
