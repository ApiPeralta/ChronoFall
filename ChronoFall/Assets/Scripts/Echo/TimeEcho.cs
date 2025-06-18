using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEcho : MonoBehaviour
{
    public float playbackInterval = 0.02f;

    private List<TimeRecorder.FrameData> playbackData;

    public void SetPlayback(List<TimeRecorder.FrameData> data)
    {
        playbackData = data;
        StartCoroutine(PlaybackRoutine());
        Debug.Log("Reproduciendo eco...");
    }

    IEnumerator PlaybackRoutine()
    {
        foreach (var frame in playbackData)
        {
            transform.position = frame.position;

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (frame.facingRight ? 1 : -1);
            transform.localScale = scale;

            yield return new WaitForSeconds(playbackInterval);
        }

        Destroy(gameObject); // Eco desaparece al terminar
    }
}

