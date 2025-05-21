using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorGate : MonoBehaviour
{
    [SerializeField] GameObject mimic;
    private Vector3 enterPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterPosition = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            float offset_x = other.transform.position.x - transform.position.x;
            Vector3 mimicPos = new Vector3(transform.position.x - offset_x, other.transform.position.y, 0);
            Instantiate(mimic, mimicPos, Quaternion.identity);
        }
    }
}
