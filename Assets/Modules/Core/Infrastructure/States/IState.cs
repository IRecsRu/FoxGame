using System.Threading.Tasks;

namespace Modules.Core.Infrastructure.States
{
  public interface IState
  {
    Task Enter();
    Task<bool> TryExit();
  }
}