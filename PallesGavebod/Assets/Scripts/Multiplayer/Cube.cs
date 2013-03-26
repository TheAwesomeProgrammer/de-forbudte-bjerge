using System;
using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    public Transform playerPrefab;
    public ArrayList playerScripts = new ArrayList();

    void OnServerInitialized()
    {
        SpawnPlayer(Network.player);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        SpawnPlayer(player);
    }


    void OnPlayerDisconnected(NetworkPlayer player)
    {
        RemovePlayer(player);
    }

    void SpawnPlayer(NetworkPlayer player)
    {
        string tempPlayerString = player.ToString();
        int playerNumber = Convert.ToInt32(tempPlayerString);
        Transform newPlayerTransform =
            (Transform) Network.Instantiate(playerPrefab, transform.position, transform.rotation, playerNumber);
        playerScripts.Add(newPlayerTransform.GetComponent("MoveCubeAuthoritative"));
        NetworkView theNetworkView = newPlayerTransform.networkView;
        theNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
    }

    void RemovePlayer(NetworkPlayer player)
    {
        foreach (MoveCubeAuthoritative tPlayer in playerScripts)
        {
            if(tPlayer.theOwner == player)
            {
                Network.RemoveRPCs(tPlayer.gameObject.networkView.viewID);//remove the bufferd SetPlayer call
                Network.Destroy(tPlayer.gameObject);//Destroying the GO will destroy everything
                playerScripts.Remove(tPlayer);//Remove this player from the list
                break;
            }
        }
        int playerNumber = 0;
       int.TryParse(player+"",out playerNumber);
	Network.RemoveRPCs(Network.player, playerNumber);
	
	
	// The next destroys will not destroy anything since the players never
	// instantiated anything nor buffered RPCs
	Network.RemoveRPCs(player);
	Network.DestroyPlayerObjects(player);
    }
   


    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
