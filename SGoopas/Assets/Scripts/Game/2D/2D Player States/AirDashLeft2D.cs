using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class AirDashLeft2D : Base2DState
    {
        private float afterImageTime = 0.1f;
        private float timeEllapsed;
        private GameObject afterimage;
        private Vector3 direction;
        private IEnumerator coroutine;
        private float origGrav;
        public AirDashLeft2D(BasePlayerState previousState) : base(previousState)
        {
            direction = DashVector;
            PseudoConstructor();
        }
        public AirDashLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck) : base(player, playerStateMachine, groundCheck)
        {
            direction = DashVector;
            PseudoConstructor();
        }

        public AirDashLeft2D(BasePlayerState previousState, Vector3 direction) : base(previousState)
        {
            this.direction = direction;
            PseudoConstructor();
        }
        public AirDashLeft2D(GameObject player, MasterPlayerStateMachine playerStateMachine, Transform groundCheck, Vector3 direction) : base(player, playerStateMachine, groundCheck)
        {
            this.direction = direction;
            PseudoConstructor();
        }

        private void PseudoConstructor()
        {
                afterimage = (GameObject)Resources.Load("AfterImage");
                timeEllapsed = 0.0f;
                origGrav = rb.gravityScale;
                rb.gravityScale = 0.0f;
                rb.velocity = direction.normalized * DashDistance / DashTime;
                coroutine = AfterImage();
                if (MasterMonoBehaviour.Instance != null)
                    MasterMonoBehaviour.Instance.StartCoroutine(coroutine);
        }

        private void PseudoDestructor()
        {
            rb.gravityScale = origGrav;
            if (MasterMonoBehaviour.Instance != null)
                MasterMonoBehaviour.Instance.StopCoroutine(coroutine);
            SetState(new JumpingLeft2D(this));
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
            rb.velocity = direction.normalized * DashDistance / DashTime;
            if (timeEllapsed >= DashTime)
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
            tempAfterImage.transform.localScale = new Vector3(tempAfterImage.transform.localScale.x * -1, tempAfterImage.transform.localScale.y, tempAfterImage.transform.localScale.z);
            Object.Destroy(tempAfterImage, DashTime);
        }

        public override void EnemyCollision(GameObject Enemy)
        {
            dash = true;
            Object.Destroy(Enemy);
        }

        public override void StoreState()
        {
            if (MasterMonoBehaviour.Instance != null)
                MasterMonoBehaviour.Instance.StopCoroutine(coroutine);
            base.StoreState();
        }

        public override void RestoreState()
        {
            coroutine = AfterImage();
            if (MasterMonoBehaviour.Instance != null)
                MasterMonoBehaviour.Instance.StartCoroutine(coroutine);
            base.RestoreState();
        }
    }
}
