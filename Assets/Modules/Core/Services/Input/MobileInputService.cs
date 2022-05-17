using UnityEngine;

namespace Modules.Core.Services.Input
{
  public class MobileInputService : InputService
  {
    public override Vector2 Axis => 
      SimpleInputAxis();
  }
}