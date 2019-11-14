namespace Hunter.DataBase.Interfaces
{
    public interface IExists<TKey>
    {
        bool Exists(TKey id);
    }
}
