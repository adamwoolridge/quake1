using UnityEngine;
using System.Collections;
using System.IO;

public class AnimTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
       

        DirectoryInfo levelDirectoryPath = new DirectoryInfo("Assets/Resources/Models/");
        FileInfo[] fileInfo = levelDirectoryPath.GetFiles("*.mdl", SearchOption.AllDirectories);

        float x = 0f;

        foreach (FileInfo fi in fileInfo)
        {
            MDL mdl = new MDL( fi.Name );
            mdl.go.transform.position = Vector3.zero + new Vector3(x, 0f, 0f);
            mdl.go.transform.eulerAngles = new Vector3(0f, -90f, 0f);
            x -= 60f;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
