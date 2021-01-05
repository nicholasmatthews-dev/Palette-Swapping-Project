using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteIndexScript : MonoBehaviour
{
   
    public float _Speed;
    private float offset = 0;
    private Renderer objectRenderer;
    private Material objectMaterial;
    private Shader objectShader;
    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectMaterial = objectRenderer.material;
        objectMaterial.SetFloat("_Offset", offset);
    }

    // Update is called once per frame
    void Update()
    {
        offset += _Speed;
        if (offset >= 1)
        {
            offset -= 1;
        }
        objectMaterial.SetFloat("_Offset", offset);
    }
}
