using System.Threading.Tasks;

namespace Action.Common.Mongo
{
    public interface IDatabaseSeeder
    {
         Task SeedAsync();
    }
}