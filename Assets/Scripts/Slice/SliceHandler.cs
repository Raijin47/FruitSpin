using UnityEngine;
using UnityEngine.UI;

public class SliceHandler : MonoBehaviour
{
    public Image rectangleImage;
    public Slider slider;
    public GameObject finger;
    public bool isActive = false;

    private Camera mainCamera;
    private Vector3 touchStartPosition;
    private Vector3 touchCurrentPosition;
    private bool isTouching = false;
    private bool isIntersecting = false;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }


    private void Update()
    {
        if (!IsActive()) return;


        HandleInput();
        CheckIntersection();
    }

    public void Activate()
    {
        isActive = true;
        slider.value = slider.minValue;
        finger.SetActive(true);
    }

    public void Deactivate()
    {
        isActive = false;
        slider.value = slider.minValue;
        finger.SetActive(false);

        isTouching = false;
        isIntersecting = false;
    }

    public bool IsActive()
    {
        return isActive;
    }

    private void CheckIntersection()
    {
        if (isTouching && rectangleImage != null)
        {
            isIntersecting = LineIntersectsUIRect(touchStartPosition, touchCurrentPosition, rectangleImage.rectTransform);

            if (isIntersecting)
            {
                Deactivate();
                slider.value = slider.maxValue;

                // —юда добавить звук
            }
        }
    }

    private bool LineIntersectsUIRect(Vector2 lineStart, Vector2 lineEnd, RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        for (int i = 0; i < 4; i++)
        {
            corners[i] = RectTransformUtility.WorldToScreenPoint(mainCamera, corners[i]);
        }

        return LineIntersectsLine(lineStart, lineEnd, corners[0], corners[1])
            && LineIntersectsLine(lineStart, lineEnd, corners[2], corners[3]);
    }

    private bool LineIntersectsLine(Vector3 line1Start, Vector3 line1End, Vector3 line2Start, Vector3 line2End)
    {
        Vector3 line1Dir = line1End - line1Start;
        Vector3 line2Dir = line2End - line2Start;
        Vector3 lineStartDiff = line2Start - line1Start;

        float cross = line1Dir.x * line2Dir.y - line1Dir.y * line2Dir.x;
        if (Mathf.Abs(cross) < 1e-8)
            return false;

        float t1 = (lineStartDiff.x * line2Dir.y - lineStartDiff.y * line2Dir.x) / cross;
        if (t1 < 0 || t1 > 1)
            return false;

        float t2 = (lineStartDiff.x * line1Dir.y - lineStartDiff.y * line1Dir.x) / cross;
        if (t2 < 0 || t2 > 1)
            return false;

        return true;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            touchStartPosition = GetInputPosition();
            touchCurrentPosition = touchStartPosition;
            isTouching = true;
        }
        else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            touchCurrentPosition = GetInputPosition();
        }
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isTouching = false;
        }
    }

    private Vector3 GetInputPosition()
    {
        if (Input.touchCount > 0)
            return Input.GetTouch(0).position;
        else
            return Input.mousePosition;
    }
}