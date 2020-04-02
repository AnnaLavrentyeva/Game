using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float StartingHealth;
    private float MaxHealth;
    public float health;

    AudioSource source;
    public AudioClip hurtSound;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        health = StartingHealth;
        MaxHealth = StartingHealth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            MakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            AddHealth(10);
        }

        if (health <= 0)
        {
            health = 0;
        }
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return MaxHealth;
    }
    public float GetHealthPercentage()
    {
        return health / MaxHealth;
    }
    public void MakeDamage(float dmg)
    {
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Health is 0.");
            return;
        }
        DamageUI.Instance.ShowDamageNum(dmg, transform.position);
        health -= dmg;
        source.clip = hurtSound;
        source.Play();

    }

    public void AddHealth(float value)
    {
        health += value;
    }

}
