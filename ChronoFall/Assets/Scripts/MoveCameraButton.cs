using UnityEngine;
using System.Collections;
using UnityEngine;
public class MoveUIObject : MonoBehaviour
{
    public RectTransform uiObject;
    public float moveAmount = 500f;
    public float moveDuration = 0.5f;

    private Vector2 initialPosition;
    private Coroutine currentMove;

    void Start()
    {
        if (uiObject == null)
            uiObject = GetComponent<RectTransform>();

        initialPosition = uiObject.anchoredPosition;
    }

    public void MoveDown()
    {
        Vector2 target = initialPosition - new Vector2(0, moveAmount);
        StartMove(target);
    }

    public void MoveUp()
    {
        StartMove(initialPosition);
    }

    private void StartMove(Vector2 targetPosition)
    {
        if (currentMove != null)
            StopCoroutine(currentMove);

        currentMove = StartCoroutine(MoveToPosition(targetPosition));
    }

    private IEnumerator MoveToPosition(Vector2 target)
    {
        Vector2 start = uiObject.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            uiObject.anchoredPosition = Vector2.Lerp(start, target, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        uiObject.anchoredPosition = target;
        currentMove = null;
    }
}




