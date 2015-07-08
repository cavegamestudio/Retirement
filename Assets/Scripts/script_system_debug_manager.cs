using UnityEngine;
using System.Collections;

public class script_system_debug_manager : MonoBehaviour {

	public string[] callStack;

	public void func_call(string function_name){
		Debug.Log("STACK");
	}
}
