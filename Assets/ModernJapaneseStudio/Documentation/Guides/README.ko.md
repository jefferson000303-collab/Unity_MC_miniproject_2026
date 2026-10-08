# Modern Apartment Interior Pack

Unity_URP | ko | Docs4L_v1_1

<!-- section:scope -->
## 먼저 읽기

이 ZIP은 `shared storefront`의 `Unity_URP`판입니다. 표시명은 Modern Apartment Interior Pack이며 내부 파일명·에셋 ID·코드명은 유지합니다. 설명서 개정 식별자는 `Docs4L_v1_1`입니다. 설명서만 보완한 복사본으로 기존 모델·텍스처·런타임 바이트는 보존됩니다. 영어·일본어·간체 중국어·한국어 설명서는 하나의 영어 기준 내용을 따릅니다. 중국어 지역 지정이 없어 간체(`zh-CN`)를 기본으로 사용합니다. Unreal은 별도판이며 이 ZIP에 포함되지 않습니다. 여기서 별도판의 완료 상태나 엔진 버전을 확정하지 않습니다. 선택적인 Windows 미리보기 데모도 이 ZIP에 첨부되지 않습니다.

<!-- section:inventory -->
## 구성과 수량

카탈로그는 건축 부품과 이전 문 변형을 포함한 개별 ID `174`개이며 서로 다른 생활 소품 `174`종이라는 뜻이 아닙니다. Unity는 프리팹 `174`개를 제공합니다. FBX/GLB판은 각각 개별 에셋 `174`개와 조립 예시 `3`개로 구성된 모델 `177`개입니다. 재질 `37`개와 PNG `39`개가 있으며 `1024×1024`가 `30`개, `2048×2048`이 `9`개입니다. 카탈로그 삼각형 합계는 Unity `567,250`, FBX/GLB `566,950`입니다. 보존된 `Interior_Slider_r19`는 Unity에서 `720`, 교환 형식에서 `420`삼각형이며 가구가 배치된 예시에서는 사용하지 않습니다. 형상 수량이며 프레임 렌더 비용이 아닙니다. 카탈로그 가동 피벗은 `34`개이며 예시별 가동 인스턴스는 순서대로 `21`, `26`, `22`개입니다. 형식별 `Documentation/Asset_Catalog.csv`를 참고하세요.

<!-- section:files -->
## 폴더와 파일

ZIP 루트에는 `Modern_Japanese_Apartment_Unity_URP.unitypackage`가 있습니다. 임포트한 내용은 `Assets/ModernJapaneseStudio`의 `Prefabs`, `Scenes`, `Settings`, `Runtime`, 임포트 메시·재질·텍스처입니다. 내부 엔진 패키지는 변경하지 않았습니다.

`Documentation/README.md`는 영어판이며 동등한 번역본은 `Documentation/README.ja.md`, `Documentation/README.zh-CN.md`, `Documentation/README.ko.md`입니다. 보충 파일은 `Documentation/TECHNICAL_SPECIFICATIONS.txt`, `Documentation/AI_PROVENANCE.txt`, `Documentation/Third-Party_Notices.txt`입니다. 실제 파일명·경로·키·에셋 ID는 번역하지 않습니다. 각 압축을 푼 구성을 함께 유지하세요.

<!-- section:install -->
## 설치와 요구사항

ZIP 전체를 풉니다. 필요한 URP 버전이 설치된 Unity 프로젝트에 제공된 `.unitypackage`의 모든 파일을 임포트하세요. Project Settings > Graphics와 활성 Quality 레벨에 `Assets/ModernJapaneseStudio/Settings/StudioPipeline.asset`을 지정합니다. Player 색 공간은 Linear, Active Input Handling은 Input Manager (Old) 또는 Both로 설정합니다. 패키지가 이 프로젝트 설정을 자동 변경하지 않습니다.

Unity판의 검증 환경은 Windows, Unity `2022.3.5f1`, Universal RP `14.0.8`, Linear 색 공간, Input Manager (Old) 또는 Both입니다. Unity/URP는 외부 의존성이며 재배포하지 않습니다. 다른 Asset Store 패키지는 필요하지 않고 Built-in/HDRP 재질은 제공하지 않습니다. FBX/GLB에는 호환 임포터와 대상 엔진의 수동 설정이 필요하며 네이티브 엔진 프로젝트는 없습니다.

<!-- section:maps -->
## 세 가지 주거 예시

`Assets/ModernJapaneseStudio/Scenes/MJS_Studio.unity`, `Assets/ModernJapaneseStudio/Scenes/MJS_TwoRoom.unity`, `Assets/ModernJapaneseStudio/Scenes/MJS_LivedIn.unity`를 Build Settings에 추가하고 `MJS_Studio`를 열어 Play를 누릅니다. 다른 씬도 같은 방식으로 직접 열거나 아래 선택 조작을 사용하세요.

