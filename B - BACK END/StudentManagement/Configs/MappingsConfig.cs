using AutoMapper;
using Database.Models.Entities;
using SharedService.ViewModels;

namespace StudentManagement.Configs
{
    public class MappingsConfig : Profile
    {
        public MappingsConfig()
        {
            Configure();
        }

        public override string ProfileName => "MappingsConfig";

        public void Configure()
        {
            CreateMap<Student, ProfileViewModel>();
        }
    }
}