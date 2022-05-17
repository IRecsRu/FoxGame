using Modules.Core.Data;

namespace Modules.Core.Infrastructure.Services.PersistentProgress
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerProgress Progress { get; set; }
  }
}