using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float timeDuration = 0.5f;
    public string easingCurve = Easing.InOut;

    [Header("Set Dynamically")]
    public TextMesh tMesh; // shows the char
    public Renderer tRend; // renderer of 3D text, this will determine whether char is visible
    public bool big = false; // big letters act little differently
    // linear interpolation fields
    public List<Vector3> pts = null;
    public float timeStart = -1;

    private char _c; // the char shown on this letter
    private Renderer rend;

    void Awake()
    {
        tMesh = GetComponentInChildren<TextMesh>();
        tRend = tMesh.GetComponent<Renderer>();
        rend = GetComponent<Renderer>();
        visible = false;
    }

    void Update()
    {
        if (timeStart == -1)
            return;

        // standard linear interpolation code
        var u = (Time.time - timeStart) / timeDuration;
        u = Mathf.Clamp01(u);
        var u1 = Easing.Ease(u, easingCurve);
        var v = Utils.Bezier(u1, pts);
        transform.position = v;

        // if the interpolation is done, set timeStart back to -1
        if (u == 1)
            timeStart = -1;
    }

    public char c
    {
        get { return _c; }
        set
        {
            _c = value;
            tMesh.text = _c.ToString();
        }
    }

    public string str
    {
        get { return _c.ToString(); }
        set { c = value[0]; }
    }

    public bool visible
    {
        get { return tRend.enabled; }
        set { tRend.enabled = value; }
    }

    public Color color
    {
        get { return rend.material.color; }
        set { rend.material.color = value; }
    }

    public Vector3 pos
    {
        set
        {
            // Find a midpoint that is a random distance from the actual
            // midpoint between the current position and the value passed in
            var mid = (transform.position + value) / 2f;

            // the random distance will be within 1/4 of the magnitude of the
            // line from the actual midpoint
            var mag = (transform.position - value).magnitude;
            mid += Random.insideUnitSphere * mag * 0.25f;

            // create a list<vector3> of Bezier points
            pts = new List<Vector3>() { transform.position, mid, value };

            // if timeStart is at the default -1, then set it
            if (timeStart == -1)
                timeStart = Time.time;
        }
    }

    public Vector3 posImmediate
    {
        set { transform.position = value; }
    }
}
