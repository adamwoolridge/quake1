using UnityEngine;
using System.Collections;

public class MDLAnimator : MonoBehaviour {

    public MDL mdl;

    private int frame = 0;
    private float delta = 0f;
    		
	// Update is called once per frame
	void Update () {
        delta += Time.deltaTime;
        
        if ( delta > 0.1f )
        {
            delta = 0f;
            frame++;
            if ( frame >= mdl.frames.Count )
                frame = 0;
        }

        int nextFrame = frame + 1;
        if ( nextFrame >= mdl.frames.Count )
            nextFrame = 0;

        mdl.SetFrame( frame, nextFrame, delta * 10f );        
	}
}
