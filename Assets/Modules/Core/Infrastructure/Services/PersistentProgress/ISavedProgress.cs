using Modules.Core.Data;

namespace Modules.Core.Infrastructure.Services.PersistentProgress
{
  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }
}