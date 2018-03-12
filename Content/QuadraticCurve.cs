using UnityEngine;

public class QuadraticCurve
{
	private Vector3 p1;
	private Vector3 p2;
	private Vector3 pivot;

	private int pointAmount;

	private Vector3[] points;

	private int count;

	public QuadraticCurve(Vector3 p1, Vector3 p2, Vector3 pivot, int pointAmount)
	{
		this.p1 = p1;
		this.p2 = p2;
		this.pivot = pivot;
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

	public void Update(Vector3 p1, Vector3 p2, Vector3 pivot)
	{
		this.p1 = p1;
		this.p2 = p2;
		this.pivot = pivot;

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

		Debug.DrawLine(p1, pivot, Color.blue);
		Debug.DrawLine(p2, pivot, Color.blue);
	}

	private void RecalculatePoints ()
	{
		points = new Vector3[pointAmount];
		points[0] = p1; //Origin is first point
		points[pointAmount-1] = p2; //End is second point

		float loopAmount = 1f/pointAmount; //Divide t into same-sized segments to evaluate

		for(int currentAmount = 0; currentAmount < pointAmount; currentAmount++)
		{
			float t;
			if (currentAmount != 0) // Index 0 will be set to the origin
			{
				t = loopAmount * (currentAmount+1); //+1 is because the loop starts at 0,
				points[currentAmount] = Evaluate(t);
			}
			else
			{
				points[currentAmount] = p1; //Set index 0
			}

		}
			
	}

	public Vector3 Evaluate(float t)
	{
		t = Mathf.Clamp01(t);
		Vector3 lerp1 = Vector3.Lerp(p1, pivot, t);
		Vector3 lerp2 = Vector3.Lerp(pivot, p2, t);

		return Vector3.Lerp(lerp1, lerp2, t);
	}

	//For external use
	public static Vector3 Evaluate(Vector3 p1, Vector3 p2, Vector3 pivot, float t)
	{
		t = Mathf.Clamp01(t);
		Vector3 lerp1 = Vector3.Lerp(p1, pivot, t);
		Vector3 lerp2 = Vector3.Lerp(pivot, p2, t);

		return Vector3.Lerp(lerp1, lerp2, t);
	}
}
