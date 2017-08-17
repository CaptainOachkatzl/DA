using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InputFunctionality.MouseAdapter
{
    public class MouseAdvanced
    {
        MouseState m_currentState;
        MouseState m_oldState;

        public void Initialize()
        {
            m_currentState = Mouse.GetState();
            m_oldState = m_currentState;
        }

        public void UpdateState()
        {
            m_oldState = m_currentState;
            m_currentState = Mouse.GetState();
        }

        public bool LeftHold { get { return m_currentState.LeftButton == ButtonState.Pressed; } }
        public bool LeftWasHold { get { return m_oldState.LeftButton == ButtonState.Pressed; } }
        public bool LeftClicked { get { return !LeftWasHold && LeftHold; } }
        public bool LeftReleased { get { return LeftWasHold && !LeftHold; } }

        public bool RightHold { get { return m_currentState.RightButton == ButtonState.Pressed; } }
        public bool RightWasHold { get { return m_oldState.RightButton == ButtonState.Pressed; } }
        public bool RightClicked { get { return !RightWasHold && RightHold; } }
        public bool RightReleased { get { return RightWasHold && !RightHold; } }

        public int MouseWheelValue { get { return m_currentState.ScrollWheelValue - m_oldState.ScrollWheelValue; } }
        public bool MouseWheelHold { get { return m_currentState.MiddleButton == ButtonState.Pressed; } }
        public bool MouseWheelWasHold { get { return m_oldState.MiddleButton == ButtonState.Pressed; } }
        public bool MouseWheelClicked { get { return !MouseWheelWasHold && MouseWheelHold; } }
        public bool MouseWheelReleased { get { return MouseWheelWasHold && !MouseWheelHold; } }

        public int PositionX { get { return m_currentState.X; } }
        public int PositionY { get { return m_currentState.Y; } }

        public Vector2 Position { get { return new Vector2(PositionX, PositionY); } }
    }
}
