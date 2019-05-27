using System.Collections.Generic;

namespace Blog.Api.Command
{
    public interface ICommand<T> where T : class
    {
        IEnumerable<T> WriteTransaction(string queryString, object param);
        bool ExcuteTransaction(string queryString, object param);
    }
}
