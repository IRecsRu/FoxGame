using System;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Modules.MainMenuScene
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;

        public string Name => _name.text;
        
        public void UpdateInfo(Player player) =>
            _name.text = player.NickName;
    }
}
