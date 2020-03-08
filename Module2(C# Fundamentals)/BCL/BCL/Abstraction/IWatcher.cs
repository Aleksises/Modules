using BCL.EventArgs;
using System;

namespace BCL.Abstraction
{
    public interface IWatcher<TModel>
    {
        event EventHandler<CreatedEventArgs<TModel>> Created;
    }
}
