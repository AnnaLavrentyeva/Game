using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeestSound : MonoBehaviour
{
    AudioSource source;
    public AudioClip forestTheme;
    public AudioClip MainTheme;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        source.clip = MainTheme;
        source.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            source.clip = forestTheme;
            source.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        source.clip = MainTheme;
        source.Play();
    }
}
