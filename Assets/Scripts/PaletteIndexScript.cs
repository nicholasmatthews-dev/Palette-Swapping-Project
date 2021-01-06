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
        /*Fetches the reference to the renderer from the object the script is attached to
         * then grabs the material reference from the renderer in order to adjust the attached shader
         */
        objectRenderer = GetComponent<Renderer>();
        objectMaterial = objectRenderer.material;

        //Sets the initial offset of the gradient to the initial offset value
        objectMaterial.SetFloat("_Offset", offset);
    }

    // Update is called once per frame
    void Update()
    {
        //Adjusts the offset by the speed defined in the editor, so as to animate the colors of the image
        offset += _Speed;

        //Resets the offset to a value between 0 and 1 if the offset is over or under
        if (offset >= 1)
        {
            offset -= 1;
        }
        else if (offset < 0)
        {
            offset += 1;
        }

        //Sets the offset of the gradient to the calculated value of offset
        objectMaterial.SetFloat("_Offset", offset);
    }
}
