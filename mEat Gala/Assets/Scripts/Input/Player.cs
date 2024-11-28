using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerControls controls;

    [SerializeField] AudioClip lockSFX;
    [SerializeField] AudioClip biteSFX;
    [SerializeField] AudioClip twistSFX;
    [SerializeField] AudioClip grabSFX;
    
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator iconAnimator;

    //Move
    Vector2 move;
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float autoMoveSpeedMultiplier = 3; //the higher the value, the slower the AutoMove()
    [SerializeField] int reverseThreshold = 65; //if saturation is lower than this, move vector is inverted
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

    public List<GameObject> foodPositions; //the positions of all current foods. Used to calculate auto movement
    GameObject chasedFood; //the food the player autoomatically moves towards

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
            iconAnimator.SetBool("Eating", false);
            if(foodPositions.Count > 0)
            {
                CompareDistance();
                AutoMove();
            }
            Move();
        }
    }

    void Move()
    {
        if(GameManager.gameManager.playerSaturation >= reverseThreshold)
        {
            transform.position = new Vector2 (transform.position.x, transform.position.y) + (move * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.position = new Vector2 (transform.position.x, transform.position.y) - (move * Time.deltaTime * moveSpeed);
        }
    }

    void AutoMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, chasedFood.transform.position , Time.deltaTime * moveSpeed/autoMoveSpeedMultiplier);
    }

    void Bite()
    {
        if(bit)
        {
            target.resistance = target.resistance - 1;

            GameManager.gameManager.audioManager.PlayInputSFX(biteSFX);

            bit = false;
        }
    }

    void Twist()
    {
        newRotation = Mathf.Atan2(turn.y, turn.x);
        if(oldRotation != 0)
        {
            float angleDifference = newRotation - oldRotation;
            //Debug.Log(angleDifference);
            if(angleDifference > 1 || angleDifference < -1)
            {
                Debug.Log("Cheater");
            }
            else
            {
                target.resistance = target.resistance - Mathf.Abs(angleDifference/twistMultiplier); //removing Mathf.Abs makes it so resistance only decreases when twisting in one direction

                GameManager.gameManager.audioManager.PlayInputSFX(twistSFX);
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

                GameManager.gameManager.audioManager.PlayInputSFX(grabSFX);
            }
            oldGrab = newGrab;
        }
    }

    void CompareDistance()
    {
        float distance = 0;

        foreach(GameObject food in foodPositions)
        {
            if(Vector2.Distance(food.transform.position, transform.position) < distance || distance == 0)
            {
                distance = Vector2.Distance(food.transform.position, transform.position);
                chasedFood = food;
            }
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
            iconAnimator.SetBool("Eating", true);
            switch(target.inputType)
            {
                case 1:
                {
                    iconAnimator.Play("IconTap");
                    break;
                }
                case 2:
                {
                    iconAnimator.Play("IconTwist");
                    break;
                }
                case 3:
                {
                    iconAnimator.Play("IconGrab");
                    break;
                }
            }

            GameManager.gameManager.audioManager.PlayCourseSFX(lockSFX);
        }
    }
}
