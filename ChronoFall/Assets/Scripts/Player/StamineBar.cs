using UnityEngine;
using UnityEngine.UI;

public class StamineBar : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public Slider staminaSlider;

    // Para chequear si se puede usar el poder (tener al menos algo de stamina)
    public bool puedeUsarPoder => currentStamina >= 0;

    void Start()
    {
        currentStamina = maxStamina;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
        else
        {
            Debug.LogWarning("No asignaste el Slider en StamineBar");
        }
    }

    public void UsarPoder(float cantidad)
    {
        if (currentStamina >= cantidad)
        {
            currentStamina -= cantidad;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

            if (staminaSlider != null)
                staminaSlider.value = currentStamina;
        }
        else
        {
            Debug.Log("No tenés suficiente stamina.");
        }
    }
}
