namespace Artisan.Interface
{
    public interface IHomePivotListItem
    {
        //Objects of HomePage pivot ListViewItem
        string AuthorName { get; set; } //Set per-post's author's name
        string PostTime { get; set; } //Set per-post's posted time
        string PostInfo { get; set; } //Set per-post's info
        string PostSource { get; set; } //Set per-post's thumbnail image fetch url
    }
}