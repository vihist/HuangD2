using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

[LuaCallCSharp]
public class TestClass
{
	public int a;
	public void inc()
	{
		a++;
	}
}

[CSharpCallLua]
public interface ItfOption
{
	string op1 { get; }
	string op2 { get; }
	string op3 { get; }
	string op4 { get; }
	string op5 { get; }
	int process(string op);
}

[CSharpCallLua]
public interface ItfEvent
{
	string title { get; }
	string desc { get; }
	//ItfOption option { get;}
	//ItfOption option { get; }
}


public class MainScript : MonoBehaviour
{
	// Use this for initialization
	LuaEnv luaenv = null;
	// Use this for initialization
	void Start()
	{
        Debug.Log("start!");

        luaenv = new LuaEnv();

		LuaEnv.CustomLoader method = CustomLoaderMethod;

		//添加自定义装载机Loader  
		luaenv.AddLoader(method);
		luaenv.DoString("require 'event'");

		ItfEvent eventobj = luaenv.Global.Get<ItfEvent>("event");

		luaenv.DoString("require 'event2'");

		ItfEvent eventobj2 = luaenv.Global.Get<ItfEvent>("event");

		Debug.Log("title = " + eventobj.title);
		Debug.Log("desc = " + eventobj.desc);

/*		Debug.Log("op1 = " + eventobj.option.op1);
		Debug.Log("op2 = " + eventobj.option.op2);
		Debug.Log("op3 = " + eventobj.option.op3);
		Debug.Log("op4 = " + eventobj.option.op4);
		Debug.Log("op5 = " + eventobj.option.op5);
		Debug.Log("process = " + eventobj.option.process(eventobj.option.op1));

		Debug.Log("title = " + eventobj2.title);
		Debug.Log("desc = " + eventobj2.desc);

		Debug.Log("op1 = " + eventobj2.option.op1);
		Debug.Log("op2 = " + eventobj2.option.op2);
		Debug.Log("op3 = " + eventobj2.option.op3);
		Debug.Log("op4 = " + eventobj2.option.op4);
		Debug.Log("op5 = " + eventobj2.option.op5);
		Debug.Log("process = " + eventobj2.option.process(eventobj2.option.op1));

		//Debug.Log("_G.c = " + luaenv.Global.Get<bool>("c"));
     */   
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private byte[] CustomLoaderMethod(ref string fileName)
	{
		//找到指定文件  
		fileName = Application.streamingAssetsPath + "/native/event/" + fileName.Replace('.', '/') + ".lua";
		Debug.Log(fileName);
		if (File.Exists(fileName))
		{
			return File.ReadAllBytes(fileName);
		}
		else
		{
			return null;
		}
	}
}
