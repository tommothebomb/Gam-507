using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class CameraPathManager : MonoBehaviour
{
    public Camera cam;

    [System.Serializable] public class PathSection
    {
        public Transform[] CurvePoints = new Transform[3];
        public float Speed;
        public Color SectionColour = Color.red;
    }


    public bool on;
    private void Update()
    {
        if (on)
        {
            Play();
        }
    }

    public PathSection[] Paths;
    public int Index;
    float CurrentTIme;
    int CurrentIndex;
    public Transform viewObj;
    public void Play()
    {
        if(CurrentTIme >= 1)
        {
            CurrentIndex += 1;
            CurrentTIme = 0;
            if(CurrentIndex >= Paths.Length)
            {
                on = false;
                CurrentIndex = 0;
                return;
            }
        }

        //ON PLAY LERP THROUGHT THE PATH OF INDEX 

        Vector3 A = Vector3.Lerp(Paths[CurrentIndex].CurvePoints[0].position, Paths[CurrentIndex].CurvePoints[1].position, CurrentTIme);
        Vector3 B = Vector3.Lerp(Paths[CurrentIndex].CurvePoints[1].position, Paths[CurrentIndex].CurvePoints[2].position, CurrentTIme);

        Vector3 C = Vector3.Lerp(A, B, CurrentTIme);

        CurrentTIme += Time.deltaTime * Paths[CurrentIndex].Speed;
        cam.transform.position = C;
        cam.transform.LookAt(viewObj);

    }


    [Header("Debug")]
    public bool renderLine;
    public int resoloution;
    private void OnDrawGizmos()
    {
        if(renderLine)
        {
            Gizmos.color = Color.red;
           // Vector3 PrevPoint = Paths[0].CurvePoints[0].position;
            foreach(PathSection path in Paths)
            {
                Gizmos.color = path.SectionColour;
                Vector3 PrevPoint = path.CurvePoints[0].position;
                for (int i = 0; i <= resoloution; i++)
                {
                    float time = (float)i / (float)resoloution;
                    Vector3 A = Vector3.Lerp(path.CurvePoints[0].position, path.CurvePoints[1].position, time);
                    Vector3 B = Vector3.Lerp(path.CurvePoints[1].position, path.CurvePoints[2].position, time);

                    Vector3 C = Vector3.Lerp(A, B, time);

                    Gizmos.DrawLine(PrevPoint, C);
                    PrevPoint = C;
                    //draw a line from C to prev C
                }
            }
        }


    }


}
