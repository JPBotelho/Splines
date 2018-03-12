using UnityEngine;

public class CubicCurve
{
	private Vector3 p1;
	private Vector3 p2;

	private Vector3 pivot1;
	private Vector3 pivot2;

	private int pointAmount;

	private Vector3[] points; //Generated spline points

	private int count; //Inner count, used for Next()

	public CubicCurve(Vector3 p1, Vector3 p2, Vector3 pivot1, Vector3 pivot2, int pointAmount)
	{
		this.p1 = p1;
		this.p2 = p2;
		this.pivot1 = pivot1;
		this.pivot2 = pivot2;
		this.pointAmount = pointAmount;

		RecalculatePoints();
	}

	public Vector3 Next()
	{
		if (count == pointAmount - 1)
		{
			count = 0;
			return points[0];
		}
		return points[count++];
	}

	public void UpdatePointCount(int pointAmount)
	{
		this.pointAmount = pointAmount;

		RecalculatePoints();
	}

	public void Update(Vector3 p1, Vector3 p2, Vector3 pivot1, Vector3 pivot2)
	{
		this.p1 = p1;
		this.p2 = p2;
		this.pivot1 = pivot1;
		this.pivot2 = pivot2;

		RecalculatePoints();
	}

	public Vector3[] GetPoints ()
	{
		return points;
	}

	public void Draw()
	{
		if (points != null)
		{
			for(int i = 0; i < points.Length; i++)
			{
				if (i != points.Length - 1)
				{
					Debug.DrawLine(points[i], points[i+1], Color.red);
				}
			}
		}

		Debug.DrawLine(p1, pivot1, Color.blue);
		Debug.DrawLine(p2, pivot1, Color.blue);

		Debug.DrawLine(p1, pivot2, Color.blue);
		Debug.DrawLine(p2, pivot2, Color.blue);
	}

	private void RecalculatePoints ()
	{
		points = new Vector3[pointAmount];
		points[0] = p1;

		float loopAmount = 1f/pointAmount; //Divide t into same-sized segments to evaluate

		for(int currentAmount = 0; currentAmount < pointAmount; currentAmount++)
		{
			if (currentAmount != 0) // Index 0 will be set to the origin
			{
				float t = loopAmount * (currentAmount+1); //+1 is because the loop starts at 0,
				points[currentAmount] = Evaluate(t);
			}
		}
			
	}

	public Vector3 Evaluate(float t)
	{
		t = Mathf.Clamp01(t);
		Vector3 quadratic1 = QuadraticCurve.Evaluate(p1, pivot1, p2, t);
        Vector3 quadratic2 = QuadraticCurve.Evaluate(pivot1, p2, pivot2, t);
        return Vector3.Lerp(quadratic1, quadratic2, t);
	}

	//For external use
	public static Vector3 Evaluate(Vector3 p1, Vector3 p2, Vector3 pivot1, Vector3 pivot2, float t)
	{
		t = Mathf.Clamp01(t);
		Vector2 quadratic1 = QuadraticCurve.Evaluate(p1, pivot1, p2, t);
        Vector2 quadratic2 = QuadraticCurve.Evaluate(pivot1, p2, pivot2, t);
        return Vector2.Lerp(quadratic1, quadratic2, t);
	}
}
