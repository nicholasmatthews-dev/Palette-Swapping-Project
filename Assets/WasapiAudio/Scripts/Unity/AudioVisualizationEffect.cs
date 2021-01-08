﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.WasapiAudio.Scripts.Unity
{
    public abstract class AudioVisualizationEffect : MonoBehaviour
    {
        // Inspector Properties
        public WasapiAudioSource WasapiAudioSource;
        
        [SerializeReference]
        public List<SpectrumTransformer> Transformers = new List<SpectrumTransformer>();
        
        protected int SpectrumSize { get; private set; }

        public virtual void Awake()
        {
            if (WasapiAudioSource == null)
            {
                Debug.Log("You must set a WasapiAudioSource");
                return;
            }

            SpectrumSize = WasapiAudioSource.SpectrumSize;
        }

        protected float[] GetSpectrumData()
        {
            if (WasapiAudioSource == null)
            {
                Debug.Log("You must set a WasapiAudioSource");
                return null;
            }

            // Get raw / unmodified spectrum data
            var spectrumData = WasapiAudioSource.GetSpectrumData();

            // Run spectrum data through all configured transformers
            if (Transformers != null && Transformers.Count > 0)
            {
                foreach (var transformer in Transformers)
                {
                    spectrumData = transformer.Transform(spectrumData);
                }
            }

            return spectrumData;
        }
    }
}
