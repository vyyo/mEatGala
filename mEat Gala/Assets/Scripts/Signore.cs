using System.Collections;
using UnityEngine;

public class Signore : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] float turnTime = 3;
    private float countDown;

    private int stance;
    [SerializeField] int stanceChance1 = 50;
    private int currentStanceChance1; //chance to go from stance 0 to 1 on TurnLogic()
    [SerializeField] int stanceChance2 = 40;
    private int currentStanceChance2; //chance to go from stance 1 to 2 on TurnLogic()
    [SerializeField] int stanceIncrement = 10; //how much a stanceChance goes up after a failed stance change

    void Awake()
    {
        countDown = turnTime; //time between TurnLogic() calls

        stance = 0; //inital stance(0 = external, 1 = internal, 2 = front)
        currentStanceChance1 = stanceChance1;
        currentStanceChance2 = stanceChance2;
    }

    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0)
        {
            TurnLogic();
            countDown = turnTime;
        }
    }

    void TurnLogic()
    {
        switch(stance)
        {
            case 0:
                if(currentStanceChance1 >= Random.Range(0,100))
                {
                    stance = 1;
                    //UpdateAnimator()
                }
                else
                {
                    currentStanceChance1 = currentStanceChance1 + stanceIncrement;
                }
            break;

            case 1:
                if(currentStanceChance2 >= Random.Range(0,100))
                {
                    stance = 2;
                    //UpdateAnimator()
                }
                else
                {
                    currentStanceChance2 = currentStanceChance2 + stanceIncrement;
                }
            break;

            case 2:
                if(player.target != null)
                {
                    player.gameObject.transform.position = GameManager.startingPosition;
                    player.target = null;
                    Debug.Log("Damage");
                }
                stance = 3;
            break;

            default:
            stance = 0;
            currentStanceChance1 = stanceChance1;
            currentStanceChance2 = stanceChance2;
            break;
        }
        Debug.Log(stance);
    }
}
