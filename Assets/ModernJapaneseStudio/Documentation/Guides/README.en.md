# Modern Apartment Interior Pack

Unity_URP | en | Docs4L_v1_1

<!-- section:scope -->
## Read first

This is the `Unity_URP` edition from `shared storefront`. Display name: Modern Apartment Interior Pack. Internal filenames, asset IDs and code names are unchanged. Documentation revision: `Docs4L_v1_1`. This is a documentation-only copy; original model, texture and runtime bytes are preserved. English, Japanese, Simplified Chinese and Korean guides share one English basis. Chinese defaults to Simplified Chinese (`zh-CN`) because no region was specified. Unreal is a separate edition, not included in this ZIP; its readiness and engine version are not asserted here. The optional Windows preview demo is not attached to this ZIP.

<!-- section:inventory -->
## Contents and counts

The catalog contains `174` individual asset IDs including architectural and earlier door variants, not `174` distinct household prop types. Unity supplies `174` prefabs. Each FBX/GLB edition has `177` models: `174` individual assets and `3` assembled examples. There are `37` materials and `39` PNG maps: `30` at `1024×1024`, `9` at `2048×2048`. Catalog geometry totals `567,250` triangles in Unity and `566,950` in FBX/GLB. The retained `Interior_Slider_r19` has `720` triangles in Unity and `420` in interchange files; the furnished examples do not use it. These are geometry counts, not frame rendering cost. There are `34` catalog moving pivots; the examples contain `21`, `26`, `22` moving instances in order. Consult the format-specific `Documentation/Asset_Catalog.csv`.

<!-- section:files -->
## Folders and files

The ZIP root contains `Modern_Japanese_Apartment_Unity_URP.unitypackage`. Imported content is under `Assets/ModernJapaneseStudio`: `Prefabs`, `Scenes`, `Settings`, `Runtime`, imported meshes, materials and textures. The nested engine package is unchanged.

`Documentation/README.md` is English. Equivalent guides are `Documentation/README.ja.md`, `Documentation/README.zh-CN.md` and `Documentation/README.ko.md`. Supporting files are `Documentation/TECHNICAL_SPECIFICATIONS.txt`, `Documentation/AI_PROVENANCE.txt` and `Documentation/Third-Party_Notices.txt`. Actual filenames, paths, keys and asset IDs are not translated. Keep each extracted archive together.

<!-- section:install -->
## Installation and requirements

Extract the entire ZIP. In a Unity project with the required URP version installed, import every file from the supplied `.unitypackage`. Set `Assets/ModernJapaneseStudio/Settings/StudioPipeline.asset` in Project Settings > Graphics and the active Quality level. Set Player color space to Linear and Active Input Handling to Input Manager (Old) or Both. The package does not change these project settings automatically.

For Unity only, the tested environment is Windows, Unity `2022.3.5f1`, Universal RP `14.0.8`, Linear color space, Input Manager (Old) or Both. Unity/URP are external dependencies and are not redistributed. No other Asset Store package is required. Built-in/HDRP materials are not supplied. FBX/GLB need a compatible importer and manual target-engine setup; they do not contain a native engine project.

<!-- section:maps -->
## Three example homes

Add `Assets/ModernJapaneseStudio/Scenes/MJS_Studio.unity`, `Assets/ModernJapaneseStudio/Scenes/MJS_TwoRoom.unity` and `Assets/ModernJapaneseStudio/Scenes/MJS_LivedIn.unity` to Build Settings. Open `MJS_Studio` and press Play. Open the other scenes directly the same way, or use the selection controls below.

In order, the examples are a work-oriented `1K`, a one-bedroom `1LDK`, and a lived-in `1R`. Interior footprints: `3.6×7.8 m`, `5.6×8.3 m`, `5.4×5.7 m`; placed assets: `80`, `87`, `82`; triangle totals: `257,606`, `292,046`, `261,766`. These are interior footprints and geometry totals, not full exterior bounds or performance measurements. Corrected layouts remove the isolated hallway post/header and entrance blind pocket, retain the `8 cm` genkan step, close exposed sliding-door edges with overlaps/returns, and use hinged bathroom/WC doors. The washroom hamper clears the revised door swing. Earlier compatible door variants remain in the catalog; these examples use corrected parts.

<!-- section:controls -->
## Controls and interaction

Unity example controls only: `WASD` moves; mouse looks; `E` operates a moving door, drawer, lid or wall light switch within `2 m`; `F1/F2/F3` select examples in the order above; `R` returns to the current entrance; `Esc/Tab` opens the menu and releases the mouse. Choose Continue to resume from the menu. If the menu is closed but the mouse is unlocked, a left click recaptures it. The player capsule is `0.60 m` wide and `1.75 m` tall. No jump, crouch or save feature is supplied. FBX/GLB do not include these controls or gameplay code; separated pivots and motion metadata are provided for integration. No skeletal rigs or baked animation clips are included. The separate optional Windows demo UI is Korean; editable Unity example UI is English. Translated guides do not localize runtime UI.

