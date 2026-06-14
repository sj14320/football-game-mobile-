# Football Game Mobile - Unity

완전한 3D 축구 게임 with 커리어 모드 (모바일 최적화)

## 📋 프로젝트 개요

- **엔진**: Unity 2022 LTS 이상
- **플랫폼**: Android, iOS
- **게임 타입**: 풀 3D 매치 플레이 + 커리어 모드
- **그래픽**: 3D 실시간 렌더링

## 🎮 주요 기능

### 1. Match System (매치 시스템)
- 실시간 3D 축구 매치
- PvP 및 싱글플레이 모드
- 동적 AI 상대팀
- 실제 축구 규칙 적용

### 2. Career Mode (커리어 모드)
- 리그 진행 시스템
- 선수 개발 및 성장
- 팀 관리
- 시즌 진행

### 3. Player System (플레이어 시스템)
- 플레이어 스쿼드 관리
- 선수 개발 및 업그레이드
- 포지션별 능력치
- 계약 관리

### 4. Mobile Optimization (모바일 최적화)
- 터치 제스처 컨트롤
- 성능 최적화
- 배터리 절약 모드
- 반응형 UI

## 📁 폴더 구조

```
Assets/
├── Scripts/
│   ├── Core/                 # 핵심 게임 시스템
│   │   ├── Match/           # 매치 관리
│   │   ├── Player/          # 플레이어 관리
│   │   ├── Ball/            # 공 물리
│   │   ├── Team/            # 팀 관리
│   │   └── GameManager/     # 게임 매니저
│   ├── Gameplay/            # 게임플레이
│   │   ├── PlayerController/
│   │   ├── AIController/
│   │   ├── Animation/
│   │   └── Input/
│   ├── Career/              # 커리어 시스템
│   │   ├── CareerManager/
│   │   ├── PlayerDevelopment/
│   │   ├── League/
│   │   └── Squad/
│   ├── UI/                  # UI 시스템
│   │   ├── Menu/
│   │   ├── HUD/
│   │   ├── Career/
│   │   └── Settings/
│   └── Utilities/           # 유틸리티
│       ├── Manager/
│       ├── Helper/
│       └── Data/
├── Prefabs/                 # 프리팹
├── Scenes/                  # 씬
├── Materials/               # 머티리얼
├── Animations/              # 애니메이션
├── Audio/                   # 오디오
├── Resources/               # 리소스
└── StreamingAssets/         # 스트리밍 에셋
```

## 🚀 시작하기

1. Unity 2022 LTS 이상 설치
2. 이 저장소 클론
3. Unity에서 프로젝트 열기
4. Assets/Scenes/MainMenu 씬 실행

## 📖 개발 가이드

자세한 개발 가이드는 [DEVELOPMENT.md](./DEVELOPMENT.md)를 참조하세요.

## 🎮 게임플레이

### 매치 모드
- **좌측 스틱**: 플레이어 이동
- **우측 스틱**: 카메라 회전
- **탭**: 패스
- **더블 탭**: 슈팅
- **스와이프**: 드리블

### 커리어 모드
- 팀 관리
- 선수 개발
- 리그 진행
- 경기 결과 분석

## 📝 라이선스

MIT License

## 👨‍💻 개발자

- sj14320

---

**더 많은 정보는 Wiki를 참조하세요!**