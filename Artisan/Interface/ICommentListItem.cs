namespace Artisan.Interface
{
    public interface ICommentListItem
    {
        string CommentAuthorAvatarLink { get; set; } //Set comment's author's avatar's fetch url
        string CommentAuthorName { get; set; } //Set comment's author's name
        string CommentContent { get; set; } //Set comment content
        string CommentSupportedNumber { get; set; } //Set the number of people who supported this comment
    }
}