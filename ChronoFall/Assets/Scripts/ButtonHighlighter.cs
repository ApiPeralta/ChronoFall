using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour
{
    [System.Serializable]
    public class ButtonKeyPair
    {
        public KeyCode key;
        public Image buttonImage;
        [HideInInspector] public Color originalColor;
    }

    public float darkenAmount = 0.5f;
    public ButtonKeyPair[] buttons;

    void Start()
    {
        // Guardamos el color original de cada botón
        foreach (var pair in buttons)
        {
            if (pair.buttonImage != null)
                pair.originalColor = pair.buttonImage.color;
        }
    }

    void Update()
    {
        foreach (var pair in buttons)
        {
            if (pair.buttonImage == null) continue;

            if (Input.GetKey(pair.key))
            {
                pair.buttonImage.color = pair.originalColor * darkenAmount;
            }
            else
            {
                pair.buttonImage.color = pair.originalColor;
            }
        }
    }
}


