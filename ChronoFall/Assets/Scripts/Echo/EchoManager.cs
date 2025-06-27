using UnityEngine;

public class EchoManager : MonoBehaviour
{
    public GameObject echoPrefab;
    public TimeRecorder recorder;

    private bool isRecording = false;
    private Vector3 savedSpawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isRecording)
        {
            savedSpawnPoint = recorder.transform.position;
            isRecording = true;
            recorder.StartRecording();
            Debug.Log("Grabando desde: " + savedSpawnPoint);
            Invoke(nameof(StopRecordingAndSpawnEcho), recorder.recordDuration);
        }
    }
    public void SetTimeRecorder(TimeRecorder newRecorder)
    {
        recorder = newRecorder;
    }
    void StopRecordingAndSpawnEcho()
    {
        isRecording = false;
        recorder.StopRecording();

        GameObject echo = Instantiate(echoPrefab, savedSpawnPoint, Quaternion.identity);
        Debug.Log("Eco creado en: " + savedSpawnPoint);

        TimeEcho echoScript = echo.GetComponent<TimeEcho>();
        echoScript.SetPlayback(recorder.GetRecordedFrames());
    }
}


