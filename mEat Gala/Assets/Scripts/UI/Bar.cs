using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] AudioClip hungerSFX;
    Image barFill;
    [SerializeField] float depletionMultiplier = 2;

    bool hungerTrigger = false;
    void Awake()
    {
        barFill = GetComponent<Image>();
    }
    void FixedUpdate()
    {
        if(GameManager.gameManager.player.target == null)
        {
            GameManager.gameManager.playerSaturation -= Time.fixedDeltaTime * depletionMultiplier;
            barFill.fillAmount = GameManager.gameManager.playerSaturation / 100f;
        }
        
        if(!hungerTrigger && GameManager.gameManager.playerSaturation <= 65)
        {
            hungerTrigger = true;
            GameManager.gameManager.audioManager.PlayHungerSFX(hungerSFX);
        }

        if(hungerTrigger && GameManager.gameManager.playerSaturation > 65)
        {
            hungerTrigger = false;
        }

        if(GameManager.gameManager.playerSaturation <= 0)
        {
            GameManager.gameManager.GameOver();
        }
    }
}
