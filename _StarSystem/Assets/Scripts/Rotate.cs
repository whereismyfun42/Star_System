using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	public float speed = 10.0f;
	
	public enum whichWayToRotate {AroundX, AroundY, AroundZ, minusX, minusY, minusZ}

	public whichWayToRotate way = whichWayToRotate.AroundX;
	
	// Update is called once per frame
	void Update () {

		switch(way)
		{
		case whichWayToRotate.AroundX:
			transform.Rotate(Vector3.right * Time.deltaTime * speed);
			break;
		case whichWayToRotate.AroundY:
			transform.Rotate(Vector3.up * Time.deltaTime * speed);
			break;
		case whichWayToRotate.AroundZ:
			transform.Rotate(Vector3.forward * Time.deltaTime * speed);
			break;
            case whichWayToRotate.minusX:
                transform.Rotate(-Vector3.right * Time.deltaTime * speed);
                break;
            case whichWayToRotate.minusY:
                transform.Rotate(-Vector3.up * Time.deltaTime * speed);
                break;
            case whichWayToRotate.minusZ:
                transform.Rotate(-Vector3.forward * Time.deltaTime * speed);
                break;
        }	
	}
}