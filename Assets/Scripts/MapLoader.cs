using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

    public string MapFileName = "";

    private BSPMap map;

    // Use this for initialization
    void Start () {
        map = new BSPMap(MapFileName);

        foreach (BSPModel model in map.models)
        {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
