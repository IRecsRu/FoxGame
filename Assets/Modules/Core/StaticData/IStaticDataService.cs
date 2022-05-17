using System.Threading.Tasks;
using Modules.Core.Infrastructure.Services;

namespace Modules.Core.StaticData
{
	public interface IStaticDataService : IService
	{
		Task<PlayerStaticData> LoadPlayer();
	}
}