using ITISHub.Core.Models;
using ITISHub.Persistence.Entities;
using AutoMapper;

namespace ITISHub.Persistence;

public class DataBaseMappings: Profile
{
    public DataBaseMappings()
    {
        CreateMap<UserEntity, User>();
    }
}