예시는 순서대로 작업형 `1K`, 침실 분리형 `1LDK`, 생활감 있는 `1R`입니다. 실내 치수는 `3.6×7.8 m`, `5.6×8.3 m`, `5.4×5.7 m`, 배치 에셋은 `80`, `87`, `82`개, 삼각형 합계는 `257,606`, `292,046`, `261,766`입니다. 실내 치수와 형상 합계이며 전체 외부 경계나 성능 측정값이 아닙니다. 수정 배치에는 고립된 통로 기둥·상부 벽 잔여물과 현관 막다른 홈 제거, `8 cm` 현관 단차 유지, 겹침·리턴 구조를 통한 미닫이문 노출 틈 차폐, 욕실/WC 여닫이문이 반영됐습니다. 세면실 빨래바구니는 수정된 문 이동 범위를 피합니다. 카탈로그에는 이전 호환 문 변형이 남아 있으나 이 예시들은 수정 부품을 사용합니다.

<!-- section:controls -->
## 조작과 상호작용

Unity 예제에서만 제공되는 조작입니다. `WASD` 이동, 마우스 시점, `E`로 `2 m` 이내 가동 문·서랍·뚜껑 또는 벽 조명 스위치를 조작합니다. `F1/F2/F3`는 위 순서로 예시 선택, `R`은 현재 현관 복귀, `Esc/Tab`은 메뉴와 마우스 해제입니다. 메뉴에서 재개하려면 Continue를 선택합니다. 메뉴가 닫힌 상태에서 마우스만 풀려 있으면 왼쪽 클릭으로 다시 잡습니다. 플레이어 캡슐은 폭 `0.60 m`, 높이 `1.75 m`입니다. 점프·앉기·저장은 제공하지 않습니다. FBX/GLB에는 이 조작이나 게임 코드가 없으며 통합용 분리 피벗·동작 메타데이터를 제공합니다. 스켈레탈 리그·베이크 애니메이션 클립은 없습니다. 별도 선택 Windows 데모 UI는 한국어, 편집 가능한 Unity 예제 UI는 영어입니다. 번역 설명서가 런타임 UI를 번역하지는 않습니다.

<!-- section:materials -->
## 재질과 텍스처

메시에는 UV가 있습니다. 제공된 경우 베이스 컬러·노멀·패킹 ORM 맵을 사용하며 빨강은 차폐, 초록은 거칠기, 파랑은 금속성입니다. 일부 재질은 맵 대신 상수를 사용합니다. Unity는 Universal Render Pipeline/Lit을 사용합니다. 다른 임포터에서는 텍스처 재연결·채널 변환·투명도·셰이더 설정이 필요할 수 있습니다. 교환 형식판의 `../../Textures` 상대 경로를 유지하세요. Unity 거울은 맵 진입 시 `128×128` 큐브맵을 한 번 촬영하고 이후 문·조명 변화에는 갱신되지 않습니다. 다시 촬영하려면 씬을 재로드하세요. 근사 환경 반사이며 평면 거울이 아닙니다. 교환 파일에는 이 런타임 반사가 구현되지 않았습니다. 다른 엔진에서는 조명·렌더가 달라질 수 있습니다.

<!-- section:reuse -->
## 재사용·크기·충돌

교환 형상의 단위는 미터이며 임포터의 축 변환을 확인하세요. Unity 프리팹 루트는 위치·회전이 0이고 단위 스케일이며 자식 `Surface` 변환은 유지합니다. `Assets/ModernJapaneseStudio/Prefabs`의 프리팹을 씬으로 드래그하세요. 제공 동작에는 피벗 부모와 `MJSPart`를 유지하고, 정적 사용 시 원본 계층을 병합·삭제하지 않고 해당 컴포넌트를 제거합니다. Unity 정적 표면은 메시 콜라이더, 가동부는 박스 콜라이더입니다. 막히면 동작이 멈춥니다. 문 앞을 비키고 배치·크기 변경 후 이동 여유를 재확인하세요. FBX/GLB에는 Unity 콜라이더나 런타임 동작이 없습니다.

Unity 통합: `MJSHost`는 제공된 `MJS_` 예제 씬에 자동 설치됩니다. 이 동작을 수정할 경우가 아니면 직접 만든 씬에는 다른 이름을 쓰세요. `MJSWalker`에는 `CharacterController`와 `eye` Camera 참조가 필요합니다. `MJSSwitch.lights`에 대상 Light를 지정하고 `Toggle()`로 전환합니다. `MJSPart`에는 `kind`, `axis`, `closedPosition`, `closedRotation`, `restValue`, `openValue`, `Toggle()`, `TryApply()`가 있습니다. `MJSAsset`은 카탈로그/배치 ID를 저장합니다. 예제 로직이며 완성된 게임 프레임워크가 아닙니다.

