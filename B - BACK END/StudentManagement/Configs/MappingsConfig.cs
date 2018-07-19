using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Database.Models.Entities;
using StudentManagement.ViewModels.Account;

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