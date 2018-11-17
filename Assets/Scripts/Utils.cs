using UnityEngine;
using System.Collections;

public class Utils{

	private const float EPSILON = .01f;
	private static Utils instance = new Utils (); 

	public static Utils Instance{
		get{
			return instance;
		}
	}

	public Utils(){
	}

	public bool equalsWithinEps(Vector3 a, Vector3 b){
		return (equalsWithinEps (a.x, b.x) && equalsWithinEps (a.y, b.y));
	}

	// Helper to check if x and y are "close enough"
	public bool equalsWithinEps(float x, float y){
		return (x <= (y + EPSILON)) && (x >= (y - EPSILON));
	}


}