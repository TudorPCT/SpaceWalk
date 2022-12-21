using Photon.Pun;
using UnityEngine;

namespace com.unity.photon
{
    /// <summary>
    /// Player manager.
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion


        #region MonoBehaviour CallBacks

        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = this.gameObject;
            }
            else
            { 
                var camera = this.gameObject.GetComponentInChildren<Camera>();
                Debug.Log("sunt in else ");
                Debug.Log(camera);
                camera.enabled = false;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            Debug.Log("added new player");

        }

        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
        }

        void OnTriggerStay(Collider other)
        {
            // we dont' do anything if we are not the local player.
            if (!photonView.IsMine)
            {
                return;
            }
        }
        #endregion

    }
}