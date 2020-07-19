using System;
using System.Collections.Generic;
using System.Linq;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Dtos;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FreeSqlBuilder.Infrastructure.Extensions
{
    public static class DtoToEntity
    {
        public static Project ToEntity(this ProjectDto dto)
        {
            var project = new Project()
            {
                Id = dto.Id,
                ProjectInfoId = dto.ProjectInfoId,
                ProjectInfo = dto.ProjectInfo,
                GeneratorModeConfig = dto.GeneratorModeConfig,
                GeneratorModeConfigId = dto.GeneratorModeConfigId,
                ProjectBuilders = new List<ProjectBuilder>()
            };
            var builders = new List<ProjectBuilder>();
            if (dto.BuildersId?.Any() ?? false)
            {
                builders.AddRange(dto.BuildersId.Select(x => new ProjectBuilder()
                {
                    BuilderId = x,
                    ProjectId = dto.Id
                }));
            }

            if (dto.GlobalBuildersId?.Any() ?? false)
            {
                builders.AddRange(dto.GlobalBuildersId.Select(x => new ProjectBuilder()
                {
                    BuilderId = x,
                    ProjectId = dto.Id
                }));

            }
            project.ProjectBuilders = builders;

            return project;
        }

    }
}