using System;
using System.Collections.Generic;
using System.Text;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;

namespace FreeSqlBuilder.Repository
{
    /// <summary>
    /// 项目仓储
    /// </summary>
    public interface IProjectRepository : IRepositoryBase<Project, long>
    {
    }
}
