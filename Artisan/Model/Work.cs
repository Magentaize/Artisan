using Artisan.Interface;
using Artisan.Toolkit;

namespace Artisan.Model
{
    public class Work:NotifyPropertyObject,IWork
    {
        private string _wid;

        public string Wid
        {
            get { return _wid; }
            set { UpdateProperty(ref _wid, value);}
        }

        private string _uid;

        public string Uid
        {
            get { return _uid; }
            set { UpdateProperty(ref _uid, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { UpdateProperty(ref _name, value);}
        }

        private ImageSize _size;

        public ISize Size
        {
            get { return _size; }
            set { UpdateProperty(ref _size, (ImageSize)value);}
        }

        private string _intro;

        public string Intro {
            get { return _intro; }
            set { UpdateProperty(ref _intro, value);}
        }

        private int _sell;

        public int Sell
        {
            get { return _sell; }
            set { UpdateProperty(ref _sell, value);}
        }

        private string _pic;

        public string Pic
        {
            get { return _pic; }
            set { UpdateProperty(ref _pic, value);}
        }

        private int _commentsNum;

        public int CommentsNum
        {
            get { return _commentsNum; }
            set { UpdateProperty(ref _commentsNum, value);}
        }

        private int _likesNum;

        public int LikesNum
        {
            get { return _likesNum; }
            set { UpdateProperty(ref _likesNum, value);}
        }
    }

    public class ImageSize : NotifyPropertyObject, ISize
    {
        private int _height;

        public int Height
        {
            get { return _height; }
            set { UpdateProperty(ref _height, value);}
        }

        private int _width;

        public int Width
        {
            get { return _width; }
            set { UpdateProperty(ref _width, value);}
        }
        public override string ToString()
        {
            return $"{Height}x{Width}";
        }
    }
}