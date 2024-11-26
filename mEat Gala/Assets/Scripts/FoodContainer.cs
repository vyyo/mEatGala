using UnityEngine;

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
        
    }

    public void FillContainer(Food food)
    {
        resistance = food.resistance;
        saturation = food.saturation;

        spriteRenderer.sprite = food.foodSprite;
        animator.runtimeAnimatorController = food.animations;

        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.GetComponent<PolygonCollider2D>().useDelaunayMesh = true;
    }
}
