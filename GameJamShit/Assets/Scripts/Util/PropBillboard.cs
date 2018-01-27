using UnityEngine;

namespace Assets.Scripts.Util
{
    [ExecuteInEditMode]
    public class PropBillboard : MonoBehaviour
    {

        public float ReductionFactor = 0.5f;

        public void Update()
        {
            Quaternion start = Quaternion.identity;
            transform.LookAt(Camera.main.transform.position);
            //Quaternion q1 = Quaternion.AngleAxis(( transform.eulerAngles.y), Vector3.up);

            Quaternion q2 = Quaternion.AngleAxis(transform.eulerAngles.x, Vector3.left);
            //Quaternion q3 = Quaternion.Slerp(start, q1, .85f);
            transform.rotation = Quaternion.Slerp(start, q2, ReductionFactor);
        }
    }
}
