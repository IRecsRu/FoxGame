using Modules.Core.Data;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Modules.Core.PlayerLogic
{
  [RequireComponent(typeof(CharacterController))]
  public class PlayerMove : MonoBehaviour, ISavedProgress
  {
    private CharacterController _characterController;
    
    private MoveStats _moveStats = new MoveStats();

    private IInputService _inputService;
    private Camera _camera;

    public void Constructor(IInputService inputService)
    {
      _inputService = inputService;
      _characterController = GetComponent<CharacterController>();
    }

    private void Start() =>
      _camera = Camera.main;

    private void Update()
    {
      Vector3 movementVector = Vector3.zero;

      if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
      {
        movementVector = _camera.transform.TransformDirection(_inputService.Axis);
        movementVector.y = 0;
        movementVector.Normalize();

        transform.forward = movementVector;
      }

      movementVector += Physics.gravity;

      _characterController.Move( _moveStats.MoveSpeed * movementVector * Time.deltaTime);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
      progress.MoveStats = _moveStats;
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

      _moveStats = progress.MoveStats;
      Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
      if (savedPosition != null) 
        Warp(to: savedPosition);
    }

    private static string CurrentLevel() => 
      SceneManager.GetActiveScene().name;

    private void Warp(Vector3Data to)
    {
      _characterController.enabled = false;
      transform.position = to.AsUnityVector().AddY(_characterController.height);
      _characterController.enabled = true;
    }
  }
}