using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject player3D;
    public GameObject player2D;

    private bool is2D;
    Vector3 initialCameraOffset3D;

    private GameObject relevantGameObject;

    void Start() {
        relevantGameObject = player3D;
        initialCameraOffset3D = player3D.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update() {
        Vector3 posDiv = (relevantGameObject.transform.position - transform.position) - initialCameraOffset3D;
        transform.Translate(posDiv.x, 0f, posDiv.z, Space.World);
    }

    private void Follow2DPlayer() {
        relevantGameObject = player2D;
    }

    private void Follow3DPlayer() {
        relevantGameObject = player3D;
    }

    public void SwitchTo2D(Cancellable cancellable) {
        cancellable.PerformCancellable(Follow2DPlayer, Follow3DPlayer);
    }

    public void SwitchTo3D(Cancellable cancellable) {
        cancellable.PerformCancellable(Follow3DPlayer, Follow2DPlayer);
    }

}
