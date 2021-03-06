
using TestTask.Core;
using UnityEngine;

namespace TestTask.Movement
{
    public class PlayerMovement : Movement
    {

        bool setAttack = false;
        public float speed;
        private new void Awake() {
            base.Awake();
        }
       
        private void Update()
        {

            if (Input.anyKey)
                Move();
            else if (!setAttack)
            {
                setAttack = true;
                if (GameHandler.instance.EnemyExits())
                    attack.SetCanAttack(enemy);
            }
        }
        ///<summary>
        ///Control the player with the use of arrow key.
        ///</summary>
        void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            //Determine if the player is pressing the WASD or ARROW key
            if (horizontal == 0f && vertical == 0f)
            {
                return;
            }
            setAttack = false;
            CollisionCheck();
            behaviour.ChangeBehaviour(this);
            float perFrameTime = Time.deltaTime;
            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
            //Move player by speed value every franme
            transform.position = transform.position + moveDirection * perFrameTime * speed;
            //Look towards the move direction
            transform.rotation = LookAtDirection(animationMesh.rotation, moveDirection);
        }
    }

}
