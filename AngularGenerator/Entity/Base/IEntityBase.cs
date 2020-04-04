namespace GRES.Framework.Entity
{
    /// <summary>
    /// 实体基类接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntityBase<out TKey> : IKey<TKey>
    {
    }
}
