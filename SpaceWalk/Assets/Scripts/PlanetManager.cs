using Photon.Pun;
using UnityEngine;
using com.unity.photon;
using Photon.Realtime;

public class PlanetManager : MonoBehaviourPunCallbacks
{
    public GameObject _character;
    public GameObject _spawn;
    public static PlanetManager Instance;
    void Start()
    {
        Instance = this;
        if (PlayerManager.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(_character.name, _spawn.transform.position, Quaternion.identity, 0);
        }
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }
    void Update()
    {

    }
}
