# Sari PMXPlugins
Various plugins from [PMXEditor(PMDEditor)](https://kkhk22.seesaa.net/category/14045451-1.html).

## [Bone] Create RigidBody and linked Joint(aligned)
This plugin creates RigidBody and Joint based on a bone or sequence of bones. Comparatively to the default behavior in PMXEditor aligns joint to the bone local axes.
| Default behavior in PMXEditor | This plugin |
| :-: | :-: |
| ![Default behavior for adding rigid bodies and joints in PMXEditor](img/linkedJointDefaultEditor.png) | ![This plugin behavior](img/linkedJoint2.png) |

1. Select bones which will be used to create rigid body with joints. The plugin relies on local bone axis, so make sure you have them set up.   
![Bones of the braids are selected](img/linkedJoint0.png)
2. Choose **[Bone] Create RigidBody and linked Joint(aligned)** in the plugins menu.   
![Sari PMXPlugins → [Bone] Create RigidBody and linked Joint(aligned)](img/linkedJoint1.png)
3. The plugin creates rigid bodies and joints with custom axis rotation.   
![Rigid bodies and joints with custom axis rotation are created](img/linkedJoint2.png)

## [Joint] AddSideJoint
This plugin creates additional joints between rigid bodies.
![Side joints](img/sideJointPreview.png)
1. Select rigid bodies which will be connected with joints. Selection will be split: bodies from the first half will be connected from bodies with second half.   
![Two chains of bones are selected](img/sideJoint0.png)
2. Choose **[Joint] AddSideJoint** in plugins in the menu.
![Sari PMXPlugins → [Joint] AddSideJoint](img/sideJoint1.png)
3. There will be prompt with rigid bodies pairs.   
![Bone pairs for joints](img/sideJoint2.png)
4. Based on pairs joints will be created   
![side joints for a skirt were created](img/sideJoint3.png)

## [Morph] Hide Morph
This plugin hides Morph from from facial manipulation panel in MMD.
| Before | After |
| :-: | :-: |
| ![Before](img/hideMorphPreviewA.png) | ![After](img/hideMorphPreviewB.png) |

1. Select morph that needs to be hidden on **Morph** tab.   
![Morph E.Y.E is selected](img/hideMorph0.png)
2. Choose **[Morph] Hide Morph** in plugins in the menu.   
![Sari PMXPlugins → [Morph] Hide Morph](img/hideMorph1.png)
3. The plugin sets panel index to 0, so selected morph won't be visible in MMD.   
![Morph E.Y.E is hidden](img/hideMorph2.png)

# Usage
Put the Sari PMXPlugins folder from [Releases](https://github.com/SarinaCFG/Sari-PMXPlugins/releases/latest) to the ```PmxEditor\_plugin\User\``` folder.  
Tested on 
[English PmxEditor x64 vr.0254f ](https://www.deviantart.com/inochi-pm/art/PmxEditor-vr-0254f-English-Version-v2-0-766313588) and [PmxEditor 0275 x64](https://kkhk22.seesaa.net/article/498439954.html).

# Build
Please refer [here](https://kkhk22.seesaa.net/article/389788186.html) for build and debug guide. Mostly you will have to reference ```PEPlugin.dll``` and ```SlimDX.dll``` from your **PMXEditor** installation.
