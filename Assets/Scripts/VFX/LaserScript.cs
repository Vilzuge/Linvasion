using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    private LineRenderer laserLine;
    
    
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = 0.2f;
        laserLine.endWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
    }
}
