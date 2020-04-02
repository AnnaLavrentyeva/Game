using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CharacterHealth healthModel;
    public Text HealthText;
    public RectTransform HealthBarImage;

    void Update()
    {
        UpdateText();
        UpdateHealthBar();
    }

   void  UpdateText()
    {
        HealthText.text = Mathf.RoundToInt(healthModel.GetHealth()) + "/" +
                            Mathf.RoundToInt(healthModel.GetMaxHealth());
    }

    void UpdateHealthBar()
    {
        HealthBarImage.localScale = new Vector3(healthModel.GetHealthPercentage(), 1f, 1f);
    }
}
