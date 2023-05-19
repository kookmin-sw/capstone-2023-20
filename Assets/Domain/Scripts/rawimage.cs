using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rawimage : MonoBehaviour
{
    // Start is called before the first frame update
    private RawImage rawImage;
    private RectTransform rectTransform;
    public Texture texture;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = rawImage.GetComponent<RectTransform>();

        ResizeRawImage();
    }

    private void Update()
    {
        ResizeRawImage();
    }

    private void ResizeRawImage()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        float rawImageWidth = rectTransform.rect.width;
        float rawImageHeight = rectTransform.rect.height;

        float textureWidth = texture.width;
        float textureHeight = texture.height;

        float ratio = Mathf.Min(screenWidth / textureWidth, screenHeight / textureHeight);
        float newWidth = textureWidth * ratio;
        float newHeight = textureHeight * ratio;

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
    }
}
