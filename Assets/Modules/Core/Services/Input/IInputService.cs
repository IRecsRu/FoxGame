using Modules.Core.Infrastructure.Services;
using UnityEngine;

namespace Modules.Core.Services.Input
{
  public interface IInputService : IService
  {
    Vector2 Axis { get; }

    bool IsAttackButtonUp();
  }
}