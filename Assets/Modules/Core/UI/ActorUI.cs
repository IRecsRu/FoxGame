using System;
using Modules.Core.Logic;
using UnityEngine;

namespace Modules.Core.UI
{
  public class ActorUI : MonoBehaviour
  {
    public HpBar HpBar;

    private IHealth _health;

    public void Construct(IHealth health)
    {
      _health = health;
      _health.HealthChanged += UpdateHpBar;
    }
    
    private void OnDestroy()
    {
      if (_health != null)
        _health.HealthChanged -= UpdateHpBar;
    }

    private void UpdateHpBar() =>
      HpBar.SetValue(_health.Current, _health.Max);

  }
}