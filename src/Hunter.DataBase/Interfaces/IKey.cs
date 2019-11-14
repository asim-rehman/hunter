namespace Hunter.DataBase.Interfaces
{
    public interface IKey<out TKey>
    {
        TKey Id { get; }
    }
}
