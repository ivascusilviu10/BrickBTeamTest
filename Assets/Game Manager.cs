using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string[] levelNames = { "Level 1", "Level 2", "Level 3" };

    public int levelIndex = 0;
    public int score = 0;
    public int lives = 3;
    public int delay = 5;

    // public BrickSpawner[] brick { get; private set; }
    // public Paddle paddle { get; private set; }
    // public Ball ball { get; private set; }

    // Singleton + OnLevelLoaded refer pt obiecte daca se da reset lvl 
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    // Luam referintele prin tags si apelam NewGame
    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.levelIndex = 0;
        this.score = 0;
        this.lives = 3;

        LoadLevel(levelIndex);
    }

    // verif daca indexul este intre 0 si 2,daca nu incarcam scena Global(meniul)
    private void LoadLevel(int index)
    {
        if (index >= 0 && index < levelNames.Length) {
            SceneManager.LoadScene(levelNames[index]);
            this.levelIndex = index;
        } else {
            SceneManager.LoadScene("Global");
            this.levelIndex = 0;
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
       // this.brick = GameObject.FindWithTag("BrickSpawner");
       // this.paddle = GameObject.FindWithTag("Paddle");
       // this.ball = GameObject.FindWithTag("Ball");
    }

    private void ResetLevel()
    {
        LoadLevel(this.levelIndex);
    }

    // apelam LoadLevel de length pt ca length-ul array-ului este 3 
    // si asa intra pe a 2-a conditie din if
    private void GameOver()
    {
        LoadLevel(levelNames.Length);
    }

    // TO DO:delay ca parametrii
    private IEnumerator DelayedResetLevel()
    {
        yield return new WaitForSeconds(delay);
        ResetLevel();
    }

    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(delay);
        GameOver();
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

    // in caz ca pica mingea, scadem o viata si resetam nivelul sau dam game over dupa caz
    public void Miss()
    {
        this.lives--;

        if (this.lives > 0) {
            StartCoroutine(DelayedResetLevel());
        } else {
            StartCoroutine(DelayedGameOver());
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
