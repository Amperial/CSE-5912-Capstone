using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject player3D;
    public GameObject player2D;
    private bool is2D;
    
    [Range(0, 3)]
    public float cameraStiffness = 1.5f;
    public int maxLookDistance = 10;
    private GameObject relevantGameObject;
    public Vector3 distanceFromTarget = new Vector3(0, -8, 10);

    void Start() {
        relevantGameObject = player3D;
    }

    // Update is called once per frame
    void Update() {
        Vector3 target = relevantGameObject.transform.position - distanceFromTarget;
        Vector3 center = target;
        if(Input.GetKey(KeyCode.I)){
            target += Vector3.up*maxLookDistance;
        }
        if(Input.GetKey(KeyCode.K)){
            target += Vector3.down*maxLookDistance;
        }

        if(Input.GetKey(KeyCode.J)){
            target += Vector3.left*maxLookDistance;
        }
        if(Input.GetKey(KeyCode.L)){
            target += Vector3.right*maxLookDistance;
        }
        
        //This will rubberband the camera around the center
        transform.position += (center + target - 2*transform.position) * cameraStiffness * Time.deltaTime;
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
