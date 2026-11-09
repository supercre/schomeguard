SuperCreative 홈 PC 보안 점검 (SCHomeGuard) v1.1  —  보안관제(SOC) 2026-10-13
==========================================================================

■ 실행 방법
  1. 받은 SCHomeGuard_v1.1.zip 을 오른쪽 클릭 → [속성] → 아래쪽 [차단 해제] 체크 → [확인]
     (이렇게 하면 "Windows의 PC 보호" 경고 없이 실행됩니다)
  2. 압축을 풀고 SCHomeGuard.exe 더블클릭 → 관리자 권한 창에서 [예]
  · 차단 해제를 깜빡해서 "Windows의 PC 보호" 창이 뜨면 [추가 정보] → [실행]을 누르세요.
    (회사 내부 도구라 인증서 서명이 없어서 나오는 안내입니다)
  · exe 실행이 막히면 Run_PowerShell.cmd 를 더블클릭해도 같은 화면이 뜹니다.

■ 사용 순서
  1. 왼쪽 [모두 켜기] — 보호 3가지를 한 번에 켭니다. (브라우저는 다시 열어야 적용)
  2. 오른쪽 [검사 시작] — 1분 안팎 걸립니다.
  3. 발견된 항목이 있으면 [선택 항목 제거]
     · '위험'은 자동으로 선택됩니다. '의심'(회선 공유 SDK가 들어 있는 다른 앱)은 직접 확인 후 선택하세요.
     · 제거 전에 시스템 복원 지점을 만들고, 지우는 레지스트리·폴더는 백업합니다.
  4. [결과 저장] — 바탕화면에 결과 파일이 생깁니다. 문제가 있으면 이 파일을 SOC로 보내 주세요.

■ 무엇을 하나요
  1. 프록시웨어 검사·제거
     PC 인터넷 회선을 남에게 빌려주는 프로그램(DeskRest, Apprunner, Honeygain, EarnApp,
     Pawns, PacketStream, Peer2Profit, Repocket, Traffmonetizer, ProxyRack, Grass, Nodepay,
     Bitping, Infatica 등)을 찾아 지웁니다. 설치 폴더·레지스트리·자동 실행·예약 작업·서비스·
     바로가기·사용 기록·오류 덤프·휴지통까지 확인합니다.
  2. Softonic 차단
     Softonic(무료 프로그램 다운로드 사이트)이 스폰서 설치파일로 프록시웨어를 끼워 배포했습니다.
     Chrome·Edge·Whale·Brave·Firefox에서 도메인 이름으로 막습니다(다른 사이트에는 영향 없음).
  3. 방탄호스팅 통신 차단
     공격에 쓰이는 방탄호스팅·Spamhaus DROP 대역(약 1,800개)을 Windows 방화벽에서
     들어오고 나가는 양방향으로 막습니다. 매주 월요일 12시에 최신 목록으로 자동 갱신합니다.
  4. 원치 않는 앱(PUA) 차단
     Windows Defender·Edge가 끼워팔기 설치파일·애드웨어를 막도록 켭니다.

■ 참고
  · 기록·백업 위치: C:\ProgramData\SCHomeGuard (logs / backup / quarantine)
  · 원래대로 돌리기: 각 카드의 [끄기]
  · 브라우저에 "조직에서 관리" 표시가 생기는 것은 Softonic 차단 정책 때문이며 정상입니다.
  · 다른 백신을 쓰는 PC에서는 PUA 차단이 적용되지 않을 수 있습니다.
  · v1.0에서 바뀐 점: hosts 파일을 더 이상 쓰지 않습니다(Windows가 되돌리는 경우가 있어서).
    v1.0을 실행했던 PC는 v1.1을 실행하면 이전에 넣은 hosts 항목이 자동으로 지워집니다.
  · 명령줄: SCHomeGuard.exe -Cli -Action Scan | Clean | Protect | Unprotect | Status
