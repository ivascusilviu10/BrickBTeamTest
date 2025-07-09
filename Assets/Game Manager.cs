using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int levelIndex = 1;
    public int score = 0;
    public int lives = 3;

    // Singleton + OnLevelLoaded refer pt obiecte daca se da reset lvl 
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    // Luam referintele prin tags si apelam NewGame
    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        // GameObject brickSpawner = GameObject.FindWithTag("Spawner");

        NewGame();
    }

    private void NewGame()
    {
        this.levelIndex = 1;
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel(int levelIndex)
    {
        this.levelIndex = levelIndex;

        if (this.levelIndex > 3) {
            this.levelIndex = 1;
        }

        SceneManager.LoadScene("Level " + levelIndex);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    private void ResetLevel()
    {
        LoadLevel(this.levelIndex);
    }

    private void GameOver()
    {
        NewGame();
    }

    /* Dupa ce e gata partea lui Stefan cu brickurile,updatez scorul
    public void AddScore(int placeholder)
    {
        this.score += BrickSpawner.points; // Placeholder pt punctele unui brick

        if(LevelCompleted())
        {
            LoadLevel(this.levelIndex + 1);
        }
    }
    */

    public void Miss()
    {
        this.lives--;

        if (this.lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }

    }

    /* Inca nu avem referinta pt brickuri
    private bool LevelCompleted()
    {
        for (int i = 1; i <= this.bricks.Length; i++)
        {
            if (this.referintaBricks[i].gameObject.activeInHierarchy) {
                return false;
            }
        }

        return true;
    }
    */
}
