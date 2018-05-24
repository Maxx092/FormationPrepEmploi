using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float count;
    private float jumpForce = 400.0f;

    public Rigidbody enemyRigidBody;

    private void Start()
    {
        count = 3.0f;
    }

    void Update ()
    {
        //StartCoroutine(WaitBeforeJump());
        count -= Time.deltaTime;
        if (count <= 0.0f)
        {
            Jump();
            count += 4.0f;
        }
    }

    private void Jump()
    {
        enemyRigidBody.AddForce(Vector3.up * jumpForce);
    }

   /* private IEnumerator WaitBeforeJump()
    {
        yield return new WaitForSeconds(2.0f);
        Jump();
    }*/
}
