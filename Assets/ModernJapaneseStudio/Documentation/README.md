# Modern Japanese Apartment — store edition

This kit contains stylized modern Japanese interiors and everyday objects. Choose the download that matches your workflow:

- Modern_Japanese_Apartment_Unity_URP.zip: Unity .unitypackage, documentation and catalog.
- Modern_Japanese_Apartment_FBX.zip: 174 individual FBX assets, 3 assemblies, 39 shared PNG textures and metadata.
- Modern_Japanese_Apartment_GLB.zip: 174 individual GLB assets, 3 assemblies, 39 shared external PNG textures and metadata.
- The separately offered Windows demo is for previewing the example rooms; it is not an editable asset package.

The 174 catalog entries include architecture and door variants. See Asset_Catalog.csv for individual triangle counts. The three examples demonstrate a 1K work studio, a one-bedroom 1LDK and a lived-in 1R.

## Unity quick start
Use Unity 2022.3.5f1 with Universal RP 14.0.8 installed. Import all files from the .unitypackage. All kit files live under Assets/ModernJapaneseStudio.
Set Settings/StudioPipeline.asset as the render pipeline in Project Settings > Graphics and for your active Quality level. Set Player color space to Linear and Active Input Handling to Input Manager (Old) or Both. These settings are not changed automatically by the package.
Add Scenes/MJS_Studio.unity, MJS_TwoRoom.unity and MJS_LivedIn.unity to Build Settings. Open MJS_Studio and press Play.
WASD moves, the mouse looks, E interacts within 2 metres, F1/F2/F3 change examples, R resets to the entrance, and Esc/Tab opens the menu. The example capsule is 0.60 m wide and 1.75 m tall.

## Reusing the assets
Drag a Prefabs item into your scene. Prefab roots use position/rotation zero and scale one. Child Surface objects reference editable imported FBX meshes; retain their transforms. Keep the pivot parent and MJSPart component when you want the supplied moving-part behavior. Remove the component for a static prop, without merging or deleting its source hierarchy.
Static mesh colliders and moving box colliders are configured on the prefabs. Validate clearance after changing scale, walls or furniture placement. Example interaction logic is a starting point, not a complete game framework.
MJSHost automatically installs only for scenes whose names start with MJS_. For your own scene, use another name unless you intentionally adapt that behavior. MJSWalker requires a CharacterController and its eye Camera reference. MJSSwitch exposes a lights array and Toggle(); assign the intended Lights in the Inspector. MJSPart exposes kind, axis, closed position/rotation, rest/open values and Toggle()/TryApply(). MJSAsset stores catalog and placement IDs.
MJSMirrorCapture installs on example-scene ReflectionProbes and captures one 128×128 cubemap when the scene loads. Reflection content is static until that scene reloads.

## FBX / GLB use
Extract the entire ZIP. Keep Assets/FBX/Models or Assets/GLB/Models alongside Assets/Textures; texture paths use ../../Textures. FBX imports may require manual texture relinking depending on the application. GLB files use external shared PNGs rather than embedding repeated copies. The FBX and GLB editions do not contain game logic or baked animation clips. Separate pivots and custom motion metadata are provided for integration.
Materials use base color, normal and ORM (occlusion/red, roughness/green, metallic/blue) maps where supplied. Follow the material metadata; a target engine may need channel conversion, shader setup and transparency adjustments. Metre scale, axes and collision setup should be checked in your target engine.

## Requirements and limits
Tested Unity setup: 2022.3.5f1 + URP 14.0.8 on Windows. No Built-in, HDRP or native Unreal package. No character rigs, baked animation clips, procedural room generator or LOD chain. Architectural content includes both reusable segments and layout-specific pieces. No mobile/VR/console optimization claim is made. Windows example executables are distributed separately from the Unity Asset Store package.

License: use is governed by the license displayed at the storefront where you obtained the pack. This readme does not grant an alternative license. Keep proof of purchase and consult that storefront's terms. A direct itch.io license must be selected by the seller before release.
