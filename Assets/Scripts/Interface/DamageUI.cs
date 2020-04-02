using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    public static DamageUI Instance;
    public GameObject numbersPrefab;

    RectTransform rectTransform;

    void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    public void ShowDamageNum(float dmg, Vector3 position)
    {
        GameObject newDmgNum = Instantiate<GameObject>(numbersPrefab);
        newDmgNum.transform.GetComponentInChildren<Text>().text = Mathf.RoundToInt(dmg).ToString();

        Vector3 screenPosition = Camera.main.WorldToViewportPoint(position);

        RectTransform damageNumTransform = newDmgNum.GetComponent<RectTransform>();

        damageNumTransform.SetParent(transform, true);
        damageNumTransform.localScale = Vector3.one;
        damageNumTransform.anchoredPosition = new Vector2(screenPosition.x * rectTransform.rect.width, 
                                                        screenPosition.y * rectTransform.rect.height);

        Destroy(newDmgNum, 1f);
    }



}
