using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[ExecuteInEditMode()]
public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI TooltipHeader;
    public TextMeshProUGUI TooltipContent;
    public LayoutElement TooltipLayout;
    public int CharWrapLimit;
    public RectTransform rectTransform;
    //Tooltip 내용 크기에 따라 패널 레이아웃 조정
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        int headerLength = TooltipHeader.text.Length;
        int contentLength = TooltipContent.text.Length;
        TooltipLayout.enabled = (headerLength > CharWrapLimit || contentLength > CharWrapLimit) ? true : false;
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            TooltipHeader.gameObject.SetActive(false);
        }
        else
        {
            TooltipHeader.gameObject.SetActive(true);
            TooltipHeader.text = header;
        }
        
        TooltipContent.text = content;

    }
}
