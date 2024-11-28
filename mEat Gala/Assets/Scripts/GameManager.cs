using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    [SerializeField] int gameOverSceneIndex;

    [SerializeField] FoodManager foodManager;
    public Player player;
    public int foodCount = 0; //remaining amount of foodContainer objs. When it hits 0, currentCourse goes up and a new course is displayed
    public int playerHealth = 3;

    public float playerSaturation = 100f; //gradually goes down to 0. Increases if a food is removed. If it reaches 0, it's game over. Cappped at 100f

    public static Vector2 startingPosition = new Vector2(0, -6); //the hands are set to this position at the start of the game and inbetween waves

    public Balloon balloon;

    void Awake()
    {
        if(gameManager != null)
        {
            GameObject.Destroy(gameManager);
        }
        else
        {
            gameManager = this;
        }

        DontDestroyOnLoad(this);
    }

    public void RemoveFood(int saturation)
    {
        GameManager.gameManager.foodCount = GameManager.gameManager.foodCount - 1;
        playerSaturation = playerSaturation + saturation;
        if(playerSaturation > 100)
        {
            playerSaturation = 100;
        }

        if(GameManager.gameManager.foodCount <= 0)
        {
            player.GetComponent<Transform>().position = startingPosition;
            balloon.balloonAnimator.Play("BalloonWaiter");
            foodManager.NextCourse();
        }
    }

    public void AddFood()
    {
        foodCount = foodCount + 1;
    }

    public void Damage()
    {
        player.gameObject.transform.position = startingPosition;
        player.target = null;
        playerHealth = playerHealth - 1;
        if(playerHealth <= 0)
        {
            GameOver();
        }
        else
        {
            Debug.Log("Remaining Health: " + playerHealth);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game over!");
        SceneManager.LoadScene(gameOverSceneIndex);
    }

    public void NextScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
