using UnityEngine;

[System.Serializable]
public class ConfigData
{
    public bool Gamepad = false;
    public bool Autoshoot = false;
    public ConfigData()
    {
        Gamepad = Config.Gamepad;
        Autoshoot = Config.Autoshoot;
    }
}
