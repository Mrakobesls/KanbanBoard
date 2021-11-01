using AutoMapper;
using BoardApp.Common.Models;
using BoardApp.WebApi.Models;
using BoardApp.WebApi.Models.RequestModels;
using BoardApp.WebApi.Models.RequestModels.Comments;
using BoardApp.WebApi.Models.ResponseModels.Comments;
using BoardApp.WebApi.Models.RequestModels.Column;
using BoardApp.WebApi.Models.ResponseModels;
using BoardApp.WebApi.Models.ResponseModels.Column;
using System.Collections.Generic;

namespace BoardApp.WebApi.Mapping
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<RegisterUserRequest, UserDto>();
            CreateMap<AddCardRequest, CardDto>();
            CreateMap<UpdateCardRequest, CardDto>();
            CreateMap<UserDto, UserModel>();
            CreateMap<EditCommentRequest, CommentDto>();
            CreateMap<AddCommentRequest, CommentDto>();
            CreateMap<CommentDto, GetCommentResponse>();
            CreateMap<CardDto, CardModel>();
            CreateMap<CardDto, CardGetMembersResponse>();
            CreateMap<IList<UserDto>, List<UserModel>>();
            CreateMap<BoardModel, BoardDto>();
            CreateMap<BoardDto, BoardsGetByIdResponse>();
            CreateMap<BoardDto, BoardsGetByUserIdResponse>();
            CreateMap<BoardAccessDto, BoardModel>();
            CreateMap<UserDto, BoardsMemberListResponse>();
            CreateMap<AddUserRequest, BoardAccessDto>();
            CreateMap<EditColumnRequest, ColumnDto>();
            CreateMap<AddColumnRequest, ColumnDto>();
            CreateMap< ColumnDto, GetColumnResponse>();
            CreateMap<LabelDto, LabelModel>();
        }
    }
}
