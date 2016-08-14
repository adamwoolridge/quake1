using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

    public string MapFileName = "";

    private BSPMap map;

    // Use this for initialization
    void Start () {
        map = new BSPMap(MapFileName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
