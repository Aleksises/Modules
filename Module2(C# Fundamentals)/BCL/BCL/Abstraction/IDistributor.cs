using System.Threading.Tasks;

namespace BCL.Abstraction
{
    public interface IDistributor<TModel>
    {
        Task MoveAsync(TModel item);
    }
}
