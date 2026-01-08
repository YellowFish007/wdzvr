using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour
{
    public UIRoot uiRoot;

    // Start is called before the first frame update
    void Start()
    {
        uiRoot.OpenUI(UIConfig.Login);
    }

}
