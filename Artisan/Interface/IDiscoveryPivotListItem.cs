namespace Artisan.Interface
{
    public interface IDiscoveryPivotListItem
    {
        IUser[] Users { get; set; }
        IWork[] Works { get; set; }
        //int Id { get; set; }
        //string Title { get; set; }
        //string Intro { get; set; }
        //string Pic { get; set; }
    }
}