using UnityEngine;
using System.Collections;

public class ColorScreen : MonoBehaviour 
{

		public Color color;

		public Shader colorShader = null;

        static Material m_Material = null;
        protected Material material {
            get {
                if (m_Material == null) {
					m_Material = new Material( colorShader );
                    m_Material.hideFlags = HideFlags.DontSave;
                }
                return m_Material;
            }
        }

        protected void OnDisable() {
            if ( m_Material ) {
                DestroyImmediate( m_Material );
            }
        }

        // --------------------------------------------------------

        protected void Start()
        {
            // Disable if we don't support image effects
            if (!SystemInfo.supportsImageEffects) {
                enabled = false;
                return;
            }
            // Disable if the shader can't run on the users graphics card
			if (!colorShader || !material .shader .isSupported)
			{
                enabled = false;
                return;
            }
        }

        // Called by the camera to apply the image effect
        void OnRenderImage (RenderTexture source, RenderTexture destination) {

            int rtW = source.width;
            int rtH = source.height;
			material .SetVector( "_Color" , color );

			Graphics .Blit( source , destination , material );
        }
}
