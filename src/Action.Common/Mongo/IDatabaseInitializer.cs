using System.Threading.Tasks;

namespace Action.Common.Mongo
{
    public interface IDatabaseInitializer
    {
         Task InitializeAsync();
    }
}