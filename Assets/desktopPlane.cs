using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;

[Serializable]
public class Myclass {
	public string[] data;
}


public class desktopPlane : MonoBehaviour {

	// Use this for initialization

	public GameObject image;
 	GameObject[] gameObj;
     Texture2D[] textList;
     
     string[] files;
     string pathPreFix; 
	void Start () {
		Debug.Log("Load pantalla");		
		StartCoroutine("GetPaths");
	}
	
	public void render_image_from_file(){
		string path = @"C:\Users\paula\Pictures\UnityImages";
 		files = System.IO.Directory.GetFiles(path, "*.jpg");
		Debug.Log("Imgs in path: "+ files+ "lenght"+files.Length);       
		foreach (string file in files)
		{
			Debug.Log("File Name: "+file);
		}
         
		for (int j=0; j<4;j++){
			for (int i = 0; i < 6; i++)
			{
				int number = j*6+i;
				if (number >= files.Length){
					return;
				}
				GameObject img = Instantiate(image, transform);
				img.transform.Translate(j*0.06F+0.02F,0 ,i*0.055F);
				Debug.Log("=====fig n "+number+"j:"+j+"i:"+i);
				Texture2D mat = LoadPNG(files[number]);
				img.transform.GetChild(0).GetComponent<imagePlane>().url = files[number];
				img.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = mat;
			}
		}

	}

	public void render_image_from_server() {
		StartCoroutine("GetPaths");
	}

	public void remove_images() {
		foreach (Transform child in transform) {
     		GameObject.Destroy(child.gameObject);
 		}
	}
	
	private IEnumerator load_image_preview(string _path)
 {
     WWW www = new WWW(_path);
     yield return www;
     Texture2D texTmp = new Texture2D(256, 256, TextureFormat.RGB24, false);
 
     www.LoadImageIntoTexture(texTmp);
     Texture2D cur_image_loaded = new Texture2D(256, 256, TextureFormat.RGB24, false);
     cur_image_loaded = texTmp;
 }

 	 private IEnumerator GetPaths()
    {
		string getUrl = "http://192.168.43.88:5000/";
        Debug.Log("getting name from " + getUrl);
 
        WWW getName = new WWW(getUrl);

		while (!getName.isDone)
		{
			Debug.Log("Download image on progress" + getName.progress);
			yield return null;
		}

		if (!string.IsNullOrEmpty(getName.error))
		{
			Debug.Log("Download failed");
		}
		else
		{

			Debug.Log("Teeeext: "+ getName.text);
			Myclass wrapper = JsonUtility.FromJson<Myclass>(getName.text);
			Debug.Log(wrapper.data.Length);
			files = wrapper.data;
			Debug.Log("Imgs in path: "+ files+ "lenght"+files.Length);       
			
			foreach (string file in files)
			{
				Debug.Log("File Name: "+file);
			}
			
			for (int j=0; j<4;j++){
				for (int i = 0; i < 6; i++)
				{
					int number = j*6+i;
					if (number >= files.Length){
						break;
					}
					GameObject img = Instantiate(image, transform);
					img.transform.Translate(j*0.06F+0.02F,0 ,i*0.055F);
					Debug.Log("=====fig n "+number+"j:"+j+"i:"+i);
					StartCoroutine(loadSpriteImageFromUrl(files[number], img));

				}
			}
		}       
    }

	IEnumerator loadSpriteImageFromUrl(string URL, GameObject img)
	{
		Debug.Log("Obtain url:" + URL);
		WWW www = new WWW(URL);
		while (!www.isDone)
		{
			Debug.Log("Download image on progress" + www.progress);
			yield return null;
		}

		if (!string.IsNullOrEmpty(www.error))
		{
			Debug.Log("Download failed");
		}
		else
		{
			Debug.Log("Download succes");
			Texture2D texture = new Texture2D(1, 1);
			www.LoadImageIntoTexture(texture);
			
			img.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = texture;
		}
	}

 public static Texture2D LoadPNG(string filePath) {
 
     Texture2D tex = null;
     byte[] fileData;
 
     if (File.Exists(filePath))     {
         fileData = File.ReadAllBytes(filePath);
         tex = new Texture2D(2, 2);
         tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
     }
     return tex;
 }



	// Update is called once per frame
	void Update () {
		
	}
}
