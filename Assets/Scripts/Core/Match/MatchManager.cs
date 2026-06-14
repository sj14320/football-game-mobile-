using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 매치를 관리하는 클래스
/// 스코어, 시간, 이벤트 처리
/// </summary>
public class MatchManager : MonoBehaviour
{
    public static MatchManager Instance { get; private set; }

    [SerializeField] private float matchDuration = 5400f; // 90분 (초)
    [SerializeField] private float halfTimeDuration = 300f; // 하프타임 (초)
    [SerializeField] private bool debugMode = false;

    private MatchData matchData;
    private float currentMatchTime = 0f;
    private float matchTimer = 0f;
    private bool isMatchActive = false;
    private bool isHalfTime = false;

    public event Action<int, int> OnScoreChanged;
    public event Action<float> OnMatchTimeUpdated;
    public event Action OnHalfTime;
    public event Action OnMatchEnd;

    private List<MatchEvent> matchEvents = new List<MatchEvent>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (debugMode)
        {
            Debug.Log("[MatchManager] Initialized");
        }
    }

    private void Update()
    {
        if (!isMatchActive || GameManager.Instance.IsPaused())
            return;

        UpdateMatchTime();
    }

    /// <summary>
    /// 매치 시작
    /// </summary>
    public void StartMatch(MatchData data)
    {
        matchData = data;
        currentMatchTime = 0f;
        matchTimer = 0f;
        isMatchActive = true;
        isHalfTime = false;

        if (debugMode)
        {
            Debug.Log($"[MatchManager] Match Started: {matchData.HomeTeam.TeamName} vs {matchData.AwayTeam.TeamName}");
        }
    }

    /// <summary>
    /// 매치 시간 업데이트
    /// </summary>
    private void UpdateMatchTime()
    {
        matchTimer += Time.deltaTime;
        currentMatchTime = matchTimer;

        OnMatchTimeUpdated?.Invoke(currentMatchTime);

        // 45분 (하프타임)
        if (currentMatchTime >= matchDuration * 0.5f && !isHalfTime)
        {
            isHalfTime = true;
            OnHalfTime?.Invoke();

            if (debugMode)
            {
                Debug.Log("[MatchManager] Half Time!");
            }
        }

        // 90분 (매치 종료)
        if (currentMatchTime >= matchDuration)
        {
            EndMatch();
        }
    }

    /// <summary>
    /// 매치 종료
    /// </summary>
    public void EndMatch()
    {
        isMatchActive = false;
        OnMatchEnd?.Invoke();

        if (debugMode)
        {
            Debug.Log($"[MatchManager] Match Ended. Score: {matchData.HomeScore} - {matchData.AwayScore}");
        }
    }

    /// <summary>
    /// 현재 매치 시간 반환 (분:초)
    /// </summary>
    public string GetMatchTimeFormatted()
    {
        int minutes = (int)(currentMatchTime / 60);
        int seconds = (int)(currentMatchTime % 60);
        return $"{minutes:D2}:{seconds:D2}";
    }

    /// <summary>
    /// 홈팀 골
    /// </summary>
    public void ScoreGoalHome()
    {
        matchData.HomeScore++;
        RecordMatchEvent("Goal", matchData.HomeTeam.TeamName);
        OnScoreChanged?.Invoke(matchData.HomeScore, matchData.AwayScore);

        if (debugMode)
        {
            Debug.Log($"[MatchManager] GOAL! {matchData.HomeTeam.TeamName} Score: {matchData.HomeScore}");
        }
    }

    /// <summary>
    /// 어웨이팀 골
    /// </summary>
    public void ScoreGoalAway()
    {
        matchData.AwayScore++;
        RecordMatchEvent("Goal", matchData.AwayTeam.TeamName);
        OnScoreChanged?.Invoke(matchData.HomeScore, matchData.AwayScore);

        if (debugMode)
        {
            Debug.Log($"[MatchManager] GOAL! {matchData.AwayTeam.TeamName} Score: {matchData.AwayScore}");
        }
    }

    /// <summary>
    /// 매치 이벤트 기록
    /// </summary>
    private void RecordMatchEvent(string eventType, string playerName)
    {
        MatchEvent evt = new MatchEvent
        {
            EventType = eventType,
            PlayerName = playerName,
            Time = currentMatchTime,
            TimeFormatted = GetMatchTimeFormatted()
        };

        matchEvents.Add(evt);
    }

    /// <summary>
    /// 현재 스코어 반환
    /// </summary>
    public (int home, int away) GetScore()
    {
        return (matchData.HomeScore, matchData.AwayScore);
    }

    /// <summary>
    /// 현재 매치 시간 반환
    /// </summary>
    public float GetCurrentMatchTime()
    {
        return currentMatchTime;
    }

    /// <summary>
    /// 매치가 진행 중인지 반환
    /// </summary>
    public bool IsMatchActive()
    {
        return isMatchActive;
    }

    /// <summary>
    /// 하프타임인지 반환
    /// </summary>
    public bool IsHalfTime()
    {
        return isHalfTime;
    }

    /// <summary>
    /// 모든 매치 이벤트 반환
    /// </summary>
    public List<MatchEvent> GetMatchEvents()
    {
        return matchEvents;
    }

    /// <summary>
    /// 매치 데이터 반환
    /// </summary>
    public MatchData GetMatchData()
    {
        return matchData;
    }
}

/// <summary>
/// 매치 이벤트 클래스
/// </summary>
[System.Serializable]
public class MatchEvent
{
    public string EventType;      // Goal, Card, Injury, etc.
    public string PlayerName;
    public float Time;
    public string TimeFormatted;
}

/// <summary>
/// 매치 데이터 클래스
/// </summary>
[System.Serializable]
public class MatchData
{
    public int MatchId;
    public TeamData HomeTeam;
    public TeamData AwayTeam;
    public int HomeScore = 0;
    public int AwayScore = 0;
    public float MatchTime = 0f;
    public List<MatchEvent> Events = new List<MatchEvent>();
}