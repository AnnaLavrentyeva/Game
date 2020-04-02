using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    static DialogBox Instance;
    private Text text;
    private Image dialogImage;


    void Awake()
    {
        Instance = this;

        dialogImage = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }

    public static void Show(string displayText)
    {
        Instance.DoShow(displayText);
    }

    public static void Hide()
    {
        Instance.DoHide();
    }

    public static bool IsVisible()
    {
        return Instance.dialogImage.enabled;
    }

    void DoHide()
    {
        dialogImage.enabled = false;
        text.enabled = false;
    }

    void DoShow(string displayText)
    {
        dialogImage.enabled = true;
        
        text.enabled = true;
        text.text = displayText;

    }
}
