using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] Transform door;
    [SerializeField] float closed;
    [SerializeField] float open;
    [SerializeField] float time;

    [field: SerializeField] public EType Type { get; private set; }
    [field: SerializeField] public EState State {  get; private set; }
    float startTime;

    float elapsedTime => Time.time - startTime;
    float progress => elapsedTime / time;

    bool IsAngleType => Type == EType.Angle;
    bool IsSlidingTypeZ => Type == EType.SlidingZ;
    bool IsSlidingTypeX => Type == EType.SlidingX;

    public enum EType
    {
        Angle,
        SlidingZ,
        SlidingX
    }

    public enum EState
    {
        Open,
        Closed,
        Opening,
        Closing
    }

    private void Start()
    {
        if (State == EState.Open) EndOpening();
        else if (State == EState.Closed) EndClosing();
    }

    private void Update()
    {
        switch (State)
        {
            case EState.Opening:
                if (progress < 1)
                {
                    if (IsAngleType) SetAngle(Mathf.LerpAngle(closed, open, progress));
                    else if (IsSlidingTypeZ) SetZPos(Mathf.Lerp(closed, open, progress));
                    else if (IsSlidingTypeX) SetXPos(Mathf.Lerp(closed, open, progress));
                }
                else EndOpening();
                break;

            case EState.Closing:
                if (progress < 1)
                {
                    if (IsAngleType) SetAngle(Mathf.LerpAngle(open, closed, progress));
                    else if (IsSlidingTypeZ) SetZPos(Mathf.Lerp(open, closed, progress));
                    else if (IsSlidingTypeX) SetXPos(Mathf.Lerp(open, closed, progress));
                }
                else EndClosing();
                break;
        }
    }

    public void Interact()
    {
        if (State == EState.Open) BeginClosing();
        else if (State == EState.Closed) BeginOpening();
    }

    private void BeginOpening()
    {
        State = EState.Opening;
        startTime = Time.time;
    }

    private void BeginClosing()
    {
        print(name + ": Starting Closing");
        State = EState.Closing;
        startTime = Time.time;
    }

    private void EndClosing()
    {
        State = EState.Closed;
        if (IsAngleType) SetAngle(closed);
        else if (IsSlidingTypeZ) SetZPos(closed);
        else if (IsSlidingTypeX) SetXPos(closed);
    }

    private void EndOpening()
    {
        State = EState.Open;
        if (IsAngleType) SetAngle(open);
        else if (IsSlidingTypeZ) SetZPos(open);
        else if (IsSlidingTypeX) SetXPos(open);
    }

    private void SetAngle(float angle)
    {
        Vector3 angles = door.localEulerAngles;
        angles.y = angle;
        door.localEulerAngles = angles;
    }

    private void SetZPos(float position)
    {
        Vector3 localPos = door.localPosition;
        localPos.z = position;
        door.localPosition = localPos;
    }

    private void SetXPos(float position)
    {
        Vector3 localPos = door.localPosition;
        localPos.x = position;
        door.localPosition = localPos;
    }

    public string InteractionLabel()
    {
        if (State == EState.Open) return "Close Door";
        else if (State == EState.Closed) return "Open Door";
        else return "";
    }
}
