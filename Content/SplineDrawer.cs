using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SplineDrawer : MonoBehaviour {

	public Transform p1;
	public Transform p2;
	public Transform pivot;

	[Range(2, 20)]
	public int splinePointCount;

	private QuadraticCurve curve;
	
	void Update () 
	{
		if (p1 != null && p2 != null && pivot != null)
		{
			if (curve == null)
			{
				curve = new QuadraticCurve(p1.position, p2.position, pivot.position, splinePointCount);
			}
			
			UpdateCurve();
			curve.Draw();
		}
	}

	private void UpdateCurve()
	{
		curve.Update(p1.position, p2.position, pivot.position);
		curve.UpdatePointCount(splinePointCount);	
	}
	
}
