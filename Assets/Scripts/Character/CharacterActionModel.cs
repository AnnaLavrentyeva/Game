using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionModel : MonoBehaviour
{
    public GameObject bombPrefab;

    Character character;

    AudioSource source;
    public AudioClip bombExplore;

    void Awake()
    {
        character = GetComponent<Character>();
        source = GetComponent<AudioSource>();
    }
    public void PlaceBomb()
    {
        if (character.InventoryModel.GetItemCount(ItemType.Bomb) > 0)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
            character.InventoryModel.AddItem(ItemType.Bomb, -1, PickUpType.None);
            StartCoroutine(WaitBeforeExplore());
        }
    }

    private IEnumerator WaitBeforeExplore()
    {
        yield return new WaitForSeconds(2.65f);
        source.clip = bombExplore;
        source.Play();
    }
}
