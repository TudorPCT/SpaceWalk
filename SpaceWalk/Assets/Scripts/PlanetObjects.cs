using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlanetObjects : MonoBehaviour
{
    public List<GameObject> objectList;

    public void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var obj in objectList)
            {
                PhotonNetwork.InstantiateRoomObject(obj.name, obj.transform.position, Quaternion.identity);
            }
        }
    }
}