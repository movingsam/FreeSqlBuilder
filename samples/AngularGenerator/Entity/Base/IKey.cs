namespace GRES.Framework.Entity
{
    /// <summary>
    /// 主键接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IKey<out TKey>
    {
        TKey Id { get; }
    }
}
