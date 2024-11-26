using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] Food[] foods;
    [SerializeField] int currentCourse;
    [SerializeField] GameObject foodContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Food food in foods)
        {
            if((int) food.course == currentCourse)
            {
                Debug.Log(food.resistance);
                FoodSpawn(food);
            }
        }
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
}
