using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayerDeviceManager
{
    // Stores controllers in the order they pressed Ready
    public static List<InputDevice> ReadyDevices = new List<InputDevice>();

    // Clear when starting a new selection scene
    public static void Clear()
    {
        ReadyDevices.Clear();
    }
}
