using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonFeedbackScript : MonoBehaviour
    {
        public void OnButtonDown(Hand fromHand)
        {
            ChangeColor(Color.cyan);

            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {
            ChangeColor(Color.magenta);
        }

        private void ChangeColor(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();

            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}