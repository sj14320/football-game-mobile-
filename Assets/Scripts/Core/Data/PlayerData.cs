using UnityEngine;

/// <summary>
/// 플레이어 데이터 클래스
/// 선수의 능력치 및 정보를 관리
/// </summary>
[System.Serializable]
public class PlayerData
{
    [SerializeField] private int id;
    [SerializeField] private string playerName;
    [SerializeField] private int age;
    [SerializeField] private string position;
    [SerializeField] private string nationality;
    
    // 능력치 (0-100)
    [SerializeField] private float pace;
    [SerializeField] private float shooting;
    [SerializeField] private float passing;
    [SerializeField] private float dribbling;
    [SerializeField] private float defense;
    [SerializeField] private float physical;

    // 게임 진행 정보
    [SerializeField] private int level = 1;
    [SerializeField] private int experience = 0;
    [SerializeField] private int experienceToNextLevel = 1000;
    [SerializeField] private float matchesPlayed = 0;
    [SerializeField] private int goalsScored = 0;
    [SerializeField] private int assists = 0;

    // 계약 정보
    [SerializeField] private int salary;
    [SerializeField] private int contractYears;

    public int Id => id;
    public string PlayerName => playerName;
    public int Age => age;
    public string Position => position;
    public string Nationality => nationality;
    public float Pace => pace;
    public float Shooting => shooting;
    public float Passing => passing;
    public float Dribbling => dribbling;
    public float Defense => defense;
    public float Physical => physical;
    public int Level => level;
    public int Experience => experience;
    public float MatchesPlayed => matchesPlayed;
    public int GoalsScored => goalsScored;
    public int Assists => assists;

    /// <summary>
    /// 플레이어 생성자
    /// </summary>
    public PlayerData(int id, string name, int age, string position, string nationality)
    {
        this.id = id;
        this.playerName = name;
        this.age = age;
        this.position = position;
        this.nationality = nationality;
        
        // 기본 능력치 설정 (포지션에 따라)
        InitializeAbilities(position);
    }

    /// <summary>
    /// 포지션에 따른 기본 능력치 초기화
    /// </summary>
    private void InitializeAbilities(string position)
    {
        // 기본 능력치
        pace = Random.Range(70, 90);
        shooting = Random.Range(70, 90);
        passing = Random.Range(70, 90);
        dribbling = Random.Range(70, 90);
        defense = Random.Range(70, 90);
        physical = Random.Range(70, 90);

        // 포지션별 능력치 조정
        switch (position.ToLower())
        {
            case "st": // Striker
                shooting = Mathf.Min(shooting + 15, 100);
                pace = Mathf.Min(pace + 10, 100);
                dribbling = Mathf.Min(dribbling + 10, 100);
                defense = Mathf.Max(defense - 15, 40);
                break;

            case "cm": // Central Midfielder
                passing = Mathf.Min(passing + 15, 100);
                dribbling = Mathf.Min(dribbling + 10, 100);
                defense = Mathf.Min(defense + 10, 100);
                break;

            case "cb": // Center Back
                defense = Mathf.Min(defense + 20, 100);
                physical = Mathf.Min(physical + 15, 100);
                pace = Mathf.Max(pace - 10, 40);
                shooting = Mathf.Max(shooting - 15, 40);
                break;

            case "gk": // Goalkeeper
                defense = Mathf.Min(defense + 25, 100);
                shooting = Mathf.Max(shooting - 30, 20);
                dribbling = Mathf.Max(dribbling - 20, 30);
                pace = Mathf.Max(pace - 15, 40);
                break;

            case "lb": // Left Back
            case "rb": // Right Back
                defense = Mathf.Min(defense + 15, 100);
                pace = Mathf.Min(pace + 10, 100);
                physical = Mathf.Min(physical + 10, 100);
                break;

            case "lw": // Left Winger
            case "rw": // Right Winger
                pace = Mathf.Min(pace + 15, 100);
                dribbling = Mathf.Min(dribbling + 15, 100);
                shooting = Mathf.Min(shooting + 10, 100);
                break;
        }
    }

    /// <summary>
    /// 경험치 추가
    /// </summary>
    public void AddExperience(int exp)
    {
        experience += exp;

        // 레벨업 체크
        while (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// 레벨업
    /// </summary>
    private void LevelUp()
    {
        level++;
        experience -= experienceToNextLevel;
        experienceToNextLevel = (int)(experienceToNextLevel * 1.1f);

        // 능력치 상향
        pace = Mathf.Min(pace + Random.Range(1, 3), 100);
        shooting = Mathf.Min(shooting + Random.Range(1, 3), 100);
        passing = Mathf.Min(passing + Random.Range(1, 3), 100);
        dribbling = Mathf.Min(dribbling + Random.Range(1, 3), 100);
        defense = Mathf.Min(defense + Random.Range(1, 3), 100);
        physical = Mathf.Min(physical + Random.Range(1, 3), 100);

        Debug.Log($"[PlayerData] {playerName} leveled up to level {level}!");
    }

    /// <summary>
    /// 매치 통계 업데이트
    /// </summary>
    public void UpdateMatchStats(int goals = 0, int assists = 0)
    {
        matchesPlayed++;
        goalsScored += goals;
        this.assists += assists;

        // 매치 플레이 경험치 (평균 50-100)
        int matchExp = Random.Range(50, 100);
        if (goals > 0)
            matchExp += goals * 50;
        if (assists > 0)
            matchExp += assists * 25;

        AddExperience(matchExp);
    }

    /// <summary>
    /// 평균 능력치 계산
    /// </summary>
    public float GetOverallRating()
    {
        return (pace + shooting + passing + dribbling + defense + physical) / 6f;
    }

    /// <summary>
    /// 플레이어 정보 출력
    /// </summary>
    public override string ToString()
    {
        return $"{playerName} ({Position}) - OVR: {GetOverallRating():F1} | " +
               $"PAC: {pace:F0} | SHO: {shooting:F0} | PAS: {passing:F0} | " +
               $"DRI: {dribbling:F0} | DEF: {defense:F0} | PHY: {physical:F0}";
    }
}