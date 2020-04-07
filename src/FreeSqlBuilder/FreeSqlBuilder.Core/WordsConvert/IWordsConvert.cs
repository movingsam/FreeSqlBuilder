namespace FreeSqlBuilder.Core.WordsConvert
{
    public interface IWordsConvert
    {
        string Name { set; }
        string Convert(string words);
    }
}