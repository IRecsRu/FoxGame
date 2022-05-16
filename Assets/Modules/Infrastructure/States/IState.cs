using System.Threading.Tasks;

namespace Modules.Infrastructure.States
{
  public interface IState
  {
    Task Enter();
    Task<bool> TryExit();
  }
}