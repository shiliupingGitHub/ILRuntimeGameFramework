using UnityEngine;
using System.Collections;
using ILRuntime;
public class main : MonoBehaviour {

    public enum RunType
    {
        MONO,
        ILRuntime,
    }
    public RunType mRunType = RunType.ILRuntime;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
