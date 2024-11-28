using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] AudioClip newCourseSFX;
    [SerializeField] Food[] foods; //all the foods available throughout the run
    [SerializeField] int currentCourse = 0; //the course to be displayed. The amount of food for each course is double this value
    [SerializeField] int[] foodAmounts; //how many FoodContainers will be spawned on each course
    [SerializeField] GameObject foodContainer; //empty food prefab, instantiated on FoodSpawn

    [SerializeField] GameObject[] targets; //target positions for FoodSpawn()

    [SerializeField] int nextScene; //scene selected when a wave generates no foodContainers

    void Start()
    {
        NextCourse();
    }

    void Update()
    {
        
    }

    private void FoodSpawn(Food food, GameObject target)
    {
        var newContainer = Instantiate(foodContainer, target.transform.position, new Quaternion(0,0,0,0));
        newContainer.GetComponent<FoodContainer>().FillContainer(food);
        
        GameManager.gameManager.player.foodPositions.Add(newContainer);
    }



    public void NextCourse()
    {
        currentCourse = currentCourse + 1;

        List<GameObject> freeTargets = new List<GameObject>();

        foreach(GameObject target in targets)
        {
            freeTargets.Add(target);
        }

        List<Food> availableFoods = new List<Food>(); 

        foreach(Food food in foods)
        {
            if((int) food.course == currentCourse)
            {
                availableFoods.Add(food);
                Debug.Log(availableFoods);
            }
        }

        if(availableFoods.Count > 0)
        {
            for(int i = 0; i < foodAmounts[currentCourse - 1]; i++)
            {
                int randomTarget = Random.Range(0, freeTargets.Count);
                FoodSpawn(availableFoods[Random.Range(0, availableFoods.Count)],freeTargets[randomTarget]);
                freeTargets.RemoveAt(randomTarget);
                GameManager.gameManager.AddFood();
            }
        }
        else
        {
            Debug.Log("Meal completed!");
            GameManager.gameManager.NextScene(nextScene);
        }
    }
}
