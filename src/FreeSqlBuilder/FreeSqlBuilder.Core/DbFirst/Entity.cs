using FreeSqlBuilder.Core.WordsConvert;

namespace FreeSqlBuilder.Core.DbFirst
{
    public class Entity : BuilderOptions
    {
        public Entity() : base("Entity",  outputPath: "Entity", preFix: "", suffix: "", isIgnorePrefix: true, mode: ConvertMode.None)
        {

        }
        public Column Column { get; set; }
    }
}
