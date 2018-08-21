using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Implementando a biblioteca do server
using UnityEngine.Networking;
public class Player : NetworkBehaviour {

    public float velocidade;
    private bool batiParede;
	
	void Update () {
		if (!isLocalPlayer)
        {
            return;
        }

        
        float y = Input.GetAxis("Vertical") * velocidade * Time.deltaTime;
        transform.Translate(0.0f, y, 0.0f);


    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

}
