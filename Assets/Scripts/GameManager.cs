using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    /*  Create a singleton by creating a static reference to this
     *  GameManager script.
     *  
     *  In Awake check if there is already an singleton instance
     *  for this script and if there is, delete the second instance
     *  imemediately using DestoryImmediate(), else assign the 
     *  instance. DontDestroyOnLoad() to ensure this instance of the
     *  GameManager is not deleted upon a new scene being loaded.
     */

    /*
     * Using OnDestroy() ensure that the instance of game manager is
     * destroyed upon the gameobject being destoryed.
     */

    public static GameManager Instance { get; private set; }

    // World State Variables
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    /* In Start call a new function called NewGame. New game will
     * be responsible for resetting the game state to World 1 -
     * Stage 1. As well as reseting any currently stored score and 
     * lives.
     */

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;

        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLLevel()
    {
        if(stage < 10)
        {
            LoadLevel(world, stage + 1);
        }
        else
        {
            stage = 1;
            LoadLevel(world + 1, stage);
        }
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if(lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Invoke(nameof(NewGame), 2f);
    }
}
