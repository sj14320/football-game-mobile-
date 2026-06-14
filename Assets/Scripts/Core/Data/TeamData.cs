using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 팀 데이터 클래스
/// 팀의 선수, 포메이션, 성적 관리
/// </summary>
[System.Serializable]
public class TeamData
{
    [SerializeField] private int id;
    [SerializeField] private string teamName;
    [SerializeField] private string city;
    [SerializeField] private List<PlayerData> squad = new List<PlayerData>();
    
    // 팀 설정
    [SerializeField] private string formation = "4-3-3";
    [SerializeField] private int budget = 10000000;

    // 리그 성적
    [SerializeField] private int wins = 0;
    [SerializeField] private int draws = 0;
    [SerializeField] private int losses = 0;
    [SerializeField] private int goalsFor = 0;
    [SerializeField] private int goalsAgainst = 0;

    public int Id => id;
    public string TeamName => teamName;
    public string City => city;
    public List<PlayerData> Squad => squad;
    public string Formation => formation;
    public int Budget => budget;
    public int Wins => wins;
    public int Draws => draws;
    public int Losses => losses;
    public int GoalsFor => goalsFor;
    public int GoalsAgainst => goalsAgainst;

    /// <summary>
    /// 팀 생성자
    /// </summary>
    public TeamData(int id, string name, string city)
    {
        this.id = id;
        this.teamName = name;
        this.city = city;
    }

    /// <summary>
    /// 선수 추가
    /// </summary>
    public void AddPlayer(PlayerData player)
    {
        if (squad.Count < 23) // 최대 23명까지만 추가 가능
        {
            squad.Add(player);
        }
        else
        {
            Debug.LogWarning($"[TeamData] {teamName} 스쿼드가 가득 찼습니다!");
        }
    }

    /// <summary>
    /// 선수 제거
    /// </summary>
    public void RemovePlayer(PlayerData player)
    {
        squad.Remove(player);
    }

    /// <summary>
    /// 포지션별 선수 반환
    /// </summary>
    public List<PlayerData> GetPlayersByPosition(string position)
    {
        List<PlayerData> players = new List<PlayerData>();
        foreach (var player in squad)
        {
            if (player.Position.ToLower() == position.ToLower())
            {
                players.Add(player);
            }
        }
        return players;
    }

    /// <summary>
    /// 포메이션 변경
    /// </summary>
    public void SetFormation(string newFormation)
    {
        formation = newFormation;
        Debug.Log($"[TeamData] {teamName} 포메이션 변경: {newFormation}");
    }

    /// <summary>
    /// 경기 결과 기록 (승리)
    /// </summary>
    public void RecordWin(int scored, int conceded)
    {
        wins++;
        goalsFor += scored;
        goalsAgainst += conceded;
    }

    /// <summary>
    /// 경기 결과 기록 (무승부)
    /// </summary>
    public void RecordDraw(int scored, int conceded)
    {
        draws++;
        goalsFor += scored;
        goalsAgainst += conceded;
    }

    /// <summary>
    /// 경기 결과 기록 (패배)
    /// </summary>
    public void RecordLoss(int scored, int conceded)
    {
        losses++;
        goalsFor += scored;
        goalsAgainst += conceded;
    }

    /// <summary>
    /// 전체 경기수 반환
    /// </summary>
    public int GetTotalMatches()
    {
        return wins + draws + losses;
    }

    /// <summary>
    /// 총 포인트 반환 (승리 3점, 무승부 1점)
    /// </summary>
    public int GetTotalPoints()
    {
        return (wins * 3) + draws;
    }

    /// <summary>
    /// 골 차이 반환
    /// </summary>
    public int GetGoalDifference()
    {
        return goalsFor - goalsAgainst;
    }

    /// <summary>
    /// 평균 능력치 반환
    /// </summary>
    public float GetTeamOverallRating()
    {
        if (squad.Count == 0)
            return 0f;

        float totalRating = 0f;
        foreach (var player in squad)
        {
            totalRating += player.GetOverallRating();
        }

        return totalRating / squad.Count;
    }

    /// <summary>
    /// 최고의 선수 반환
    /// </summary>
    public PlayerData GetBestPlayer()
    {
        if (squad.Count == 0)
            return null;

        PlayerData bestPlayer = squad[0];
        foreach (var player in squad)
        {
            if (player.GetOverallRating() > bestPlayer.GetOverallRating())
            {
                bestPlayer = player;
            }
        }

        return bestPlayer;
    }

    /// <summary>
    /// 리그 테이블 정보 출력
    /// </summary>
    public override string ToString()
    {
        int totalMatches = GetTotalMatches();
        int points = GetTotalPoints();
        int goalDiff = GetGoalDifference();

        return $"{teamName}: {points}pt | " +
               $"{wins}W-{draws}D-{losses}L | " +
               $"GF: {goalsFor} GA: {goalsAgainst} | " +
               $"GD: {goalDiff:+0;-0;0} | " +
               $"OVR: {GetTeamOverallRating():F1}";
    }
}