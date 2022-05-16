using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Modules.MainMenuScene.RoomPanel
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        private Player _player;
        
        public string Name => _player.NickName;
        public string ID => _player.UserId;
        
        public void UpdateInfo(Player player)
        {
            _player = player;
            _name.text = player.NickName;
        }
    }
}
