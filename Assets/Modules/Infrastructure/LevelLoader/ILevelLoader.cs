using System.Threading.Tasks;

namespace Modules.Infrastructure.LevelLoader
{
	public interface ILevelLoader
	{
		Task Initialization();
	}
}