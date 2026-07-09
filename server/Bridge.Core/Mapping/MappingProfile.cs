using AutoMapper;
using Bridge.Core.Models;
using Bridge.Core.Resources;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResource>()
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src =>
                src.Location.Y))
            .ForMember(dest => dest.Lng, opt => opt.MapFrom(src =>
                src.Location.X));

            CreateMap<UserResource, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Location, opt => opt.MapFrom((src, dest) =>
            src.Lat.HasValue && src.Lng.HasValue
             ? new Point(src.Lng.Value, src.Lat.Value) { SRID = 4326 }
             : dest.Location))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Request, RequestResource>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom
    (src => src.User != null ? src.User.Name : null))
    .ForMember(dest => dest.Lat, opt => opt.MapFrom
    (src => src.Location != null ? src.Location.Y : (double?)null))
    .ForMember(dest => dest.Lng, opt => opt.MapFrom
    (src => src.Location != null ? src.Location.X : (double?)null))
    .ForMember(dest => dest.Category, opt => opt.MapFrom
    (src => src.Category)); 

            CreateMap<RequestResource, Request>()
    .ForMember(dest => dest.Id, opt => opt.Ignore())     
    .ForMember(dest => dest.User, opt => opt.Ignore())    
    .ForMember(dest => dest.Category, opt => opt.Ignore())
    .ForMember(dest => dest.Location, opt => opt.MapFrom((src, dest) =>
        src.Lat.HasValue && src.Lng.HasValue
            ? new Point(src.Lng.Value, src.Lat.Value) { SRID = 4326 }
            : dest.Location))
    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Answer, AnswerResource>().ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom
                (src => src.User.Name)).ForMember(
                dest => dest.RequestTitle,
                opt => opt.MapFrom
                (src => src.Request.Title))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<AnswerResource, Answer>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.User, opt => opt.Ignore())
           .ForMember(dest => dest.Request, opt => opt.Ignore())
           .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
               srcMember != null &&
               (!(srcMember is Guid g) || g != Guid.Empty) && 
               (!(srcMember is string s) || !string.IsNullOrEmpty(s)) 
           ));

           CreateMap<Category, CategoryResource>().ReverseMap();
        } 
    }
}
