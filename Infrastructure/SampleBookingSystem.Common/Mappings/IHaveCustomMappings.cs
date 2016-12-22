using AutoMapper;

namespace SampleBookingSystem.Common.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
