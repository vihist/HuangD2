using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[CSharpCallLua]
public interface PersionName
{
	List<string> family { get; }
	List<string> given { get; }
}

[CSharpCallLua]
public interface YearName
{
	List<string> first { get; }
	List<string> second { get; }
}

public class InitScene : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		LuaEnv luaenv = new LuaEnv();
		luaenv.AddLoader(CustomLoaderMethod);

		luaenv.DoString("require 'name'");

		PersionName  persionName = luaenv.Global.Get<PersionName>("persion_name");
		YearName 	 yearName 	 = luaenv.Global.Get<YearName>("year_name");
		List<string> periodName  = luaenv.Global.Get<List<string>>("period_name");

		foreach(string str in persionName.family)
		{
			Debug.Log("tb = " + str);
		}

		foreach(string str in persionName.given)
		{
			Debug.Log("tb = " + str);
		}

		foreach(string str in yearName.first)
		{
			Debug.Log("tb = " + str);
		}

		foreach(string str in yearName.second)
		{
			Debug.Log("tb = " + str);
		}

		foreach(string str in periodName)
		{
			Debug.Log("tb = " + str);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private byte[] CustomLoaderMethod(ref string fileName)
	{
		//找到指定文件  
		fileName = Application.streamingAssetsPath + "/native/static/" + fileName.Replace('.', '/') + ".lua";
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
