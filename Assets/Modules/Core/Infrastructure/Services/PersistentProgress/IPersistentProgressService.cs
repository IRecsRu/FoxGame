using Modules.Core.Data;

namespace Modules.Core.Infrastructure.Services.PersistentProgress
{
  public interface IPersistentProgressService : IService
  {
    PlayerProgress Progress { get; set; }
  }
}