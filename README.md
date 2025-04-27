## 📑 주요 기술 스택
| 영역            | 기술                                      |
|-----------------|-------------------------------------------|
| Language        | C# (.NET 8)                               |
| Web API         | ASP.NET Core                              |
| Database        | MySQL + Dapper.FastCrud                   |
| Cache           | Redis                                     |
| Authentication  | JWT (Access Token / Refresh Token)        |
| Logging         | Serilog (File, Console, Seq 지원)          |
| WebSocket       | ASP.NET Core WebSocket                    |
| Architecture    | Clean Architecture (Domain, Application, Infrastructure, Persistence, API) |
| Mediation       | MediatR (Command/Query 분리)              |
| Mapping         | AutoMapper                               |

## 📑 주요 기능
| 기능            | 설명                                      |
|-----------------|-------------------------------------------|
| JWT 인증        | Access Token + Refresh Token 구조 지원    |
| WebSocket 통신  | 채팅, 파티, 실시간 동기화 처리             |
| Redis 캐시      | 빠른 데이터 조회 및 세션 관리              |
| CQRS 적용       | Command/Query 구조 완전 분리               |
| Named Lock      | 데이터베이스 락을 통한 동시성 제어         |
| Serilog 로깅    | Console, 파일, Seq로 로깅 출력             |
| 자동 매핑       | DTO ↔ Entity 변환 (AutoMapper 사용)        |
| Dapper.FastCrud | 빠르고 간결한 DB CRUD 처리                |
| 에러 핸들링     | 미들웨어를 통한 예외 통합 처리             |

## 📁 폴더 구조

```plaintext
/GameServer
 ├── GameServer.Api           # Web API 엔트리 포인트 (컨트롤러, Swagger)
 ├── GameServer.Application   # CQRS(Command, Query), DTO, 서비스 로직
 ├── GameServer.Contracts     # Domain, Application, Infrastructure 간의 의존성을 분리하기 위한 공통 인터페이스 모음
 ├── GameServer.Domain        # 엔티티, 인터페이스, Enum 등 핵심 도메인
 ├── GameServer.Infrastructure # 외부 연동: JWT, Redis, Serilog, WebSocket 핸들러
 ├── GameServer.Persistance    # Dapper 기반 DB 레포지토리 구현
 ├── GameServer.Shared         # 클라이언트와 공유할 DTO, Enum, Response 모델 등
 └── README.md                 # 프로젝트 설명 문서
