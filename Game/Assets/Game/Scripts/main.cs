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
    public static  GameScriptFactory mScriptFactory = null;
    public static  ResourceManager mResMgr = null;
	void Start () {
        InitBaseRunTime();
        switch (mRunType)
        {
            case RunType.MONO:
                RunAsMono();
                break;
            case RunType.ILRuntime:
                RunAsIL();
                break;
        }
	}
    void InitBaseRunTime()
    {
        if (null != mScriptFactory)
            mScriptFactory.Clear();
        if (null != mResMgr)
            mResMgr.Clear();
        mScriptFactory = new GameScriptFactory();
        mResMgr = new ResourceManager();
    }
	void RunAsIL()
    {
       UnityEngine.Object o = mResMgr.LoadRes("Logic/Entrance");
        TextAsset ta = o as TextAsset;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        using (System.IO.MemoryStream fs = new System.IO.MemoryStream(ta.bytes))
        {      
             appdomain.LoadAssembly(fs, null, new Mono.Cecil.Pdb.PdbReaderProvider());            
        }
        InitILRuntime(appdomain);
        appdomain.Invoke("Entrance", "Main", null);
    }
    void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
    {
        appdomain.RegisterCrossBindingAdaptor(new GameScriptInterfaceAdapter());
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
