using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JLGame : MonoBehaviour
{
    public static readonly string PLAYER_LIVES = "PlayerLives";
    public static readonly string PLAYER_READY = "IsPlayerReady";
    public static readonly string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";

    private static readonly Color[] _colors =
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.cyan,
        Color.magenta,
        Color.yellow
    };

    public static Color GetColor(int choice)
    {
        if (choice < 0 || choice >= _colors.Length) return Color.black;
        return _colors[choice];
    }
}
