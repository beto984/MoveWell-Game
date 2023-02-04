using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInterceptor : MonoBehaviour
{
    Dictionary<string, int> positionsMap = new Dictionary<string, int>();
    string[] currentSpheresPositions = new string[5];

    public SmartToyEventManager EV_A;
    public SmartToyEventManager EV_B;
    public SmartToyEventManager EV_C;
    public SmartToyEventManager EV_D;
    public SmartToyEventManager EV_E;
    [SerializeField] public DisplayManager disManager;
    bool init = true;
    public string[] solution;

    // Start is called before the first frame update
    void Start()
    {
        positionsMap.Add("A", 1);
        //positionsMap.Add("B", 2);
        //positionsMap.Add("C", 3);
        //positionsMap.Add("D", 4);
        //positionsMap.Add("E", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (init)
        {
            EV_A.readRFID.AddListener(ReadTagA);
            EV_A.releaseRFID.AddListener(() => ReleaseTagA());
            EV_B.readRFID.AddListener(ReadTagB);
            EV_B.releaseRFID.AddListener(() => ReleaseTagB());
            EV_C.readRFID.AddListener(ReadTagC);
            EV_C.releaseRFID.AddListener(() => ReleaseTagC());
            EV_D.readRFID.AddListener(ReadTagD);
            EV_D.releaseRFID.AddListener(() => ReleaseTagD());
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

    public void ReadTagA(string tag)
    {
        Debug.Log("Hello Red");
        currentSpheresPositions[positionsMap[tag]] = "R";
    }

    public void ReleaseTagA()
    {
        Debug.Log("Bye Red");
        removeSphere("R");
    }
    
    public void ReadTagB(string tag)
    {
        Debug.Log("Hello Pink");
        currentSpheresPositions[positionsMap[tag]] = "P";
    }

    public void ReleaseTagB()
    {
        Debug.Log("Bye Pink");
        removeSphere("P");
    }
    
    public void ReadTagC(string tag)
    {
        Debug.Log("Hello Green");
        currentSpheresPositions[positionsMap[tag]] = "G";
    }

    public void ReleaseTagC()
    {
        Debug.Log("Bye Green");
        removeSphere("G");
    }

    public void ReadTagD(string tag) {
        Debug.Log("Hello Yellow");
        currentSpheresPositions[positionsMap[tag]] = "Y";

    }
    public void ReleaseTagD()
    {
        Debug.Log("Bye Yellow");
        removeSphere("Y");
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
        removeSphere("B");
            
    }

    private void removeSphere(string sphere)
    {
        for (int i = 0; i < currentSpheresPositions.Length; i++)
        {
            if (currentSpheresPositions[i] == sphere)
            {
                currentSpheresPositions[i] = "";
                break;
            }
        }
    }
}
