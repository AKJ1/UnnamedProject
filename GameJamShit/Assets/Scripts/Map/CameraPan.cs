using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public Vector2 PanDireciton;

    private Vector2 accumulated;
    private Vector2 initialPos;

    private Vector3 initialCamPosition;

    private bool isScrolling;

    public float currentWeight = .4f;

    public void Update()
    {
        UpdatePan();
    }

    public void UpdatePan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask.NameToLayer("NewsPaperPlane");
            if (Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask("ScrollPlane")))
            {
                isScrolling = true;
                initialPos = Input.mousePosition;
                initialCamPosition = transform.position;
            }
            
        }
        else if (Input.GetMouseButton(0))
        {
            if (isScrolling)
            {
                Vector2 delta = initialPos - new Vector2(Input.mousePosition.x,  Input.mousePosition.y);
                accumulated = Vector2.Lerp(accumulated, delta, currentWeight);
                
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PanDireciton = accumulated;
        }
        transform.Translate(accumulated);
    }

    public void UpdateZoom()
    {
        
    }
}
