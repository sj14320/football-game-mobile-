using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 메인 메뉴 UI 관리
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;

    private void Start()
    {
        if (debugMode)
        {
            Debug.Log("[MainMenu] Main Menu loaded");
        }
    }

    /// <summary>
    /// 새 커리어 시작 버튼
    /// </summary>
    public void OnNewCareerButtonClicked()
    {
        if (debugMode)
        {
            Debug.Log("[MainMenu] New Career button clicked");
        }

        // 팀 선택 화면으로 이동
        SceneManager.LoadScene("TeamSelection");
    }

    /// <summary>
    /// 빠른 경기 시작 버튼
    /// </summary>
    public void OnQuickMatchButtonClicked()
    {
        if (debugMode)
        {
            Debug.Log("[MainMenu] Quick Match button clicked");
        }

        // 테스트용 매치 시작
        StartTestMatch();
    }

    /// <summary>
    /// 설정 버튼
    /// </summary>
    public void OnSettingsButtonClicked()
    {
        if (debugMode)
        {
            Debug.Log("[MainMenu] Settings button clicked");
        }

        GameManager.Instance.ChangeGameState(GameState.Settings);
    }

    /// <summary>
    /// 게임 종료 버튼
    /// </summary>
    public void OnQuitButtonClicked()
    {
        if (debugMode)
        {
            Debug.Log("[MainMenu] Quit button clicked");
        }

        GameManager.Instance.QuitGame();
    }

    /// <summary>
    /// 테스트 매치 시작 (개발용)
    /// </summary>
    private void StartTestMatch()
    {
        // 테스트 팀 생성
        TeamData homeTeam = new TeamData(1, "Test Home", "City A");
        TeamData awayTeam = new TeamData(2, "Test Away", "City B");

        // 스쿼드 생성
        GenerateTestSquad(homeTeam);
        GenerateTestSquad(awayTeam);

        // 매치 데이터 생성
        MatchData matchData = new MatchData
        {
            MatchId = 1,
            HomeTeam = homeTeam,
            AwayTeam = awayTeam,
            HomeScore = 0,
            AwayScore = 0
        };

        // 게임 매니저에 설정
        GameManager.Instance.SetMatchData(matchData);
        GameManager.Instance.ChangeGameState(GameState.MatchPlaying);

        // 매치 씬으로 이동
        SceneManager.LoadScene("Match");
    }

    /// <summary>
    /// 테스트 스쿼드 생성
    /// </summary>
    private void GenerateTestSquad(TeamData team)
    {
        string[] positions = { "GK", "CB", "LB", "RB", "CM", "LM", "RM", "ST" };
        string[] firstNames = { "John", "James", "Michael", "David", "Carlos", "Sergio" };
        string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Silva", "Garcia" };

        for (int i = 0; i < 11; i++)
        {
            string name = firstNames[Random.Range(0, firstNames.Length)] + " " + 
                         lastNames[Random.Range(0, lastNames.Length)];
            string position = positions[Random.Range(0, positions.Length)];
            int age = Random.Range(20, 35);

            PlayerData player = new PlayerData(i, name, age, position, "Country");
            team.AddPlayer(player);
        }
    }
}