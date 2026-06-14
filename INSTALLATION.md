# Installation Guide - Football Game Mobile

## 필수 요구사항

- **Unity 2022 LTS** 이상
- **C# 9.0** 이상
- **TextMesh Pro** (Unity에 기본 포함)
- Android / iOS SDK (모바일 빌드용)

## 설치 단계

### 1단계: 저장소 클론

```bash
git clone https://github.com/sj14320/football-game-mobile.git
cd football-game-mobile
```

### 2단계: Unity 프로젝트 열기

1. Unity Hub 실행
2. **Add project from disk** 클릭
3. football-game-mobile 폴더 선택
4. Unity 버전이 2022 LTS 이상인지 확인
5. 프로젝트 열기

### 3단계: 초기 설정

프로젝트가 열리면 다음을 확인하세요:

1. **TextMesh Pro 임포트**
   - Window > TextMeshPro > Import TMP Essential Resources
   - 대화상자에서 Import 클릭

2. **Scenes 폴더 구조 생성**
   ```
   Assets/Scenes/
   ├── MainMenu.unity
   ├── Match.unity
   ├── CareerMode.unity
   ├── TeamSelection.unity
   └── Settings.unity
   ```

3. **Build Settings 설정**
   - File > Build Settings
   - Platform 선택 (Android 또는 iOS)
   - Build

## 빠른 시작

### 에디터에서 테스트

1. Assets/Scenes/MainMenu 씬 열기
2. **Play** 버튼 클릭
3. "Quick Match" 버튼으로 테스트 매치 시작

### 모바일 빌드

#### Android

```bash
# Unity 에디터에서:
File > Build Settings
Platform: Android
Build
```

#### iOS

```bash
# Unity 에디터에서:
File > Build Settings
Platform: iOS
Build
# Xcode에서 최종 빌드 및 배포
```

## 프로젝트 구조

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── Match/
│   │   │   └── MatchManager.cs
│   │   └── Data/
│   │       ├── PlayerData.cs
│   │       └── TeamData.cs
│   ├── Gameplay/
│   │   ├── Input/
│   │   │   └── InputManager.cs
│   │   └── PlayerController.cs
│   ├── Career/
│   │   └── CareerManager.cs
│   └── UI/
│       ├── MainMenu.cs
│       ├── MatchHUD.cs
│       └── ...
├── Prefabs/
├── Scenes/
├── Materials/
├── Animations/
└── ...
```

## 주요 클래스

### Core Systems

| 클래스 | 설명 |
|-------|------|
| **GameManager** | 게임 전체 상태 관리 |
| **MatchManager** | 매치 진행 및 점수 관리 |
| **CareerManager** | 커리어 모드 및 리그 관리 |
| **InputManager** | 터치 입력 처리 |

### Data

| 클래스 | 설명 |
|-------|------|
| **PlayerData** | 선수 정보 및 능력치 |
| **TeamData** | 팀 정보 및 스쿼드 관리 |
| **MatchData** | 매치 정보 및 통계 |

### UI/Gameplay

| 클래스 | 설명 |
|-------|------|
| **MainMenu** | 메인 메뉴 UI |
| **MatchHUD** | 매치 중 HUD |
| **PlayerController** | 플레이어 조작 |

## 문제 해결

### Unity가 TextMesh Pro를 찾을 수 없음

**해결책:**
1. Window > TextMeshPro > Import TMP Essential Resources
2. 또는 Package Manager에서 TextMesh Pro 재설치

### 스크립트 컴파일 오류

**해결책:**
1. Assets > Reimport All
2. 또는 Asset Store에서 Missing 스크립트 재다운로드

### 모바일 빌드 실패

**해결책:**
1. Build Settings에서 Platform 올바르게 선택
2. Player Settings에서 Bundle Identifier 설정
3. Android: Android SDK / iOS: Xcode 설치 확인

## 개발 팁

### 디버그 모드

모든 Manager 클래스에 `debugMode` 옵션이 있습니다:
- 인스펙터에서 체크 박스로 활성화
- 콘솔에서 상세 로그 확인 가능

### 성능 최적화

1. **Profiler 사용**
   - Window > Analysis > Profiler
   - CPU, Memory, GPU 성능 분석

2. **Graphics 최적화**
   - Edit > Project Settings > Quality
   - 모바일에 맞는 품질 프리셋 선택

3. **Physics 최적화**
   - 불필요한 Collider 제거
   - FixedUpdate 간격 조정

## 라이선스

MIT License

## 지원

문제가 발생하면:
1. [Issues](https://github.com/sj14320/football-game-mobile/issues) 페이지 확인
2. 새 Issue 등록
3. 스택 트레이스 및 재현 방법 포함

---

**Happy Coding! ⚽**