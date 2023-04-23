using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [AddComponentMenu ("Image Effects/Color Adjustments/Color Correction (Curves, Saturation)")]
    public class ColorCorrectionCurves
	{
        public enum ColorCorrectionMode
		{
            Simple = 0,
            Advanced = 1
        }

        public AnimationCurve redChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));
        public AnimationCurve greenChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));
        public AnimationCurve blueChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));

        public bool  useDepthCorrection = false;

        public AnimationCurve zCurve = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));
        public AnimationCurve depthRedChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));
        public AnimationCurve depthGreenChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));
        public AnimationCurve depthBlueChannel = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));

        private Material ccMaterial;
        private Material ccDepthMaterial;
        private Material selectiveCcMaterial;

        private Texture2D rgbChannelTex;
        private Texture2D rgbDepthChannelTex;
        private Texture2D zCurveTex;

        public float saturation = 1.0f;

        public bool  selectiveCc = false;

        public Color selectiveFromColor = Color.white;
        public Color selectiveToColor = Color.white;

        public ColorCorrectionMode mode;

        public bool  updateTextures = true;

        public Shader colorCorrectionCurvesShader = null;
        public Shader simpleColorCorrectionCurvesShader = null;
        public Shader colorCorrectionSelectiveShader = null;

        private bool  updateTexturesOnStartup = true;
    }
}
