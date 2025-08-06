using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform,
            Input.mousePosition,
            null,
            out pos);
        transform.localPosition = pos + new Vector2(50, 50); // Desplazado un poco
    }
}
