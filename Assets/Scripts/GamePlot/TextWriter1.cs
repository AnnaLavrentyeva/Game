using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter1 : MonoBehaviour
{
    /*
    private static TextWriter1 instance;

    private List<TextQueue2> textQueueList2;

    private void Awake()
    {
        instance = this;
        textQueueList2 = new List<TextQueue2>();
    }

    public static void WriteTextToDialogBox_Static(Text textObjcet, string textToWrite, float timeForWritingString, bool invisibleCharacters, bool removeWriterBeforeAdd) {
        if (removeWriterBeforeAdd) {
          instance.RemoveWriter(textObjcet);
        }
        instance.WriteTextToDialogBox(textObjcet, textToWrite, timeForWritingString, invisibleCharacters);
    }
    private void WriteTextToDialogBox(Text textObjcet, string textToWrite, float timeForWritingString, bool invisibleCharacters) {
        textQueueList2.Add(new TextQueue2(textObjcet, textToWrite, timeForWritingString, invisibleCharacters));

    }

    private void RemoveWriter(Text textObject)
    {
        for (int i = 0; i < textQueueList2.Count; ++i)
        {
            if (textQueueList2[i].GetText() == textObject) {
                textQueueList2.RemoveAt(i);
                i--;
            }
        }
    }


    void Update()
    {
        for (int i = 0; i < textQueueList2.Count; ++i)
        {
           bool destroyInstance = textQueueList2[i].Update();
            if (destroyInstance) {
                textQueueList2.RemoveAt(i);
                --i;
            }
        }
    }

}

public class TextQueue2{

    private Text textObjcet;
    private string textToWrite;
    private float timeForWritingString;
    private float timer;
    private int characterIndex; //HH
    private bool invisibleCharacters;

    public TextQueue2(Text textObjcet, string textToWrite, float timeForWritingString, bool invisibleCharacters)
    {
        this.textObjcet = textObjcet;
        this.textToWrite = textToWrite;
        this.timeForWritingString = timeForWritingString;
        this.invisibleCharacters = invisibleCharacters;
        characterIndex = 0;
    }

    public bool Update()  //returns true if complited 
    {
        timer -= Time.deltaTime;
        while (timer <= 0f)
        {
            //Display next string
            timer += timeForWritingString;
            characterIndex++;
            string text = textToWrite.Substring(0, characterIndex);
            if (invisibleCharacters)
            {
                text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
            }
            textObjcet.text = text;

            if (characterIndex >= textToWrite.Length)
            {
                return true;
            }
        }
        return false;
    }

    public Text GetText() {
        return textObjcet;
    }

    */
}
