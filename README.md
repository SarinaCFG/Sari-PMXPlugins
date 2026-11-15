# Sari PMXPlugins
Various plugins from [PMXEditor(PMDEditor)](https://kkhk22.seesaa.net/category/14045451-1.html).

## [Bone] Create RigidBody and linked Joint(aligned)
This plugin creates RigidBody and Joint based on a bone or sequence of bones. Comparatively to the default behavior in PMXEditor aligns joint to the bone local axes.
| Default behavior in PMXEditor | This plugin |
| :-: | :-: |
| ![Default behavior for adding rigid bodies and joints in PMXEditor](https://media.githubusercontent.com/media/SarinaCFG/Sari-PMXPlugins/main/img/linkedJointDefaultEditor.png) | ![This plugin behavior](img/linkedJoint2.png) |

## [Joint] AddSideJoint
This plugin creates additional joints between rigid bodies.
![Side joints](img/sideJointPreview.png)

## [Morph] Hide Morph
This plugin hides Morph from from facial manipulation panel in MMD.
| Before | After |
| :-: | :-: |
| ![Before](img/hideMorphPreviewA.png) | ![After](img/hideMorphPreviewB.png) |

# Usage
Put the Sari PMXPlugins folder from [Releases](https://github.com/SarinaCFG/Sari-PMXPlugins/releases/latest) to the ```PmxEditor\_plugin\User\``` folder.  
Tested on 
[English PmxEditor x64 vr.0254f ](https://www.deviantart.com/inochi-pm/art/PmxEditor-vr-0254f-English-Version-v2-0-766313588) and [PmxEditor 0275 x64](https://kkhk22.seesaa.net/article/498439954.html). Please check [wiki](https://github.com/SarinaCFG/Sari-PMXPlugins/wiki), for additional info on each plugin.

# Build
Please refer [here](https://kkhk22.seesaa.net/article/389788186.html) for build and debug guide. Mostly you will have to reference ```PEPlugin.dll``` and ```SlimDX.dll``` from your **PMXEditor** installation.
