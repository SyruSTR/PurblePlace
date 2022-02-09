using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAnimate : MonoBehaviour
{
    [SerializeField] private float _offsetY = 5f;
    private Vector3 _startPosition;    

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + _offsetY, transform.position.z);        
    }

    private void Update()
    {        
        if (!Mathf.Approximately(transform.position.y, _startPosition.y))
        {
            transform.position = Vector3.Lerp(transform.position, _startPosition, 0.1f);
        }
        else
            Destroy(GetComponent<FallingAnimate>());
    }


}
