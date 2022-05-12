using Cysharp.Threading.Tasks;

namespace Modules.Infrastructure.LevelLoader
{
	public interface ILevelLoader
	{
		UniTask Initialization();
	}
}