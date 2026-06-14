using UnityEngine;

/// <summary>
/// 모바일 터치 입력을 관리하는 클래스
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private bool debugMode = false;

    // 터치 입력 상태
    private Vector2 touchStartPosition;
    private Vector2 touchCurrentPosition;
    private float touchStartTime;
    private bool isTouching = false;

    // 터치 임계값
    [SerializeField] private float swipeThreshold = 50f;
    [SerializeField] private float doubleTapThreshold = 0.3f;
    [SerializeField] private float longPressThreshold = 1f;

    // 입력 이벤트
    public delegate void TouchEvent(Vector2 position);
    public delegate void SwipeEvent(Vector2 direction);

    public event TouchEvent OnTap;
    public event TouchEvent OnDoubleTap;
    public event TouchEvent OnLongPress;
    public event SwipeEvent OnSwipe;

    private float lastTapTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        HandleTouchInput();
    }

    /// <summary>
    /// 터치 입력 처리
    /// </summary>
    private void HandleTouchInput()
    {
        // 마우스 입력 (에디터 테스트용)
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPosition = Input.mousePosition;
            touchCurrentPosition = Input.mousePosition;
            touchStartTime = Time.time;
            isTouching = true;
        }

        if (Input.GetMouseButton(0) && isTouching)
        {
            touchCurrentPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && isTouching)
        {
            ProcessTouchEnd();
            isTouching = false;
        }

        // 모바일 터치 입력
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPosition = touch.position;
                    touchCurrentPosition = touch.position;
                    touchStartTime = Time.time;
                    isTouching = true;
                    break;

                case TouchPhase.Moved:
                    touchCurrentPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    ProcessTouchEnd();
                    isTouching = false;
                    break;
            }
        }
    }

    /// <summary>
    /// 터치 종료 처리
    /// </summary>
    private void ProcessTouchEnd()
    {
        float touchDuration = Time.time - touchStartTime;
        Vector2 swipeDelta = touchCurrentPosition - touchStartPosition;
        float swipeDistance = swipeDelta.magnitude;

        // 롱 프레스 (1초 이상)
        if (touchDuration >= longPressThreshold && swipeDistance < swipeThreshold)
        {
            OnLongPress?.Invoke(touchStartPosition);
            if (debugMode)
                Debug.Log("[InputManager] Long Press");
        }
        // 스와이프 (50px 이상)
        else if (swipeDistance >= swipeThreshold)
        {
            Vector2 direction = swipeDelta.normalized;
            OnSwipe?.Invoke(direction);
            if (debugMode)
                Debug.Log($"[InputManager] Swipe: {GetSwipeDirection(direction)}");
        }
        // 더블 탭
        else if (Time.time - lastTapTime <= doubleTapThreshold)
        {
            OnDoubleTap?.Invoke(touchStartPosition);
            lastTapTime = 0f;
            if (debugMode)
                Debug.Log("[InputManager] Double Tap");
        }
        // 싱글 탭
        else
        {
            OnTap?.Invoke(touchStartPosition);
            lastTapTime = Time.time;
            if (debugMode)
                Debug.Log("[InputManager] Tap");
        }
    }

    /// <summary>
    /// 스와이프 방향 문자열로 반환
    /// </summary>
    private string GetSwipeDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? "Right" : "Left";
        }
        else
        {
            return direction.y > 0 ? "Up" : "Down";
        }
    }

    /// <summary>
    /// 현재 터치 위치 반환 (정규화된 좌표)
    /// </summary>
    public Vector2 GetTouchPosition()
    {
        return new Vector2(
            touchCurrentPosition.x / Screen.width,
            touchCurrentPosition.y / Screen.height
        );
    }

    /// <summary>
    /// 터치 중인지 반환
    /// </summary>
    public bool IsTouching()
    {
        return isTouching;
    }

    /// <summary>
    /// 특정 UI 요소가 터치되었는지 확인
    /// </summary>
    public bool IsPointerOverUIElement()
    {
        return Input.GetMouseButton(0) && IsPointerOverUIElement(Input.mousePosition);
    }

    /// <summary>
    /// 포인터가 UI 위에 있는지 확인
    /// </summary>
    private bool IsPointerOverUIElement(Vector3 screenPosition)
    {
        UnityEngine.EventSystems.PointerEventData eventData = 
            new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        eventData.position = screenPosition;

        var results = new System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }
}