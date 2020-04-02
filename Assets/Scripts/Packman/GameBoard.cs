using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private static int boardWidth = 28;
    private static int boardHeight = 36;

    public int totalPellets = 0;
    public int score = 0;

    public GameObject[,] board = new GameObject[boardWidth, boardHeight];

    public AudioClip normalTheme;
    public AudioClip frightenedTheme;
    public AudioClip endOfLvl;

    private bool endStarts = false;
    

    void Start()
    {
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));

        foreach(GameObject o in objects)
        {
            Vector2 pos = o.transform.position;

            if (o.name != "PacMan" && o.name !="Nodes" && o.name !="Pellets" && o.name != "Maze" && o.tag!= "Ghost" && o.tag !="GhostHome")
            {
                if(o.GetComponent<Tile>() != null)
                {
                    if(o.GetComponent<Tile>().isPellet || o.GetComponent<Tile>().isSupPellet)
                    {
                        totalPellets++;
                    }
                }
                board[(int)pos.x, (int)pos.y] = o;
            }
            else
            {

            }
        }
    }

    private void Update()
    {
        if(score == 231)
        {
            GameObject.Find("Values").GetComponent<CharacterValues>().rps += 231;
            GameObject.Find("Values").GetComponent<CharacterValues>().ExitFromDungeon();
            Application.LoadLevel("World");
        }
    }

    public void StartTheEnd()
    {
        if (!endStarts)
        {
            endStarts = true;
        }

        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        foreach (GameObject ghost in ghosts)
        {
            ghost.transform.GetComponent<Ghost>().canMove = false;
        }

        GameObject pacMan = GameObject.Find("PacMan");
        pacMan.transform.GetComponent<PacMan>().canMove = false;

        pacMan.transform.GetComponent<Animator>().enabled = false;

        transform.GetComponent<AudioSource>().Stop();

        StartCoroutine(ProcessTheEnd(0.3f));
    }

    IEnumerator ProcessTheEnd(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        foreach (GameObject ghost in ghosts)
        {
            ghost.transform.GetComponent<SpriteRenderer>().enabled= false;
        }
        StartCoroutine(ProcessAnimation(1f));
    }

    IEnumerator ProcessAnimation(float delay)
    {
        GameObject pacMan = GameObject.Find("PacMan");

        pacMan.transform.localScale = new Vector3(1, 1, 1);
        pacMan.transform.localRotation = Quaternion.Euler(0,0,0);
        pacMan.transform.GetComponent<Animator>().runtimeAnimatorController = pacMan.transform.GetComponent<PacMan>().lastAnimation;
        pacMan.transform.GetComponent<Animator>().enabled = true;

        transform.GetComponent<AudioSource>().clip = endOfLvl;
        transform.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(delay);

        StartCoroutine(ProcessRestart(0.8f));
    }

    IEnumerator ProcessRestart(float delay)
    {
        GameObject pacMan = GameObject.Find("PacMan");
        pacMan.transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<AudioSource>().Stop();

        yield return new WaitForSeconds(delay);

        Restart();
    }
  
    public void Restart()
    {
        GameObject pacMan = GameObject.Find("PacMan");
        pacMan.transform.GetComponent<PacMan>().Restart();

        //GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        //foreach( GameObject ghost in ghosts)
        //{
        //    ghost.transform.GetComponent<Ghost>().Restart();
        //}

     //   transform.GetComponent<AudioSource>().clip = normalTheme;
      //  transform.GetComponent<AudioSource>().Play();
        endStarts = false;
    }
          
}
