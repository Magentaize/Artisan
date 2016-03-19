namespace Artisan.Interface
{
    public interface IUserInfo:IUser
    {
        bool IsFollow { get; set; }
        int Works { get; set; }
        int Article { get; set; }
        int Follows { get; set; }
        int Fans { get; set; }
    }
}