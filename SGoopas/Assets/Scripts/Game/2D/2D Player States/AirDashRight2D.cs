using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class AirDashRight2D : Base2DState
    {
        private float dashTime = 0.3f;
        private float afterImageTime = 0.1f;
        private float timeEllapsed;
        private GameObject afterimage;
        private Vector3 direction;
        private IEnumerator coroutine;
        private Rigidbody2D rigidbody;
        private float origGrav;
        public AirDashRight2D(BasePlayerState previousState) : base(previousState)
        {
            direction = new Vector3();
            PseudoConstructor();
        }
        public AirDashRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck)
        {
            direction = new Vector3();
            PseudoConstructor();
        }

        public AirDashRight2D(BasePlayerState previousState, Vector3 direction) : base(previousState)
        {
            this.direction = direction;
            PseudoConstructor();
        }
        public AirDashRight2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck, Vector3 direction) : base(player, playerStateMachine, groundCheck)
        {
            this.direction = direction;
            PseudoConstructor();
        }

        private void PseudoConstructor()
        {
            afterimage = (GameObject)Resources.Load("AfterImage");
            timeEllapsed = 0.0f;
            rigidbody = PlayerObject.GetComponent<Rigidbody2D>();
            origGrav = rigidbody.gravityScale;
            rigidbody.gravityScale = 0.0f;
            rigidbody.velocity = direction.normalized * DashDistance / dashTime;
            coroutine = AfterImage();
            if(MasterMonoBehaviour.Instance != null)
             MasterMonoBehaviour.Instance.StartCoroutine(coroutine);
        }

        private void PseudoDestructor()
        {
            rigidbody.gravityScale = origGrav;
            if (MasterMonoBehaviour.Instance != null)
                MasterMonoBehaviour.Instance.StopCoroutine(coroutine);
            SetState(new JumpingRight2D(this));
        }

        public override void Action()
        {
            
        }

        public override void FixedUpdate()
        {

        }

        public override void Jump()
        {
            
        }

        public override void MoveDown()
        {

        }

        public override void MoveLeft()
        {
            
        }

        public override void MoveRight()
        {
            
        }

        public override void Update()
        {
            timeEllapsed += Time.deltaTime;
            if (timeEllapsed >= dashTime)
                PseudoDestructor();
        }

        public override void Death()
        {
            
        }

        IEnumerator AfterImage()
        {
            while(true)
            {
                SpawnAfterImage();
                yield return new WaitForSeconds(afterImageTime);
            }
        }

        private void SpawnAfterImage()
        {
            GameObject tempAfterImage = Object.Instantiate(afterimage, PlayerObject.transform.root);
            tempAfterImage.transform.position = PlayerObject.transform.position;
            Object.Destroy(tempAfterImage, dashTime);
        }

        public override void EnemyCollision(GameObject Enemy)
        {
            dash = true;
            Object.Destroy(Enemy);
        }
    }
}
