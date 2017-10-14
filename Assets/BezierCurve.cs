using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour {


    public LineRenderer lineRenderer;

    //parameters.
    public GameObject Point0, Point1;
    int number_of_points = 50;
    public Vector3[] positions = new Vector3[50];

    void Start () {
        lineRenderer = GetComponent<LineRenderer>();

        //setting starting position and how many points will be used by line renderer.
        lineRenderer.SetPosition(0, Point0.transform.position);
        lineRenderer.positionCount = number_of_points;
    }
	
	void Update () {
        //calling Drawline passing public interactive GameObjects position. 
        DrawLine(Point0.transform.position,Point1.transform.position);
    }

    //generate points along the curve and store them in the linerenderer designated position.
    private void DrawLine(Vector3 pointA, Vector3 pointB) {
        //loop from 1 to size+1 trying avoiding 0/0 division.
        for (int i = 1; i < number_of_points+1; i++)
        {
            //generating values from 0 to 1
            float t = (float)i / number_of_points;
            //creating vector of bezier curve positions
            positions[i - 1] = generate_linear_points(t,pointA, pointB);
        }
        //passing created vector to linerender
        lineRenderer.SetPositions(positions);
    }

    //formula for linear bezier curves
    private Vector3 generate_linear_points(float t,Vector3 p0, Vector3 p1) {
        return p0 + t * (p1 - p0);
    }
}
