using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class InteractionObject2 : MonoBehaviour {

    public List<GameObject> innerObjects;
    public desktopPlane actionsBase;
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
            case "Share_selector":
                SetShareActions();
                break;
            case "Server_selector":
                SetServerActions();
                break;
            default:
                break;
        }
    }

    public void SelectAction (string actionName, string url="") {
        UnityEngine.Debug.Log("activeLayer: "+ activeLayer);

        switch (actionName) {
            case "ImagePlane":
                UnityEngine.Debug.Log("=====image touch====");
                UnityEngine.Debug.Log("=====url====" + url);
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
        SceneManager.LoadScene("virtual-wearables");

    }
    public void SetShareActions () {
        actionsBase.remove_images();
        actionsBase.render_image_from_file();
    }

    public void SetServerActions () {
        actionsBase.remove_images();
        actionsBase.render_image_from_server();
    }

    public void setBaseColor (Color color) {
        Renderer rend = actionsBase.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", color);
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", color);
    }
}
