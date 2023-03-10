using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventInterceptor : MonoBehaviour
{
    Dictionary<string, int> positionsMap = new Dictionary<string, int>();
    string[] currentSpheresPositions;
    private const int MENU_SCENE_INDEX = 1;

    public SmartToyEventManager EV_A;
    public SmartToyEventManager EV_B;
    public SmartToyEventManager EV_C;
    public SmartToyEventManager EV_D;
    public SmartToyEventManager EV_E;
    private bool inreward = false;
    [SerializeField] public DisplayManager disManager;
    bool init = true;
    public string[] solution;
    public bool isVertical;

    private AudioSource rewardSound;

    // Start is called before the first frame update
    void Awake()
    {
        currentSpheresPositions = new string[5];
        solution = new string[5];

        
            positionsMap.Add("0V", 0);
            positionsMap.Add("1V", 1);
            positionsMap.Add("2V", 2);
            positionsMap.Add("3V", 3);
            positionsMap.Add("4V", 4);
            positionsMap.Add("0H", 0);
            positionsMap.Add("1H", 1);
            positionsMap.Add("2H", 2);
            positionsMap.Add("3H", 3);
            positionsMap.Add("4H", 4);
        
    }

    private void Start()
    {
        rewardSound = disManager.gameObject.GetComponent<AudioSource>();
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
        if (allSpheresArePlaced() && solution[1] != null)
        {
            Debug.Log("Balls placed");
            if (checkAnswer(currentSpheresPositions, solution))
            {
                currentSpheresPositions = new string[5];
                solution = new string[5];
                //reward
                StartCoroutine(reward());
            }
            else if ( !inreward) {
                inreward = true;
                StartCoroutine(badReward());
            }
        } 
        
    }

    public void ReadTagA(string tag)
    {
        Debug.Log("Hello Red");        
        if (positionsMap.ContainsKey(tag))
        {
            currentSpheresPositions[positionsMap[tag]] = "R";

        }
        Debug.Log("Current sphere positions [" +
            currentSpheresPositions[0] +
            ", " +
            currentSpheresPositions[1] +
            ", " +
            currentSpheresPositions[2] +
            ", " +
            currentSpheresPositions[3] +
            ", " +
            currentSpheresPositions[4] +
            "]");
        Debug.Log("solution [" +
            solution[0] +
            ", " +
            solution[1] +
            ", " +
            solution[2] +
            ", " +
            solution[3] +
            ", " +
            solution[4] +
            "]");
    }

    public void ReleaseTagA()
    {
        Debug.Log("Bye Red");
        removeSphere("R");
    }
    
    public void ReadTagB(string tag)
    {
        Debug.Log("Hello Pink");
        if (positionsMap.ContainsKey(tag))
        {
            currentSpheresPositions[positionsMap[tag]] = "P";

        }
        Debug.Log("Current sphere positions [" +
            currentSpheresPositions[0] +
            ", " +
            currentSpheresPositions[1] +
            ", " +
            currentSpheresPositions[2] +
            ", " +
            currentSpheresPositions[3] +
            ", " +
            currentSpheresPositions[4] +
            "]");
        Debug.Log("solution [" +
            solution[0] +
            ", " +
            solution[1] +
            ", " +
            solution[2] +
            ", " +
            solution[3] +
            ", " +
            solution[4] +
            "]");
    }

    public void ReleaseTagB()
    {
        Debug.Log("Bye Pink");
        removeSphere("P");
    }
    
    public void ReadTagC(string tag)
    {
        Debug.Log("Hello Green");        
        if (positionsMap.ContainsKey(tag))
        {
            currentSpheresPositions[positionsMap[tag]] = "G";

        }
        Debug.Log("Current sphere positions [" +
            currentSpheresPositions[0] +
            ", " +
            currentSpheresPositions[1] +
            ", " +
            currentSpheresPositions[2] +
            ", " +
            currentSpheresPositions[3] +
            ", " +
            currentSpheresPositions[4] +
            "]");
        Debug.Log("solution [" +
            solution[0] +
            ", " +
            solution[1] +
            ", " +
            solution[2] +
            ", " +
            solution[3] +
            ", " +
            solution[4] +
            "]");
    }

    public void ReleaseTagC()
    {
        Debug.Log("Bye Green");
        removeSphere("G");
    }

    public void ReadTagD(string tag) {
        Debug.Log("Hello Yellow");
        if (positionsMap.ContainsKey(tag))
        {
            currentSpheresPositions[positionsMap[tag]] = "Y";

        }
        Debug.Log("Current sphere positions [" +
            currentSpheresPositions[0] +
            ", " +
            currentSpheresPositions[1] +
            ", " +
            currentSpheresPositions[2] +
            ", " +
            currentSpheresPositions[3] +
            ", " +
            currentSpheresPositions[4] +
            "]");
        Debug.Log("solution [" +
            solution[0] +
            ", " +
            solution[1] +
            ", " +
            solution[2] +
            ", " +
            solution[3] +
            ", " +
            solution[4] +
            "]");

    }
    public void ReleaseTagD()
    {
        Debug.Log("Bye Yellow");
        removeSphere("Y");
    }

    public void ReadTagE(string tag)
    {
        Debug.Log("HI blue");
        
        if (positionsMap.ContainsKey(tag))
        {
            currentSpheresPositions[positionsMap[tag]] = "B";

        }
        Debug.Log("Current sphere positions [" +
            currentSpheresPositions[0] +
            ", " +
            currentSpheresPositions[1] +
            ", " +
            currentSpheresPositions[2] +
            ", " +
            currentSpheresPositions[3] +
            ", " +
            currentSpheresPositions[4] +
            "]"); 
        Debug.Log("solution [" +
                            solution[0] +
                            ", " +
                            solution[1] +
                            ", " +
                            solution[2] +
                            ", " +
                            solution[3] +
                            ", " +
                            solution[4] +
                            "]");
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

    private bool allSpheresArePlaced()
    {
        for(int i = 0; i < currentSpheresPositions.Length; i++)
        {
            if(currentSpheresPositions[i] == "" || currentSpheresPositions[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    private bool checkAnswer(string[] positions, string[] solution)
    {
        

        for(int i = 0; i<positions.Length; i++)
        {
            if(positions[i] != solution[i])
            {
                return false;
            }
        }

        return true;
    }

    private IEnumerator reward()
    {
        //if it's the final level
        if (disManager.level == ((Configuration)GameSetting.instance.configuration).numberOfSteps) 
        {
            //turn light green
            MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
            //play Claps
            rewardSound.Play();
            //turn on bubble machine
            foreach (string s in MagicRoomManager.instance.MagicRoomAppliancesManager.Appliances)
            {
                MagicRoomManager.instance.MagicRoomAppliancesManager.SendChangeCommand(s, "ON");
            }

            yield return new WaitForSeconds(5);
            
            //turn off bubble machine
            foreach (string s in MagicRoomManager.instance.MagicRoomAppliancesManager.Appliances)
            {
                MagicRoomManager.instance.MagicRoomAppliancesManager.SendChangeCommand(s, "OFF");
            }
            MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);

            rewardSound.Stop();

            //reset level in Display Manager
            disManager.level = 0;
            
            //load menu
            SceneManager.LoadSceneAsync(MENU_SCENE_INDEX);
            //stop experience manager
            MagicRoomManager.instance.ExperienceManagerComunication.SendEvent("stopped");
        }
        else
        {
            //turn light green
            MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
            //play Claps
            rewardSound.Play();
            yield return new WaitForSeconds(3);
            //turn lights off
            MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);
            //load new level 
            disManager.SetUpConfiguration();
            rewardSound.Stop();
        }
        
        
        
    }

    private IEnumerator badReward()
    {
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.red);
        yield return new WaitForSeconds(3);
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);
        inreward = false;

        
    }
}
