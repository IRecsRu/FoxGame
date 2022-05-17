using Modules.Core.Data;

namespace Modules.Core.Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}