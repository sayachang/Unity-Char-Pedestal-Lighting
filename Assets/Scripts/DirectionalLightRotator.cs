using KUtil;
using UnityEngine;

namespace CharPedestalLighting
{
    public class DirectionalLightRotator : MonoBehaviour
    {
        public Light directionalLight;
        public float sec = 3;
        public float interval = 0;
        public bool easing = false;
        public bool reverse = false;
        private float elapsedTrack = 0;
        private bool isRotating = true;

        private Vector3 originalAngle = new Vector3();

        private void Start()
        {
            originalAngle = directionalLight.transform.eulerAngles;
        }

        void Update()
        {
            float elapsedInTurn = Time.time % (sec + interval);

            if (elapsedInTurn < sec)
            {
                isRotating = true;
                elapsedTrack += Time.deltaTime;
            }
            else
            {
                isRotating = false;
                elapsedTrack = 0;
            }

            if (isRotating)
            {
                float t = elapsedTrack / sec;

                if (easing)
                {
                    t = Easing.QuintInOut(0, 1, t);
                }

                float r = t * 360.0f;
                r = reverse ? r : r * -1;
                directionalLight.transform.eulerAngles = originalAngle - new Vector3(0, originalAngle.y - r, 0);

                if (elapsedTrack > sec) elapsedTrack -= sec;
            }

        }
    }
}
