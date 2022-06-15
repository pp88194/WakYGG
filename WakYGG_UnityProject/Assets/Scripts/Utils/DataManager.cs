using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyData
{
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode rollKey = KeyCode.LeftShift;
    public KeyCode guardKey = KeyCode.LeftAlt;
}
public static class DataManager
{
    public static KeyData keyData = new KeyData();

}
