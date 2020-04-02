using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstDialog1 : MonoBehaviour
{

    public GameObject disabledDialogBox;
    public AudioClip testSound;

    private IEnumerator coroutine;

    private Text dialogText;
    AudioSource source;

    string[] messageArray = new string[] { "What was that?", "Nightmare?", "WHAAAT?"};
    string massage;
    int i = 0;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        dialogText = transform.Find("Dialog").Find("dialogText").GetComponent<Text>();
        massage = messageArray[0];
    }

    void Start()
    {
        disabledDialogBox.SetActive(false);
        StartPlotDialog();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (i >= messageArray.Length - 1)
            {
                return;
            }
            i++;

            massage = messageArray[i];
            TextWriter.WriteTextToDialogBox_Static(dialogText, massage, .04f, true, true);
            source.clip = testSound;
            source.Play();
            if(i == 2)
            {
                StartCoroutine( WaitForNextScene());
            }
        }
    }
      

    void StartPlotDialog()
    {
        disabledDialogBox.SetActive(true);
        source.clip = testSound;
        source.Play();
        TextWriter.WriteTextToDialogBox_Static(dialogText, massage, .04f, true, true);
    }

    IEnumerator WaitForNextScene()
    {
        yield return new WaitForSeconds(1f);

        Application.LoadLevel("Plot 2");
    }
}
