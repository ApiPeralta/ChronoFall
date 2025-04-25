using UnityEngine;

public class TimeFall : MonoBehaviour
{
    public float slowMultiplier = 0.3f;         // Qué tan lento va todo
    public float playerBoost = 0.5f;            // Cuánto más rápido se mueve el jugador en slow
    public float slowDuration = 3f;             // Cuántos segundos dura el slow motion

    private bool isSlowing = false;
    private float timer = 0f;
    private Movement playerMovement;

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
    }

    void ActivateSlowMotion()
    {
        Time.timeScale = slowMultiplier;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (playerMovement != null)
        {
            float playerMultiplier = 1f / slowMultiplier * playerBoost;
            playerMovement.SetSpeedMultiplier(playerMultiplier);
        }

        timer = slowDuration;
        isSlowing = true;
    }

    void ResetTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(1f);
        }

        isSlowing = false;
        timer = 0f;
    }
}

