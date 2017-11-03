using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public BSPEntity Ent;
    private MDL mdl;

    public Dictionary<string, string> mdlFiles = new Dictionary<string, string>() {
        { "monster_army",   "soldier.mdl" },
        { "monster_dog",    "dog.mdl" }
    };
    
    public void Init(BSPEntity ent)
    {
        this.Ent = ent;

        transform.position = ent.GetVector3("origin");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, ent.GetInt("angle"), transform.eulerAngles.z);

        mdl = new MDL(mdlFiles[ent.KeyValues["classname"]]);
        mdl.go.transform.SetParent(transform);
        mdl.go.transform.localPosition = Vector3.zero;        
    }    
}
