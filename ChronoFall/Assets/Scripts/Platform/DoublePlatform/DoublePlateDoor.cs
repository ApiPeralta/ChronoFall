using UnityEngine;

public class DoublePlateDoor : MonoBehaviour
{
    public PressurePlate plate1;
    public PressurePlate plate2;
    public Transform door;
    public float raiseHeight = 2f;
    public float speed = 2f;

    private Vector3 originalPos;
    private Vector3 raisedPos;
    private bool doorRaised = false;

    void Start()
    {
        if (door != null)
        {
            originalPos = door.position;
            raisedPos = originalPos + new Vector3(0f, raiseHeight, 0f);
        }
    }

    void Update()
    {
        if (door == null || plate1 == null || plate2 == null) return;

        if (!doorRaised && plate1.IsPressed && plate2.IsPressed)
        {
            doorRaised = true;
            plate1.LockPlate();
            plate2.LockPlate();
        }

        if (doorRaised)
        {
            door.position = Vector3.MoveTowards(door.position, raisedPos, speed * Time.deltaTime);
        }
    }
}
