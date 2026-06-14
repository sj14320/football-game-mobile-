using UnityEngine;
using System;

/// <summary>
/// 게임 전체를 관리하는 매니저 클래스
/// 싱글톤 패턴 사용
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool debugMode = false;

    private GameState currentGameState = GameState.Menu;
    private MatchData currentMatchData;
    private PlayerData currentPlayerData;

    public event Action<GameState> OnGameStateChanged;
    public event Action OnGamePaused;
    public event Action OnGameResumed;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        if (debugMode)
        {
            Debug.Log("[GameManager] Initialized");
        }
    }

    private void Update()
    {
        // ESC 키로 일시정지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// 게임 상태 변경
    /// </summary>
    public void ChangeGameState(GameState newState)
    {
        if (currentGameState == newState)
            return;

        currentGameState = newState;
        OnGameStateChanged?.Invoke(newState);

        if (debugMode)
        {
            Debug.Log($"[GameManager] Game State Changed: {newState}");
        }
    }

    /// <summary>
    /// 게임 일시정지
    /// </summary>
    public void PauseGame()
    {
        if (isPaused)
            return;

        isPaused = true;
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();

        if (debugMode)
        {
            Debug.Log("[GameManager] Game Paused");
        }
    }

    /// <summary>
    /// 게임 재개
    /// </summary>
    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;
        Time.timeScale = 1f;
        OnGameResumed?.Invoke();

        if (debugMode)
        {
            Debug.Log("[GameManager] Game Resumed");
        }
    }

    /// <summary>
    /// 게임 일시정지 토글
    /// </summary>
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    /// <summary>
    /// 현재 게임 상태 반환
    /// </summary>
    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }

    /// <summary>
    /// 게임이 일시정지 중인지 반환
    /// </summary>
    public bool IsPaused()
    {
        return isPaused;
    }

    /// <summary>
    /// 매치 데이터 설정
    /// </summary>
    public void SetMatchData(MatchData matchData)
    {
        currentMatchData = matchData;
    }

    /// <summary>
    /// 현재 매치 데이터 반환
    /// </summary>
    public MatchData GetCurrentMatchData()
    {
        return currentMatchData;
    }

    /// <summary>
    /// 플레이어 데이터 설정
    /// </summary>
    public void SetPlayerData(PlayerData playerData)
    {
        currentPlayerData = playerData;
    }

    /// <summary>
    /// 현재 플레이어 데이터 반환
    /// </summary>
    public PlayerData GetCurrentPlayerData()
    {
        return currentPlayerData;
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    public void QuitGame()
    {
        if (debugMode)
        {
            Debug.Log("[GameManager] Game Quit");
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

/// <summary>
/// 게임 상태 열거형
/// </summary>
public enum GameState
{
    Menu,           // 메인 메뉴
    Career,         // 커리어 모드
    MatchPrep,      // 매치 준비
    MatchPlaying,   // 매치 진행 중
    MatchPaused,    // 매치 일시정지
    MatchEnd,       // 매치 종료
    Settings,       // 설정
    Loading         // 로딩 중
}