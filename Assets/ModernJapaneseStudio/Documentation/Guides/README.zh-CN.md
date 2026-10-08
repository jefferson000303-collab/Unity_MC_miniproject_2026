# Modern Apartment Interior Pack

Unity_URP | zh-CN | Docs4L_v1_1

<!-- section:scope -->
## 阅读前说明

这是来自 `shared storefront` 的 `Unity_URP` 版本。显示名称为Modern Apartment Interior Pack。内部文件名、资源ID和代码名称保持不变。说明书修订标识为 `Docs4L_v1_1`。本副本仅更新说明文档，原有模型、纹理和运行时文件的字节保持不变。英文、日文、简体中文和韩文说明书采用同一份英文基准内容。因未指定中文地区，默认使用简体中文（`zh-CN`）。Unreal为独立版本，不包含在此ZIP中；本说明不声明其完成状态或引擎版本。可选的Windows预览演示也未附在此ZIP中。

<!-- section:inventory -->
## 内容与数量

目录包含 `174` 个独立资源ID，其中有建筑部件和早期门款变体，并非 `174` 种不同生活道具。Unity提供 `174` 个Prefab。每种FBX/GLB版本有 `177` 个模型：`174` 个独立资源和 `3` 个组装示例。共有 `37` 个材质、`39` 张PNG：`30` 张为 `1024×1024`，`9` 张为 `2048×2048`。目录几何总数为Unity `567,250` 个三角形，FBX/GLB `566,950` 个。保留的 `Interior_Slider_r19` 在Unity中为 `720` 个，在交换格式中为 `420` 个，带家具的示例未使用它。这些是几何数量，不是单帧渲染成本。目录有 `34` 个可动枢轴，示例的可动实例按顺序为 `21`、`26`、`22`。请查看对应格式的 `Documentation/Asset_Catalog.csv`。

<!-- section:files -->
## 文件夹与文件

ZIP根目录包含 `Modern_Japanese_Apartment_Unity_URP.unitypackage`。导入内容位于 `Assets/ModernJapaneseStudio`：`Prefabs`、`Scenes`、`Settings`、`Runtime`、导入网格、材质和纹理。内嵌引擎包未改变。

`Documentation/README.md` 为英文版。等同译文位于 `Documentation/README.ja.md`、`Documentation/README.zh-CN.md` 和 `Documentation/README.ko.md`。补充文件为 `Documentation/TECHNICAL_SPECIFICATIONS.txt`、`Documentation/AI_PROVENANCE.txt` 和 `Documentation/Third-Party_Notices.txt`。实际文件名、路径、操作键和资源ID不翻译。请完整保留每个解压包的结构。

<!-- section:install -->
## 安装与要求

完整解压ZIP。在已安装所需URP版本的Unity项目中导入所附 `.unitypackage` 全部文件。在Project Settings > Graphics及当前Quality级别指定 `Assets/ModernJapaneseStudio/Settings/StudioPipeline.asset`。Player色彩空间设为Linear，Active Input Handling设为Input Manager (Old)或Both。包不会自动修改这些项目设置。

仅Unity版的已验证环境为Windows、Unity `2022.3.5f1`、Universal RP `14.0.8`、Linear色彩空间、Input Manager (Old)或Both。Unity/URP是外部依赖，不重新分发。不需要其他Asset Store资源包，不提供Built-in/HDRP材质。FBX/GLB需要兼容导入器及目标引擎中的手动设置，不包含原生引擎项目。

<!-- section:maps -->
## 三个住宅示例

将 `Assets/ModernJapaneseStudio/Scenes/MJS_Studio.unity`、`Assets/ModernJapaneseStudio/Scenes/MJS_TwoRoom.unity` 和 `Assets/ModernJapaneseStudio/Scenes/MJS_LivedIn.unity` 加入Build Settings，打开 `MJS_Studio` 并按Play。其他场景可同样直接打开，或通过下方选择操作切换。

示例依次为工作型 `1K`、卧室独立的 `1LDK` 和带生活痕迹的 `1R`。室内尺寸为 `3.6×7.8 m`、`5.6×8.3 m`、`5.4×5.7 m`；放置资源为 `80`、`87`、`82` 个；三角形总数为 `257,606`、`292,046`、`261,766`。这些是室内尺寸和几何总数，不是完整外部边界或性能测量值。修正布局移除了孤立走廊立柱/上方残墙和玄关死角凹槽，保留 `8 cm` 玄关高差，通过重叠/侧向回折封闭推拉门边缘的可见缝隙，并在浴室/WC使用平开门。洗衣篮避开修正后的洗漱间门扇运动范围。目录保留早期兼容门款，而这些示例使用修正部件。

<!-- section:controls -->
## 操作与交互

以下仅为Unity示例操作：`WASD` 移动；鼠标控制视角；`E` 操作 `2 m` 内的可动门、抽屉、盖子或墙壁灯光开关；`F1/F2/F3` 按上述顺序选择示例；`R` 返回当前入口；`Esc/Tab` 打开菜单并释放鼠标。从菜单恢复时请选择Continue。若菜单已关闭而鼠标未锁定，单击左键可重新捕获。玩家胶囊体宽 `0.60 m`、高 `1.75 m`。不提供跳跃、下蹲或存档。FBX/GLB不含这些操作或游戏代码，提供分离枢轴和运动元数据用于集成，不含骨骼绑定或烘焙动画片段。独立可选Windows演示的UI为韩文，可编辑Unity示例的UI为英文。翻译说明书不会本地化运行时界面。

