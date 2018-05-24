using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 250f;
    [SerializeField]
    private float jumpForce = 550f;
    private Vector3 positionInitial;
    private Vector3 direction;
    private Vector3 positionOffset;
    private Rigidbody myRigidBody;
    private bool canJump;
    
    public LayerMask myLayerMask;

	void Start ()
    {
        positionInitial = transform.position;
        positionOffset = new Vector3();
        myRigidBody = gameObject.GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
		if (Input.GetKey(KeyCode.A))
        {
            direction = -transform.right * (speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = transform.right * (speed * Time.deltaTime);
        }
        else
        {
            direction = Vector3.zero;
        }

        if (canJump == true)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space ))
            {
                myRigidBody.AddForce(Vector3.up * jumpForce);
                
            }
        }
        direction.y = myRigidBody.velocity.y;
        myRigidBody.velocity = direction;

        if (transform.position.y <= -3)
        {
            GameManager.Instance.LifeLost();
            GameManager.Instance.Init();
            transform.position = positionInitial;
        }
        positionOffset = transform.position;
        positionOffset.x = transform.position.x - (transform.localScale.x / 2);
        Ray leftRay = new Ray(positionOffset, Vector3.down);
        positionOffset.x = transform.position.x + (transform.localScale.x / 2);
        Ray rightRay = new Ray( positionOffset, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(rightRay, out hit, 0.6f, myLayerMask) || Physics.Raycast(leftRay, out hit, 0.6f, myLayerMask))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

    }

    private void OnTriggerEnter(Collider aCol)
    {
        if (aCol.gameObject.tag == "Coin")
        {
            GameManager.Instance.TakeCoin(aCol);
                
        }

        if (aCol.gameObject.tag == "EnemyHead")
        {
            Enemy enemy = aCol.GetComponentInParent<Enemy>();
            enemy.gameObject.SetActive(false);
            myRigidBody.velocity = Vector3.zero;
            myRigidBody.AddForce(Vector3.up * (jumpForce / 1.5f));
        }
    }

    private void OnCollisionEnter(Collision aCol)
    {
        if (aCol.gameObject.tag == "Enemy")
        {
            GameManager.Instance.LifeLost();
            GameManager.Instance.Init();

            transform.position = positionInitial;
        }

        if (aCol.gameObject.tag == "Goal")
        {
            GameManager.Instance.GoalReach();
            GameManager.Instance.LifeLost();
            transform.position = positionInitial;
        }
    }
}
