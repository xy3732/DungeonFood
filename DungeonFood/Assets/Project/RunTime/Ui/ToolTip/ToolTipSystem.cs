using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : Singleton<ToolTipSystem>
{
    public ToolTip toolTip;

    public static void Show(string context, string header = "")
    {
        instance.toolTip.SetText(context, header);
        instance.toolTip.transform.position = Input.mousePosition;
        instance.toolTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance.toolTip.gameObject.SetActive(false);
    }
}
