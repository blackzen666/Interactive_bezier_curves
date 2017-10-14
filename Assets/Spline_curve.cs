using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline_curve : MonoBehaviour {
    public int number = 3;
    public List<int> factorials = new List<int>();


    public List<Transform> points = new List<Transform>();


    private LineRenderer lineRenderer;
    [Range(2, 50)]
    public int resolution = 50;
    private List<Vector3> positions = new List<Vector3>();


    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();

        generate_DP_factorial(10);

        //Debug.Log(calculate_binomial_distribution(3, 1));

        

    }

    void Update()
    {
        //number = points.Count -1;
        //iniciate coroutine
        StartCoroutine("DrawLine");
    }


    //generate points along the curve and store them in the linerenderer designated position.
    IEnumerator DrawLine()
    {
        //dinamic resolution for the curve
        positions = new List<Vector3>();
        lineRenderer.positionCount = resolution + 1;
        //for loop generating curve points
        for (int i = 0; i <= resolution; i++)
        {
            //generating values from 0 to 1
            float t = (float)i / resolution;
            //creating list of bezier curve positions
            positions.Add(spline_series(number, t, points.ToArray()));
            Debug.Log(spline_series(number, t, points.ToArray()));
        }
        //passing created vector to linerender
        lineRenderer.SetPositions(positions.ToArray());
        yield return null;
    }



    Vector3 spline_series (int n,float t,Transform[] control_points) {
        Vector3 total = Vector3.zero;
        
        for (int i = 0; i < control_points.Length; i++)
        {
            //Debug.Log(control_points[i]);
            total += calculate_binomial_distribution(n,i)*control_points[i].position*Mathf.Pow((1-t),n-i)*Mathf.Pow(t,i);
        }
        return total;
	}


    void generate_DP_factorial(int number) {
        for (int i = 0; i <= number; i++)
        {
            factorials.Add(calculate_factorial(i));
        }
    }


    int calculate_factorial(int num) {
        int factorial = 1;

        for (int i = 1; i <= num; i++)
        {
            factorial *= i;

        }
        return factorial;
        //Debug.Log("The factorial " + number + " : " + factorial);
    }

    int calculate_binomial_distribution(int n, int k) {
        int result = 0;
            if (n == k || (n== 0 || k == 0)) {
                return 1;
            }
            else {
                if (n - k != 0)
                {
                //Debug.Log(n + " " + k);
                    result = factorials[n] / (k * factorials[n - k]);
                }
                return result;
            }
    }

   


}
