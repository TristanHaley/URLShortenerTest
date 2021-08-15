using System.Reflection;
using AutoMapper;

namespace Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        #region Constructors

        public AutoMapperProfile()
        {
            LoadStandardMappings();
            LoadCustomMappings();
            LoadConverters();
        }

        #endregion

        private void LoadConverters() { }

        private void LoadCustomMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom) map.CreateMappings(this);
        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom) CreateMap(map.Source, map.Destination).ReverseMap().MaxDepth(5);
        }
    }
}