namespace Artisan.Interface
{
    public interface IDiscoveryPivotListItem
    {
        IUserInfo[] Users { get; set; }
        IWork[] Works { get; set; }
        //int Id { get; set; }
        //string Title { get; set; }
        //string Intro { get; set; }
        //string Pic { get; set; }
    }
}