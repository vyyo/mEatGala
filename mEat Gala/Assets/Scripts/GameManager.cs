using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    [SerializeField] FoodManager foodManager;
    public int foodCount = 0; //remaining amount of foodContainer objs. When it hits 0, currentCourse goes up and a new course is displayed

    float playerSaturation = 100f; //gradually goes down to 0. Increases if a food is removed. If it reaches 0, it's game over. Cappped at 100f
    bool eating = false; //whether or not the player is eating. Blocks movement until food is removed if true

    Vector2 startingPosition = new Vector2(0, -540); //the hands are set to this position at the start of the game and inbetween waves

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
            foodManager.NextCourse();
        }
    }

    public void AddFood()
    {
        foodCount = foodCount + 1;
    }
}
