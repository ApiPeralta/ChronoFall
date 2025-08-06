using UnityEngine;

public class PlayerAbilitiesManager : MonoBehaviour
{
    [Header("Slow Motion")]
    public float maxSlowStamina = 5f;
    public float slowRegenRate = 1f;
    public float slowDrainRate = 1.5f;
    public float currentSlowStamina;

    [Header("Glide")]
    public float maxGlideStamina = 4f;
    public float glideDrainRate = 1f;
    public float glideRegenRate = 0.5f;
    public float currentGlideStamina;

    [Header("Clones")]
    public int maxClones = 3;
    public int currentClones;

    void Start()
    {
        currentSlowStamina = maxSlowStamina;
        currentGlideStamina = maxGlideStamina;
        currentClones = maxClones;
    }

    void Update()
    {
        RegenerateStamina();
    }

    void RegenerateStamina()
    {
        // Solo regenera si no se está usando slow
        if (!Input.GetKey(KeyCode.X) && currentSlowStamina < maxSlowStamina)
        {
            currentSlowStamina += slowRegenRate * Time.deltaTime;
        }

        if (!Input.GetKey(KeyCode.Space) && currentGlideStamina < maxGlideStamina)
        {
            currentGlideStamina += glideRegenRate * Time.deltaTime;
        }

        // Clamp para que no se pase
        currentSlowStamina = Mathf.Min(currentSlowStamina, maxSlowStamina);
        currentGlideStamina = Mathf.Min(currentGlideStamina, maxGlideStamina);
    }

    public bool CanUseSlow() => currentSlowStamina > 0f;
    public bool CanGlide() => currentGlideStamina > 0f;
    public bool CanSpawnClone() => currentClones > 0;

    public void DrainSlow(float amount)
    {
        currentSlowStamina -= amount * Time.deltaTime;
    }

    public void DrainGlide(float amount)
    {
        currentGlideStamina -= amount * Time.deltaTime;
    }

    public void UseClone()
    {
        if (currentClones > 0)
            currentClones--;
    }

    public void RecoverClone()
    {
        if (currentClones < maxClones)
            currentClones++;
    }
    public float GetSlowStaminaNormalized()
    {
        return currentSlowStamina / maxSlowStamina;
    }
    public float GetGliderStaminaNormalized()
    {
        return currentGlideStamina / maxGlideStamina;
    }
}

