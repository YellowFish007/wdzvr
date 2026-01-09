using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEngine;

public class UIChat : UIBase
{
    public override string Name => "UIChat";
    
    public UIChatBoxItem chatBoxItem;

    public override void OnOpen()
    {
        chatBoxItem.SetContent("我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭我要吃饭");
    }

}

