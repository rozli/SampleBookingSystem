using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using SampleBookingSystem.Common.Mappings;
using SampleBookingSystem.Data.Models;

namespace SampleBookingSystem.Web.Models.Home
{
    public class RoomViewModel : IMapFrom<Room>, IHaveCustomMappings
    {
        [Required]
        [Range(1, 15)]
        public int Number { get; set; }

        [Required]
        [Range(1, 6)]
        public int Capacity { get; set; }

        [Required]
        [Range(1, 3)]
        public int Floor { get; set; }

        [Required]
        public bool IsFree { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public DateTime BookedFrom { get; set; }

        public DateTime FreeFrom { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Room, RoomViewModel>()
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.RoomNumber));               
        }
    }
}