using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image remainingHealth;
    public static UIHealthBar instance { get; private set; }

    float originalSize;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //gets the original length of the health bar as reference when setting health
        originalSize = remainingHealth.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        remainingHealth.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
