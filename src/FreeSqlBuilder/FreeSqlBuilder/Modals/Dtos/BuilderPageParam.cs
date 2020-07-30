using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;

namespace FreeSqlBuilder.Modals.Dtos
{
    public class BuilderPageParam : PageRequest
    {
        public BuilderType? BuilderType { get; set; }
    }
}