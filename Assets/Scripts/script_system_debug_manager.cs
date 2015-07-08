using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class script_system_debug_manager : MonoBehaviour {

//	public int stackCount;
	public List<string> callStack;

	void Start(){

	}

	public void func_call(string obj_name, string function_name){
		string debugMsg = "";
		foreach(string parent in callStack){
			debugMsg += ">>" + parent;
		}
		callStack.Add (obj_name + "." + function_name);
		Debug.Log("STACK::" + debugMsg + ">>" + obj_name + "." + function_name + "\n");
	}

	public void func_exit(string obj_name, string function_name){
		callStack.Remove (obj_name + "." + function_name);
		string debugMsg = "";
		foreach(string parent in callStack){
			debugMsg += parent + "=>";
		}
		Debug.Log("STACK::" + debugMsg + "<<" + obj_name + "." + function_name + "\n");
	}

}
