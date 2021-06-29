using UnityEngine;
public static class Config
{
    public static bool Gamepad = true;
    public static bool Autoshoot = false;
    public static void Load(ConfigData data)
    {
        Gamepad = data.Gamepad;
        Autoshoot = data.Autoshoot;
    }
}