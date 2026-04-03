using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    private float maxHealth;

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        fillImage.fillAmount = 1;
    }

    public void SetHealth(int health)
    {
        fillImage.fillAmount = (float)health / maxHealth;
    }
}
