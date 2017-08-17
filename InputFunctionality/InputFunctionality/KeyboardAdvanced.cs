using Microsoft.Xna.Framework.Input;

namespace InputFunctionality.KeyboardAdapter
{
    public class KeyboardAdvanced
    {
        protected KeyboardState m_oldState;
        protected KeyboardState m_currentState;

        public void Initialize()
        {
            m_currentState = Keyboard.GetState();
            m_oldState = m_currentState;
        }

        /// <summary>
        /// Call this every game cycle to get the present keyboard state.
        /// </summary>
        public void UpdateState()
        {
            m_oldState = m_currentState;
            m_currentState = Keyboard.GetState();
        }

        public bool KeyCurrentlyPressed(Keys key)
        {
            return m_currentState.IsKeyDown(key);
        }

        public bool KeyWasPressed(Keys key)
        {
            return m_oldState.IsKeyDown(key);
        }

        public bool KeyNowPressed(Keys key)
        {
            return !KeyWasPressed(key) && KeyCurrentlyPressed(key);
        }

        public bool KeyNowReleased(Keys key)
        {
            return KeyWasPressed(key) && !KeyCurrentlyPressed(key);
        }
    }
}
