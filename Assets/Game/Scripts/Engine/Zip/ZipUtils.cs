using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public class ZipUtils
    {
        public static void UnZipByByte(byte[] zipData, string destPath, Action<bool, float, string> statusAction)
        {
            ZipSharp zip = new ZipSharp();
            zip.UnZipByByte(zipData, destPath, statusAction);
        }
    }
}