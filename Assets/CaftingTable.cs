using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CaftingTable : MonoBehaviour
{
    private PhotonView _view;

    [SerializeField] private Light light;
    [SerializeField] private Transform exit;
    [SerializeField] private GameObject exitObject;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        exit = GameObject.FindWithTag("Exit").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collectables = GameObject.FindGameObjectsWithTag("Collectable");
        if (collectables.Length <= 0)
        {
            _view.RPC("ChangeLightColor", RpcTarget.AllViaServer, Color.green);
            _view.RPC("CraftExit",RpcTarget.AllViaServer);
        }
        else
        {
            _view.RPC("ChangeLightColor", RpcTarget.AllViaServer, Color.red);
        }
    }

    [PunRPC]
    public void CraftExit()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject(exitObject.name,exit.position,Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    public void ChangeLightColor(Color color)
    {
        light.color = color;
    }
}
