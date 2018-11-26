using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LinearAnchor : AnchorPoint {
    [Header("Movement")]
    [SerializeField] private Axis movementAxis;
    [Tooltip("The total amount of position that can be removed from this object, if 0 min position becomes transform.position")]
    [SerializeField] private float minPosition;
    [SerializeField] private bool locksAtMin;
    [Tooltip("The total amount of position that can be added from this object, if 0 max position becomes transform.position")]
    [SerializeField] private float maxPosition;
    [SerializeField] private bool locksAtMax;
    [SerializeField] private float moveSpeed;
    [SerializeField] private AudioClip limitReachedSound;

    [Header("Events")]
    [SerializeField] private UnityEvent onMinPosition;
    [SerializeField] private UnityEvent onMaxPosition;

    [Header("Returning")]
    [SerializeField] private bool returnsToOrigin;
    [SerializeField] private float returnDelay;
    [SerializeField] private float returnTime;
    [SerializeField] private AnimationCurve returnCurve;
    [SerializeField] private AudioClip returnSound;

#if UNITY_EDITOR
    [Header("Debug Visualization")]
    [SerializeField] private bool drawGizmos;
    [SerializeField] private bool onlySelected;
    [SerializeField] private Color minPositionGizmoColour;
    [SerializeField] private Color maxPositionGizmoColour;
    [SerializeField] private float gizmoEndRadius;
#endif

    private Vector3 minPoint;
    private Vector3 maxPoint;
    private Vector3 originPoint;
    private IEnumerator returnRoutine;

    protected override void Start()
    {
        base.Start();

        maxPoint = minPoint = originPoint = transform.position;

        switch (movementAxis)
        {
            case Axis.X:
                if (minPosition != 0)
                    minPoint.x -= minPosition;
                if (maxPosition != 0)
                    maxPoint.x += maxPosition;
                break;
            case Axis.Y:
                if (minPosition != 0)
                    minPoint.y -= minPosition;
                if (maxPosition != 0)
                    maxPoint.y += maxPosition;
                break;
            case Axis.Z:
                if (minPosition != 0)
                    minPoint.z -= minPosition;
                if (maxPosition != 0)
                    maxPoint.z += maxPosition;
                break;
            default:
                break;
        }

        if (maxPoint == originPoint && locksAtMax)
            locksAtMax = false;
        else if (minPoint == originPoint && locksAtMin)
            locksAtMin = false;
    }

    public override void Anchored(Vector3 anchorPoint)
    {
        if (anchoredObject == null)
            return;

        base.Anchored(anchorPoint);

        if((transform.position - minPoint).sqrMagnitude > (transform.position - maxPoint).sqrMagnitude)
        {
            if (anchoredObject.transform.localScale.y > 0)
                anchoredObject.transform.localScale = new Vector3(anchoredObject.transform.localScale.x,
                    -anchoredObject.transform.localScale.y, anchoredObject.transform.localScale.z);
        }
        else
        {
            if (anchoredObject.transform.localScale.y < 0)
                anchoredObject.transform.localScale = new Vector3(anchoredObject.transform.localScale.x,
                    -anchoredObject.transform.localScale.y, anchoredObject.transform.localScale.z);
        }
    }

    public override void MoveObject(Vector3 velocity, Vector3 followingPoint)
    {
        if (isLocked)
            return;

        velocity = velocity.normalized;

        switch (movementAxis)
        {
            case Axis.X:
                if (velocity.x == 0)
                    return;

                transform.position += new Vector3(velocity.x * moveSpeed * Time.deltaTime, 0, 0);

                if(transform.position.x <= minPoint.x)
                {
                    transform.position = minPoint;
                    onMinPosition.Invoke();

                    if (locksAtMin)
                    {
                        BreakAnchor(followingPoint);
                        isLocked = true;
                    }
                }
                else if(transform.position.x >= maxPoint.x)
                {
                    transform.position = maxPoint;
                    onMaxPosition.Invoke();

                    if (locksAtMax)
                    {
                        BreakAnchor(followingPoint);
                        isLocked = true;
                    }
                }

                break;
            case Axis.Y:
                if (velocity.y == 0)
                    return;

                transform.position += new Vector3(0, velocity.y * moveSpeed * Time.deltaTime, 0);

                if (transform.position.y <= minPoint.y)
                {
                    transform.position = minPoint;
                    onMinPosition.Invoke();

                    if (locksAtMin)
                    {
                        BreakAnchor(followingPoint);
                        isLocked = true;
                    }
                }
                else if (transform.position.y >= maxPoint.y)
                {
                    transform.position = maxPoint;
                    onMaxPosition.Invoke();
                    if (locksAtMax)
                    {
                        BreakAnchor(followingPoint);
                        isLocked = true;
                    }
                }

                break;
            case Axis.Z:
                if (velocity.z == 0)
                    return;

                transform.position += new Vector3(0, 0, velocity.z * moveSpeed * Time.deltaTime);

                if (transform.position.z <= minPoint.z)
                {
                    transform.position = minPoint;
                    onMinPosition.Invoke();

                    if (locksAtMin)
                    {
                        BreakAnchor(followingPoint);
                        isLocked = true;
                    }
                }
                else if (transform.position.z >= maxPoint.z)
                {
                    transform.position = maxPoint;
                    onMaxPosition.Invoke();

                    if (locksAtMax)
                    {
                        BreakAnchor(followingPoint);
                        isLocked = true;
                    }
                }

                break;
            default:
                break;
        }

        if (returnsToOrigin && isLocked)
        {
            if (limitReachedSound)
                AudioSource.PlayClipAtPoint(limitReachedSound, transform.position);

            returnRoutine = ReturnToOrigin();
            StartCoroutine(returnRoutine);
        }
    }

    private IEnumerator ReturnToOrigin()
    {
        yield return new WaitForSeconds(returnDelay);

        if (returnSound)
            AudioSource.PlayClipAtPoint(returnSound, transform.position);

        float t = 0f;
        Vector3 startPos = transform.position;

        while(t <= returnTime)
        {
            t += Time.deltaTime;

            transform.position = Vector3.Lerp(startPos, originPoint, returnCurve.Evaluate(t / returnTime));

            yield return null;
        }

        if ((locksAtMax || locksAtMin) && isLocked)
            isLocked = false;
    }

#if UNITY_EDITOR
    private void DrawGizmos()
    {
        if (minPosition == 0f && maxPosition == 0f)
            return; 

        Vector3 startPoint = transform.position;

        if(minPosition != 0)
        {
            switch (movementAxis)
            {
                case Axis.X:
                    startPoint.x -= minPosition;
                    break;
                case Axis.Y:
                    startPoint.y -= minPosition;
                    break;
                case Axis.Z:
                    startPoint.z -= minPosition;
                    break;
                default:
                    break;
            }
        }

        Vector3 endPoint = transform.position;

        if (maxPosition != 0)
        {
            switch (movementAxis)
            {
                case Axis.X:
                    endPoint.x += maxPosition;
                    break;
                case Axis.Y:
                    endPoint.y += maxPosition;
                    break;
                case Axis.Z:
                    endPoint.z += maxPosition;
                    break;
                default:
                    break;
            }
        }

        if (Gizmos.color != minPositionGizmoColour)
            Gizmos.color = minPositionGizmoColour;

        Gizmos.DrawSphere(startPoint, gizmoEndRadius);
        Gizmos.DrawLine(startPoint, endPoint);

        if (Gizmos.color != maxPositionGizmoColour)
            Gizmos.color = maxPositionGizmoColour;

        Gizmos.DrawSphere(endPoint, gizmoEndRadius);
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos && onlySelected == false)
            DrawGizmos();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        if (drawGizmos && onlySelected)
            DrawGizmos();
    }

    private void OnValidate()
    {
        if (minPosition < 0)
            minPosition = -minPosition;

        if (maxPosition < 0)
            maxPosition = -maxPosition;
    }
#endif 
}
