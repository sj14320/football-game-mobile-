# Development Guide - Football Game Mobile

## 프로젝트 설정

### 필수 요구사항
- Unity 2022 LTS 이상
- C# 9.0 이상
- Android SDK (모바일 빌드용)
- iOS SDK (iOS 빌드용)

### 프로젝트 초기화

1. Clone the repository
```bash
git clone https://github.com/sj14320/football-game-mobile.git
cd football-game-mobile
```

2. Unity 프로젝트 열기
- Unity Hub에서 "Add project from disk" 선택
- football-game-mobile 폴더 선택

3. 초기 설정
- Unity 버전 확인 (2022 LTS 이상)
- Import TextMesh Pro essentials
- Build Settings에서 Platform 선택

## 아키텍처 개요

### 1. Core Systems

#### GameManager
- 게임 전체 상태 관리
- 씬 전환
- 저장/로드 기능

#### MatchManager
- 매치 진행
- 스코어 관리
- 타이머

#### PlayerManager
- 플레이어 데이터 관리
- 선수 능력치
- 장비 관리

#### TeamManager
- 팀 구성
- 포메이션 설정
- 선수 배치

### 2. Gameplay Systems

#### PlayerController
- 플레이어 입력 처리
- 캐릭터 이동
- 패스/슈팅 로직

#### AIController
- AI 상대팀 행동
- 전술 시스템
- 의사 결정

#### BallPhysics
- 공 물리 시뮬레이션
- 충돌 처리
- 궤적 계산

### 3. Career System

#### CareerManager
- 커리어 진행
- 리그 관리
- 시즌 시스템

#### PlayerDevelopment
- 선수 성장
- 경험치 시스템
- 능력치 업그레이드

#### LeagueSystem
- 리그 테이블
- 매치 스케줄
- 결과 관리

### 4. UI System

#### MainMenu
- 게임 시작
- 계정 설정
- 설정

#### MatchHUD
- 실시간 스코어
- 플레이어 정보
- 시간 표시

#### CareerUI
- 팀 관리
- 선수 선택
- 리그 정보

## 데이터 구조

### Player Data
```csharp
public class PlayerData
{
    public int Id;
    public string Name;
    public int Age;
    public float Pace;
    public float Shooting;
    public float Passing;
    public float Dribbling;
    public float Defense;
    public float Physical;
    public string Position;
    public int Level;
    public int Experience;
}
```

### Team Data
```csharp
public class TeamData
{
    public int Id;
    public string Name;
    public List<PlayerData> Squad;
    public int Budget;
    public string Formation;
    public int Wins;
    public int Draws;
    public int Losses;
}
```

### Match Data
```csharp
public class MatchData
{
    public int MatchId;
    public TeamData HomeTeam;
    public TeamData AwayTeam;
    public int HomeScore;
    public int AwayScore;
    public float MatchTime;
    public List<MatchEvent> Events;
}
```

## 개발 워크플로우

### 1. Feature Development

```bash
# 새 기능 브랜치 생성
git checkout -b feature/your-feature-name

# 코드 작성 및 테스트
# ...

# 커밋
git add .
git commit -m "Add your feature description"

# PR 생성
git push origin feature/your-feature-name
```

### 2. Code Style

- C# Coding Standards 준수
- PascalCase for classes and public methods
- camelCase for private fields and local variables
- 모든 public 메서드에 XML 주석 작성

### 3. Testing

- Unit tests for core systems
- Integration tests for gameplay
- Mobile device testing

## 디버깅

### 콘솔 명령어
```csharp
// FPS 표시
Debug.Log($"FPS: {1f / Time.deltaTime}");

// 메모리 사용량
Debug.Log($"Memory: {System.GC.GetTotalMemory(false) / 1024 / 1024} MB");
```

### Profiler 사용
- Window > Analysis > Profiler
- CPU, Memory, GPU 성능 분석

## 빌드 및 배포

### Android 빌드
1. File > Build Settings
2. Platform: Android 선택
3. Player Settings에서 패키지명 설정
4. Build

### iOS 빌드
1. File > Build Settings
2. Platform: iOS 선택
3. Build (Xcode에서 최종 빌드)

## 성능 최적화

### 1. Graphics
- LOD (Level of Detail) 시스템
- Draw call 최소화
- Texture 최적화

### 2. Physics
- Fixed Timestep 조정
- 불필요한 Collider 제거

### 3. Memory
- Object pooling 사용
- 텍스처 압축
- 불필요한 리소스 언로드

## 트러블슈팅

### 문제: 앱이 느림
**해결책:**
1. Profiler로 병목 지점 찾기
2. Draw call 최소화
3. Physics 최적화
4. 메모리 누수 확인

### 문제: 터치 입력이 반응 없음
**해결책:**
1. EventSystem 확인
2. Canvas 설정 확인
3. Input Manager 재설정

### 문제: AI가 움직이지 않음
**해결책:**
1. NavMesh 생성 확인
2. AIController 활성화 확인
3. 콘솔에서 오류 메시지 확인

## 리소스

- [Unity Manual](https://docs.unity3d.com/Manual/)
- [Unity API Reference](https://docs.unity3d.com/ScriptReference/)
- [Best Practices](https://docs.unity3d.com/Manual/BestPractices.html)

---

**더 많은 도움이 필요하신가요? Issues를 등록해주세요!**