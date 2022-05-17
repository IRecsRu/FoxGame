using Modules.Core.Data;
using Modules.Core.Logic;
using Modules.Core.Services.Input;
using UnityEngine;

namespace Modules.Core.PlayerLogic
{
	[RequireComponent(typeof(PlayerAnimator), typeof(CharacterController))]
	public class PlayerAttack : MonoBehaviour
	{
		private const string HittableLayerName = "Hittable";
		
		private PlayerAnimator _animator;
		private CharacterController _characterController;

		private IInputService _inputService;

		private static int _layerMask;
		private readonly Collider[] _hits = new Collider[3];
		private AttackStats _attackStats = new();

		public void Constructor(IInputService inputService)
		{
			_inputService = inputService;
			_layerMask = 1 << LayerMask.NameToLayer(HittableLayerName);

			_animator = GetComponent<PlayerAnimator>();
			_characterController = GetComponent<CharacterController>();
		}

		private void Update()
		{
			if(_inputService.IsAttackButtonUp() && !_animator.IsAttacking)
				_animator.PlayAttack();
		}

		private void OnAttack()
		{
			PhysicsDebug.DrawDebug(StartPoint() + transform.forward, _attackStats.DamageRadius, 1.0f);
			for( int i = 0; i < Hit(); ++i )
			{
				_hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_attackStats.Damage);
			}
		}

		private int Hit() =>
			Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _attackStats.DamageRadius, _hits, _layerMask);

		private Vector3 StartPoint() =>
			new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);
	}
}