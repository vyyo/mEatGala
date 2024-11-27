using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    Image barFill;
    [SerializeField] float depletionMultiplier = 2;
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
        

        if(GameManager.gameManager.playerSaturation <= 0)
        {
            GameManager.gameManager.GameOver();
        }
    }
}
