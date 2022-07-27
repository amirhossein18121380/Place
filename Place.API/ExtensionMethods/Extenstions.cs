using Place.Application.Dto;
using Place.Domain.Models;

namespace Place.API.ExtensionMethods;

public static class Extenstions
{
    public static FullUserInfoDto? AsDto(this User user, string uploadDirectory = null)
        => user == null
            ? null
            : new FullUserInfoDto()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                IsActive = user.IsActive,
                RegisterDate = user.RegisterDate,
            };
}
