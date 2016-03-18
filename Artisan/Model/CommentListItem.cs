using Artisan.Interface;
using Artisan.Toolkit;

namespace Artisan.Model
{
    public class CommentListItem:NotifyPropertyObject,ICommentListItem
    {
        public CommentListItem() { }

        private string _commentAuthorAvatarLink;
        private string _commentAuthorName;
        private string _commentContent;
        private string _commentSupportedNumber;

        public string CommentAuthorAvatarLink
        {
            get { return _commentAuthorAvatarLink; }
            set { UpdateProperty(ref _commentAuthorAvatarLink, value); }
        }

        public string CommentAuthorName
        {
            get { return _commentAuthorName;}
            set { UpdateProperty(ref _commentAuthorName, value);}
        }

        public string CommentContent
        {
            get { return _commentContent; }
            set { UpdateProperty(ref _commentContent, value); }
        }

        public string CommentSupportedNumber
        {
            get { return _commentSupportedNumber; }
            set { UpdateProperty(ref _commentSupportedNumber, value);}
        }
    }
}