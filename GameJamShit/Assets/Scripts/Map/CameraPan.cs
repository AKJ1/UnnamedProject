using System.Collections;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public Vector3 PanDireciton;

    private Vector3 previousHitPosition;

    private bool isScrolling;

    public float TotalPanTime = 2f;

    public void LateUpdate()
    {
        UpdatePan(); 
    }


    public void UpdatePan()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            PanDireciton = Vector2.zero;
            if (Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask("ScrollPlane")))
            {
                isScrolling = true;
                previousHitPosition = hit.point;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isScrolling)
            {
                if (Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask("ScrollPlane")))
                {
                    Vector3 delta = previousHitPosition - hit.point;
                    previousHitPosition = hit.point + delta;
                    transform.position += delta;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask("ScrollPlane")))
            {
                Vector3 delta = previousHitPosition - hit.point;
                isScrolling = false;
                PanDireciton =new Vector3(delta.x, 0, delta.z) ;
                StartCoroutine(ZeroOut());
            }
        }
        transform.position += PanDireciton;
    }

    IEnumerator ZeroOut()
    {
        float current = 0;
        while (PanDireciton != Vector3.zero)
        {
            float t = current/TotalPanTime;
            current += Time.deltaTime;
            PanDireciton = Vector3.Lerp(PanDireciton, Vector3.zero, t);
            yield return null;
        }
    }

    public void UpdateZoom()
    {
        
    }
}
