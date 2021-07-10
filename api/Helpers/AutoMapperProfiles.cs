using System;
using System.Linq;
using api.DTOs;
using api.Entity;
using api.Extentions;
using AutoMapper;

namespace api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApiUser,MemberDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDto>();
            CreateMap<MemberUpdateDto,ApiUser>();
            CreateMap<RegisterDtos,ApiUser>();
            CreateMap<Message,MessageDto>()
            .ForMember(dest => dest.SenderPhotoUrl , opt=> opt.MapFrom(src=> src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.RecipientPhotoUrl , opt=> opt.MapFrom(src=> src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
           
        //    CreateMap<DateTime,DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d,DateTimeKind.Utc));
        }
    }
}