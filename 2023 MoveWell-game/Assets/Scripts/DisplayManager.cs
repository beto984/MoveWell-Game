using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    JArray manifest = new JArray();
    public Image backgroundImage;
    public GameObject grid;
    public EventInterceptor EI;
    public GameObject floorMask;
    public int level = 0;

    // Start is called before the first frame update
    void Start()
    {

        string fullPath = Path.Combine(Application.streamingAssetsPath, "manifest", "manifest.json");
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            manifest = JArray.Parse(json);
        }

        SetUpConfiguration();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            SetUpConfiguration();
        }
    }

    public void SetUpConfiguration()
    {
        int random = Random.Range(0, manifest.Count - 1);

        JObject o = (JObject)manifest[level];

        Debug.Log(o.ToString());
        StreamingAssetManager.instance.LoadImageFromStreamingAsset("images/background", o["background"].ToString(), (tex) =>
        {
            backgroundImage.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        });

        for (int i = 0; i < grid.transform.childCount; i++)
        {
            int color = GetColorForPosition(i, (JArray)o["options"]);
            switch (color)
            {
                case 0: grid.transform.GetChild(i).GetComponent<Image>().color = Color.red; break;
                case 1: grid.transform.GetChild(i).GetComponent<Image>().color = Color.blue; break;
                case 2: grid.transform.GetChild(i).GetComponent<Image>().color = Color.green; break;
                case 3: grid.transform.GetChild(i).GetComponent<Image>().color = Color.yellow; break;
                case 4: grid.transform.GetChild(i).GetComponent<Image>().color = Color.magenta; break;
            }



            if (color >= 0)
            {
                Image img = grid.transform.GetChild(i).GetComponent<Image>();
                img.enabled = false;
                StreamingAssetManager.instance.LoadImageFromStreamingAsset("images/images", o["optionImage"].ToString(), (tex) =>
                {
                    img.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    img.enabled = true;
                });
            }
            else
            {
                grid.transform.GetChild(i).GetComponent<Image>().sprite = null;
                grid.transform.GetChild(i).GetComponent<Image>().color = Color.clear;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            EI.solution[i] = o["solution"][i].ToString();


        }

        if (o["orientation"].ToString() == "v")
        {
            EI.isVertical = true;
            floorMask.transform.GetChild(0).gameObject.SetActive(true);
            floorMask.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            EI.isVertical = false;
            floorMask.transform.GetChild(0).gameObject.SetActive(false);
            floorMask.transform.GetChild(1).gameObject.SetActive(true);
        }

        level++;

        /*if (false)//o["orientation"].ToString() == "horizontal")
        {
            floorMask.transform.GetChild(0).gameObject.SetActive(false);
            floorMask.transform.GetChild(1).gameObject.SetActive(true);
        }
        else {
            floorMask.transform.GetChild(0).gameObject.SetActive(true);
            floorMask.transform.GetChild(1).gameObject.SetActive(false);
        }*/
    }

    int GetColorForPosition(int i, JArray pos)
    {
        foreach (JObject opt in pos)
        {
            if (i == (int)opt["position"]) {
                return ((int)opt["color"]);
            }
        }
        return (-1);
    }
}