<!-- section:materials -->
## 材质与纹理

网格具有UV。材质在有相应贴图时使用基础颜色、法线和打包ORM：红色为遮蔽，绿色为粗糙度，蓝色为金属度。部分材质使用常量而非贴图。Unity使用Universal Render Pipeline/Lit。其他导入器可能需要重连纹理、转换通道、设置透明度和着色器。交换格式应保留 `../../Textures` 相对路径。Unity镜子仅在进入地图时捕获一次 `128×128` 立方体贴图，此后门/灯变化不更新反射；重新加载场景才能再次捕获。这是近似环境反射而非平面镜，交换格式文件没有实现此运行时反射。其他引擎的光照/渲染可能不同。

<!-- section:reuse -->
## 复用、比例与碰撞

交换几何以米为单位，请检查导入器的轴向转换。Unity Prefab根节点的位置/旋转为0，缩放为单位值；保留子级 `Surface` 变换。将 `Assets/ModernJapaneseStudio/Prefabs` 中的Prefab拖入场景。使用提供的运动时保留枢轴父节点及 `MJSPart`；作为静态资源时移除此组件，但不要合并/删除源层级。Unity静态表面使用网格碰撞体，可动部件使用盒形碰撞体。受阻时运动停止；请让开门的路径，并在改变摆放/比例后重新检查空间。FBX/GLB不含Unity碰撞体或运行时行为。

Unity集成：`MJSHost` 在提供的 `MJS_` 示例场景中自动安装；除非调整此行为，自建场景应使用其他名称。`MJSWalker` 需要 `CharacterController` 和 `eye` Camera引用。向 `MJSSwitch.lights` 指定目标灯光，通过 `Toggle()` 切换。`MJSPart` 包含 `kind`、`axis`、`closedPosition`、`closedRotation`、`restValue`、`openValue`、`Toggle()` 和 `TryApply()`。`MJSAsset` 保存目录/放置ID。这是示例逻辑，不是完整游戏框架。

<!-- section:troubleshooting -->
## 常见问题

- Unity材质异常/粉色：检查URP安装、Graphics及当前Quality的管线设置和Linear色彩空间。
- Unity无法移动/切换地图：让Game视图获得焦点，检查Input Manager (Old)或Both及Build Settings中的全部场景。缺失脚本/网格时，连同依赖完整导入包。
- 交换纹理缺失：完整解压，并将 `Assets/Textures` 保留在格式文件夹旁；必要时手动重连FBX图像。不要单独移动GLB。
- 门/抽屉停止：离开其运动范围，检查附近碰撞体。任意重新摆放未经过全面碰撞测试。
- 反射不变：Unity捕获按设计在场景重载前保持静态；FBX/GLB需要设置对应渲染器的镜面。

<!-- section:limits -->
## 限制

建筑由可复用部件和特定布局外壳构成，并非程序化生成器。部分网格有意开放，并非水密制造模型或建筑规范认证模型。不提供LOD链、烘焙光照贴图/GI、角色、NPC、任务或完整家电模拟。这些ZIP不包含Blender制作源文件。不声明移动端、VR、主机、macOS、Linux、任意引擎版本或所有硬件的性能/兼容性。示例图片是实际模型渲染，部分剖视图隐藏天花板以展示内部。不保证其他渲染器光照相同。独立可选Windows演示为预览可执行文件，不是可编辑资源。

<!-- section:validation -->
## 验证范围

此前Unity发布版于 `2026-10-02` 在新项目中验证，自动运行时 `141/141`、关闭Domain Reload的生命周期 `6/6`、官方验证器 `36/36` 项通过。静态检查发现 `174` 个Prefab、`3` 个场景、`249` 个放置资源，没有缺失脚本、网格、材质、碰撞体或引用GUID。本次仅更新文档，未重跑这些历史引擎测试。此前验证任务未发送物理键盘/鼠标事件。本次检查ZIP完整性、所有语言说明及受保护事实、非文档条目和内嵌Unity包不变。翻译审阅同时核对含义与数字/路径，但不确立新的引擎/硬件兼容性。

<!-- section:license -->
## 许可证参考

使用受获取此包的商店许可证约束。本说明不授予替代许可证。此通用商店包不包含itch专用许可证。

翻译说明仅供参考，不替代或修改法律原文。原有Unity内部文档可能保留早期准备状态表述；本次交付请参照外层说明书及购买时适用的许可证。请保留购买凭证。本说明不新增支持服务合同或许可条款。

<!-- section:provenance -->
## 制作与声明

ChatGPT/Codex (Astra Extra High) 协助编写Blender建模脚本、程序纹理、Unity示例代码及文档。模型在Blender生成/修订，再导入Unity渲染并进行程序化检查。未捆绑第三方模型、纹理图像、音频或字体文件。Unity示例UI调用操作系统已安装字体而非分发字体。保留声明见 `Documentation/AI_PROVENANCE.txt` 和 `Documentation/Third-Party_Notices.txt`。外部软件受各自条款约束。
