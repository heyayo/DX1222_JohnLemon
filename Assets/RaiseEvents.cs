using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class RaiseEvents : MonoBehaviour
{
    public enum EVENT_INDEX : byte
    {
        NULL_EVENT = 0,
        BOMB_EXPLOSION
    }

    public delegate void OnExplode(string position);
    public static event OnExplode explodeEvent;

    private void OnEnable()
    { PhotonNetwork.AddCallbackTarget(this); }

    private void OnDisable()
    { PhotonNetwork.RemoveCallbackTarget(this); }

    public void OnEvent(EventData data)
    {

    }
}
