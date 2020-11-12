using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [Header("Set Dynamically")]
    public TextMesh tMesh; // shows the char
    public Renderer tRend; // renderer of 3D text, this will determine whether char is visible
    public bool big = false; // big letters act little differently

    private char _c; // the char shown on this letter
    private Renderer rend;

    void Awake()
    {
        tMesh = GetComponentInChildren<TextMesh>();
        tRend = tMesh.GetComponent<Renderer>();
        rend = GetComponent<Renderer>();
        visible = false;
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
            transform.position = value;
            // TODO: finish implementation
        }
    }
}
