using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 커리어 모드를 관리하는 클래스
/// 리그 진행, 시즌 관리
/// </summary>
public class CareerManager : MonoBehaviour
{
    public static CareerManager Instance { get; private set; }

    [SerializeField] private bool debugMode = false;

    private TeamData playerTeam;
    private List<TeamData> allTeams = new List<TeamData>();
    private int currentSeason = 1;
    private int currentWeek = 1;
    private const int TOTAL_WEEKS = 38;
    private const int TEAMS_IN_LEAGUE = 20;

    private bool careerActive = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 새 커리어 시작
    /// </summary>
    public void StartNewCareer(TeamData selectedTeam)
    {
        playerTeam = selectedTeam;
        currentSeason = 1;
        currentWeek = 1;
        careerActive = true;

        InitializeLeague();

        if (debugMode)
        {
            Debug.Log($"[CareerManager] New career started with {playerTeam.TeamName}");
        }
    }

    /// <summary>
    /// 리그 초기화
    /// </summary>
    private void InitializeLeague()
    {
        allTeams.Clear();
        allTeams.Add(playerTeam);

        // 다른 팀들 생성 (임시)
        string[] teamNames = 
        {
            "Manchester United", "Manchester City", "Liverpool", "Chelsea",
            "Arsenal", "Tottenham", "Brighton", "Aston Villa",
            "Newcastle", "Fulham", "Brentford", "Wolves",
            "West Ham", "Crystal Palace", "Everton", "Leicester",
            "Leeds", "Southampton", "Nottingham", "Luton"
        };

        for (int i = 0; i < TEAMS_IN_LEAGUE - 1; i++)
        {
            if (i < teamNames.Length && teamNames[i] != playerTeam.TeamName)
            {
                TeamData newTeam = new TeamData(i, teamNames[i], "City" + i);
                GenerateSquad(newTeam);
                allTeams.Add(newTeam);
            }
        }

        if (debugMode)
        {
            Debug.Log($"[CareerManager] League initialized with {allTeams.Count} teams");
        }
    }

    /// <summary>
    /// 팀의 스쿼드 생성
    /// </summary>
    private void GenerateSquad(TeamData team)
    {
        string[] firstNames = { "John", "James", "Michael", "David", "Carlos", "Sergio", "Victor", "Marcus" };
        string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Silva", "Garcia", "Martinez", "Rodriguez" };
        string[] positions = { "GK", "CB", "LB", "RB", "CM", "LM", "RM", "ST" };

        for (int i = 0; i < 23; i++)
        {
            string name = firstNames[Random.Range(0, firstNames.Length)] + " " + 
                         lastNames[Random.Range(0, lastNames.Length)];
            string position = positions[Random.Range(0, positions.Length)];
            int age = Random.Range(20, 35);

            PlayerData player = new PlayerData(i, name, age, position, "Country");
            team.AddPlayer(player);
        }
    }

    /// <summary>
    /// 다음 주 진행
    /// </summary>
    public void ProgressToNextWeek()
    {
        currentWeek++;

        if (currentWeek > TOTAL_WEEKS)
        {
            EndSeason();
        }

        if (debugMode)
        {
            Debug.Log($"[CareerManager] Week {currentWeek}/{TOTAL_WEEKS}");
        }
    }

    /// <summary>
    /// 시즌 종료
    /// </summary>
    private void EndSeason()
    {
        currentSeason++;
        currentWeek = 1;

        // 리그 테이블 정렬 (포인트 기준)
        SortLeagueTable();

        if (debugMode)
        {
            Debug.Log($"[CareerManager] Season {currentSeason - 1} ended. New season started!");
            PrintLeagueTable();
        }
    }

    /// <summary>
    /// 리그 테이블 정렬
    /// </summary>
    private void SortLeagueTable()
    {
        allTeams.Sort((a, b) =>
        {
            int pointsDiff = b.GetTotalPoints().CompareTo(a.GetTotalPoints());
            if (pointsDiff != 0)
                return pointsDiff;

            int goalDiffDiff = b.GetGoalDifference().CompareTo(a.GetGoalDifference());
            if (goalDiffDiff != 0)
                return goalDiffDiff;

            return b.GoalsFor.CompareTo(a.GoalsFor);
        });
    }

    /// <summary>
    /// 리그 테이블 출력
    /// </summary>
    private void PrintLeagueTable()
    {
        Debug.Log("=== League Table ===");
        for (int i = 0; i < allTeams.Count; i++)
        {
            Debug.Log($"{i + 1}. {allTeams[i].ToString()}");
        }
    }

    /// <summary>
    /// 현재 플레이어 팀 반환
    /// </summary>
    public TeamData GetPlayerTeam()
    {
        return playerTeam;
    }

    /// <summary>
    /// 모든 팀 반환
    /// </summary>
    public List<TeamData> GetAllTeams()
    {
        return allTeams;
    }

    /// <summary>
    /// 현재 시즌 반환
    /// </summary>
    public int GetCurrentSeason()
    {
        return currentSeason;
    }

    /// <summary>
    /// 현재 주 반환
    /// </summary>
    public int GetCurrentWeek()
    {
        return currentWeek;
    }

    /// <summary>
    /// 커리어가 진행 중인지 반환
    /// </summary>
    public bool IsCareerActive()
    {
        return careerActive;
    }

    /// <summary>
    /// 플레이어 팀의 리그 순위 반환
    /// </summary>
    public int GetPlayerTeamPosition()
    {
        for (int i = 0; i < allTeams.Count; i++)
        {
            if (allTeams[i].Id == playerTeam.Id)
            {
                return i + 1;
            }
        }
        return -1;
    }
}