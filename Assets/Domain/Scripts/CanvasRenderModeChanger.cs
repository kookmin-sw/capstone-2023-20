using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class CanvasRenderModeChanger : MonoBehaviour
{
    // 렌더 모드를 변경할 캔버스
    public Canvas canvas;
    public bool state;

    private Canvas tempCanvas;
    private Transform pos;
    private RectTransform rectTransform;
    private RectTransform tempTransform;
    // 변경할 렌더 모드
    public RenderMode renderMode;

    // 캔버스 정보를 저장할 변수
    private Vector2 size;
    private Vector3 position;
    private Quaternion rotation;
    private Vector3 localScale;

    void Start()
    {
        // 캔버스의 렌더 모드를 변경
        canvas.renderMode = renderMode;
        state = false;

        rectTransform = canvas.GetComponent<RectTransform>();
        size = rectTransform.sizeDelta;
        position = rectTransform.position;
        rotation = rectTransform.rotation;
        localScale = rectTransform.localScale;


    }



    // 저장한 캔버스 정보를 가져옴
    public CanvasInfo GetCanvasInfo()
    {
        CanvasInfo info = new CanvasInfo();
        info.size = size;
        info.position = position;
        info.rotation = rotation;
        info.localScale = localScale;
        return info;
    }

    public void Activate()
    {

        if (state == false)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            state = true;

        }
        else
        {
            canvas.renderMode = RenderMode.WorldSpace;
            rectTransform.sizeDelta = size;
            rectTransform.position = position;
            rectTransform.rotation = rotation;
            rectTransform.localScale = localScale;
            state = false;
        }

    }
}

public class CanvasInfo
{
    public Vector2 size;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 localScale;
}