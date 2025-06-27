using UnityEngine;

public class TimeFall : MonoBehaviour
{
    public float slowMultiplier = 0.3f;
    public float playerBoost = 0.5f;
    public float transitionSpeed = 5f;

    private bool isSlowing = false;
    private Movement playerMovement;
    private PlayerAbilitiesManager stamina;

    private float targetTimeScale = 1f;

    void Start()
    {
        playerMovement = FindObjectOfType<Movement>();
        stamina = FindObjectOfType<PlayerAbilitiesManager>();
    }

    void Update()
    {
        // Activar slow motion al presionar X si hay estamina
        if (Input.GetKeyDown(KeyCode.X) && !isSlowing && stamina.CanUseSlow())
        {
            ActivateSlowMotion();
        }

        // Cancelar slow si se suelta X o no hay más estamina
        if ((Input.GetKeyUp(KeyCode.X) && isSlowing) || (isSlowing && !stamina.CanUseSlow()))
        {
            ResetTime();
        }

        // Si se está usando slow, drenamos estamina
        if (isSlowing)
        {
            stamina.DrainSlow(1.5f);
        }

        // Transición suave
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, transitionSpeed * Time.unscaledDeltaTime);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    void ActivateSlowMotion()
    {
        isSlowing = true;
        targetTimeScale = slowMultiplier;

        if (playerMovement != null)
        {
            float playerMultiplier = 1f / slowMultiplier * playerBoost;
            playerMovement.SetSpeedMultiplier(playerMultiplier);
            playerMovement.SetIsSlowing(true);
        }
    }

    void ResetTime()
    {
        isSlowing = false;
        targetTimeScale = 1f;

        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(1f);
            playerMovement.SetIsSlowing(false);
        }
    }
}

