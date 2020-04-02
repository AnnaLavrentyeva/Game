using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float moveSpeed = 5.9f;
    public float frightenedSpeed = 2.9f;
    private float previousSpeed;
    public float normalSpeed = 5.9f;
    public float consumedSpeed = 15f;

    public Node startingPosition;
    public Node homeNode;
    public Node ghostHouse;

    public float ghostReleaseTimer = 0;
    public int secondEnemyReleaseTimer = 5;
    public int ThirdEnemyReleaseTimer = 14;
    public int FourthEnemyReleaseTimer = 21;

    public bool canMove = true;
    public bool isInGhostHouse = false;

    public int scatterModeTimer1 = 7;
    public int scatterModeTimer2 = 7;
    public int scatterModeTimer3 = 5;
    public int scatterModeTimer4 = 5;
    public int chaseModeTimer1 = 20;
    public int chaseModeTimer2 = 20;
    public int chaseModeTimer3 = 20;
    public int chaseModeTimer4 = 20;

    private int modeChangeInteration = 1;
    private float modeChangeTimer = 0;
    private float frightenedTimer = 0;
    private float blinkTimer = 0;
    private bool frightenedModeIsTwo = false;
    public int frightenedDuration = 10;
    public int blinkingStart = 7;

    public RuntimeAnimatorController ghostUp;
    public RuntimeAnimatorController ghostDown;
    public RuntimeAnimatorController ghostLeft;
    public RuntimeAnimatorController ghostRight;

    public RuntimeAnimatorController fastGhostUp;
    public RuntimeAnimatorController fastGhostDown;
    public RuntimeAnimatorController fastGhostLeft;
    public RuntimeAnimatorController fastGhostRight;

    public RuntimeAnimatorController ghostScary;
    public RuntimeAnimatorController ghostScary2;

    private AudioSource bgAudio;

    public enum Mode
    {
        Chase,
        Scatter,
        Frightened,
        Consumed
    }

    public enum GhostType
    {
        First,
        Second,
        Third,
        Fourth
    }

    public GhostType ghostType = GhostType.First;

    Mode currentMode = Mode.Scatter;
    Mode previousMode;

    private GameObject pacMan;
    private Node currentNode, targetNode, previousNode;
    private Vector2 direction, nextDirection;


    void Start()
    {
        bgAudio = GameObject.Find("Game").transform.GetComponent<AudioSource>();
        pacMan = GameObject.FindGameObjectWithTag("Pacman");
        Node node = GetNodeAtPosition(transform.localPosition);

        if (node != null)
        {
            currentNode = node;
        }

        if (isInGhostHouse)
        {
            direction = Vector2.up;
            targetNode = currentNode.neighbors[0];
        }
        else
        {
            direction = Vector2.left;
            targetNode = ChooseNextNode();
        }
        previousNode = currentNode;
        UpdateAnimatorController();
    }

    void Update()
    {
        if (canMove)
        {
            ModeUpdate();
            Move();
            ReleaseGhosts();
            CheckCollision();
            CheckHouse();
        }
    }

    void CheckHouse()
    {
        if(currentMode == Mode.Consumed)
        {
            GameObject tile = GetTileAtPosition(transform.position);
            if(tile != null)
            {
                if(tile.transform.GetComponent<Tile>() != null)
                {
                    if (tile.transform.GetComponent<Tile>().isGhostHouse)
                    {
                        moveSpeed = normalSpeed;
                        Node node = GetNodeAtPosition(transform.position);

                        if(node != null)
                        {
                            currentNode = node;
                            direction = Vector2.up;
                            targetNode = currentNode.neighbors[0];

                            previousNode = currentNode;
                            currentMode = Mode.Chase;

                            UpdateAnimatorController();
                        }
                    }
                }
            }
        }

    }

    void UpdateAnimatorController()
    {
        if (currentMode != Mode.Frightened && currentMode != Mode.Consumed)
        {

            if (direction == Vector2.up)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostUp;
            }
            else if (direction == Vector2.down)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostDown;

            }
            else if (direction == Vector2.left)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostLeft;

            }
            else if (direction == Vector2.right)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostRight;

            }
            else
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostDown;
            }
        }
        else if (currentMode == Mode.Frightened)
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = ghostScary;
        } else if(currentMode == Mode.Consumed)
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = null;

            if (direction == Vector2.up)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = fastGhostUp;
            }
            else if (direction == Vector2.down)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = fastGhostDown;

            }
            else if (direction == Vector2.left)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = fastGhostLeft;

            }
            else if (direction == Vector2.right)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = fastGhostRight;

            }
        }
    }

    void Consumed()
    {
        currentMode = Mode.Consumed;
        previousSpeed = moveSpeed;
        moveSpeed = consumedSpeed;
        UpdateAnimatorController();
    }

    void Move()
    {
        if(targetNode != currentNode && targetNode != null && !isInGhostHouse)
        {
            if (OverShotTraget())
            {
                currentNode = targetNode;
                transform.localPosition = currentNode.transform.position;
                GameObject otherPortal = GetPortal(currentNode.transform.position);

                if(otherPortal != null)
                {
                    transform.localPosition = otherPortal.transform.position;
                    currentNode = otherPortal.GetComponent<Node>();
                }

                targetNode = ChooseNextNode();
                previousNode = currentNode;
                currentNode = null;
                UpdateAnimatorController();
            }
            else
            {
                transform.localPosition += (Vector3)direction * moveSpeed * Time.deltaTime;
            }
        }
    }


    void ModeUpdate()
    {
        if(currentMode != Mode.Frightened)
        {
            modeChangeTimer += Time.deltaTime;
            if(modeChangeInteration == 1)
            {
                if(currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer1)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }

                if(currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer1)
                {
                    modeChangeInteration = 2;
                    ChangeMode(Mode.Scatter);
                    modeChangeTimer = 0;
                }
            }
            else if(modeChangeInteration == 2)
            {
                if(currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer2)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }
                if(currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer2)
                {
                    modeChangeInteration = 3;
                    ChangeMode(Mode.Scatter);
                    modeChangeTimer = 0;
                }
            }
            else if (modeChangeInteration == 3)
            {
                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer3)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }
                if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer3)
                {
                    modeChangeInteration = 4;
                    ChangeMode(Mode.Scatter);
                    modeChangeTimer = 0;
                }


            } else if (modeChangeInteration == 4)
            {
                if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer4)
                {
                    ChangeMode(Mode.Chase);
                    modeChangeTimer = 0;
                }
            }
        } else if (currentMode == Mode.Frightened)
        {
            frightenedTimer += Time.deltaTime;

            if(frightenedTimer >= frightenedDuration)
            {
                bgAudio.clip = GameObject.Find("Game").transform.GetComponent<GameBoard>().normalTheme;
                bgAudio.Play();

                frightenedTimer = 0;
                ChangeMode(previousMode);
            }
            if(frightenedTimer >= blinkingStart)
            {
                blinkTimer += Time.deltaTime;

                if(blinkTimer >= 0.1f)
                {
                    blinkTimer = 0f;

                    if (frightenedModeIsTwo)
                    {
                        transform.GetComponent<Animator>().runtimeAnimatorController = ghostScary;
                        frightenedModeIsTwo = false;
                    }
                    else
                    {
                        transform.GetComponent<Animator>().runtimeAnimatorController = ghostScary2;
                        frightenedModeIsTwo = true;

                    }
                }
            }
        }
    }

    Vector2 GetFirstGhostTargetTile()
    {
        Vector2 pacmanPosition = pacMan.transform.localPosition;
        Vector2 targetTile = new Vector2(Mathf.RoundToInt(pacmanPosition.x), Mathf.RoundToInt(pacmanPosition.y));

        return targetTile;
    }

    Vector2 GetSecondGhostTargetTile()
    {
        Vector2 pacmanPosition = pacMan.transform.localPosition;
        Vector2 pacmanOrientation = pacMan.GetComponent<PacMan>().orientation;

        int pacmanPositionX = Mathf.RoundToInt(pacmanPosition.x);
        int pacmanPositionY = Mathf.RoundToInt(pacmanPosition.y);

        Vector2 pacmanTile = new Vector2(pacmanPositionX, pacmanPositionY);
        Vector2 targetTile = pacmanTile + (4 * pacmanOrientation);

        return targetTile;
    }


    Vector2 GetThirdTargetTile()
    {
        Vector2 pacmanPosition = pacMan.transform.position;
        Vector2 pacmanOrienation = pacMan.GetComponent<PacMan>().orientation;

        int pacmanPositionX = Mathf.RoundToInt(pacmanPosition.x);
        int pacmanPositionY = Mathf.RoundToInt(pacmanPosition.y);

        Vector2 pacmanTile = new Vector2(pacmanPositionX, pacmanPositionY);
        Vector2 targetTile = pacmanTile + (2*pacmanOrienation);

        Vector2 tempFirstPosition = GameObject.Find("PacManEnemy").transform.localPosition;

        int firstPosX = Mathf.RoundToInt(tempFirstPosition.x);
        int firstPosY = Mathf.RoundToInt(tempFirstPosition.y);

        tempFirstPosition = new Vector2(firstPosX, firstPosY);

        float distance = GetDistance(tempFirstPosition, targetTile);
        //distance *= 2;
        targetTile = new Vector2(tempFirstPosition.x + distance, tempFirstPosition.y + distance);
        return targetTile;
    }

    Vector2 GetFourthTargetTile()
    {
        Vector2 pacmanPosition = pacMan.transform.position;
        float distance = GetDistance(transform.localPosition, pacmanPosition);
        Vector2 targetTile = Vector2.zero;

        if(distance > 8)
        {
            targetTile = new Vector2(Mathf.RoundToInt(pacmanPosition.x), Mathf.RoundToInt(pacmanPosition.y));
        }
        else if(distance < 8)
        {
            targetTile = homeNode.transform.position;
        }

        return targetTile;
    }

    Vector2 GetRandomTile()
    {
        int x = Random.Range(0, 28);
        int y = Random.Range(0, 36);

        return new Vector2(x, y);
    }


    Vector2 GetTargetTile()
    {
        Vector2 targetTile = Vector2.zero;
        if(ghostType == GhostType.First)
        {
            targetTile = GetFirstGhostTargetTile(); 
        }
        else if( ghostType == GhostType.Second)
        {
            targetTile = GetSecondGhostTargetTile();
        }
        else if (ghostType == GhostType.Third)
        {
            targetTile = GetThirdTargetTile();
        }
        else if (ghostType == GhostType.Fourth)
        {
            targetTile = GetFourthTargetTile();
        }
        return targetTile;
    }

    

    void ReleaseSecondGhost()
    {
        if(ghostType == GhostType.Second && isInGhostHouse)
        {
            isInGhostHouse = false;

        }
    }

    void ReleaseGhosts()
    {
        ghostReleaseTimer += Time.deltaTime;
        if (ghostReleaseTimer > secondEnemyReleaseTimer)
        {
            ReleaseSecondGhost();
        }
        if (ghostReleaseTimer > ThirdEnemyReleaseTimer)
        {
            ReleaseThirdGhost();
        }
        if (ghostReleaseTimer > FourthEnemyReleaseTimer)
        {
            ReleaseFourthGhost();
        }
    }

    void ReleaseThirdGhost()
    {
        if (ghostType == GhostType.Third && isInGhostHouse)
        {
            isInGhostHouse = false;
        }
    }

    void ReleaseFourthGhost()
    {
        if (ghostType == GhostType.Fourth && isInGhostHouse)
        {
            isInGhostHouse = false;

        }
    }

    Node ChooseNextNode()
    {
        Vector2 targetTile = Vector2.zero;

        if(currentMode == Mode.Chase)
        {
            targetTile = GetTargetTile();
        }
        else if (currentMode == Mode.Scatter)
        {
            targetTile = homeNode.transform.position;
        }
        else if (currentMode == Mode.Frightened)
        {
            targetTile = GetRandomTile();
        }
        else if (currentMode == Mode.Consumed)
        {
            targetTile = ghostHouse.transform.position;
        }

     //   targetTile = GetTargetTile();

        Node moveToNode = null;
        Node[] foundNodes = new Node[4];
        Vector2[] foundNodesDirection = new Vector2[4];

        int nodeCounter = 0;
        for(int i=0; i<currentNode.neighbors.Length; i++)
        {
            if (currentNode.validDirection[i] != direction * -1)
            {
                if(currentMode != Mode.Consumed)
                {
                    GameObject tile = GetTileAtPosition(currentNode.transform.position);
                    if(tile.transform.GetComponent<Tile>().isGhostHouseEnter == true)
                    {
                        if(currentNode.validDirection[i] != Vector2.down)
                        {
                            foundNodes[nodeCounter] = currentNode.neighbors[i];
                            foundNodesDirection[nodeCounter] = currentNode.validDirection[i];
                            nodeCounter++;
                        }
                    }
                    else
                    {
                        foundNodes[nodeCounter] = currentNode.neighbors[i];
                        foundNodesDirection[nodeCounter] = currentNode.validDirection[i];
                        nodeCounter++;
                    }
                }
                else
                {
                    foundNodes[nodeCounter] = currentNode.neighbors[i];
                    foundNodesDirection[nodeCounter] = currentNode.validDirection[i];
                    nodeCounter++;
                }
            }
        }
        if(foundNodes.Length == 1)
        {
            moveToNode = foundNodes[0];
            direction = foundNodesDirection[0];
        }

        if(foundNodes.Length > 1)
        {
            float leastDistance = 1000000f;
            for(int i=0; i<foundNodes.Length; i++)
            {
                if(foundNodesDirection[i] != Vector2.zero)
                {
                    float distance = GetDistance(foundNodes[i].transform.position, targetTile);
                    if(distance < leastDistance)
                    {
                        leastDistance = distance;
                        moveToNode = foundNodes[i];
                        direction = foundNodesDirection[i]; 
                    }
                }
            }
        }
        return moveToNode; 
    }

    void ChangeMode(Mode mode)
    {
        if(currentMode == Mode.Frightened)
        {
            moveSpeed = previousSpeed;
        }

        if(mode == Mode.Frightened)
        {
            previousSpeed = moveSpeed;
            moveSpeed = frightenedSpeed;
        }

        if (currentMode != mode)
        {
            previousMode = currentMode;
            currentMode = mode;
        }
        UpdateAnimatorController();
    }

    public void StartFrightenedMode()
    {
        if (currentMode != Mode.Consumed)
        {
            frightenedTimer = 0;
            bgAudio.clip = GameObject.Find("Game").transform.GetComponent<GameBoard>().frightenedTheme;
            bgAudio.Play();
            ChangeMode(Mode.Frightened);
        }
    }

    Node GetNodeAtPosition(Vector2 position)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)position.x, (int)position.y];
        if(tile != null)
        {
            if(tile.GetComponent<Node>() != null)
            {
                return tile.GetComponent<Node>();
            }
        }

        return null;
    }

    GameObject GetTileAtPosition(Vector2 position)
    {
        int tileX = Mathf.RoundToInt(position.x);
        int tileY = Mathf.RoundToInt(position.y);

        GameObject tile = GameObject.Find("Game").transform.GetComponent<GameBoard>().board[tileX, tileY];

        if(tile != null)
        {
            return tile;
        }

        return null;
    }

    GameObject GetPortal (Vector2 position)
    {
        GameObject tile = GameObject.Find("Game").GetComponent<GameBoard>().board[(int)position.x, (int)position.y];
        Debug.Log(tile);

        if (tile != null)
        {
            if (tile.GetComponent<Tile>().isPortal)
            {
                GameObject otherPortal = tile.GetComponent<Tile>().portalReciver;
                return otherPortal;
            }
        }
        return null;
    }

  float LenghtFromNode(Vector2 targetPosition)
    {
        Vector2 vector = targetPosition - (Vector2)previousNode.transform.position;
        return vector.sqrMagnitude;
    }

    bool OverShotTraget()
    {
        float nodeToTarget = LenghtFromNode(targetNode.transform.position);
        float nodeToSelf = LenghtFromNode(transform.localPosition);

        return nodeToSelf > nodeToTarget;
    }

    float GetDistance (Vector2 posA, Vector2 posB)
    {
        float dx = posA.x - posB.x;
        float dy = posA.y - posB.y;

        float distance = Mathf.Sqrt(dx * dx + dy * dy);

        return distance;
    }

    void CheckCollision()
    {
        Rect ghostRect = new Rect(transform.position, transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 4);
        Rect pacmanRect = new Rect(pacMan.transform.position, pacMan.transform.GetComponent<SpriteRenderer>().sprite.bounds.size / 4);

        if (ghostRect.Overlaps(pacmanRect))
        {
            //  Debug.Log("Collided");
            if (currentMode == Mode.Frightened)
            {
                Consumed();

            }
            else
            {
                if (currentMode != Mode.Consumed)
                {
                    GameObject.Find("Game").transform.GetComponent<GameBoard>().StartTheEnd();
                }
            }
        }
    }

    //public void Restart()
    //{
    //    canMove = true;
    //    transform.GetComponent<SpriteRenderer>().enabled = true;
    //    currentMode = Mode.Scatter;
    //    moveSpeed = normalSpeed;
    //    previousSpeed = 0;

    //    transform.position = startingPosition.transform.position;

    //    ghostReleaseTimer = 0;
    //    modeChangeInteration = 1;
    //    modeChangeTimer = 0;

    //    if(transform.name != "PacManEnemy")
    //    {
    //        isInGhostHouse = true;
    //    }
    //    currentNode = startingPosition;

    //    if (isInGhostHouse)
    //    {
    //        direction = Vector2.up;
    //        targetNode = currentNode.neighbors[0];
    //    }
    //    else
    //    {
    //        direction = Vector2.left;
    //        targetNode = ChooseNextNode();
    //    }

    //    previousNode = currentNode;
    //    UpdateAnimatorController();
    //}

}
