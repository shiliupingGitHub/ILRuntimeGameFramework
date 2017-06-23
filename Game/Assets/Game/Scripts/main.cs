using UnityEngine;
using System.Collections;
using ILRuntime;
using System.Reflection;
using System;
public class main : MonoBehaviour {

    public enum RunType
    {
        MONO,
        ILRuntime,
    }
    public RunType mRunType = RunType.ILRuntime;
	void Start () {
	    switch(mRunType)
        {
            case RunType.MONO:
                RunAsMono();
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void RunAsMono()
    {
        CallMethod("Entrance", "Main");
    }
    void CallMethod(string typeName,string methodname)
    {
        GetAssembly(typeName).GetType(typeName).GetMethod(methodname).Invoke(null,null);
    }
    Assembly GetAssembly(string szName)
    {
        Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
        foreach(var a in asms)
        {
            if (a.ManifestModule.Name == szName + ".dll")
                return a;
        }
        return null;
    }
}
