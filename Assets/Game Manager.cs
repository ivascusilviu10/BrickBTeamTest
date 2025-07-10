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

    // public BrickManager[] bricks;
    // public Paddle paddle;
    // public BallMovement ball;

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
       // bricks = GameObject.FindWithTag("Brick");  //tag de pus pe brick prefab
       // paddle = GameObject.FindWithTag("Paddle");
       // ball = GameObject.FindWithTag("Ball");  // rezolvat
    }

    private void ResetLevel()
    {
        LoadLevel(this.levelIndex);
        // ball.ResetBall();
        // paddle.ResetPaddle();
    }

    // apelam LoadLevel de length pt ca length-ul array-ului este 3 
    // si asa intra pe a 2-a conditie din if
    private void GameOver()
    {
        LoadLevel(levelNames.Length);
    }

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
    public void AddScore(BrickManager brick)
    {
        this.score += brick.brickPoints; // Placeholder pt punctele unui brick

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
            if (this.bricks[i].gameObject.activeInHierarchy) {
                return false;
            }
        }
        return true;
    }
    */
}
