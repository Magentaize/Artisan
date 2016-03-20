using Artisan.Interface;
using Artisan.Toolkit;

namespace Artisan.Model
{
    public class PostListItem:NotifyPropertyObject,IPostListItem
    {
        private User _user;

        public IUser User
        {
            get { return _user; }
            set { UpdateProperty(ref _user, (User)value); }
        }

        private Work _work;

        public IWork Work
        {
            get { return _work; }
            set { UpdateProperty(ref _work, (Work)value); }
        }
    }
}