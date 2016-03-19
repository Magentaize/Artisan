namespace Artisan.Interface
{
    public interface IUser
    {
        string Uid { get; set; }
        string NickName { get; set; }
        string HeadPic { get; set; }
        int Gender { get; set; }
        IGeo Geo { get; set; }
        string Intro { get; set; }
    }
}