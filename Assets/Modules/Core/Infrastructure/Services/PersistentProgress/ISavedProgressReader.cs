using Modules.Core.Data;

namespace Modules.Core.Infrastructure.Services.PersistentProgress
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }
}