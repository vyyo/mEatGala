using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] Food[] foods; //all the foods available throughout the run
    [SerializeField] int currentCourse = 0; //the course to be displayed. The amount of food for each course is double this value
    [SerializeField] GameObject foodContainer; //empty food prefab, instantiated on FoodSpawn

    [SerializeField] GameObject[] targets; //target positions for FoodSpawn()
    /*[SerializeField] GameObject table; //the table object/boundary, on which food will be spawned
    List<Bounds> tableBounds = new List<Bounds>(); //list of spawn areas for FoodSpawn*/

    void Start()
    {
        //fill tableBounds
        /*BoxCollider2D[] tableColliders = table.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D collider in tableColliders)
        {
            tableBounds.Add(collider.bounds);
        }*/
        //first course
        NextCourse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FoodSpawn(Food food, GameObject target)
    {
        /*int i = Random.Range(0, tableBounds.Count);
        float randomPosX = Random.Range(tableBounds[i].min.x, tableBounds[i].max.x);
        float randomPosY = Random.Range(tableBounds[i].min.y, tableBounds[i].max.y);*/
        var newContainer = Instantiate(foodContainer, target.transform.position, new Quaternion(0,0,0,0));
        newContainer.GetComponent<FoodContainer>().FillContainer(food);
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
            for(int i = 0; i < currentCourse * 2; i++)
            {
                int randomTarget = Random.Range(0, freeTargets.Count);
                FoodSpawn(availableFoods[Random.Range(0, availableFoods.Count)],freeTargets[randomTarget]);
                freeTargets.RemoveAt(randomTarget);
                GameManager.gameManager.AddFood();
            }
        }
        else
        {
            Debug.Log("No valid foods for the current meal");
        }
    }
}
