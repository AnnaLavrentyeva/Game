
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementModel : MonoBehaviour
{
    public float speed;
    private Vector3 movementDirection;
    private Vector3 facingDirection;

    private ItemType equipedWeapon = ItemType.None;
    private ItemType equipedShield = ItemType.None;
    public Transform WeaponParent;
    public Transform ShieldParent;
    public Transform PickupItemParent;
    public GameObject SwordPrefab;
    public Sprite pinkSword;
    public Sprite blueSword;
    int flag;
    private GameObject pickedUpItem;

    Rigidbody2D rb;

    private bool isFrozen;
    private bool isAttacking;
    private bool isSetDirectionFrozen;

    private PickUpType pickupTypeObject = PickUpType.None;

    private ItemType pickUpObject = ItemType.None;

    private Vector2  pushDirection;
    private float pushTime;

    private int lastDirectionSet;
    private bool isAbleToAttack = true;

    AudioSource source;
    public AudioClip AttackSound;
    Vector2 receivedDirection;
    private float m_LastFreezeTime;
    private float OverrideSpeed;
    private bool isOverrideSpeed;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdatePushTime();
        UpdateDirection();
        RemoveReceivedDirection();

        if (SwordPrefab != null)
        {
            SwordPrefab.SetActive(true);
           // SwordPrefab.GetComponentInChildren<SpriteRenderer>().sprite = blueSword;

        }
    }

    //private void LateUpdate()
    //{
    //    if (SwordPrefab != null && flag >0)
    //    {
    //        SwordPrefab.GetComponentInChildren<SpriteRenderer>().sprite = pinkSword;
    //    }
    //}

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void UpdatePushTime()
    {
        pushTime = Mathf.MoveTowards(pushTime, 0f, Time.deltaTime);
    }

    void RemoveReceivedDirection()
    {
        receivedDirection = Vector2.zero;
    } 

    void UpdateMovement()
    {
        if (isFrozen == true || isAttacking == true)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (movementDirection != Vector3.zero)
        {
            movementDirection.Normalize();
        }
        if (IsPushed() == true)
        {
            rb.velocity = pushDirection;
        }
        else
        {
          float newSpeed = speed;

            if(isOverrideSpeed == true)
            {
                newSpeed = OverrideSpeed; 
            }

            rb.velocity = movementDirection * newSpeed;
            
        }
    }

    public void SetOverrideSpeed(bool doOverride, float thisSpeed = 0f)
    {
        isOverrideSpeed = doOverride;
        OverrideSpeed = thisSpeed;
    }

    void UpdateDirection()
    {
        if (isFrozen == true)
        {
            if (receivedDirection != Vector2.zero && GetItemThatIsBeingPickUp() != ItemType.None && GetTimeSinceFrozen() > 0.5f)
            {
                pickUpObject = ItemType.None;
                SetFrozen(false, false, true);
                Destroy(pickedUpItem);
            }
        }

        if (isSetDirectionFrozen == true && receivedDirection != Vector2.zero)
        {
            return;
        }
         if (isAttacking == true)
        {
            return;
        }

        if (IsPushed() == true)
        {
            movementDirection = pushDirection;
        }


        if (Time.frameCount == lastDirectionSet)
        {
            return;
        }

        movementDirection = new Vector3(receivedDirection.x, receivedDirection.y, 0);

        if (receivedDirection != Vector2.zero)
        {
            Vector3 facingDirection2 = movementDirection;

            if (facingDirection2.x != 0 && facingDirection2.y != 0)
            {
                if (facingDirection2.x == facingDirection.x)
                {
                    facingDirection2.y = 0;
                }
                else if (facingDirection2.y == facingDirection.y)
                {
                    facingDirection2.x = 0;
                }
                else
                {
                    facingDirection2.x = 0;
                }
            }

            facingDirection = facingDirection2;
            lastDirectionSet = Time.frameCount;
        }

    }

    public void SetDirection(Vector2 direction)
    {
        if(direction == Vector2.zero)
        {
            return;
        }
        receivedDirection = direction;
    }

    public Vector3 GetDirection()
    {
        return movementDirection;
    }

    public Vector3 GetFacingDirection()
    {
        return facingDirection;
    }

    public bool IsMoving()
    {
        //For normal animation
        if(isFrozen == true)
        {
            return false;
        }


        //Check if is need

        //if(isSetDirectionFrozen == true && movementDirection != facingDirection)
        //{
        //    return false;
        //}

        return movementDirection != Vector3.zero;
    }

    public void SetFrozen(bool IsFrozen, bool isDirectionFrozen, bool affectGameTime)
    {
        isFrozen = IsFrozen;
        isSetDirectionFrozen = isDirectionFrozen;
        if (affectGameTime == true)
        {
            if(IsFrozen == true)
            {
                m_LastFreezeTime = Time.realtimeSinceStartup;
                StartCoroutine(FreezeTimeRoutine());
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    IEnumerator FreezeTimeRoutine()
    {
        yield return null;
        Time.timeScale = 0;
    }

    public bool IsFrozen()
    {
        return isFrozen;
    }

    float GetTimeSinceFrozen()
    {
        if (IsFrozen() == false)
        {
            return 0f;
        }

        return Time.realtimeSinceStartup - m_LastFreezeTime;
    }

    public bool IsPushed()
    {
        return pushTime > 0;
    }

    public void Attack()
    {   
    }
    public void SetAbleToAttack(bool able)
    {
        isAbleToAttack = able;
    }

    public void AttackStarted()
    {
        isAttacking = true;
        source.clip = AttackSound;
        source.Play();
    }

    public void AttackFinished()
    {
        isAttacking = false;
    }

    public void Unequip()
    {
        //if (SwordPrefab != null)
        //{
            flag = 1;
           // SwordPrefab.GetComponentInChildren<SpriteRenderer>().sprite = pinkSword;
        //}
    }


    public void EquipWeapon(ItemType itemType)
    {
        EquipItem(itemType, ItemData.EquipPosition.SwordHand,
                            WeaponParent, ref equipedWeapon);
    }

    public void EquipShield(ItemType itemType)
    {
        EquipItem(itemType, ItemData.EquipPosition.ShieldHand,
                            ShieldParent, ref equipedShield);
    }

    public void EquipItem(ItemType itemType, ItemData.EquipPosition equipPosition, 
                            Transform itemParent, ref ItemType equippedItemSlot) { 

        if (itemParent == null)
        {
            return;
        }
        ItemData itemData = Database.Item.FindItem(itemType);

        if (itemData == null)
        {
            return;
        }

        if (itemData.isEquipable != equipPosition)
        {
            return;
        }
        equippedItemSlot = itemType;

        GameObject newSwordObject = (GameObject)Instantiate(itemData.prefab);

        newSwordObject.transform.parent = WeaponParent;
        newSwordObject.transform.localPosition = Vector2.zero;
        newSwordObject.transform.localRotation = Quaternion.identity;
    }

    public ItemType GetItemThatIsBeingPickUp()
    {
        return pickUpObject;
    }

    public void ShowItemPickedUp(ItemType itemType, PickUpType pickUpType)
    {
        if (PickupItemParent == null)
        {
            return;
        }

        ItemData itemData = Database.Item.FindItem(itemType);

        if (itemData == null)
        {
            return;
        }

        SetFrozen(true, true, true);
        pickUpObject = itemType;
        pickupTypeObject = pickUpType;
        pickedUpItem = (GameObject)Instantiate(itemData.prefab);

        pickedUpItem.transform.parent = PickupItemParent;
        pickedUpItem.transform.localPosition = Vector2.zero;
        pickedUpItem.transform.localRotation = Quaternion.identity;
    }

    public PickUpType GetPickUpType()
    {
        return pickupTypeObject;
    }

    public void PushCharacter(Vector2 pushDirectionRef, float timeRef)
    {
        if(isAttacking == true)
        {
            isAttacking = false;
            //  AttackFinished();
        }
        pushDirection = pushDirectionRef;
        pushTime = timeRef;
    }


    public bool CanAttack()
    {
        if (IsPushed() == true)
        {
            return false;
        }
        if (isAttacking == true)
        {
            return false;
        }

        if (equipedWeapon == ItemType.None)
        {
            return false;
        }
        if (isAbleToAttack == false)
        {
            return false;
        }

        return true;
    }

}




