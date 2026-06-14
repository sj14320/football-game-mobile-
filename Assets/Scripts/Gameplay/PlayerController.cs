using UnityEngine;

/// <summary>
/// 플레이어 캐릭터 컨트롤러
/// 입력 처리 및 플레이어 이동
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float passForce = 20f;
    [SerializeField] private float shootForce = 30f;
    [SerializeField] private bool debugMode = false;

    private Rigidbody rb;
    private InputManager inputManager;
    private bool hasControl = true;
    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = InputManager.Instance;

        if (inputManager == null)
        {
            Debug.LogError("[PlayerController] InputManager not found!");
            return;
        }

        // 입력 이벤트 구독
        inputManager.OnTap += OnTap;
        inputManager.OnDoubleTap += OnDoubleTap;
        inputManager.OnSwipe += OnSwipe;

        if (debugMode)
        {
            Debug.Log("[PlayerController] Initialized");
        }
    }

    private void Update()
    {
        if (!hasControl || GameManager.Instance.IsPaused())
            return;

        HandleMovement();
    }

    /// <summary>
    /// 플레이어 이동 처리
    /// </summary>
    private void HandleMovement()
    {
        // 키보드 입력 (에디터 테스트용)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (rb != null && moveDirection.magnitude > 0)
        {
            rb.velocity = moveDirection * moveSpeed;

            // 플레이어 회전
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 탭 처리 (패스)
    /// </summary>
    private void OnTap(Vector2 position)
    {
        if (!hasControl)
            return;

        PerformPass();

        if (debugMode)
        {
            Debug.Log("[PlayerController] Pass performed");
        }
    }

    /// <summary>
    /// 더블 탭 처리 (슈팅)
    /// </summary>
    private void OnDoubleTap(Vector2 position)
    {
        if (!hasControl)
            return;

        PerformShot();

        if (debugMode)
        {
            Debug.Log("[PlayerController] Shot performed");
        }
    }

    /// <summary>
    /// 스와이프 처리 (드리블)
    /// </summary>
    private void OnSwipe(Vector2 direction)
    {
        if (!hasControl)
            return;

        PerformDribble(direction);

        if (debugMode)
        {
            Debug.Log($"[PlayerController] Dribble in direction: {direction}");
        }
    }

    /// <summary>
    /// 패스 수행
    /// </summary>
    private void PerformPass()
    {
        // 패스 로직 구현
        if (rb != null)
        {
            rb.velocity *= 0.8f; // 패스 시 속도 감소
        }
    }

    /// <summary>
    /// 슈팅 수행
    /// </summary>
    private void PerformShot()
    {
        // 슈팅 로직 구현
        Vector3 shootDirection = transform.forward;
        
        if (rb != null)
        {
            rb.velocity = shootDirection * shootForce;
        }

        if (debugMode)
        {
            Debug.Log($"[PlayerController] Shooting in direction: {shootDirection}");
        }
    }

    /// <summary>
    /// 드리블 수행
    /// </summary>
    private void PerformDribble(Vector2 direction)
    {
        Vector3 dribbleDirection = new Vector3(direction.x, 0f, direction.y).normalized;
        
        if (rb != null)
        {
            rb.velocity = dribbleDirection * moveSpeed * 0.7f;
        }

        // 플레이어 회전
        if (dribbleDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dribbleDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 플레이어 컨트롤 활성화/비활성화
    /// </summary>
    public void SetControl(bool active)
    {
        hasControl = active;
    }

    /// <summary>
    /// 플레이어가 컨트롤 가능한지 반환
    /// </summary>
    public bool HasControl()
    {
        return hasControl;
    }

    private void OnDestroy()
    {
        if (inputManager != null)
        {
            inputManager.OnTap -= OnTap;
            inputManager.OnDoubleTap -= OnDoubleTap;
            inputManager.OnSwipe -= OnSwipe;
        }
    }
}