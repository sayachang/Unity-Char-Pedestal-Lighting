using KUtil;
using UnityEngine;

namespace CharPedestalLighting
{
    public class PedestalRotator : MonoBehaviour
    {
        public float sec = 3;
        public float interval = 0;
        public bool reverse = false;
        public bool easing = false;
        private float elapsedTrack = 0;
        private bool isRotating = true;

        private float originalAngle = 0;

        private void Start()
        {
            originalAngle = transform.eulerAngles.y;
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
                transform.eulerAngles = new Vector3(0, originalAngle + r, 0);

                if (elapsedTrack > sec) elapsedTrack -= sec;
            }

        }
    }
}
