using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLife : MonoBehaviour
{
    public int life = 6;
    private void Update()
    {
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            life--;
        }
    }
}
