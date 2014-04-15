using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Phobia
{
    public class Camera
    {
        #region Fields
        private Matrix transform;
        public Vector2 center;
        private Viewport viewPort;
        #endregion

        #region Properties

        //get the camera transfomation matrix  in all directions.
        public Matrix Transform
        {
            get { return transform; }
        }

        #endregion

        #region Constructor
        public Camera(Viewport newViewPort)
        {
            viewPort = newViewPort;
        }
        #endregion

        #region Update Camera Position

        //Updates the position of the camera due to game changes.
        public void Update(Vector2 postion, int X_Offset, int Y_Offset)
        {
            if (postion.X < viewPort.Width / 2)
                center.X = viewPort.Width / 2;
            else if (postion.X > X_Offset - (viewPort.Width / 2))
                center.X = X_Offset - (viewPort.Width / 2);
            else
                center.X = postion.X;

            if (postion.Y < viewPort.Height / 2)
                center.Y = viewPort.Height / 2;
            else if (postion.Y > Y_Offset - (viewPort.Height / 2))
                center.Y = Y_Offset - (viewPort.Height / 2);
            else
                center.Y = postion.Y;

            transform = Matrix.CreateTranslation(new Vector3((viewPort.Width / 2) - center.X, (viewPort.Height / 2) - center.Y, 0));
        }
        #endregion
    }
}
