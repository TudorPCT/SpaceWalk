using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

    public void Map(Vector3 position, Quaternion rotation)
    {
        rigTarget.SetPositionAndRotation(position, rotation);
    }
}

public class VRRig : MonoBehaviourPunCallbacks
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;

    public Vector3 headBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        UpdateVRConstraints();
        var headPosition = head.rigTarget.position;
        var headRotation = head.rigTarget.rotation;
        var leftHandPosition = leftHand.rigTarget.position;
        var leftHandRotation = leftHand.rigTarget.rotation;
        var rightHandPosition = rightHand.rigTarget.position;
        var rightHandRotation = rightHand.rigTarget.rotation;
        photonView.RPC("OnChangeConstraints", RpcTarget.All, PhotonNetwork.LocalPlayer, headPosition, headRotation, leftHandPosition, leftHandRotation, rightHandPosition, rightHandRotation);
    }

    [PunRPC]
    private void OnChangeConstraints(Player targetPlayer, Vector3 headPosition, Quaternion headRotation, Vector3 leftHandPosition,
        Quaternion leftHandRotation, Vector3 rightHandPosition, Quaternion rightHandRotation)
    {
        if (photonView.Owner == targetPlayer)
        {
            UpdateVRConstraints();
            UpdateVRConstraints(headPosition, headRotation, leftHandPosition, leftHandRotation, rightHandPosition, rightHandRotation);
        }
    }

    private void UpdateVRConstraints()
    {
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }

    private void UpdateVRConstraints(Vector3 headPosition, Quaternion headRotation, Vector3 leftHandPosition,
        Quaternion leftHandRotation, Vector3 rightHandPosition, Quaternion rightHandRotation)
    {
        head.Map(headPosition, headRotation);
        leftHand.Map(leftHandPosition, leftHandRotation);
        rightHand.Map(rightHandPosition, rightHandRotation);
    }
}
