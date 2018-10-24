using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace TouchInput
{
    public static class TouchScreenInput
    {
        #region TouchScreen Class
        public class ScreenTouch
        {
            public GameObject objectTouchStartedOn;
            public GameObject objectTouchingNow;
            public Component behaviourUsingThisTouch;
            public Action onTouchStarted;
            public Action onTouchMoved;
            public Action onTouchEnded;
            public float timeHeld;
        }
        #endregion

        #region Public Variables 
        public static Action onAnyTouchStarted;
        public static Action onAnyTouchEnded;

        public static Dictionary<int, ScreenTouch> allTouches = new Dictionary<int, ScreenTouch>();
        #endregion

        #region Public Methods
        /// <summary>
        /// This will return the first touch found that is related to an object...
        /// please dont call this one often this function is pretty expensive especially if you search children.
        /// </summary>
        /// <param name="gameObject">Object to search</param>
        /// <param name ="searchChildren">Should we search children of the game object</param>
        /// <returns></returns>
        public static ScreenTouch GetTouchRelatedToObject(GameObject gameObject, bool searchChildren)
        {
            var components = (searchChildren) ? gameObject.GetComponentsInChildren<Component>() : gameObject.GetComponents<Component>();
            var touches = allTouches.Values.ToList<ScreenTouch>();
            for (int i = 0; i < components.Length; i++)
            {
                for (int j = 0; j < touches.Count; j++)
                {
                    if (touches[j].behaviourUsingThisTouch == components[i])
                    {
                        return touches[j];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the first touch that has reference to the specified script.
        /// This function is much faster than the game object version.
        /// </summary>
        /// <param name="script">Script to be checked</param>
        /// <returns></returns>
        public static ScreenTouch GetTouchRelatedToScript(Component script)
        {
            var touches = allTouches.Values.ToList<ScreenTouch>();
            for (int i = 0; i < touches.Count; i++)
            {
                if (touches[i].behaviourUsingThisTouch == script)
                    return touches[i];
            }
            return null;
        }
        #endregion


    }
}