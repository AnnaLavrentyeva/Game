using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeItem : MonoBehaviour
{
    public ItemType itemType;
    public int amount;

    AudioSource source;
    public AudioClip RupeeSound;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventoryModel inventoryModel = collision.GetComponent<CharacterInventoryModel>();

        if (inventoryModel != null)
        {
            inventoryModel.AddItem(itemType, amount, PickUpType.FromDrop);
            MakeRupeeDestoyed();
        }
    }
    void MakeRupeeDestoyed()
    {
        source.clip = RupeeSound;
        source.Play();
       StartCoroutine( DestroyTime());
        //  Destroy(gameObject);
    }
    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.13f);
        Destroy(gameObject);
    }
}
