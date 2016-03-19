namespace Artisan.Interface
{
    public interface IDiscoveryPivotListItem
    {
        IUser User { get; set; }
        IWork Work { get; set; }
        //int Id { get; set; }
        //string Title { get; set; }
        //string Intro { get; set; }
        //string Pic { get; set; }
    }
}