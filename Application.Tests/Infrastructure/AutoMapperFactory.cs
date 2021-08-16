using Application.Infrastructure.AutoMapper;
using AutoMapper;

namespace Application.Tests.Infrastructure
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperProfile()));

            return mappingConfig.CreateMapper();
        }
    }
}