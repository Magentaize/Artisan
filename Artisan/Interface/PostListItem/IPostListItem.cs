namespace Artisan.Interface
{
    public interface IPostListItem
    {
        IUser User { get; set; }
        IWork Work { get; set; } 
    }
}