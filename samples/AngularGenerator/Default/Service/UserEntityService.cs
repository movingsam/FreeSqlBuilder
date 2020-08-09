//*******************************
// 创建者 UnKnow
// 创建日期 2020-08-10 00:17
// 创建引擎 FreeSqlBuilder
//*******************************
using AutoMapper;
using Default.Dto;
using Default.PageRequest;
using Default.PageViewDto;
using Default.RequestDto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestEntity.Entities;

namespace Default.Service
{

    ///<summary>
    /// 服务
    ///</summary>
    public class UserEntityService
    {
        private readonly IFreeSql _freeSql;
        private readonly ILogger<UserEntityService> _logger;
        private readonly IMapper _mapper;

        ///<summary>
        /// 构造函数
        ///</summary>
        public UserEntityService(IServiceProvider service, ILogger<UserEntityService> logger)
        {
            this._freeSql = service.GetService<IFreeSql>();
            _logger = logger;
            _mapper = service.GetService<IMapper>();
        }


        ///<summary>
        /// 新增
        ///</summary>
        public async Task<bool> NewUserEntityService(UserEntityDto dto)
        {

            using var uow = _freeSql.CreateUnitOfWork();
            var rep = _freeSql.GetRepository<UserEntity>();
            rep.UnitOfWork = uow;
            var entity = _mapper.Map<UserEntity>(dto);
            var insert = await rep.InsertAsync(entity);
            uow.Commit();
            return true;

        }

        ///<summary>
        /// 修改
        ///</summary>
        public async Task<bool> UpdateUserEntity(UserEntityDto dto)
        {
            using var uow = _freeSql.CreateUnitOfWork();
            var rep = _freeSql.GetRepository<UserEntity>();
            rep.UnitOfWork = uow;
            var entity = _mapper.Map<UserEntity>(dto);
            rep.Update(entity);
            uow.Commit();
            return true;

        }
        ///<summary>
        /// 删除
        ///</summary>
        public async Task<bool> DeleteUserEntity(List<long> ids)
        {

            using var uow = _freeSql.CreateUnitOfWork();
            var rep = _freeSql.GetRepository<UserEntity>();
            rep.UnitOfWork = uow;
            await rep.DeleteAsync(x => ids.Contains(x.Id));
            uow.Commit();
            return true;

        }
        ///<summary>
        /// 分页查询
        ///</summary>
        public async Task<UserEntityPageViewDto> QueryUserEntityPage(UserEntityPageRequest request)
        {

            var rep = _freeSql.GetRepository<UserEntity>();
            var datas = await rep.Select
                         .WhereIf(request.Id != null, x => x.Id == request.Id)
                         .WhereIf(!string.IsNullOrWhiteSpace(request.Account), x => x.Account.Contains(request.Account))
                         .WhereIf(!string.IsNullOrWhiteSpace(request.Password), x => x.Password.Contains(request.Password))
                         .WhereIf(request.Gender != null, x => x.Gender == request.Gender)
                         .WhereIf(!string.IsNullOrWhiteSpace(request.NickName), x => x.NickName.Contains(request.NickName))
                         .Count(out var total)
                         .Page(request.PageNumber, request.PageSize)
                         .ToListAsync<UserEntityDto>();
            var page = new UserEntityPageViewDto(datas, total, request.PageNumber, request.PageSize);
            return page;

        }

        public async Task<UserEntityDto> QueryUserEntity(UserEntityRequestDto request)
        {

            var rep = _freeSql.GetRepository<UserEntity>();
            var res = await rep.Select.WhereIf(request.Id != null, x => x.Id == request.Id)
                            .WhereIf(!string.IsNullOrWhiteSpace(request.Account), x => x.Account.Contains(request.Account))
                            .WhereIf(!string.IsNullOrWhiteSpace(request.Password), x => x.Password.Contains(request.Password))
                            .WhereIf(request.Gender != null, x => x.Gender == request.Gender)
                            .WhereIf(!string.IsNullOrWhiteSpace(request.NickName), x => x.NickName.Contains(request.NickName))
                            .ToOneAsync<UserEntityDto>();
            return res;

        }
    }
}