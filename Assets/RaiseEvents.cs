using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RaiseEvents : MonoBehaviour, IOnEventCallback
{
    public enum EVENT_INDEX : byte
    {
        NULL_EVENT = 0,
        BOMB_EXPLOSION,
        KILL_OBJECT,
        END_GAME
    }

    public delegate void OnExplode(string position);
    public static event OnExplode explodeEvent;

    private void OnEnable()
    { PhotonNetwork.AddCallbackTarget(this); }

    private void OnDisable()
    { PhotonNetwork.RemoveCallbackTarget(this); }

    public void OnEvent(EventData data)
    {
        var code = data.Code;
        switch ((EVENT_INDEX)code)
        {
            case EVENT_INDEX.BOMB_EXPLOSION:
            {
                explodeEvent?.Invoke(data.CustomData.ToString());
                break;
            }
            case EVENT_INDEX.END_GAME:
            {
                JLGameManager.Instance.endingScript.CaughtPlayer();
                break;
            }
        }
    }
}
