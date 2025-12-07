using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public class MathUtils
    {

        /// <summary>
        /// 是否垂直
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool IsVertical(Vector3 lhs, Vector3 rhs)
        {
            float value = Vector3.Dot(lhs.normalized, rhs.normalized);
            if (Mathf.Abs(value) == 0)
            {
                return true;
            }
            return false;
        }
    }
}