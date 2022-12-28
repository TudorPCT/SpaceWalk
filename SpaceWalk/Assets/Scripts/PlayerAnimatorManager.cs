using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviourPunCallbacks
{
    #region MonoBehaviour Callbacks

    [SerializeField]
    private float directionDampTime = 0.25f;
    private Animator _animator;

    // Use this for initialization
    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (!_animator)
        {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!photonView.IsMine) return;
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        Move(h, v);
        photonView.RPC("OnChangeTransform", RpcTarget.All, PhotonNetwork.LocalPlayer, h, v);
    }

    [PunRPC]
    private void OnChangeTransform(Player targetPlayer, float h, float v)
    {
        if (photonView.Owner == targetPlayer)
        {
            Move(h, v);
        }
    }

    private void Move(float h, float v)
    {
        if (!_animator) return;
        if (v < 0)
        {
            v = 0;
        }
        _animator.SetFloat("Speed", h * h + v * v);
        _animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
    }
    #endregion
}

