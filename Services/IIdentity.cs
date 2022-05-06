namespace AutoPartsApp.Services
{
    public interface IIdentity<TTarget>
    {
        void Logout();
        TTarget StrongTarget { get; set; }
        TTarget WeakTarget { get; set; }
    }
}
