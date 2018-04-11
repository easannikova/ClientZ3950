namespace ZClient.Abstract
{
    public interface IPackageOptionsCollection
    {
        string this[string key] { get; set; }
    }
}