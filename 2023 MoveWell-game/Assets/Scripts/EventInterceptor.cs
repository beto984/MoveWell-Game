using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInterceptor : MonoBehaviour
{
    Dictionary<string, int> positionsMap = new Dictionary<string, int>();

    public SmartToyEventManager EV_A;
    public SmartToyEventManager EV_B;
    public SmartToyEventManager EV_C;
    public SmartToyEventManager EV_D;
    public SmartToyEventManager EV_E;
    public DisplayManager disManager;
    bool init = true;
    string[] currentSpheresPositions = new string[5];
    public string[] solution;

    // Start is called before the first frame update
    void Start()
    {
        positionsMap.Add("A", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (init)
        {
            EV_A.readRFID.AddListener(ReadTagE);
            EV_A.releaseRFID.AddListener(() => ReleaseTagE());
            EV_B.readRFID.AddListener(ReadTagE);
            EV_B.releaseRFID.AddListener(() => ReleaseTagE());
            EV_C.readRFID.AddListener(ReadTagE);
            EV_C.releaseRFID.AddListener(() => ReleaseTagE());
            EV_D.readRFID.AddListener(ReadTagE);
            EV_D.releaseRFID.AddListener(() => ReleaseTagE());
            EV_E.readRFID.AddListener(ReadTagE);
            EV_E.releaseRFID.AddListener(() => ReleaseTagE());
            init = false;
        }

        //check for the solution
        if(currentSpheresPositions == solution)
        {
            //reward
            disManager.SetUpConfiguration();
        }
    }

    public void ReadTagD(string tag) {
        Debug.Log("HI");
    }
    public void ReleaseTagD()
    {
        Debug.Log("HI released");
    }

    public void ReadTagE(string tag)
    {
        Debug.Log("HI blue");
        currentSpheresPositions[positionsMap[tag]] = "B";
    }
    public void ReleaseTagE()
    {
        Debug.Log("HI released");
        Debug.Log("bye blue");
        for( int i =0; i < currentSpheresPositions.Length; i++)
        {
            if (currentSpheresPositions[i] == "B")
            {
                currentSpheresPositions[i] = "";
                break;
            }
        }
            
    }
}
