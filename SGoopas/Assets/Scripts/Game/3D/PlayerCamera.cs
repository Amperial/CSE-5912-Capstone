using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject player3D;
    public GameObject player2D;

    private bool is2D;
    
    [Range(0, 1)]
    public float cameraStiffness = .1f;
    private GameObject relevantGameObject;
    public Vector3 distanceFromTarget;

    void Start() {
        relevantGameObject = player3D;
    }

    // Update is called once per frame
    void Update() {
        Vector3 target = relevantGameObject.transform.position - distanceFromTarget; //10
        Vector3 translation = (target - transform.position) * cameraStiffness;
        transform.position += translation;
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
