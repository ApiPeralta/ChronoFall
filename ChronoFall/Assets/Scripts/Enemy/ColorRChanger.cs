using UnityEngine;

public class ColorRChanger : MonoBehaviour
{
    public Material material;
    public float changeSpeed = 0.5f;
    private bool increasing = true;

    private void Update()
    {
        if (material != null)
        {
            Color color = material.color;

            if (increasing)
            {
                color.r += changeSpeed * Time.deltaTime;
                if (color.r >= 1f)
                {
                    color.r = 1f;
                    increasing = false;
                }
            }
            else
            {
                color.r -= changeSpeed * Time.deltaTime;
                if (color.r <= 0f)
                {
                    color.r = 0f;
                    increasing = true;
                }
            }

            material.color = color;
        }
    }
}

