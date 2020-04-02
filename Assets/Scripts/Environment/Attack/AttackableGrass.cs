using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableGrass : Attackable
{
    public GameObject DestroyedPrefab;
    public GameObject DestroyEffect;
    private SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponentInChildren<SpriteRenderer>(); 
    }

    void CreateDestroyedPrefab()
    {
        GameObject newDestroyedGrass =  (GameObject)Instantiate(DestroyedPrefab, 
                                            transform.position, transform.rotation);
        newDestroyedGrass.transform.parent = transform.parent;
    }

    void DestroyGrass()
    {
        Destroy(gameObject);

        if (DestroyEffect != null)
        {
            GameObject destroyEffect = (GameObject)Instantiate(DestroyEffect);
            destroyEffect.transform.position = transform.position;
        }

    }
    public void DropLoot()
    {
        BroadcastMessage("LootDrop", SendMessageOptions.DontRequireReceiver);
    }
    
    public override void Hit(Collider2D collider, ItemType itemType)
    {
        DestroyGrass();
        CreateDestroyedPrefab();
        DropLoot();
    }

    void OnPickUpObject(Character character)
    {
        CreateDestroyedPrefab();
        DropLoot();
    }

    void OnObjectThrown()
    {
        DestroyGrass();
    }
}
