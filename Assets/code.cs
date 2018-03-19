using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class code : MonoBehaviour {
	public List<Texture> textures = new List<Texture>();
	public int currentTexture = 0;

	public string url = "https://www.univ-lyon1.fr/images/www/";
	public string image = "logo-lyon1.png";
	public string path = "./Assets/";

	// Use this for initialization
	IEnumerator Start()
	{
		// Start a download of the given URL
		using (WWW www = new WWW(url + image))
		{
			// Wait for download to complete
			yield return www;

			if (!System.IO.File.Exists(path + image)) {
				// Save texture
				System.IO.File.WriteAllBytes(path + image, www.bytes);
			}

			// Add to list of textures
			textures.Add(www.texture);

			// loop on file paths
			foreach (string filePath in System.IO.Directory.GetFiles(path))
			{
				if (System.IO.Path.GetExtension(filePath) == ".png") {
					print("Loading " + System.IO.Path.GetFileName(filePath) + " as a texture");

					//Thread t = new Thread(() => Load(filePath));
					//t.Start();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			currentTexture++;
			currentTexture %= textures.Count;

			// Assign texture
			GetComponent<Renderer>().material.mainTexture = textures[currentTexture];
		}
	}

	void Load(string filePath) {
		textures.Add(Resources.Load(filePath) as Texture);
	}
}