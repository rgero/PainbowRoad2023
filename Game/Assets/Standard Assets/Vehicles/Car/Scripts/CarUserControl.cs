using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
		private bool isReversed;

		public void reverseDirection(bool active){
			isReversed = active;
		}

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
			isReversed = false;
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
			int reverse = ((isReversed) ? -1:1);
			//Debug.Log (isReversed);
			//Debug.Log (reverse);

			float h = CrossPlatformInputManager.GetAxis("Horizontal") * ((isReversed) ? -1:1) ;
			float rightTrigger = CrossPlatformInputManager.GetAxis ("GasPedal");
			float leftTrigger = CrossPlatformInputManager.GetAxis ("BrakePedal");
			float v = CrossPlatformInputManager.GetAxis ("Vertical");
			if (v == 0) {
				v = rightTrigger - leftTrigger;
			}
			v = v * ((isReversed) ? -1 : 1);

#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
