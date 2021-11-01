using AutoMapper;
using BoardApp.Common.Models;
using BoardApp.DAL.Model;

namespace BoardApp.BLL.Mappings
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<BoardDto, Board>().ReverseMap();
            CreateMap<BoardAccessDto, BoardAccess>().ReverseMap();
            CreateMap<CardDto, Card>().ReverseMap();
            CreateMap<ColumnDto, Column>()
                .BeforeMap((s, d) => { if (s.BoardId == 0) s.BoardId = d.BoardId; })
                .ForMember(x=>x.BoardId, opt => opt.MapFrom(src => src.BoardId))
                .ForMember(x=>x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x=>x.Title, opt => opt.MapFrom(src => src.Title));
            CreateMap<Column, ColumnDto>();
            CreateMap<CommentDto, Comment>()
                .BeforeMap((s, d) => { if (s.CardId == 0) s.CardId = d.CardId; })
                .BeforeMap((s, d) => { if (d.DateTime != default) s.DateTime = d.DateTime; })
                .ForMember(x => x.CardId, opt => opt.MapFrom(src => src.CardId))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.DateTime, opt => opt.MapFrom(src => src.DateTime))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<Comment, CommentDto>();
            CreateMap<LabelDto, Label>().ReverseMap();
            CreateMap<PermissionDto, Permission>().ReverseMap();
            CreateMap<BoardAccessDto, BoardDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
