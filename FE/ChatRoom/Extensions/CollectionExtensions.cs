using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Extensions
{
    public static class CollectionExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            return enumerableList != null ? new ObservableCollection<T>(enumerableList) : null;
        }
    }
}
