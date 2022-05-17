using UnityEngine;

namespace Modules.Core.PlayerLogic
{
  [RequireComponent(typeof(PlayerHealth), typeof(PlayerAttack), typeof(PlayerAnimator))]
  public class PlayerDeath : MonoBehaviour
  {
    private PlayerHealth _health;
    private PlayerMove _move;
    private PlayerAttack _attack;
    private PlayerAnimator _animator;

    public GameObject DeathFx;
    private bool _isDead;

    private void Awake()
    {
      _health = GetComponent<PlayerHealth>();
      _move = GetComponent<PlayerMove>();
      _attack = GetComponent<PlayerAttack>();
      _animator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
      _health.HealthChanged += HealthChanged;
    }

    private void OnDestroy()
    {
      _health.HealthChanged -= HealthChanged;
    }

    private void HealthChanged()
    {
      if (!_isDead && _health.Current <= 0) 
        Die();
    }

    private void Die()
    {
      _isDead = true;
      
      if(_move != null)
        _move.enabled = false;
      
      _attack.enabled = false;
      _animator.PlayDeath();

      Instantiate(DeathFx, transform.position, Quaternion.identity);
    }
  }
}