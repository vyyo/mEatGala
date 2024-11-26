using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] Food[] foods; //all the foods available throughout the run
    [SerializeField] int currentCourse = 0; //the course to be displayed. The amount of food for each course is double this value
    [SerializeField] GameObject foodContainer; //empty food prefab, instantiated on FoodSpawn

    void Start()
    {
        NextCourse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FoodSpawn(Food food)
    {
        var newContainer = Instantiate(foodContainer, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        newContainer.GetComponent<FoodContainer>().FillContainer(food);
    }



    public void NextCourse()
    {
        Debug.Log("CAZZOOOOOOO");
        currentCourse = currentCourse + 1;
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
            for(int i = 0; i < currentCourse * 2; i++)
            {
                FoodSpawn(availableFoods[Random.Range(0, availableFoods.Count)]);
                GameManager.gameManager.AddFood();
            }
        }
        else
        {
            Debug.Log("No valid foods for the current meal");
        }
    }
}
