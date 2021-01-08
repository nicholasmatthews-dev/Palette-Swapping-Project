using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.WasapiAudio.Scripts.Unity;

namespace Assets.Scripts {


    public class PaletteIndexScript : AudioVisualizationEffect
    {
        public float _Speed = 5;
        public string _Mode = "Average";
        public float _MaxSpeed = 1;
        public float _MinSpeed = 0;
        public int _UpperBand = 0;
        public int _LowerBand = 0;
       

        private int modeIndex = 0;
        private float offset = 0;
        private Renderer objectRenderer;
        private Material objectMaterial;
        private float[] spectrumData;
        // Start is called before the first frame update
        void Start()
        {
            /*Fetches the reference to the renderer from the object the script is attached to
             * then grabs the material reference from the renderer in order to adjust the attached shader
             */
            objectRenderer = GetComponent<Renderer>();
            objectMaterial = objectRenderer.sharedMaterial;


            //Sets the initial offset of the gradient to the initial offset value
            objectMaterial.SetFloat("_Offset", offset);

            spectrumData = GetSpectrumData();

            modeIndex = Definitions.getModeIndex(_Mode);
        }

        // Update is called once per frame
        void Update()
        {

            spectrumData = GetSpectrumData();
            if (modeIndex == Definitions.Average)
            {
                offset += averageBands() * _Speed * Time.deltaTime;
            }
            else if (modeIndex == Definitions.Max)
            {
                offset += maxBands() * _Speed * Time.deltaTime;
            }
            
            if (Mathf.Abs(offset) > _MaxSpeed)
            {
                offset = _MaxSpeed * Mathf.Sign(offset);
            }
            else if (Mathf.Abs(offset) < _MinSpeed)
            {
                offset = _MinSpeed * Mathf.Sign(offset);
            }

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

        private float averageBands()
        {
            float output = 0f;   
            if (_UpperBand >= _LowerBand)
            {
                for (int i = _LowerBand; i <= _UpperBand; i++)
                {
                    output += spectrumData[i];
                }
                output = output / (_UpperBand - _LowerBand + 1);
                return output;
            }
            else
            {
                for (int i = _UpperBand; i < _LowerBand; i++)
                {
                    output += spectrumData[i];
                }
                output = output / (_LowerBand - _UpperBand + 1);
                return output;
            }
        }

        private float maxBands()
        {
            float output = 0f;
            if (_UpperBand >= _LowerBand)
            {
                for (int i = _LowerBand; i <= _UpperBand; i++)
                {
                    if (output < spectrumData[i])
                    {
                        output = spectrumData[i];
                    }
                }
                return output;
            }
            else
            {
                for (int i = _UpperBand; i < _LowerBand; i++)
                {
                    if (output < spectrumData[i])
                    {
                        output = spectrumData[i];
                    }
                }
                return output;
            }
        }
    }
}
