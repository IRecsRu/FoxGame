using System.Threading.Tasks;

namespace Modules.Core.Infrastructure.LevelLoader
{
	public interface ILevelLoader
	{
		Task Initialization();
	}
}