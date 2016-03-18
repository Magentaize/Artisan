using Artisan.Model;

namespace Artisan.Interface
{
    public interface IHomePivotListItem
    {
        //Objects of HomePage pivot ListViewItem
        int Id { get; set; }
        string CreatTime { get; set; } //Set per-post's posted time
        string Text { get; set; } //Set per-post's info
        string Pics { get; set; } //Set per-post's thumbnail image fetch url
        IGeo Geo { get; set; }
        IHomePivotListItemUser User { get; set; }
    }

    /// <summary>
    /// Set per-post's author's detail
    /// </summary>
    public interface IHomePivotListItemUser
    {
        int Uid { get; set; }
        string Name { get; set; }
        string Pic { get; set; }
        int Gender { get; set; }
        IGeo Geo { get; set; }
        string Intro { get; set; }
    }
}