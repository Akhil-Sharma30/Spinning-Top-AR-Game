using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;
    public Camera ARCamera;
    public GameObject BattleArenaGameObJECT;

    static List<ARRaycastHit> raycast_Hits = new List<ARRaycastHit>();

    private void Awake() {
        
        m_ARRaycastManager = GetComponent<ARRaycastManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //getting the center of the screen
        Vector3 centerOfScreen = new Vector3(Screen.width/2 , Screen.height/2);
        Ray ray = ARCamera.ScreenPointToRay(centerOfScreen); 

        if(m_ARRaycastManager.Raycast(ray,raycast_Hits,TrackableType.PlaneWithinPolygon))
        {

            //Intersection
            Pose hitPose = raycast_Hits[0].pose;

            Vector3 positionToPlaced = hitPose.position;

            BattleArenaGameObJECT.transform.position = positionToPlaced;
            

        }

    }
}
