using UnityEngine;
using UnityEngine.InputSystem;

public class FoodContainer : MonoBehaviour
{
    public int resistance;
    public int saturation;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            resistance = resistance - 1;
            animator.SetFloat("Resistance", resistance);
            Debug.Log("Resistenza attuale: " + resistance);
        }
    }

    public void FillContainer(Food food)
    {
        resistance = food.resistance;
        saturation = food.saturation;

        spriteRenderer.sprite = food.foodSprite;
        animator.runtimeAnimatorController = food.animations;
        animator.SetFloat("Resistance", resistance);


        //collision setup
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.GetComponent<PolygonCollider2D>().useDelaunayMesh = true;
    }
}
