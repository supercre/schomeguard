# SCHomeGuard — 슈크인의 홈 PC 프로텍터

**슈크인(슈퍼크리에이티브 구성원)의 집 PC를 위한** Windows 보안 점검 도구입니다.

회사 PC는 보안관제(SOC)가 관리·통제할 수 있지만, 집 PC는 그렇지 않습니다.
집 PC에서 문제가 생겼을 때 **출처가 확인되지 않은 정보나 도구**(검색으로 찾은 '정리 프로그램', 무료 다운로드 사이트의 설치파일 등)로
대응하다가 오히려 더 위험해지는 일을 막기 위해 만들었습니다. 보안관제가 확인한 위협 정보와 검증된 방법만 씁니다.

## 하는 일
| 기능 | 내용 |
|---|---|
| 프록시웨어 검사·제거 | PC 회선을 남에게 빌려주는 프로그램 15종(DeskRest, Apprunner, Honeygain, EarnApp, Pawns, PacketStream …)을 설치 폴더·레지스트리·자동 실행·예약 작업·서비스·바로가기·사용 기록까지 찾아 지웁니다. 제거 전 시스템 복원 지점과 백업을 만듭니다. |
| Softonic 차단 | 스폰서 설치파일로 프록시웨어를 끼워 배포한 다운로드 사이트를 Chrome·Edge·Whale·Brave·Firefox에서 막습니다. |
| 방탄호스팅 차단 | 공격에 쓰이는 방탄호스팅과 Spamhaus DROP 대역을 Windows 방화벽에서 막고 매주 갱신합니다. |
| 결과 저장 | 바탕화면에 결과 파일을 남겨 SOC에 보낼 수 있습니다. |

로드맵: 자동 업데이트(서명 검증), 위험 대역·신규 도메인 접속 검사, 온디바이스 AI 판정(임베딩 + 벡터 DB + 소형 LLM), Rust 상주 엔진, macOS 판.

## 사용법
1. [Releases](https://github.com/supercre/schomeguard/releases)에서 `SCHomeGuard_vX.Y.Z.zip`을 받습니다.
2. zip 오른쪽 클릭 → **속성** → **차단 해제** 체크 → 확인 (서명 전이라 SmartScreen 안내가 나옵니다)
3. 압축을 풀고 `SCHomeGuard.exe` 실행 → 관리자 권한 **예**
4. 왼쪽 **모두 켜기** → 오른쪽 **검사 시작** → 발견 항목이 있으면 **선택 항목 제거**

명령줄:
```powershell
powershell -ExecutionPolicy Bypass -File SCHomeGuard.ps1 -Cli -Action Scan|Clean|Protect|Unprotect|Status|UpdateBPH [-IncludeSuspicious]
```

## 구성
```
src/SCHomeGuard.ps1        본체 (Windows PowerShell 5.1, WPF 화면 + CLI)
launcher/                  SCHomeGuard.exe — 본체를 내장해 ProgramData에 풀고 관리자 권한으로 실행 (.NET Framework 4.8)
packaging/                 배포 zip에 함께 넣는 파일
.github/workflows/         태그(v*) 푸시 → 빌드·서명·릴리스
```
위협 목록·모델은 별도 저장소 [supercre/schomeguard-db](https://github.com/supercre/schomeguard-db)에서 서명된 형태로 배포합니다.

## 릴리스
`vX.Y.Z` 태그를 푸시하면 GitHub Actions가 실행기를 빌드하고 zip, `SCHomeGuard.ps1`, `manifest.json`(SHA-256)과
`manifest.sig`(RSA-SHA256 서명)를 릴리스에 올립니다. 클라이언트는 내장 공개키로 서명을 확인한 뒤에만 업데이트합니다.

## 신고·문의
보안 문제는 공개 이슈 대신 슈퍼크리에이티브 보안관제(SOC)로 알려 주세요.

## 라이선스
GPL-3.0. 위협 데이터 출처(Spamhaus 등)의 이용 조건은 각 출처를 따릅니다.
