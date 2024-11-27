using System.Collections;
using UnityEngine;

public class Signore : MonoBehaviour
{
    [SerializeField] Animator signoreAnimator;

    [SerializeField] float turnTime = 3;
    private float countDown;

    private float damageTimer = 0.5f;
    private float damageCountDown;
    private bool damage;

    private int stance;
    [SerializeField] int stanceChance1 = 50;
    private int currentStanceChance1; //chance to go from stance 0 to 1 on TurnLogic()
    [SerializeField] int stanceChance2 = 40;
    private int currentStanceChance2; //chance to go from stance 1 to 2 on TurnLogic()
    [SerializeField] int stanceIncrement = 10; //how much a stanceChance goes up after a failed stance change

    void Awake()
    {
        countDown = turnTime; //time between TurnLogic() calls
        damageCountDown = damageTimer;

        stance = 0; //inital stance(0 = external, 1 = internal, 2 = front)
        signoreAnimator.Play("SignoreExternal");
        currentStanceChance1 = stanceChance1;
        currentStanceChance2 = stanceChance2;
    }

    void FixedUpdate()
    {
        countDown -= Time.fixedDeltaTime;
        if(countDown <= 0)
        {
            TurnLogic();
            countDown = turnTime;
        }

        if(stance == 2)
        {
            if(GameManager.gameManager.player.target != null)
            {
                damage = true;
            }
        }
        if(damage)
        {
            damageCountDown -= Time.fixedDeltaTime;
            if(damageCountDown <= 0)
            {
                GameManager.gameManager.Damage();
                stance = 0;
                signoreAnimator.Play("SignoreExternal");
                damageCountDown = damageTimer;
                damage = false;
            }
        }
    }

    void TurnLogic()
    {
        int randomNum = Random.Range(0,100);
        Debug.Log("Random num: " + randomNum + "Chance1: " + currentStanceChance1 + "Chance2: " + currentStanceChance2);
        switch(stance)
        {
            case 0:
                if(currentStanceChance1 >= randomNum)
                {
                    stance = 1;
                }
                else
                {
                    currentStanceChance1 = currentStanceChance1 + stanceIncrement;
                }
            break;

            case 1:
                if(currentStanceChance2 >= randomNum)
                {
                    stance = 2;
                }
                else
                {
                    stance = 0;
                    currentStanceChance2 = currentStanceChance2 + stanceIncrement;
                }
            break;

            case 2:
                stance = 0;
                currentStanceChance1 = stanceChance1;
                currentStanceChance2 = stanceChance2;
            break;

            default:
            stance = 0;

            currentStanceChance1 = stanceChance1;
            currentStanceChance2 = stanceChance2;
            break;
        }
        switch(stance)
        {
            case 0:
                signoreAnimator.Play("SignoreExternal");
            break;
            case 1:
                signoreAnimator.Play("SignoreInternal");
            break;
            case 2:
                signoreAnimator.Play("SignoreFront");
            break;
        }
        Debug.Log(stance);
    }
}
