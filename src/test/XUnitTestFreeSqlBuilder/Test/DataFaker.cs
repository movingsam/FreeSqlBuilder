using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Bogus;
using FreeSql;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.WordsConvert;

namespace XUnitTestFsBuilderProject
{
    public static class DataFaker
    {
        public static Faker<ProjectInfo> GetProjectInfo() => new Faker<ProjectInfo>()
                .RuleFor(x => x.Author, f => f.Random.Words(f.Random.Number(1, 2)))
                .RuleFor(x => x.NameSpace, x => x.Random.Words(x.Random.Number(1, 2)))
                .RuleFor(x => x.RootPath, f => Directory.GetCurrentDirectory())
                .RuleFor(x => x.OutPutPath, x => "Test");

        public static Faker<ProjectInfo> GetProjectInfo(long id) => GetProjectInfo().RuleFor(x => x.Id, id);


        public static Faker<DataSource> GetDataSource() => new Faker<DataSource>()
                .RuleFor(x => x.ConnectionString, "Data Source=fsbuilder.db;Version=3")
                .RuleFor(x => x.Name, f => f.Random.Words(3))
                .RuleFor(x => x.DbType, DataType.Sqlite);

        public static Faker<DataSource> GetDataSource(long id) => GetDataSource().RuleFor(x => x.Id, id);

        /// <summary>
        /// 获取实体源
        /// </summary>
        /// <returns></returns>
        public static Faker<EntitySource> GetEntitySource() => new Faker<EntitySource>().RuleFor(x => x.EntityAssemblyName, string.Empty)
                .RuleFor(x => x.Name, f => f.Random.Words(2))
                .RuleFor(x => x.EntityBaseName, typeof(IKey<>).FullName);
        public static Faker<GeneratorModeConfig> GetDbFirstConfig() => new Faker<GeneratorModeConfig>()
                .RuleFor(x => x.GeneratorMode, GeneratorMode.DbFirst)
                .RuleFor(x => x.Name, f => f.Random.Words(2));

        public static Faker<GeneratorModeConfig> SetDataSource(this Faker<GeneratorModeConfig> faker, long id) =>
            faker.RuleFor(x => x.DataSourceId, id);

        public static Faker<GeneratorModeConfig> SetDataSourceRandom(this Faker<GeneratorModeConfig> faker,
            IEnumerable<long> ids) => faker.RuleFor(x => x.DataSourceId, f => f.PickRandom(ids));

        public static Faker<GeneratorModeConfig> SetEntitySource(this Faker<GeneratorModeConfig> faker, long id) =>
            faker.RuleFor(x => x.EntitySourceId, id);
        public static Faker<GeneratorModeConfig> SetEntitySourceRandom(this Faker<GeneratorModeConfig> faker, IEnumerable<long> id) =>
            faker.RuleFor(x => x.EntitySourceId, f => f.PickRandom(id));
        public static Faker<GeneratorModeConfig> GetDbFirstConfig(long id) => GetDbFirstConfig().RuleFor(x => x.Id, id);

        public static Faker<GeneratorModeConfig> GetCodeFirstConfig() => new Faker<GeneratorModeConfig>()
            .RuleFor(x => x.GeneratorMode, GeneratorMode.CodeFirst)
            .RuleFor(x => x.Name, f => f.Random.Words(2));

        public static Faker<GeneratorModeConfig> GetCodeFirstConfig(long id) =>
            GetCodeFirstConfig().RuleFor(x => x.Id, id);



        public static Faker<BuilderOptions> GetBuilderOption(IEnumerable<long> templateids) => new Faker<BuilderOptions>().RuleFor(x=>x.TemplateId,f=>f.PickRandom(templateids))
            .RuleFor(x=>x.Mode, ConvertMode.None)
            .RuleFor(x=>x.Name,f=>f.Random.Words(2))
            .RuleFor(x=>x.OutPutPath,t=>t.Random.Words())
            .RuleFor(x=>x.Type,f=>f.PickRandom<BuilderType>());

         
    }
}