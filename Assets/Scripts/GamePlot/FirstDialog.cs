using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstDialog : MonoBehaviour
{
   // public Animation m_Animator;
    public int timeBeforeWakeUp = 5;

    public GameObject disabledDialogBox;
    public GameObject zzz_object;
    private IEnumerator coroutine;

    AudioSource source;
    public AudioClip sleepSound;
    public AudioClip helpSound;
    public AudioClip screamSound;
    public AudioClip testSound;


    private Text dialogText;
    string[] messageArray = new string[] { "Help... ", "Someone... ", "Help!", "I know, you can hear me, Hero...",
                                            "He who must not be named abduct me...", "I am in the Dark Tower", "Hurry!"};
    string massage;
    int i = 0;

    void Awake()
    {
    //    m_Animator = gameObject.GetComponent<Animation>();
        source = GetComponent<AudioSource>();

        dialogText = transform.Find("Dialog").Find("dialogText").GetComponent<Text>();
        massage = messageArray[0];
    }

    void Start()
    {
        disabledDialogBox.SetActive(false);
        PlaySoundAtStart();
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

            CheckingForDialogSounds(i);
            massage = messageArray[i];
            source.clip = testSound;
            source.Play();
            TextWriter.WriteTextToDialogBox_Static(dialogText, massage, .04f, true, true);

            if(i == 6)
            {
               StartCoroutine( WaitForNextScene());
            }
        }
    }

    void PlaySoundAtStart()
    {
        source.clip = sleepSound;
        source.Play();

        coroutine = waitForSeconds(timeBeforeWakeUp);
        StartCoroutine(coroutine);
   //     source.Stop();
    }

    private IEnumerator waitForSeconds(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        source.Stop();
        StartPlotDialog();
    }

    void StartPlotDialog()
    {
        zzz_object.SetActive(false);
       source.clip = screamSound;
        source.Play();
        disabledDialogBox.SetActive(true);
        TextWriter.WriteTextToDialogBox_Static(dialogText, massage, .1f, true, true);
    }

    void CheckingForDialogSounds(int i) {
        if (i == 2)
        {
            source.clip = helpSound;
            source.Play();
        }
    }

    IEnumerator WaitForNextScene()
    {
        yield return new WaitForSeconds(2f);

        Application.LoadLevel("Plot 1");
    }
}
