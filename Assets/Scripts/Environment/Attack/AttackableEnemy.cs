using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEnemy : Attackable
{
    public GameObject Enemy;
    GameObject oldEnemy;
    public float MaxHealth;
    float health = 1;
    private float dmg; 

    public float HitPushStrength;
    public float HitPushDuration;
    public float destroyDelay;
    public GameObject DeathEffect;
    public float delayDeath;
    public bool doRespawn;

    public CharacterMovementModel movementModel;

    AudioSource source;
    public AudioClip DeathSound;
    public AudioClip HitSound;

    public GameObject enemyRef;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        health = MaxHealth;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.Stop();
        dmg = 10;
    }

    private void Update()
    {
        dmg = GameObject.Find("Values").GetComponent<CharacterValues>().dmg;
    }

    public float GetHealth()
    {
        return health;
    }

    public override void Hit(Collider2D hitCollider, ItemType itemType)
    {
        health -= dmg;

        DamageUI.Instance.ShowDamageNum(dmg, transform.position);

        source.clip = HitSound;
        source.Play();

        if (movementModel != null && itemType != ItemType.Bomb)
        {

            Vector3 pushDirection = transform.position - hitCollider.gameObject.transform.position;
            pushDirection = pushDirection.normalized * HitPushStrength;

           // Debug.Log(pushDirection + "   " + HitPushStrength);
            movementModel.PushCharacter(pushDirection, HitPushDuration);
        }

        if (health <= 0)
        {
            StartCoroutine(DestroyTime(destroyDelay));
            if(DeathEffect != null)
            {
                source.clip = DeathSound;
                source.Play();
                StartCoroutine(CreateDeathEffect(delayDeath));
            }
        }
    }

    IEnumerator DestroyTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Destroy(Enemy);
        oldEnemy = Enemy;
        if (doRespawn)
        {
            Invoke("Respawn", 5);
        }

        oldEnemy.SetActive(false);
        BroadcastMessage("LootDrop", SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator CreateDeathEffect(float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(DeathEffect, transform.position, Quaternion.identity);
    }

    public void Respawn()
    {
        //  Destroy(Enemy);
        //  GameObject enemyClone = (GameObject)Instantiate(enemyRef);
        Enemy = (GameObject)Instantiate(enemyRef);
        Enemy.transform.position = oldEnemy.transform.position;
        
        Enemy.SetActive(true);
        Destroy(oldEnemy);
        //    enemyClone = Enemy;


    }


}

    
