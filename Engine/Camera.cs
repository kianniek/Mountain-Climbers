using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.Engine
{
    public class Camera
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation

        //set inital values
        public Camera()
        {
            _zoom = 1f;
            _rotation = 0.0f;
            _pos = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
        }

        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }
        // Sets and gets Rotation in raidiants
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Extra function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
        //the grafics engine uses a 4x4 matrix to move the camera 
        public Matrix Get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =
              Matrix.CreateTranslation(
                  new Vector3(-_pos.X, -_pos.Y, 0)) *
                  Matrix.CreateRotationZ(Rotation) *
                  Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                  Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }
    }

}
