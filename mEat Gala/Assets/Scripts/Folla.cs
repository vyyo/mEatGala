using UnityEngine;

public class Folla : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameManager.gameManager.playerHealth)
        {
            case 0:
                animator.Play("Folla3");
            break;

            case 1:
                animator.Play("Folla2");
            break;

            case 2:
                animator.Play("Folla1");
            break;

            case 3:
                animator.Play("Folla0");
            break;
        }
    }
}
