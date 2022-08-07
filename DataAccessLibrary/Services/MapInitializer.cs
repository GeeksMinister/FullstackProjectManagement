﻿public class MapperInitializer : Profile
{
    public MapperInitializer()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<UserStory, UserStoryDto>().ReverseMap();
    }
}