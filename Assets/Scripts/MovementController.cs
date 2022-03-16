using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject targetCamera;
    public Rigidbody rb;
    //public Animator animatorSystem;

    public float moveSpeed = 10.0f;
    public float turnSpeed = 1.0f;
    public float jumpForce = 100.0f;

    bool isAttacking = false;
    bool isJumping = false;

	private void Update()
	{
        Movement();
        Jump();
	}
	void Movement()
    {
        Vector3 resultVelocity = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetKey(KeyCode.W) == true)
        {
            resultVelocity += targetCamera.transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.S) == true)
        {
            resultVelocity -= targetCamera.transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.D) == true)
        {
            resultVelocity += targetCamera.transform.right * moveSpeed;
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            resultVelocity -= targetCamera.transform.right * moveSpeed;
        }

        resultVelocity = new Vector3(resultVelocity.x, rb.velocity.y, resultVelocity.z);
        rb.velocity = resultVelocity;

        Vector3 velocityNoY = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (velocityNoY.magnitude != 0)     // Get Result Speed for All Direction as a float.
        {
            // Point to the next position that Velocity will move this object to.
            Vector3 directionPointTo = gameObject.transform.position + velocityNoY;      // Current Position + Velocity = Next Location.
            Quaternion fromQuaternion = gameObject.transform.rotation;  // Rotation before Look At
            gameObject.transform.LookAt(directionPointTo);
            Quaternion toQuaternion = gameObject.transform.rotation;    // Rotation after Look At

            gameObject.transform.rotation = Quaternion.Lerp(fromQuaternion, toQuaternion, turnSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            rb.velocity = Vector3.up * jumpForce;
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision hitWithObject)
    {
        Vector3 playerPosition = gameObject.transform.position + new Vector3(0, 1, 0);
        Vector3 contactPoint = GetCenterOfContact(hitWithObject);

        if (playerPosition.y >= contactPoint.y)  // If contact point is below the player. It means it should contact ground.
        {
            isJumping = false;
        }
    }

    Vector3 GetCenterOfContact(Collision hitWithObject)
    {
        // Get Average Position of Contact Points. Sum them all and divide by how many of them.

        Vector3 centerOfContactPoint = new Vector3(0, 0, 0);
        ContactPoint[] contactPointList = new ContactPoint[hitWithObject.contactCount]; // Create an Array to keep all contact points.
        hitWithObject.GetContacts(contactPointList);    // Get All contact points and set it into contactPointList.

        for (int index = 0; index < contactPointList.Length; index += 1)
        {
            centerOfContactPoint += contactPointList[index].point; // Sum all contant points location together.
        }

        centerOfContactPoint = centerOfContactPoint / hitWithObject.contactCount; // Calculate Average of them to find center.

        return centerOfContactPoint;
    }
}
