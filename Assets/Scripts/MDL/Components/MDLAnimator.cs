using UnityEngine;
using System.Collections;

public class MDLAnimator : MonoBehaviour {

    public MDL mdl;

    private int curFrame = 0;
    private int startFrame = 0;
    private int endFrame = 0;

    private float delta = 0f;
    			
    
    public void PlayAnim(int startFrame, int endFrame)
    {
        curFrame = startFrame;
        this.startFrame = startFrame;
        this.endFrame = endFrame;
    }   

	void Update ()
    {
        delta += Time.deltaTime;
        
        if ( delta > 0.1f )
        {
            delta = 0f;
            curFrame++;
            if ( curFrame > endFrame )
                curFrame = startFrame;
        }

        int nextFrame = curFrame + 1;
        if ( nextFrame >= mdl.frames.Count )
            nextFrame = 0;

        mdl.SetFrame( curFrame, nextFrame, delta * 10f );        
	}
}
