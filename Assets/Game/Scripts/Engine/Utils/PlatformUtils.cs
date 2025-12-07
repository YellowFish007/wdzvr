using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public class PlatformUtils
    {
        public static string GetDeviceID()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }
}