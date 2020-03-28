namespace GRES.Framework.Entity
{
    /// <summary>
    /// 逻辑删除接口
    /// </summary>
    public interface IDeleted
    {
        bool IsDeleted { get; }
    }
}
