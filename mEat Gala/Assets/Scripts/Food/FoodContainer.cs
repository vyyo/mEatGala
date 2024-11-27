using UnityEngine;
using UnityEngine.InputSystem;

public class FoodContainer : MonoBehaviour
{
    private float initialResistance;
    public float resistance = 1;
    public int saturation;
    public int inputType;
    private bool destroying = false;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(resistance > initialResistance)
        {
            resistance = initialResistance;
        }
        
        animator.SetFloat("Resistance", resistance);

        if(resistance <= 0 && !destroying)
        {
            destroying = true;
            GameManager.gameManager.RemoveFood(saturation);
            Destroy(gameObject, 0.1f);
        }
    }

    public void FillContainer(Food food)
    {
        initialResistance = food.resistance;
        resistance = initialResistance;
        saturation = food.saturation;
        inputType = (int) food.inputType;

        spriteRenderer.sprite = food.foodSprite;
        animator.runtimeAnimatorController = food.animations;
        animator.SetFloat("Resistance", resistance);


        //collision setup
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.GetComponent<PolygonCollider2D>().useDelaunayMesh = true;
    }
}
