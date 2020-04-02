using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    private Node curNode, previousNode, targetNode;
    private Vector2 nextDirection;
    private Vector2 direction = Vector2.zero;
    private int pelletsConsumed = 0;

    public Vector2 orientation;
    public float speed = 6.0f;
    public Sprite idle;
    public Animator animator;

    public bool canMove = true;

    AudioSource source;
    public AudioClip RupeeSound;
    public RuntimeAnimatorController lastAnimation;
    public RuntimeAnimatorController startAnim;

    private Node startPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        Node node = GetNodeAtPosition(transform.localPosition);
        startPosition = node;
        if (node != null)
        {
            curNode = node;
            Debug.Log(curNode);
        }

        direction = Vector2.left;
        orientation = Vector2.left; 
        ChangePosition(direction);
    }

    void Update()
    {
        if (canMove)
        {
            CheckInput();
            Move();

            UpdateOrientation();
            //  UpdateDirection();
            UpdateAnimationState();

            ConsumePellet();

        }
//        Debug.Log("SCORE: " + GameObject.Find("Game").GetComponent<GameBoard>().score);
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangePosition(Vector2.left);
            //direction = Vector2.left;
            //MoveToNode(direction);
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
            animator.SetBool("Down", false);
            animator.SetBool("Up", false);


        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangePosition(Vector2.right);
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
            animator.SetBool("Down", false);
            animator.SetBool("Up", false);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ChangePosition(Vector2.up);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Down", false);
            animator.SetBool("Up", true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangePosition(Vector2.down);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Down", true);
            animator.SetBool("Up", false);
        }
    }

    void ChangePosition(Vector2 direct)
    {
        if (direct != direction)
        {
            nextDirection = direct;
        }

        if (curNode != null)
        {
            Node moveToNode = CanMove(direct);
            if (moveToNode != null)
            {
                direction = direct;
                targetNode = moveToNode;
                previousNode = curNode;
                curNode = null;
            }
        }
    }

    void Move()
    {
        if (targetNode != curNode && targetNode != null)
        {
            if (nextDirection == direction * -1)
            {
                direction *= -1;
                Node tempNode = targetNode;

                targetNode = previousNode;
                previousNode = tempNode;
            }

            if (OverShotTratget())
            {
                curNode = targetNode;
                transform.localPosition = curNode.transform.position;

                GameObject otherPortal = GetPortal(curNode.transform.position);
                if(otherPortal != null)
                {
                    transform.localPosition = otherPortal.transform.position;
                    curNode = otherPortal.GetComponent<Node>();
                }

                Node moveToNode = CanMove(nextDirection);

                if (moveToNode != null)
                {
                    direction = nextDirection;
                }
                if (moveToNode == null)
                {
                    moveToNode = CanMove(direction);
                }
                if (moveToNode != null)
                {
                    targetNode = moveToNode;
                    previousNode = curNode;
                    curNode = null;
                }
                else
                {
                    direction = Vector2.zero;
                }
            }
            else
            {
                transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
            }
        }
    }

    void MoveToNode(Vector2 direct)
    {
        Node moveToNode = CanMove(direct);

        if (moveToNode != null)
        {
            transform.localPosition = moveToNode.transform.position;
            curNode = moveToNode;
        }
    }

 //   void UpdateDirection()
        void UpdateOrientation()
    {
        if (direction == Vector2.left)
        {
            orientation = Vector2.left;
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (direction == Vector2.right)
        {
            orientation = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction == Vector2.up)
        {
            orientation = Vector2.up;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction == Vector2.down)
        {
            orientation = Vector2.down;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void UpdateAnimationState()
    {
        if (direction == Vector2.zero)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = idle;
        }
        else
        {
            GetComponent<Animator>().enabled = true;
        }
    }

    void ConsumePellet()
    {
        GameObject obj = GetTileAtPosition(transform.position);

        if(obj != null)
        {
            Tile tile = obj.GetComponent<Tile>();
            if (tile != null)
            {
                if(!tile.didConsume && (tile.isPellet || tile.isSupPellet))
                {
                    obj.GetComponent<SpriteRenderer>().enabled = false;
                    tile.didConsume = true;
                    GameObject.Find("Game").GetComponent<GameBoard>().score += 1;
                    pelletsConsumed++;

                    source.clip = RupeeSound;
                    source.Play();

                    if (tile.isSupPellet)
                    {
                        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

                        foreach (GameObject gh in ghosts)
                        {
                            gh.GetComponent<Ghost>().StartFrightenedMode();
                        }
                    }
                }
            }
        }
    } 

    Node CanMove(Vector2 direct)
    {
        Node moveToNode = null;
        for (int i = 0; i < curNode.neighbors.Length; i++)
        {
            if (curNode.validDirection[i] == direct)
            {
                moveToNode = curNode.neighbors[i];
                break;
            }
        }

        return moveToNode;
    }

    GameObject GetTileAtPosition (Vector2 position)
    {
        int tileX = Mathf.RoundToInt(position.x);
        int tileY = Mathf.RoundToInt(position.y);

        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[tileX, tileY];

        if(tile != null)
        {
            return tile;
        }

        return null;
    }


    Node GetNodeAtPosition(Vector2 pos)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)pos.x, (int)pos.y];
        if (tile != null)
        {
            return tile.GetComponent<Node>();
        }
        return null;
    }

    bool OverShotTratget()
    {
        float nodeToTarget = LengthFromNode(targetNode.transform.position);
        float nodeToSelf = LengthFromNode(transform.localPosition);

        return nodeToSelf > nodeToTarget;
    }

    float LengthFromNode(Vector2 tragetPos)
    {
        Vector2 vect = tragetPos - (Vector2)previousNode.transform.position;
        return vect.sqrMagnitude;
    }

    GameObject GetPortal(Vector2 position)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)position.x, (int)position.y];

        if (tile != null)
        {
            if (tile.GetComponent<Tile>() != null)
            {
                if (tile.GetComponent<Tile>().isPortal)
                {
                    GameObject otherPortal = tile.GetComponent<Tile>().portalReciver;
                    return otherPortal;
                }
            }
        }
            return null;
        
    }

    public void Restart()
    {
        // -Go into world

        Application.LoadLevel("World");
/*
        //Only restart
        canMove = true;
        transform.GetComponent<Animator>().runtimeAnimatorController = startAnim;
        transform.GetComponent<Animator>().enabled = true; ;
        transform.GetComponent<SpriteRenderer>().enabled = true;

        transform.position = startPosition.transform.position;

        curNode = startPosition;
        direction = Vector2.left;
         orientation = Vector2.left;

        nextDirection = Vector2.left;
        ChangePosition(direction);

    */
    }
}
