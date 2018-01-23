using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScreen : MonoBehaviour {

    Camera m_MainCamera;
    int pxHeight;
    public float credHeightTop = 231f;
    public float credHeightBot = 1600f;
    public float speed = 4f;
	void Start () {
        m_MainCamera = Camera.main;
        pxHeight = m_MainCamera.pixelHeight;
        pxHeight /= 2;
        transform.Translate(new Vector3(0, -1 * pxHeight - credHeightTop, 0));
        credHeightBot += pxHeight;
	}
	
	void Update () {
		if (transform.position.y < credHeightBot)
        {
            transform.Translate(new Vector3(0, speed, 0));
        } else {
            MasterStateMachine.Instance.setState(new MainMenuState());
        }
	}
}
