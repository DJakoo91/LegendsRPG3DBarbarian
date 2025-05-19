using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinRing1 : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 50f, 0f);
    public LevelLoader lvl;
    public bool useLocalRotation = true;

    void Update()
    {
        if (useLocalRotation)
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
        else
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lvl.LoadLevel1VICTORY();
        }
    }
}