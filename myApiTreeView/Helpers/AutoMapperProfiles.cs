using AutoMapper;
using myApiTreeView.API.Dtos;
using myApiTreeView.Models;

namespace myApiTreeView.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TestCaseDto,TestCase>();
            CreateMap<FolderDto, Folder>();
        }
    }
}