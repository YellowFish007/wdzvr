using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Engine
{
    public static class ImageExtension
    {
        /// <summary>
        /// 设置图片的Sprite
        /// </summary>
        /// <param name="img">图片组件</param>
        /// <param name="iconName">图标名称</param>
        public static void SetSprite(this Image img, string iconName,bool setNative = true)
        {
            Asset.LoadSpriteAsync(iconName, delegate (Sprite sp)
            {
                img.sprite = sp;
                if (setNative)
                {
                    img.SetNativeSize();
                }
            });
        }
    }
}
