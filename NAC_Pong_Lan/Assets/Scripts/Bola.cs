using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bola : NetworkBehaviour {

    public Rigidbody rigidBody;
    public float velocidade=5;
    CanvasController canvasController;
    public float myVel;
    private Transform pivotBola;

    // Use this for initialization
    void Start () {
        canvasController = FindObjectOfType<CanvasController>();
        pivotBola = GameObject.FindGameObjectWithTag("SpawnBola").GetComponent<Transform>();
        canvasController.Countdown(this);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        myVel = rigidBody.velocity.x;
    }

    public void Respawn()
    {
        transform.position = new Vector3(pivotBola.position.x, pivotBola.position.y, pivotBola.position.z);
        rigidBody.velocity = new Vector2(Mathf.Sign(Random.Range(-0.1f, 0.1f)) * velocidade, 0);

    }

    private void ResetBall()
    {
        //transform.position = rigidBody.velocity = Vector3.zero;
        transform.position = pivotBola.position;
        rigidBody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision col)
    {
        //Se eu collidir com o player nada acontece
        if (col.collider.CompareTag("Player"))
        {
            return;
        }

        //Se não acontece os itens abaixo

        float y = hitFactor(transform.position,
                    col.transform.position,
                    col.collider.bounds.size.y);

        Vector2 dir;

        if (transform.position.x > col.transform.position.x)
        {
            dir = new Vector2(1, y).normalized;
        }
        else
        {
            dir = new Vector2(-1, y).normalized;
        }

        rigidBody.velocity = dir * velocidade;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player1Score"))
        {
            canvasController.PlayerOneScores();
            ResetBall();
            canvasController.Countdown(this);
            //Respawn();
        }
        else if (col.CompareTag("Player2Score"))
        {
            canvasController.PlayerTwoScores();
            ResetBall();
            canvasController.Countdown(this);
            //Respawn();
        }
    }

    private float hitFactor(Vector2 ballPos, Vector2 playerPos, float racketHeight)
    {
        Debug.Log("ENTREI HIT FACTOR");
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - playerPos.y) / racketHeight;
    }
}
