using UnityEngine;
using TMPro;

/// <summary>
/// 매치 중 HUD (Head Up Display) 관리
/// </summary>
public class MatchHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI homeTeamNameText;
    [SerializeField] private TextMeshProUGUI awayTeamNameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI possessionText;
    [SerializeField] private TextMeshProUGUI shotsText;

    [SerializeField] private bool debugMode = false;

    private MatchManager matchManager;
    private int homeScore = 0;
    private int awayScore = 0;

    private void Start()
    {
        matchManager = MatchManager.Instance;

        if (matchManager == null)
        {
            Debug.LogError("[MatchHUD] MatchManager not found!");
            return;
        }

        // 이벤트 구독
        matchManager.OnScoreChanged += UpdateScore;
        matchManager.OnMatchTimeUpdated += UpdateTime;
        matchManager.OnHalfTime += OnHalfTime;
        matchManager.OnMatchEnd += OnMatchEnd;

        // 초기 UI 설정
        UpdateUI();

        if (debugMode)
        {
            Debug.Log("[MatchHUD] Initialized");
        }
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateUI()
    {
        MatchData matchData = matchManager.GetMatchData();

        if (matchData == null)
            return;

        if (homeTeamNameText != null)
            homeTeamNameText.text = matchData.HomeTeam.TeamName;

        if (awayTeamNameText != null)
            awayTeamNameText.text = matchData.AwayTeam.TeamName;

        UpdateScore(homeScore, awayScore);
    }

    /// <summary>
    /// 스코어 업데이트
    /// </summary>
    private void UpdateScore(int home, int away)
    {
        homeScore = home;
        awayScore = away;

        if (scoreText != null)
        {
            scoreText.text = $"{home} - {away}";
        }

        if (debugMode)
        {
            Debug.Log($"[MatchHUD] Score Updated: {home} - {away}");
        }
    }

    /// <summary>
    /// 시간 업데이트
    /// </summary>
    private void UpdateTime(float currentTime)
    {
        if (timeText != null)
        {
            timeText.text = matchManager.GetMatchTimeFormatted();
        }
    }

    /// <summary>
    /// 하프타임 처리
    /// </summary>
    private void OnHalfTime()
    {
        if (debugMode)
        {
            Debug.Log("[MatchHUD] Half Time!");
        }

        // 하프타임 UI 표시 (선택사항)
    }

    /// <summary>
    /// 매치 종료 처리
    /// </summary>
    private void OnMatchEnd()
    {
        if (debugMode)
        {
            Debug.Log($"[MatchHUD] Match Ended! Final Score: {homeScore} - {awayScore}");
        }

        // 매치 종료 UI 표시 (선택사항)
        if (scoreText != null)
        {
            scoreText.text = $"FINAL: {homeScore} - {awayScore}";
        }
    }

    /// <summary>
    /// 일시정지 버튼 클릭 처리
    /// </summary>
    public void OnPauseButtonClicked()
    {
        GameManager.Instance.PauseGame();
        if (debugMode)
        {
            Debug.Log("[MatchHUD] Pause button clicked");
        }
    }

    /// <summary>
    /// 재개 버튼 클릭 처리
    /// </summary>
    public void OnResumeButtonClicked()
    {
        GameManager.Instance.ResumeGame();
        if (debugMode)
        {
            Debug.Log("[MatchHUD] Resume button clicked");
        }
    }

    private void OnDestroy()
    {
        if (matchManager != null)
        {
            matchManager.OnScoreChanged -= UpdateScore;
            matchManager.OnMatchTimeUpdated -= UpdateTime;
            matchManager.OnHalfTime -= OnHalfTime;
            matchManager.OnMatchEnd -= OnMatchEnd;
        }
    }
}