<!-- section:materials -->
## Materials and textures

Meshes have UVs. Materials use supplied base color, normal and packed ORM maps where present: red = occlusion, green = roughness, blue = metallic. Some materials use constant values instead of maps. Unity uses Universal Render Pipeline/Lit. Other importers may need texture relinking, channel conversion, transparency and shader setup. Preserve `../../Textures` relative paths in interchange editions. The Unity mirror captures a `128×128` cubemap once on map entry; later door/light changes do not update it. Reload the scene to capture again. This is approximate environment reflection, not a planar mirror. Interchange files do not implement this runtime reflection. Lighting/rendering may differ in other engines.

<!-- section:reuse -->
## Reuse, scale and collision

Interchange geometry is in metres; check importer axis conversion. Unity prefab roots use position/rotation of 0 and unit scale; retain child `Surface` transforms. Drag a prefab from `Assets/ModernJapaneseStudio/Prefabs` into your scene. Preserve the pivot parent and `MJSPart` for supplied motion; remove that component for static use without merging/deleting the source hierarchy. Unity static surfaces use mesh colliders and moving parts use box colliders. Motion stops when blocked; step clear of a door and recheck clearance after changing placement/scale. FBX/GLB do not include Unity colliders or runtime behavior.

Unity integration: `MJSHost` automatically installs for supplied `MJS_` example scenes; use another name for your own scene unless adapting this behavior. `MJSWalker` needs a `CharacterController` and `eye` Camera reference. Assign target Lights to `MJSSwitch.lights`; `Toggle()` switches them. `MJSPart` includes `kind`, `axis`, `closedPosition`, `closedRotation`, `restValue`, `openValue`, `Toggle()` and `TryApply()`. `MJSAsset` stores catalog/placement IDs. This is example logic, not a complete game framework.

<!-- section:troubleshooting -->
## Troubleshooting

- Wrong/pink Unity materials: check URP installation, the pipeline assigned in Graphics and the active Quality level, and Linear color space.
- No Unity movement/map switching: focus Game view, check Input Manager (Old) or Both and all scenes in Build Settings. Missing scripts/meshes: import the whole package with dependencies.
- Missing interchange textures: extract everything and retain `Assets/Textures` beside the format folder; manually relink FBX images if required. Do not move a GLB alone.
- Stopped doors/drawers: move clear and inspect nearby colliders. Arbitrary rearrangements are not exhaustively collision-tested.
- Unchanging reflections: Unity's capture is intentionally static until scene reload; FBX/GLB need renderer-specific mirror setup.

<!-- section:limits -->
## Limits

Architecture combines reusable pieces and layout-specific shells, not a procedural generator. Some meshes intentionally have open surfaces; this is not a watertight manufacturing or building-code-certified model. No LOD chain, baked lightmaps/GI, characters, NPCs, quests or full appliance simulation is supplied. Blender authoring files are not in these ZIPs. No mobile, VR, console, macOS, Linux, arbitrary engine-version or all-hardware performance/compatibility claim is made. Example screenshots are actual model renders; some cutaways hide ceilings for visibility. Identical lighting in another renderer is not promised. A separate optional Windows demo is a preview executable, not editable assets.

<!-- section:validation -->
## Validation scope

The prior Unity release was checked in a fresh project on `2026-10-02`: automated runtime `141/141`, Domain Reload-disabled lifecycle `6/6`, official validator `36/36` checks passed. Static checks found `174` prefabs, `3` scenes and `249` placed assets with no missing scripts, meshes, materials, colliders or reference GUIDs. These historical engine tests were not rerun for this documentation-only revision. That validation task did not send physical keyboard/mouse events. This revision checks ZIP integrity, all four guides and protected facts, unchanged non-document entries and unchanged nested Unity package. Translation review checks meaning as well as numbers/paths; this establishes no new engine/hardware compatibility.

<!-- section:license -->
## License reference

Use is governed by the license of the storefront where this archive was obtained. This guide grants no alternative license. The itch-specific license is not supplied in this shared-storefront archive.

Translated explanations are informational and do not replace or amend legal text. Existing internal Unity documentation may retain older preparation-status wording; use this outer guide and the applicable purchase license for this delivery. Keep proof of purchase. This guide adds no support contract or license terms.

<!-- section:provenance -->
## Production and notices

ChatGPT/Codex (Astra Extra High) assisted with Blender modeling scripts, procedural textures, Unity example code and documentation. Models were generated/revised in Blender, then imported, rendered and programmatically checked in Unity. No third-party models, texture images, audio or font files are bundled. Unity example UI requests an installed operating-system font rather than distributing one. Retained notices: `Documentation/AI_PROVENANCE.txt` and `Documentation/Third-Party_Notices.txt`. External software is subject to its own terms.
