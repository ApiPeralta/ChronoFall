using UnityEngine;

public class RopeBuilder : MonoBehaviour
{
    public GameObject ropeSegmentPrefab;
    public int segmentCount = 10;
    public float segmentSpacing = 0.5f;
    public Transform anchorPoint;

    void Start()
    {
        Rigidbody2D previousBody = null;
        Vector3 spawnPosition = anchorPoint.position;

        for (int i = 0; i < segmentCount; i++)
        {
            // Crear segmento
            GameObject segment = Instantiate(ropeSegmentPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = segment.GetComponent<Rigidbody2D>();
            HingeJoint2D joint = segment.GetComponent<HingeJoint2D>();

            // Posicionar siguiente segmento justo debajo
            spawnPosition.y -= segmentSpacing;

            if (i == 0)
            {
                // Primer segmento conectado al mundo (techo)
                joint.connectedBody = null;
                joint.connectedAnchor = anchorPoint.position;
            }
            else
            {
                joint.connectedBody = previousBody;
            }

            previousBody = rb;
        }
    }
}


