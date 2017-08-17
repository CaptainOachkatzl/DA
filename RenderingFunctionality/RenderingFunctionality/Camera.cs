using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingFunctionality.Camera
{
    public enum CameraType
    {
        Dimension2,
        Dimension3
    }

    abstract public class Camera
    {
        public Viewport Viewport { get; set; }

        protected CameraType Type { get; set; }

        protected float m_zoom; // Camera Zoom
        public float Zoom
        {
            get { return m_zoom; }
            set { m_zoom = value; }
        }

        virtual public Vector3 Position
        {
            get
            {
                if (Type == CameraType.Dimension2)
                    return new Vector3(Position2D.X, Position2D.Y, 0);

                return Position3D;
            }
        }

        protected Vector3 m_pos3D; // Camera Position
        public Vector3 Position3D
        {
            get { return m_pos3D; }
            set
            {
                m_pos2D = new Vector2(value.X, value.Y);
                m_pos3D = value;
            }
        }

        protected Vector2 m_pos2D;
        public Vector2 Position2D
        {
            get { return m_pos2D; }
            set
            {
                m_pos2D = value;
                m_pos3D = new Vector3(value.X, value.Y, 0);
            }
        }

        protected Vector2 m_rotation;
        public Vector2 Rotation
        {
            get { return m_rotation; }
            set { m_rotation = value; }
        }

        public Camera(Viewport viewport, CameraType type)
        {
            Type = type;
            Viewport = viewport;
            ResetCamera();
        }

        abstract public Matrix GetTranslationMatrix();

        public void ResetCamera()
        {
            Zoom = 1;
            Rotation = Vector2.Zero;
            Position2D = Vector2.Zero;
        }

        public Vector2 CalculateRelative2DPosition(Vector2 position)
        {
            Vector2 relativePosition = position / Zoom;
            relativePosition.X += Position.X - (Viewport.Width / 2 / Zoom);
            relativePosition.Y += Position.Y - (Viewport.Height / 2 / Zoom);
            return relativePosition;
        }

        public Vector2 CalculateAbsolute2DPosition(Vector2 position)
        {
            Vector2 absolutePosition = position;
            absolutePosition.X += (Viewport.Width / 2 / Zoom) - Position.X;
            absolutePosition.Y += (Viewport.Height / 2 / Zoom) - Position.Y;
            absolutePosition *= Zoom;

            return absolutePosition;
        }
    }

    public class Camera2D : Camera
    {
        public Camera2D (Viewport viewport) : base (viewport, CameraType.Dimension2) { }

        public override Matrix GetTranslationMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation.X) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(
                                             new Vector3(Viewport.Width * 0.5f,
                                             Viewport.Height * 0.5f, 0));
        }
    }
}
