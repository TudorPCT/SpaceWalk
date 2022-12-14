using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public class VisitPlanets : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 6;

    #endregion


    #region Private Fields

    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    string _gameVersion = "1";

    /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
    /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    bool _isConnecting;
    object _planet ;
    #endregion

    #region MonoBehaviour CallBacks

    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    void Start()
    {

    }

    #endregion


    #region Public Methods

    public void Connect()
    {
        _planet = ChoosePlanet._selectedOption;
        if (PhotonNetwork.IsConnected)
        {
            var expectedCustomRoomProperties = new Hashtable() {
                {"planet", _planet }
            };
            PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxPlayersPerRoom);
        }
        else
        {
            _isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        // we don't want to do anything if we are not attempting to join a room.
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (_isConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
            _isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _isConnecting = false;
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        CreateRoom(null, _planet);
    }

    public void CreateRoom(string name, object planet)
    {
        Hashtable roomProps = new Hashtable() { { "planet", planet } };
        string[] roomPropsInLobby = { "planet" };
        RoomOptions roomOptions = new();
        roomOptions.CustomRoomProperties = roomProps;
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.MaxPlayers = maxPlayersPerRoom;
        PhotonNetwork.CreateRoom(name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        if (PhotonNetwork.CurrentRoom.PlayerCount != 1) return;
        Debug.Log($" Scene name : {PhotonNetwork.CurrentRoom.CustomProperties["planet"]}");
        PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.CustomProperties["planet"].ToString());
    }

    #endregion
}
