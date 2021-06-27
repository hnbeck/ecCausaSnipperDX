using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UCausaDX
{
    class Camera
    {
        
        Viewport vport;
        Point vpCenter;

        public float vpScale { get; set; }
        public Matrix currentTransform { get; set; }

        Matrix basisTransform;
       

        public Camera(Viewport v)
        {
            vport = v;
            init();
        }
        public Camera ()
        {
            vport = new Viewport(0, 0, 1200, 800);
            init(); 
        }

        private void init()
        {
            vpScale = 1.0f;
            vpCenter = vport.Bounds.Center;
            Vector3 translation = new Vector3(vpCenter.ToVector2(),0);
            basisTransform = Matrix.CreateScale(vpScale) * Matrix.CreateTranslation(translation);
            currentTransform = basisTransform; 
        }

        public void scaleVP (float scale)
        {
            currentTransform = basisTransform * Matrix.CreateScale(scale);
        }
    }
}
