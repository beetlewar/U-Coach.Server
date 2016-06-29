﻿using System;
using AutoMapper;
using PVDevelop.UCoach.Server.Core.Domain;
using PVDevelop.UCoach.Server.Core.Mongo;
using PVDevelop.UCoach.Server.Core.Service;

namespace PVDevelop.UCoach.Server.Core.AutoMapper
{
    public class SportsmanConfirmationProfile : Profile
    {
#warning разобраться с Obsolete
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<CreateSportsmanConfirmationParams, Auth.WebDto.CreateUserParams>();
            CreateMap<CreateSportsmanConfirmationParams, ProduceConfirmationKeyParams>();
            CreateMap<SportsmanConfirmation, MongoSportsmanConfirmation>();
            CreateMap<MongoSportsmanConfirmation, SportsmanConfirmation>();
        }
    }
}
