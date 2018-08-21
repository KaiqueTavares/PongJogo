using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Usando a biblioteca do network
using UnityEngine.Networking;

//Estou herdando o script 
public class NetworkManagerScript : NetworkManager{

    //Aqui declaro as variaveis que vou isntanciar quando começa o jogo a posição
    //E flags para determinar se eu sou o host e se estamos jogando
    private bool isHost = false;
    public GameObject bola;
    public Transform spawnPositionBola;
    private bool isPlaying = false;

	
	// Update is called once per frame
	void Update () {
        //Posso inciiar o jogo e não estão jogando?
		if (CanStartGame() && !isPlaying)
        {
            //Estou jogando true, para não ficar instanciando varias bolas
            isPlaying = true;
            //Spawno a bola;
            SpawnBola();
        }
	}

    //Crio uma variavel que retorna o numero de conexoes e o host = true ou false
    private bool CanStartGame()
    {
        return NetworkServer.connections.Count == 2 && isHost;
    }

    //Se eu clico que sou host seto a variavel para true
    public override void OnStartHost()
    {
        base.OnStartHost();
        isHost = true;
    }

    //Spawner da bola
    void SpawnBola()
    {
        //Instancio a bola dentro de uma variavel
        GameObject bolaInstanciada = Instantiate(bola, spawnPositionBola.position, spawnPositionBola.rotation);
        //Instanciando a bola pelo server;
        NetworkServer.Spawn(bolaInstanciada);
    }
}
