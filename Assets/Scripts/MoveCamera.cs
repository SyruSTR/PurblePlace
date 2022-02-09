using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Передвижение камеры вслед за новыми основами торта, чтобы всегда видеть верхушка торта
public class MoveCamera : MonoBehaviour
{
    private float height = 0f;
    private Vector3 _startPosition;
    [SerializeField] private CakeCreation _baseCakeCreation;


    private void Start()
    {
        _startPosition = transform.position;
        _baseCakeCreation.HeightChange += ChangeHeight;
    }
    private void ChangeHeight(float newHeight)
    {
        height = newHeight;
        
    }
    private void Update()
    {
        Vector3 velocity = new Vector3(transform.position.x, _startPosition.y + height, transform.position.z);
        transform.position = Vector3.Lerp(transform.position,velocity, 0.1f);
    }
}
