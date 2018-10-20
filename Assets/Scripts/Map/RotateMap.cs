using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class RotateMap : MonoBehaviour {

    public PlayerData data;
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, data.Speed, 0));
	}
}
