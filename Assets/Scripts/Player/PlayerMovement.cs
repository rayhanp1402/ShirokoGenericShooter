using System;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace Nightmare
{
    public class PlayerMovement : PausibleObject
    {
        public float speed = 6f;            // The speed that the player will move at.

        public float baseSpeed = 6f; 
        public float baseAttack = 5f;
        public float initAttack = 5f;
        public int orbCount = 0;

        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

#if !MOBILE_INPUT
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.
#endif

        void Awake ()
        {
#if !MOBILE_INPUT
            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask ("Floor");
#endif

            // Set up references.
            anim = GetComponent <Animator> ();
            playerRigidbody = GetComponent <Rigidbody> ();
            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void FixedUpdate ()
        {
            if (isPaused)
                return;

            // Store the input axes.
            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

            // Move the player around the scene.
            Move (h, v);

            // Turn the player to face the mouse cursor.
            Turning ();

            // Animate the player.
            Animating (h, v);
        }

        public void BoostShooting()
        {
            baseAttack += 0.1f * initAttack;
        }

        void Move (float h, float v)
        {
            // Set the movement vector based on the axis input.
            movement.Set (h, 0f, v);
            
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition (transform.position + movement);
        }


        void Turning ()
        {
#if !MOBILE_INPUT
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

                // Apply the new rotation to your player
                transform.rotation = newRotation;

                // Vector3 newPos = floorHit.point;
                // newPos.y = target.transform.position.y;
                // target.transform.position = newPos;
            }
#else

            Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
#endif
        }

        public void SpeedBoost(float duration)
        {
            StartCoroutine(SpeedBoostCoroutine(duration));
        }

        private System.Collections.IEnumerator SpeedBoostCoroutine(float duration)
        {
            float maxSpeed = 1.2f * baseSpeed;
            speed = maxSpeed;
            yield return new WaitForSeconds(duration);
            speed = baseSpeed;
        }

        public void Reduce(float speedAmount, float attackAmount)
        {
            this.speed = speedAmount;
            this.baseAttack = attackAmount;
        }

        void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;
            if (walking) {
                float rotationY = transform.rotation.eulerAngles.y;
                Vector3 walkDirection = new Vector3(h, 0, v);
                Quaternion targetRotation = Quaternion.LookRotation(walkDirection);
                float targetRotationY = targetRotation.eulerAngles.y;

                float direction = targetRotationY - rotationY;
                if (direction < 0) {
                    direction = 360 + direction;
                }

                float yRotationAngleRadians = direction * Mathf.Deg2Rad;

                // Calculate the direction vector
                Vector3 direction3 = new Vector3(Mathf.Sin(yRotationAngleRadians), 0f, Mathf.Cos(yRotationAngleRadians));

                // Normalize the direction vector if needed
                direction3.Normalize();

                // Now 'direction' contains the direction vector based on the y rotation angle
                // Debug.Log("Direction Vector: " + direction3);

                

                // float blendx = Mathf.Acos(diff*Mathf.Deg2Rad);
                // float blendy = Mathf.Sin(diff*Mathf.Deg2Rad);

                

                anim.SetFloat("Blendx", direction3.x);
                anim.SetFloat("Blendy", direction3.z);
            } else {
                anim.SetFloat("Blendx", 0);
                anim.SetFloat("Blendy", 0);
            }

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsWalking", walking);
        }
    }
}