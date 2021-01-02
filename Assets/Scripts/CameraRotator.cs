using KUtil;
using UnityEngine;

namespace CharPedestalLighting
{
    public class CameraRotator : MonoBehaviour
    {
        public float sec = 3;
        public float interval = 0;
        public bool easing = false;
        public bool reverse = false;
        private float elapsedTrack = 0;
        private bool isRotating = true;

        private float originalAngle = 0;
        private Vector3 originalPos = new Vector3();

        private void Start()
        {
            originalAngle = transform.eulerAngles.y;
            originalPos = transform.localPosition;
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
                transform.eulerAngles = new Vector3(0, originalAngle - r, 0);

                float theta = Mathf.PI * 2 * t;
                theta = reverse ? theta : theta * -1;
                float newx = originalPos.x * Mathf.Cos(theta) - originalPos.z * Mathf.Sin(theta);
                float newz = originalPos.x * Mathf.Sin(theta) + originalPos.z * Mathf.Cos(theta);
                transform.localPosition = new Vector3(newx, originalPos.y, newz);

                if (elapsedTrack > sec) elapsedTrack -= sec;
            }

        }
    }
}
