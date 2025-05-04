using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

public class Level3CardController : MonoBehaviour
{
    [SerializeField] public Transform pointToMove;
    [SerializeField] private float smooth = 20f;
    [SerializeField] public Sprite cardFace;

    private SpriteRenderer sp;

    private Vector3 movement = new Vector3();
    private float rotation = 0f;
    private Vector3 destination;

    private bool moving = false;
    private bool flipped = false;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!moving)
            return;

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref movement, Time.deltaTime * smooth);

        float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0f, ref rotation, Time.deltaTime * smooth);

        transform.rotation = Quaternion.Euler(0, Angle, 0);

        if (!flipped && (Angle <= 90 || Angle >= 270))
        {
            flipped = true;
            SetSprite(cardFace);
        }                       
    }

    public void MoveToPoint()
    {
        moving = true;
        destination = pointToMove.position;
    }

    public void SetSprite(Sprite sprite)
    {
        sp.sprite = sprite;
    }
}
