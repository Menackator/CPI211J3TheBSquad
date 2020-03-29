using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class lightControl : MonoBehaviour
    {
        public Camera Camera;

        public Light flashLight;
        public LerpControlledBob jumpAndLandingBob = new LerpControlledBob();
        public RigidbodyFirstPersonController rigidbodyFirstPersonController;

        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;


        private void Start()
        {
            //flashLight = GetComponent<Light>();
            m_OriginalCameraPosition = Camera.transform.localPosition;
        }


        private void Update()
        {
            Vector3 nCameraPosition;
            if (rigidbodyFirstPersonController.Velocity.magnitude > 0 && rigidbodyFirstPersonController.Grounded)
            {
                nCameraPosition = Camera.transform.localPosition;
                nCameraPosition.y = Camera.transform.localPosition.y;
            }
            else
            {
                nCameraPosition = Camera.transform.localPosition;
                nCameraPosition.y = m_OriginalCameraPosition.y;
            }
            Camera.transform.localPosition = nCameraPosition;

            float fYRot = Camera.transform.eulerAngles.y;

            transform.rotation = Quaternion.Euler(Camera.transform.eulerAngles.x,Camera.transform.eulerAngles.y,Camera.transform.eulerAngles.z);

            m_PreviouslyGrounded = rigidbodyFirstPersonController.Grounded;
        }
    }
}