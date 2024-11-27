using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerControls controls;
    [SerializeField] Animator playerAnimator;

    //Move
    Vector2 move;
    [SerializeField] float moveSpeed = 3;
    //Twist
    Vector2 turn;
    private float oldRotation = 0;
    private float newRotation;
    [SerializeField] float twistMultiplier = 2; //the higher the value, the more twists required
    //Grab
    private string oldGrab = "";
    private string newGrab;
    //Bite
    private bool bit = false;

    public FoodContainer target; //targeted FoodContainer

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        controls.Player.Twist.performed += ctx => turn = ctx.ReadValue<Vector2>();
        controls.Player.Twist.canceled += ctx => turn = Vector2.zero;

        controls.Player.GrabLeft.performed += ctx => newGrab = "left";
        controls.Player.GrabRight.performed += ctx => newGrab = "right";

        controls.Player.Bite.performed += ctx => bit = true;
    }

    void Update()
    {
        if(target != null)
        {
            playerAnimator.SetBool("Eating", true);
            switch(target.inputType)
            {
                case 1:
                {
                    Bite();
                    break;
                }
                case 2:
                {
                    Twist();
                    break;
                }
                case 3:
                {
                    Grab();
                    break;
                }
            }
            //reset all eating vars to default           
        }
        else
        {
            playerAnimator.SetBool("Eating", false);
            Move();
        }
        
    }

    void Move()
    {
        transform.position = new Vector2 (transform.position.x, transform.position.y) + (move * Time.deltaTime * moveSpeed);
    }

    void Bite()
    {
        if(bit)
        {
            target.resistance = target.resistance - 1;
            bit = false;
        }
    }

    void Twist()
    {
        newRotation = Mathf.Atan2(turn.y, turn.x);
        if(oldRotation != 0)
        {
            float angleDifference = newRotation - oldRotation;
            Debug.Log(angleDifference);
            if(angleDifference > 1 || angleDifference < -1)
            {
                Debug.Log("Cheater");
            }
            else
            {
                target.resistance = target.resistance - Mathf.Abs(angleDifference/twistMultiplier); //removing Mathf.Abs makes it so resistance only decreases when twisting in one direction
            }
        }
        oldRotation = newRotation;
    }

    void Grab()
    {
        if(newGrab == "left" || newGrab == "right")
        {
            Debug.Log(newGrab);
            if(newGrab != oldGrab)
            {
                Debug.Log("DMG");
                target.resistance = target.resistance - 1;
            }
            oldGrab = newGrab;
        }
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Food") && target == null)
        {
            target = other.GetComponent<FoodContainer>();
            gameObject.transform.position = target.transform.position;
        }
    }
}
