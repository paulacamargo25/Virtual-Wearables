using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class InteractionObject : MonoBehaviour {

    public List<GameObject> innerObjects;
    public GameObject actionsBase;
    Renderer actionBaseRenderer;
    string activeLayer;
    
    // Use this for initialization
    void Start () {
        PresentLayer("Music_selector");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PresentLayer (string layerName) {
        activeLayer = layerName;
        switch (layerName) {
            case "Music_selector":
                SetMusicActions();
                break;
            case "Power_selector":
                SetPowerActions();
                break;
            case "Other_selector":
                SetOtherActions();
                break;
            default:
                break;
        }
    }

    public void SelectAction (string actionName) {
        switch (actionName) {
            case "one":
                switch (activeLayer) {
                    case "Music_selector":
                        //ExecuteAction("IExplorer.exe", "www.example.com");
                        break;
                    case "Power_selector":
                        //ExecuteAction("IExplorer.exe", "www.example.com");
                        break;
                    case "Other_selector":
                        ExecuteAction("explorer", "");
                        break;
                    default:
                        break;
                }
                break;
            case "two":
                switch (activeLayer) {
                    case "Music_selector":
                        //ExecuteAction("IExplorer.exe", "www.example.com");
                        break;
                    case "Power_selector":
                        //ExecuteAction("IExplorer.exe", "www.example.com");
                        break;
                    case "Other_selector":
                        ExecuteAction("mspaint", "");
                        break;
                    default:
                        break;
                }
                break;
            case "three":
                switch (activeLayer)
                {
                    case "Music_selector":
                        //ExecuteAction("IExplorer.exe", "www.example.com");
                        break;
                    case "Power_selector":
                        //ExecuteAction("IExplorer.exe", "www.example.com");
                        break;
                    case "Other_selector":
                        ExecuteAction("spotify", "");
                        break;
                    default:
                        break;
                }
                break;
            case "four":
                switch (activeLayer)
                {
                    case "Music_selector":
                        //ExecuteAction("IExplorer.exe", "www.example.com");
                        break;
                    case "Power_selector":
                        //ExecuteAction("IExplorer.exe", "www.example.com");
                        break;
                    case "Other_selector":
                        ExecuteAction("start", "chrome");
                        break;
                    default:
                        break;
                }
                break;
        }
    }

    public void ExecuteAction (string action, string args) {
        Process process = new Process();
        process.StartInfo.FileName = action;
        process.StartInfo.Arguments = args;
        process.Start();
    }

    public void setText(GameObject objectToSet, string text) {
        TextMesh textToChange = (TextMesh)objectToSet.GetComponentInChildren(typeof(TextMesh));
        textToChange.text = text;
    }

    public void SetMusicActions () {
        setText(innerObjects[0], "Play");
        setText(innerObjects[1], ">>");
        setText(innerObjects[2], "Stop");
        setText(innerObjects[3], "<<");
        setBaseColor(Color.green);
    }

    public void SetPowerActions () {
        setText(innerObjects[0], "Shut Down");
        setText(innerObjects[1], "Lock");
        setText(innerObjects[2], "Suspend");
        setText(innerObjects[3], "Reboot");
        setBaseColor(Color.red);
    }

    public void SetOtherActions () {
        setText(innerObjects[0], "File Manager");
        setText(innerObjects[1], "Paint");
        setText(innerObjects[2], "Spotify");
        setText(innerObjects[3], "Chrome");
        setBaseColor(Color.blue);
    }

    public void setBaseColor (Color color) {
        Renderer rend = actionsBase.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", color);
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", color);
    }
}
