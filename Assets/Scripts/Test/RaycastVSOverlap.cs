using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastVSOverlap : MonoBehaviour
{
    public bool Raycast;
    // Use this for initialization
    void Start()
    {

        StartCoroutine(RayCast());

        StartCoroutine(Overlap());


    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator RayCast()
    {
        while (true)
        {
            if (Raycast)
            {
                int i = 0;

                while (i < 10000)
                {
                    Physics.Raycast(Vector3.zero, Vector3.forward, 10f);
                    Physics.Raycast(Vector3.zero, Vector3.forward, 10f);
                    Physics.Raycast(Vector3.zero, Vector3.forward, 10f);
                    Physics.Raycast(Vector3.zero, Vector3.forward, 10f);
                    Physics.Raycast(Vector3.zero, Vector3.forward, 10f);
                    i++;
                }
            }
            yield return null;
        }
    }

    public IEnumerator Overlap()
    {
        while (true)
        {
            if (!Raycast)
            {
                int i = 0;
                while (i < 10000)
                {
                    var c = Physics.OverlapBox(Vector3.zero, new Vector3(.5f, 2f, .5f), Quaternion.identity);
                    i++;
                }
            }
            yield return null;
        }
    }
}
