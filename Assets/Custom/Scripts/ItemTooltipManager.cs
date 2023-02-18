using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTooltipManager : MonoBehaviour
{
    private static ItemTooltipManager current;
    public ItemTooltip ItemTooltip;
    
    public void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        current.ItemTooltip.SetText(content, header);
        current.ItemTooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.ItemTooltip.gameObject.SetActive(false); 
    }
}
