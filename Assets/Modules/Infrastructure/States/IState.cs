using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Modules.Infrastructure.States
{
  public interface IState: IExitableState
  {
    UniTask Enter();
  }

  public interface IPayloadedState<TPayload> : IExitableState
  {
    UniTask Enter(TPayload payload);
  }
  
  public interface IExitableState
  {
     bool TryExit();
  }
}