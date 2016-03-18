namespace Artisan.Interface
{
    public interface IUserInfo
    {
        int Uid { get; set; }
        string Name { get; set; }
        string Pic { get; set; }
        int Gender { get; set; }
        IGeo Geo { get; set; }
        string Intro { get; set; }
        int Works { get; set; }
        int Article { get; set; }
        int Follows { get; set; }
        int Fans { get; set; }
    }
}