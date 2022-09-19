using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PlaneDetectionController : MonoBehaviour
{
    ARPlaneManager m_ARplaneManager;
    ARPlacementManager m_ARRplacementManager;

    public GameObject placeButton;
    public GameObject AdjustButton;

    public GameObject SearchGameButton;

    public TextMeshProUGUI InformUIPanel_Text;

    public GameObject ScaleSlider;

    private void Awake() {
    
        m_ARRplacementManager = GetComponent<ARPlacementManager>();
        m_ARplaneManager = GetComponent<ARPlaneManager>();

    }
    // Start is called before the first frame update
    void Start()
    {
        placeButton.SetActive(true);
        AdjustButton.SetActive(false);
        SearchGameButton.SetActive(false);
        ScaleSlider.SetActive(true);

        InformUIPanel_Text.text = "Move phone to detect planes and place BattleArena!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableARPlaneAndPlacementDetection()
    {
        m_ARplaneManager.enabled = false;
        m_ARRplacementManager.enabled = false;

        SetAllPlaneActiveOrDeactive(false);

        placeButton.SetActive(false);
        AdjustButton.SetActive(true);

        SearchGameButton.SetActive(true);

        ScaleSlider.SetActive(false);

        InformUIPanel_Text.text = "Great you placed the Arena.. Now, Search for games to battle";
        
    }

      public void EnableARPlacementAndPlaneDetection(){

        m_ARplaneManager.enabled = true;
        m_ARRplacementManager.enabled = true;

        SetAllPlaneActiveOrDeactive(true);

        placeButton.SetActive(true);
        AdjustButton.SetActive(false);

        SearchGameButton.SetActive(false);

        ScaleSlider.SetActive(true);

         InformUIPanel_Text.text = "Move phone to detect planes and place BattleArena!";

    }

    private void SetAllPlaneActiveOrDeactive(bool value){

        foreach (var plane in m_ARplaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

  
}
