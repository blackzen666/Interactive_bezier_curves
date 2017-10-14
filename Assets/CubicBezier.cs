using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicBezier : MonoBehaviour {

    private LineRenderer lineRenderer;
    //parameters.
    public GameObject Point0, Point1, Point2,Point3;
    [Range(2, 50)]
    public int resolution = 50;
    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        //getting line render component
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
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
            positions.Add(generate_cubic_curve_point(t, Point0.transform.position, Point1.transform.position, Point2.transform.position, Point3.transform.position));
        }
        //passing created vector to linerender
        lineRenderer.SetPositions(positions.ToArray());
        yield return null;
    }


    private Vector3 generate_cubic_curve_point(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
        return Mathf.Pow((1 - t), 3) * p0 + 3*Mathf.Pow((1 - t), 2) * t * p1 + 3*Mathf.Pow(t, 2) * (1 - t) * p2 + Mathf.Pow(t, 3) * p3;
    }

}
