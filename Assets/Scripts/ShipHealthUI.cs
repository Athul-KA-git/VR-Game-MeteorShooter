using UnityEngine;
using UnityEngine.UI;

public class ShipHealthUI : MonoBehaviour
{
    public Image healthFill;
    public ShipHealth shipHealth;

    private int maxHealth;

    private void Start()
    {
        maxHealth = shipHealth.health;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthFill.fillAmount = (float)shipHealth.health / maxHealth;
    }
}
