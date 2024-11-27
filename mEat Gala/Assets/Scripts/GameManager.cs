using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    [SerializeField] FoodManager foodManager;
    [SerializeField] Player player;
    public int foodCount = 0; //remaining amount of foodContainer objs. When it hits 0, currentCourse goes up and a new course is displayed
    public int playerHealth = 3;

    float playerSaturation = 100f; //gradually goes down to 0. Increases if a food is removed. If it reaches 0, it's game over. Cappped at 100f
    bool eating = false; //whether or not the player is eating. Blocks movement until food is removed if true

    public static Vector2 startingPosition = new Vector2(0, -6); //the hands are set to this position at the start of the game and inbetween waves

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

    public void RemoveFood()
    {
        GameManager.gameManager.foodCount = GameManager.gameManager.foodCount - 1;
        if(GameManager.gameManager.foodCount <= 0)
        {
            player.GetComponent<Transform>().position = startingPosition;
            foodManager.NextCourse();
        }
    }

    public void AddFood()
    {
        foodCount = foodCount + 1;
    }
}
