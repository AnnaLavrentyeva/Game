using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timeTillBoom;
    public GameObject boomPrefab;
    public float ExplosionRadius;
    float createTime;

    void Start()
    {
        createTime = Time.time;
    }

    void Update()
    {
        float boomTime = Time.time - createTime;
        if(boomTime >= timeTillBoom)
        {
            DestroyInRadius();
            Explode();
        }
    }

    void DestroyInRadius()
    {
        Collider2D[] collidersNear = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        for(int i=0; i<collidersNear.Length; ++i)
        {
            ByBomb distractableByBomb = collidersNear[i].GetComponent<ByBomb>();
            if(distractableByBomb != null)
            {
                distractableByBomb.DestroyByBomb();
            }
        }
    }

    void Explode()
    {
        Destroy(gameObject);
        Instantiate(boomPrefab, transform.position, Quaternion.identity);
    }
        private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
