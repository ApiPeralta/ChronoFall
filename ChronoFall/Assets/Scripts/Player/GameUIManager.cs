using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider slowStaminaSlider;
    public TextMeshProUGUI cloneCounterText;
    public TextMeshProUGUI recordIndicatorText;

    [Header("Logic References")]
    public PlayerAbilitiesManager abilities;
    public TimeRecorder recorder;

    void Update()
    {
        if (abilities != null)
        {
            slowStaminaSlider.value = abilities.GetSlowStaminaNormalized();
            cloneCounterText.text = "x " + abilities.currentClones.ToString();
        }

        if (recorder != null)
        {
            recordIndicatorText.gameObject.SetActive(recorder.isRecording);
        }
    }
}



