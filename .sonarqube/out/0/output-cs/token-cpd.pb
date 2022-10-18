�
UD:\REGame\Assets\UnmaskForUGUI-main\UnmaskForUGUI-main\Scripts\UnmaskRaycastFilter.cs
	namespace 	
Coffee
 
. 
UIExtensions 
{ 
[		 
AddComponentMenu		 
(		 
$str		 5
,		5 6
$num		7 8
)		8 9
]		9 :
public

 

class

 
UnmaskRaycastFilter

 $
:

% &


' 4
,

4 5 
ICanvasRaycastFilter

6 J
{ 
[ 	
Tooltip	 
( 
$str Z
)Z [
][ \
[ 	
SerializeField	 
] 
private  
Unmask! '
m_TargetUnmask( 6
;6 7
public 
Unmask 
targetUnmask "
{# $
get% (
{) *
return+ 1
m_TargetUnmask2 @
;@ A
}B C
setD G
{H I
m_TargetUnmaskJ X
=Y Z
value[ `
;` a
}b c
}d e
public!! 
bool!! "
IsRaycastLocationValid!! *
(!!* +
Vector2!!+ 2
sp!!3 5
,!!5 6
Camera!!7 =
eventCamera!!> I
)!!I J
{"" 	
if$$ 
($$ 
!$$ 
isActiveAndEnabled$$ #
||$$$ &
!$$' (
m_TargetUnmask$$( 6
||$$7 9
!$$: ;
m_TargetUnmask$$; I
.$$I J
isActiveAndEnabled$$J \
)$$\ ]
{%% 
return&& 
true&& 
;&& 
}'' 
if** 
(** 
eventCamera** 
)** 
{++ 
return,, 
!,,  
RectTransformUtility,, ,
.,,, -(
RectangleContainsScreenPoint,,- I
(,,I J
(,,J K
m_TargetUnmask,,K Y
.,,Y Z
	transform,,Z c
as,,d f

),,t u
,,,u v
sp,,w y
,,,y z
eventCamera	,,{ �
)
,,� �
;
,,� �
}-- 
else.. 
{// 
return00 
!00  
RectTransformUtility00 ,
.00, -(
RectangleContainsScreenPoint00- I
(00I J
(00J K
m_TargetUnmask00K Y
.00Y Z
	transform00Z c
as00d f

)00t u
,00u v
sp00w y
)00y z
;00z {
}11 
}22 	
void<< 
OnEnable<<
(<< 
)<< 
{== 	
}>> 	
}?? 
}@@ ��
HD:\REGame\Assets\UnmaskForUGUI-main\UnmaskForUGUI-main\Scripts\Unmask.cs
	namespace 	
Coffee
 
. 
UIExtensions 
{ 
[ 
ExecuteInEditMode 
] 
[
AddComponentMenu
(
$str
,
$num
)
]
public 

class 
Unmask 
: 

,' (
IMaterialModifier) :
{ 
private 
static 
readonly 
Vector2  '
s_Center( 0
=1 2
new3 6
Vector27 >
(> ?
$num? C
,C D
$numE I
)I J
;J K
[ 	
Tooltip	 
( 
$str ?
)? @
]@ A
[ 	
SerializeField	 
] 
private  

m_FitTarget/ :
;: ;
[ 	
Tooltip	 
( 
$str Y
)Y Z
]Z [
[ 	
SerializeField	 
] 
private  
bool! %
m_FitOnLateUpdate& 7
;7 8
[ 	
Tooltip	 
( 
$str 4
)4 5
]5 6
[   	
SerializeField  	 
]   
private    
bool  ! %
m_OnlyForChildren  & 7
=  8 9
false  : ?
;  ? @
["" 	
Tooltip""	 
("" 
$str"" S
)""S T
]""T U
[## 	
SerializeField##	 
]## 
private##  
bool##! %
m_ShowUnmaskGraphic##& 9
=##: ;
false##< A
;##A B
[%% 	
Tooltip%%	 
(%% 
$str%% "
)%%" #
]%%# $
[&& 	
Range&&	 
(&& 
$num&& 
,&& 
$num&& 
)&& 
]&& 
['' 	
SerializeField''	 
]'' 
private''  
float''! &
m_EdgeSmoothing''' 6
=''7 8
$num''9 ;
;''; <
public11 
MaskableGraphic11 
graphic11 &
{11' (
get11) ,
{11- .
return11/ 5
_graphic116 >
??11? A
(11B C
_graphic11C K
=11L M
GetComponent11N Z
<11Z [
MaskableGraphic11[ j
>11j k
(11k l
)11l m
)11m n
;11n o
}11p q
}11r s
public66 

	fitTarget66 &
{77 	
get88 
{88 
return88 
m_FitTarget88 $
;88$ %
}88& '
set99 
{:: 
m_FitTarget;; 
=;; 
value;; #
;;;# $
FitTo<< 
(<< 
m_FitTarget<< !
)<<! "
;<<" #
}== 
}>> 	
publicCC 
boolCC 
fitOnLateUpdateCC #
{CC$ %
getCC& )
{CC* +
returnCC, 2
m_FitOnLateUpdateCC3 D
;CCD E
}CCF G
setCCH K
{CCL M
m_FitOnLateUpdateCCN _
=CC` a
valueCCb g
;CCg h
}CCi j
}CCk l
publicHH 
boolHH 
showUnmaskGraphicHH %
{II 	
getJJ 
{JJ 
returnJJ 
m_ShowUnmaskGraphicJJ ,
;JJ, -
}JJ. /
setKK 
{LL 
m_ShowUnmaskGraphicMM #
=MM$ %
valueMM& +
;MM+ ,
SetDirtyNN 
(NN 
)NN 
;NN 
}OO 
}PP 	
publicUU 
boolUU 
onlyForChildrenUU #
{VV 	
getWW 
{WW 
returnWW 
m_OnlyForChildrenWW *
;WW* +
}WW, -
setXX 
{YY 
m_OnlyForChildrenZZ !
=ZZ" #
valueZZ$ )
;ZZ) *
SetDirty[[ 
([[ 
)[[ 
;[[ 
}\\ 
}]] 	
publicbb 
floatbb 

{cc 	
getdd 
{dd 
returndd 
m_EdgeSmoothingdd (
;dd( )
}dd* +
setee 
{ee 
m_EdgeSmoothingee !
=ee" #
valueee$ )
;ee) *
}ee+ ,
}ff 	
publicmm 
Materialmm 
GetModifiedMaterialmm +
(mm+ ,
Materialmm, 4
baseMaterialmm5 A
)mmA B
{nn 	
ifoo 
(oo 
!oo 
isActiveAndEnabledoo #
)oo# $
{pp 
returnqq 
baseMaterialqq #
;qq# $
}rr 
	Transformtt 
	stopAftertt 
=tt  !

.tt/ 0&
FindRootSortOverrideCanvastt0 J
(ttJ K
	transformttK T
)ttT U
;ttU V
varuu 
stencilDepthuu 
=uu 

.uu, -
GetStencilDepthuu- <
(uu< =
	transformuu= F
,uuF G
	stopAfteruuH Q
)uuQ R
;uuR S
varvv 
desiredStencilBitvv !
=vv" #
$numvv$ %
<<vv& (
stencilDepthvv) 5
;vv5 6
StencilMaterialxx 
.xx 
Removexx "
(xx" #
_unmaskMaterialxx# 2
)xx2 3
;xx3 4
_unmaskMaterialyy 
=yy 
StencilMaterialyy -
.yy- .
Addyy. 1
(yy1 2
baseMaterialyy2 >
,yy> ?
desiredStencilBityy@ Q
-yyR S
$numyyT U
,yyU V
	StencilOpyyW `
.yy` a
Invertyya g
,yyg h
CompareFunctionyyi x
.yyx y
Equalyyy ~
,yy~ !
m_ShowUnmaskGraphic
yy� �
?
yy� �
ColorWriteMask
yy� �
.
yy� �
All
yy� �
:
yy� �
(
yy� �
ColorWriteMask
yy� �
)
yy� �
$num
yy� �
,
yy� �
desiredStencilBit
yy� �
-
yy� �
$num
yy� �
,
yy� �
(
yy� �
$num
yy� �
<<
yy� �
$num
yy� �
)
yy� �
-
yy� �
$num
yy� �
)
yy� �
;
yy� �
var|| 
canvasRenderer|| 
=||  
graphic||! (
.||( )
canvasRenderer||) 7
;||7 8
if}} 
(}} 
m_OnlyForChildren}} !
)}}! "
{~~ 
StencilMaterial 
.  
Remove  &
(& '!
_revertUnmaskMaterial' <
)< =
;= >#
_revertUnmaskMaterial
�� %
=
��& '
StencilMaterial
��( 7
.
��7 8
Add
��8 ;
(
��; <
baseMaterial
��< H
,
��H I
(
��J K
$num
��K L
<<
��M O
$num
��P Q
)
��Q R
,
��R S
	StencilOp
��T ]
.
��] ^
Invert
��^ d
,
��d e
CompareFunction
��f u
.
��u v
Equal
��v {
,
��{ |
(
��} ~
ColorWriteMask��~ �
)��� �
$num��� �
,��� �
(��� �
$num��� �
<<��� �
$num��� �
)��� �
,��� �
(��� �
$num��� �
<<��� �
$num��� �
)��� �
-��� �
$num��� �
)��� �
;��� �
canvasRenderer
�� 
.
�� 
hasPopInstruction
�� 0
=
��1 2
true
��3 7
;
��7 8
canvasRenderer
�� 
.
�� 
popMaterialCount
�� /
=
��0 1
$num
��2 3
;
��3 4
canvasRenderer
�� 
.
�� 
SetPopMaterial
�� -
(
��- .#
_revertUnmaskMaterial
��. C
,
��C D
$num
��E F
)
��F G
;
��G H
}
�� 
else
�� 
{
�� 
canvasRenderer
�� 
.
�� 
hasPopInstruction
�� 0
=
��1 2
false
��3 8
;
��8 9
canvasRenderer
�� 
.
�� 
popMaterialCount
�� /
=
��0 1
$num
��2 3
;
��3 4
}
�� 
return
�� 
_unmaskMaterial
�� "
;
��" #
}
�� 	
public
�� 
void
�� 
FitTo
�� 
(
�� 

�� '
target
��( .
)
��. /
{
�� 	
var
�� 
rt
�� 
=
�� 
	transform
�� 
as
�� !

��" /
;
��/ 0
rt
�� 
.
�� 
pivot
�� 
=
�� 
target
�� 
.
�� 
pivot
�� #
;
��# $
rt
�� 
.
�� 
position
�� 
=
�� 
target
��  
.
��  !
position
��! )
;
��) *
rt
�� 
.
�� 
rotation
�� 
=
�� 
target
��  
.
��  !
rotation
��! )
;
��) *
var
�� 
s1
�� 
=
�� 
target
�� 
.
�� 

lossyScale
�� &
;
��& '
var
�� 
s2
�� 
=
�� 
rt
�� 
.
�� 
parent
�� 
.
�� 

lossyScale
�� )
;
��) *
rt
�� 
.
�� 

localScale
�� 
=
�� 
new
�� 
Vector3
��  '
(
��' (
s1
��( *
.
��* +
x
��+ ,
/
��- .
s2
��/ 1
.
��1 2
x
��2 3
,
��3 4
s1
��5 7
.
��7 8
y
��8 9
/
��: ;
s2
��< >
.
��> ?
y
��? @
,
��@ A
s1
��B D
.
��D E
z
��E F
/
��G H
s2
��I K
.
��K L
z
��L M
)
��M N
;
��N O
rt
�� 
.
�� 
	sizeDelta
�� 
=
�� 
target
�� !
.
��! "
rect
��" &
.
��& '
size
��' +
;
��+ ,
rt
�� 
.
�� 
	anchorMax
�� 
=
�� 
rt
�� 
.
�� 
	anchorMin
�� '
=
��( )
s_Center
��* 2
;
��2 3
}
�� 	
private
�� 
Material
�� 
_unmaskMaterial
�� (
;
��( )
private
�� 
Material
�� #
_revertUnmaskMaterial
�� .
;
��. /
private
�� 
MaskableGraphic
�� 
_graphic
��  (
;
��( )
private
�� 
void
�� 
OnEnable
�� 
(
�� 
)
�� 
{
�� 	
if
�� 
(
�� 
m_FitTarget
�� 
)
�� 
{
�� 
FitTo
�� 
(
�� 
m_FitTarget
�� !
)
��! "
;
��" #
}
�� 
SetDirty
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
private
�� 
void
�� 
	OnDisable
�� 
(
�� 
)
��  
{
�� 	
StencilMaterial
�� 
.
�� 
Remove
�� "
(
��" #
_unmaskMaterial
��# 2
)
��2 3
;
��3 4
StencilMaterial
�� 
.
�� 
Remove
�� "
(
��" ##
_revertUnmaskMaterial
��# 8
)
��8 9
;
��9 :
_unmaskMaterial
�� 
=
�� 
null
�� "
;
��" ##
_revertUnmaskMaterial
�� !
=
��" #
null
��$ (
;
��( )
if
�� 
(
�� 
graphic
�� 
)
�� 
{
�� 
var
�� 
canvasRenderer
�� "
=
��# $
graphic
��% ,
.
��, -
canvasRenderer
��- ;
;
��; <
canvasRenderer
�� 
.
�� 
hasPopInstruction
�� 0
=
��1 2
false
��3 8
;
��8 9
canvasRenderer
�� 
.
�� 
popMaterialCount
�� /
=
��0 1
$num
��2 3
;
��3 4
graphic
�� 
.
�� 
SetMaterialDirty
�� (
(
��( )
)
��) *
;
��* +
}
�� 
SetDirty
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
private
�� 
void
�� 

LateUpdate
�� 
(
��  
)
��  !
{
�� 	
if
�� 
(
�� 
m_FitTarget
�� 
&&
�� 
(
��  
m_FitOnLateUpdate
��  1
||
��2 4
!
��5 6
Application
��6 A
.
��A B
	isPlaying
��B K
)
��K L
)
��L M
{
�� 
FitTo
�� 
(
�� 
m_FitTarget
�� !
)
��! "
;
��" #
}
�� 
	Smoothing
�� 
(
�� 
graphic
�� 
,
�� 
m_EdgeSmoothing
�� .
)
��. /
;
��/ 0
}
�� 	
private
�� 
void
�� 

OnValidate
�� 
(
��  
)
��  !
{
�� 	
SetDirty
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
void
�� 
SetDirty
��
(
�� 
)
�� 
{
�� 	
if
�� 
(
�� 
graphic
�� 
)
�� 
{
�� 
graphic
�� 
.
�� 
SetMaterialDirty
�� (
(
��( )
)
��) *
;
��* +
}
�� 
}
�� 	
private
�� 
static
�� 
void
�� 
	Smoothing
�� %
(
��% &
MaskableGraphic
��& 5
graphic
��6 =
,
��= >
float
��? D
smooth
��E K
)
��K L
{
�� 	
if
�� 
(
�� 
!
�� 
graphic
�� 
)
�� 
return
��  
;
��  !
Profiler
�� 
.
�� 
BeginSample
��  
(
��  !
$str
��! 5
)
��5 6
;
��6 7
var
�� 
canvasRenderer
�� 
=
��  
graphic
��! (
.
��( )
canvasRenderer
��) 7
;
��7 8
var
�� 
currentColor
�� 
=
�� 
canvasRenderer
�� -
.
��- .
GetColor
��. 6
(
��6 7
)
��7 8
;
��8 9
var
�� 
targetAlpha
�� 
=
�� 
$num
��  
;
��  !
if
�� 
(
�� 
graphic
�� 
.
�� 
maskable
��  
&&
��! #
$num
��$ %
<
��& '
smooth
��( .
)
��. /
{
�� 
var
�� 
currentAlpha
��  
=
��! "
graphic
��# *
.
��* +
color
��+ 0
.
��0 1
a
��1 2
*
��3 4
canvasRenderer
��5 C
.
��C D
GetInheritedAlpha
��D U
(
��U V
)
��V W
;
��W X
if
�� 
(
�� 
$num
�� 
<
�� 
currentAlpha
�� $
)
��$ %
{
�� 
targetAlpha
�� 
=
��  !
Mathf
��" '
.
��' (
Lerp
��( ,
(
��, -
$num
��- 2
,
��2 3
$num
��4 :
,
��: ;
smooth
��< B
)
��B C
/
��D E
currentAlpha
��F R
;
��R S
}
�� 
}
�� 
if
�� 
(
�� 
!
�� 
Mathf
�� 
.
�� 

�� $
(
��$ %
currentColor
��% 1
.
��1 2
a
��2 3
,
��3 4
targetAlpha
��5 @
)
��@ A
)
��A B
{
�� 
currentColor
�� 
.
�� 
a
�� 
=
��  
Mathf
��! &
.
��& '
Clamp01
��' .
(
��. /
targetAlpha
��/ :
)
��: ;
;
��; <
canvasRenderer
�� 
.
�� 
SetColor
�� '
(
��' (
currentColor
��( 4
)
��4 5
;
��5 6
}
�� 
Profiler
�� 
.
�� 
	EndSample
�� 
(
�� 
)
��  
;
��  !
}
�� 	
}
�� 
}�� 