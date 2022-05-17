using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    private const string PlayerKey = "Player";
    
    [SerializeField] private Transform _spawnPoint;
    
    public async Task<GameObject> Create() =>
        await PhotonNetwork.Instantiate(PlayerKey, _spawnPoint.position, Quaternion.identity);
}
