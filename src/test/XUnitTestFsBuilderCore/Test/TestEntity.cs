namespace XUnitTestFsBuilderCore.Test
{
    public class TestEntity : EntityBase
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
    }

    public abstract class EntityBase
    {
        public string Id { get; set; }
    }
}