<!-- section:troubleshooting -->
## 문제 해결

- Unity 재질이 이상하거나 분홍색이면: URP 설치, Graphics·활성 Quality의 파이프라인 지정, Linear 색 공간을 확인하세요.
- Unity 이동/맵 전환이 안 되면: Game 뷰 포커스, Input Manager (Old) 또는 Both, Build Settings의 모든 씬을 확인하세요. 스크립트/메시 누락 시 의존성을 포함한 패키지 전체를 임포트합니다.
- 교환 텍스처가 없으면: 전체를 풀고 형식 폴더 옆에 `Assets/Textures`를 유지하며 필요 시 FBX 이미지를 수동 연결하세요. GLB만 옮기지 마세요.
- 문/서랍이 멈추면: 경로에서 비키고 주변 콜라이더를 확인하세요. 임의 재배치의 모든 충돌을 검사한 것은 아닙니다.
- 반사가 안 바뀌면: Unity 촬영은 의도적으로 씬 재로드 전까지 고정이며 FBX/GLB는 렌더러별 거울 설정이 필요합니다.

<!-- section:limits -->
## 제한사항

건축은 재사용 부품과 배치 전용 셸로 구성되며 절차적 생성기가 아닙니다. 일부 메시에는 의도된 열린 면이 있으며 수밀 제조 모델·건축법 인증 모델이 아닙니다. LOD 체인·베이크 라이트맵/GI·캐릭터·NPC·퀘스트·완전한 가전 시뮬레이션은 제공하지 않습니다. 이 ZIP들에는 Blender 제작 원본이 없습니다. 모바일·VR·콘솔·macOS·Linux·임의 엔진 버전·모든 하드웨어의 성능/호환성을 보장하지 않습니다. 예시 이미지는 실제 모델 렌더이며 일부 단면 뷰는 내부를 보여주기 위해 천장을 숨깁니다. 다른 렌더러의 동일 조명을 보장하지 않습니다. 선택적인 별도 Windows 데모는 미리보기 실행 파일이며 편집 에셋이 아닙니다.

<!-- section:validation -->
## 검증 범위

이전 Unity 출시본은 `2026-10-02` 새 프로젝트에서 검증하여 자동 런타임 `141/141`, Domain Reload를 끈 수명주기 `6/6`, 공식 검증기 `36/36`이 통과했습니다. 정적 검사에서는 프리팹 `174`개, 씬 `3`개, 배치 에셋 `249`개를 확인했고 스크립트·메시·재질·콜라이더·참조 GUID 누락이 없었습니다. 이번 설명서 전용 개정에서 과거 엔진 검사를 재실행하지 않았습니다. 당시 검증은 물리 키보드/마우스 이벤트를 보내지 않았습니다. 이번에는 ZIP 무결성, 모든 언어 설명서와 보호할 사실, 문서 외 엔트리와 내부 Unity 패키지 불변을 검사합니다. 번역 검토는 숫자/경로와 의미를 함께 확인하지만 새 엔진/하드웨어 호환성을 확정하지 않습니다.

<!-- section:license -->
## 라이선스 참조

사용에는 이 압축을 취득한 스토어의 라이선스가 적용됩니다. 이 안내는 대체 라이선스를 부여하지 않습니다. 이 공용 스토어 압축에는 itch 전용 라이선스가 없습니다.

번역 설명은 참고용이며 법적 원문을 대체·변경하지 않습니다. 기존 Unity 내부 문서에는 과거 준비 상태 문구가 남아 있을 수 있으므로 이번 배포는 이 외부 설명서와 구매에 적용되는 라이선스를 참고하세요. 구매 증빙을 보관하세요. 이 안내는 지원 계약이나 라이선스 조건을 추가하지 않습니다.

<!-- section:provenance -->
## 제작과 고지

ChatGPT/Codex (Astra Extra High)가 Blender 모델링 스크립트·절차적 텍스처·Unity 예제 코드·문서 작성을 도왔습니다. 모델은 Blender에서 생성/수정하고 Unity로 가져와 렌더 및 프로그램 검사를 수행했습니다. 제삼자 모델·텍스처 이미지·음원·폰트 파일은 동봉하지 않습니다. Unity 예제 UI는 폰트를 배포하지 않고 운영체제 설치 폰트를 요청합니다. 보존 고지는 `Documentation/AI_PROVENANCE.txt`와 `Documentation/Third-Party_Notices.txt`입니다. 외부 소프트웨어에는 해당 약관이 적용됩니다.
