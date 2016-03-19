namespace Artisan.Interface
{
    public interface IWork
    {
        string Wid { get; set; }
        string Uid { get; set; }
        string Name { get; set; }
        ISize Size { get; set; }
        string Intro { get; set; }
        int Sell { get; set; }
        string Pic { get; set; }
        int CommentsNum { get; set; }
        int LikesNum { get; set; }
    }

    public interface ISize
    {
        int Height { get; set; }
        int Width { get; set; }
    }
}