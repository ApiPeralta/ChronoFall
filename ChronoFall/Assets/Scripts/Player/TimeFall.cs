using UnityEngine;

public class TimeFall : MonoBehaviour
{
    public float slowMultiplier = 0.3f;
    public float playerBoost = 0.5f;
    public float slowDuration = 3f;
    public float transitionSpeed = 5f; // Qué tan rápido se hace la transición

    private bool isSlowing = false;
    private float timer = 0f;
    private Movement playerMovement;

    private float targetTimeScale = 1f;

    void Start()
    {
        playerMovement = FindObjectOfType<Movement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isSlowing)
        {
            ActivateSlowMotion();
        }

        if (isSlowing)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0f)
            {
                ResetTime();
            }
        }

        // Transición suave entre escalas de tiempo
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, transitionSpeed * Time.unscaledDeltaTime);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    void ActivateSlowMotion()
    {
        targetTimeScale = slowMultiplier;

        if (playerMovement != null)
        {
            float playerMultiplier = 1f / slowMultiplier * playerBoost;
            playerMovement.SetSpeedMultiplier(playerMultiplier);
            playerMovement.SetGravityScale(0.7f);
            playerMovement.SetIsSlowing(true);
        }

        timer = slowDuration;
        isSlowing = true;
    }

    void ResetTime()
    {
        targetTimeScale = 1f;

        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(1f);
            playerMovement.SetGravityScale(1f); // Restaurar gravedad
        }
        playerMovement.SetIsSlowing(false);
        isSlowing = false;
        timer = 0f;
    }
}


