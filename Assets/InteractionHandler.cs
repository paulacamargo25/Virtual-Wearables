using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour {

    // Attributes
    public InteractionObject interactionObject;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        switch(other.name)
        {
            case "Music_selector":
                interactionObject.PresentLayer(other.name);
                break;
            case "Power_selector":
                interactionObject.PresentLayer(other.name);
                break;
            case "Other_selector":
                interactionObject.PresentLayer(other.name);
                break;
            case "one":
                interactionObject.SelectAction(other.name);
                break;
            case "two":
                interactionObject.SelectAction(other.name);
                break;
            case "three":
                interactionObject.SelectAction(other.name);
                break;
            case "four":
                interactionObject.SelectAction(other.name);
                break;
            default:
                break;
        };
    }
}
