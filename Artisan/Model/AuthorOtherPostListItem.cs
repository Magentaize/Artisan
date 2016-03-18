using Artisan.Interface;
using Artisan.Toolkit;

namespace Artisan.Model
{
    public class AuthorOtherPostListItem:NotifyPropertyObject, IAuthorOtherPostListItem
    {
        private string _postSource;

        public string PostSource
        {
            get { return _postSource; }
            set { UpdateProperty(ref _postSource,value);}
        }
    }
}