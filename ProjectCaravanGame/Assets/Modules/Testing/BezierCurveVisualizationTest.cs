using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectGeorge.Entities;
using ProjectGeorge.Controllers;

public class BezierCurveVisualizationTest : MonoBehaviour
{
    private BezierRoute r;
    private BezierRouteVisualization rv;
    [SerializeField]
    private GameObject cube;
    [Range(0.0f,1.0f)]
    public float t;
    public GameObject[] verts;
    [SerializeField]
    private GameObject token;
    // Start is called before the first frame update
    void Start()
    {
        rv = new BezierRouteVisualization(token);
        rv.Active = true; // Display the visualization
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vpos = new Vector3[4];
        for(int i =0; i< 4; i++)
        {
            vpos[i] = verts[i].transform.position;
        }
        r = new BezierRoute(vpos, vpos.Length - 1);
        cube.transform.position = r.EvaluateAt(t);
        rv.Route = r;
    }
}
