using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.UWP.Helpers
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Covert an IEnumerable collection to ObservableCollection
        /// </summary>
        /// <typeparam name="T">IEnumerable of Type</typeparam>
        /// <param name="coll">Source Collection</param>
        /// <returns>ObservableCollection of Type</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            return new ObservableCollection<T>(coll);
        }
    }
}
