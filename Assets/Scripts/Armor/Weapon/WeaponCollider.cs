using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class WeaponCollider : MonoBehaviour
{
    public ItemType type;
    Collider2D colliderr;

    AudioSource source;
    public AudioClip HitRock;

    void Awake()
    {
        colliderr = GetComponent<Collider2D>();
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Attackable attackable = collider.gameObject.GetComponent<Attackable>();
        
        if(attackable != null)
        {
            attackable.Hit(colliderr, type);
        }

        if(collider.tag == "Rock")
        {
            source.clip = HitRock;
            source.Play();
        }
    }


}
