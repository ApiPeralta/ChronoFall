using System.Collections.Generic;
using UnityEngine;

public class TimeRecorder : MonoBehaviour
{
    public float recordDuration = 3f;
    private List<FrameData> recordedFrames = new List<FrameData>();
    private float timer = 0f;
    private bool isRecording = false;

    [System.Serializable]
    public struct FrameData
    {
        public Vector2 position;
        public bool facingRight;

        public FrameData(Vector2 pos, bool facing)
        {
            position = pos;
            facingRight = facing;
        }
    }

    public void StartRecording()
    {
        recordedFrames.Clear();
        isRecording = true;
        timer = 0f;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    void Update()
    {
        if (!isRecording) return;

        timer += Time.deltaTime;
        if (timer > recordDuration)
        {
            StopRecording();
            return;
        }

        bool facingRight = transform.localScale.x > 0;
        recordedFrames.Add(new FrameData(transform.position, facingRight));
    }

    public List<FrameData> GetRecordedFrames()
    {
        return recordedFrames;
    }
}

