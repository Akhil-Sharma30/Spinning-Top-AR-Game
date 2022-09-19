using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnManager : MonoBehaviourPunCallbacks
{

    public GameObject[] playerPrefabs;
    public Transform[] spawnPositions;
    public GameObject BattleArena;

    public enum RaiseEventCodes{

        playerSpawnEventCode = 0
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += onEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnDestroy() {
        
        PhotonNetwork.NetworkingClient.EventReceived -= onEvent;
    }

    void onEvent(EventData photonEvent)
    {
        if(photonEvent.Code == (byte)RaiseEventCodes.playerSpawnEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;

            Vector3 recievePosition = (Vector3)data[0];

            Quaternion recieveRotation = (Quaternion)data[1];
            int recievePlayerSelectionData = (int)data[3];

            GameObject player = Instantiate(playerPrefabs[recievePlayerSelectionData],recievePosition + BattleArena.transform.position,recieveRotation);
            PhotonView _photoView = player.GetComponent<PhotonView>();
            _photoView.ViewID = (int)data[2];
            
        }
    }

    #region Photon Callback Methods
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {

            // object playerSelectionNumber;
            // if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerARSpinnerTopGame.PLAYER_SELECTION_NUMBER, out playerSelectionNumber))
            // {
            //     Debug.Log("Player selection number is "+ (int)playerSelectionNumber);

            //     int randomSpawnPoint = Random.Range(0, spawnPositions.Length-1);
            //     Vector3 instantiatePosition = spawnPositions[randomSpawnPoint].position;


            //     PhotonNetwork.Instantiate(playerPrefabs[(int)playerSelectionNumber].name, instantiatePosition, Quaternion.identity);

            // }

            SpawnPlayer();

        }      

       
    }


    #endregion

    private void SpawnPlayer()
    {
        object playerSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerARSpinnerTopGame.PLAYER_SELECTION_NUMBER, out playerSelectionNumber))
            {
                Debug.Log("Player selection number is "+ (int)playerSelectionNumber);

                int randomSpawnPoint = Random.Range(0, spawnPositions.Length-1);
                Vector3 instantiatePosition = spawnPositions[randomSpawnPoint].position;


                GameObject playerGameObject = Instantiate(playerPrefabs[(int)playerSelectionNumber],instantiatePosition,Quaternion.identity);

                PhotonView _photonView = playerGameObject.GetComponent<PhotonView>();

                if(PhotonNetwork.AllocateViewID(_photonView))
                {
                    object[] data = new object[]
                    {
                        playerGameObject.transform.position- BattleArena.transform.position ,playerGameObject.transform.rotation,_photonView.ViewID , playerSelectionNumber
                    };
                
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others,
                        CachingOption = EventCaching.AddToRoomCache
                    };

                    SendOptions sendOptions = new SendOptions
                    {
                        Reliability = true
                    };

                    //Raise Events!
                    PhotonNetwork.RaiseEvent((byte)RaiseEventCodes.playerSpawnEventCode,data,raiseEventOptions,sendOptions);

                }
                else{

                    Debug.Log("failed to allocate a viewID");
                    Destroy(playerGameObject);
                }

            }

    }



}
