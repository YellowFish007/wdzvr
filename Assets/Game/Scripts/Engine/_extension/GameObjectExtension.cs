using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Engine
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// 查找一个游戏物体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static GameObject Find(this GameObject obj, string path)
        {
            return obj.transform.Find(path).gameObject;
        }

        /// <summary>
        /// 设置父物体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="trans"></param>
        public static void SetParent(this GameObject obj, Transform trans)
        {
            obj.transform.SetParent(trans);
            obj.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 设置父物体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        public static void SetParent(this GameObject obj, GameObject obj2)
        {
            obj.transform.SetParent(obj2.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        public static void SetLocalPos(this GameObject obj, float x, float y)
        {
            obj.transform.localPosition = new Vector2(x, y);
        }
        public static void SetLocalPos(this GameObject obj, float x, float y, float z)
        {
            obj.transform.localPosition = new Vector3(x, y, z);
        }
        public static void SetLocalScale(this GameObject obj, float x, float y, float z)
        {
            obj.transform.localScale = new Vector3(x, y, z);
        }
        public static void ClearChildren(this GameObject obj)
        {
            Transform trans = obj.transform;

            int childCount = trans.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Object.Destroy(trans.GetChild(i).gameObject);
            }
        }

        public static GameObject AddPrefab(this GameObject parentObj, string prefabName)
        {
            GameObject prefab = Asset.LoadAssetSync<GameObject>(prefabName);
            GameObject obj = Object.Instantiate(prefab);
            obj.SetParent(parentObj);
            return obj;
        }
        /// <summary>
        /// 隐藏所有子物体
        /// </summary>
        /// <param name="parentObj"></param>
        public static void HideChildren(this GameObject parentObj)
        {
            Transform trans = parentObj.transform;

            int childCount = trans.childCount;

            for (int i = 0; i < childCount; i++)
            {
                trans.GetChild(i).gameObject.SetActive(false);
            }
        }

        public static void SetActive(this Button btn, bool isActive)
        {
            btn.gameObject.SetActive(isActive);
        }
        public static void SetActive(this TMP_Text text, bool isActive)
        {
            text.gameObject.SetActive(isActive);
        }
        public static void SetActive(this Image img, bool isActive)
        {
            img.gameObject.SetActive(isActive);
        }
        public static void SetActive(this Transform trans, bool isActive)
        {
            trans.gameObject.SetActive(isActive);
        }

    }
}