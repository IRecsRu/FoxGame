using Modules.Core.Infrastructure.States;
using Modules.Core.PhotonService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Modules.LoginScene
{
    public class LoginPanel : MonoBehaviour
    {
        private const string NickNameKey = "NickName";
    
        [SerializeField] private TMP_InputField _nickName;
        [SerializeField] private Button _joinButton;
    
        private PhotonSettings _photonSettings;
        private GameStateMachine _gameStateMachine;
    
        [Inject]
        private void Constructor(PhotonSettings photonSettings, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _photonSettings = photonSettings;
        }

        public void Initialization()
        {
            if(PlayerPrefs.HasKey(NickNameKey))
                _nickName.text = PlayerPrefs.GetString(NickNameKey);
        
            _joinButton.onClick.AddListener(OnJoin);
        }
    
        private async void OnJoin()
        {
            string nickName = _nickName.text;

            if(string.IsNullOrEmpty(nickName))
                nickName = "TestUser" + Random.Range(0, 2000);
            else
                PlayerPrefs.SetString(NickNameKey, nickName);

            _photonSettings.SetNickName(nickName);
            _joinButton.interactable = false;
            await _gameStateMachine.Enter<MaineMenuState>();
        }


    }
}
