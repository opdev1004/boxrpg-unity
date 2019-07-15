using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBar : MonoBehaviour
{
    public Image remainingHealth;
    public static UIHealthBar instance { get; private set; }
    public TextMeshProUGUI hpTextDisplay;

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

    public void SetValue(int currentHealth, int maxHealth)
    {
        remainingHealth.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (currentHealth/(float)maxHealth));
        SetHpTextDisplay(currentHealth, maxHealth);
    }

    public void SetHpTextDisplay(int currentHealth, int maxHealth)
    {
        hpTextDisplay.text = currentHealth + " / " + maxHealth;
    }
}
