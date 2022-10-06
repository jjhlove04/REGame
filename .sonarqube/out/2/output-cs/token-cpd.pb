öA
BD:\REGame\Assets\BigRookGames\Scripts\Weapons\GunfireController.cs
	namespace 	
BigRookGames
 
. 
Weapons 
{ 
public 

class 
GunfireController "
:# $
MonoBehaviour% 2
{ 
public 
	AudioClip 
GunShotClip $
;$ %
public		 
	AudioClip		 

ReloadClip		 #
;		# $
public

 
AudioSource

 
source

 !
;

! "
public 
AudioSource 
reloadSource '
;' (
public 
Vector2 

audioPitch !
=" #
new$ '
Vector2( /
(/ 0
$num0 3
,3 4
$num5 9
)9 :
;: ;
public 

GameObject 
muzzlePrefab &
;& '
public 

GameObject 
muzzlePosition (
;( )
public 
bool 
autoFire 
; 
public 
float 
	shotDelay 
=  
$num! $
;$ %
public 
bool 
rotate 
= 
true !
;! "
public 
float 
rotationSpeed "
=# $
$num% )
;) *
public 

GameObject 
scope 
;  
public 
bool 
scopeActive 
=  !
true" &
;& '
private 
bool 
lastScopeState #
;# $
[ 	
Tooltip	 
( 
$str Z
)Z [
][ \
public 

GameObject 
projectilePrefab *
;* +
[   	
Tooltip  	 
(   
$str	   ì
+
  î ï
$str!! A
)!!A B
]!!B C
public"" 

GameObject"" %
projectileToDisableOnFire"" 3
;""3 4
[%% 	
SerializeField%%	 
]%% 
private%%  
float%%! &
timeLastFired%%' 4
;%%4 5
private(( 
void(( 
Start(( 
((( 
)(( 
{)) 	
if** 
(** 
source** 
!=** 
null** 
)** 
source** %
.**% &
clip**& *
=**+ ,
GunShotClip**- 8
;**8 9
timeLastFired++ 
=++ 
$num++ 
;++ 
lastScopeState,, 
=,, 
scopeActive,, (
;,,( )
}-- 	
private// 
void// 
Update// 
(// 
)// 
{00 	
if22 
(22 
rotate22 
)22 
{33 
	transform44 
.44 
localEulerAngles44 *
=44+ ,
new44- 0
Vector3441 8
(448 9
	transform449 B
.44B C
localEulerAngles44C S
.44S T
x44T U
,44U V
	transform44W `
.44` a
localEulerAngles44a q
.44q r
y44r s
+55H I
rotationSpeed55J W
,55W X
	transform55Y b
.55b c
localEulerAngles55c s
.55s t
z55t u
)55u v
;55v w
}66 
if99 
(99 
autoFire99 
&&99 
(99 
(99 
timeLastFired99 +
+99, -
	shotDelay99. 7
)997 8
<=999 ;
Time99< @
.99@ A
time99A E
)99E F
)99F G
{:: 

FireWeapon;; 
(;; 
);; 
;;; 
}<< 
if?? 
(?? 
scope?? 
&&?? 
lastScopeState?? &
!=??' )
scopeActive??* 5
)??5 6
{@@ 
lastScopeStateAA 
=AA  
scopeActiveAA! ,
;AA, -
scopeBB 
.BB 
	SetActiveBB 
(BB  
scopeActiveBB  +
)BB+ ,
;BB, -
}CC 
}DD 	
publicKK 
voidKK 

FireWeaponKK 
(KK 
)KK  
{LL 	
timeLastFiredNN 
=NN 
TimeNN  
.NN  !
timeNN! %
;NN% &
varQQ 
flashQQ 
=QQ 
InstantiateQQ #
(QQ# $
muzzlePrefabQQ$ 0
,QQ0 1
muzzlePositionQQ2 @
.QQ@ A
	transformQQA J
)QQJ K
;QQK L
ifTT 
(TT 
projectilePrefabTT  
!=TT! #
nullTT$ (
)TT( )
{UU 

GameObjectVV 
newProjectileVV (
=VV) *
InstantiateVV+ 6
(VV6 7
projectilePrefabVV7 G
,VVG H
muzzlePositionVVI W
.VVW X
	transformVVX a
.VVa b
positionVVb j
,VVj k
muzzlePositionVVl z
.VVz {
	transform	VV{ Ñ
.
VVÑ Ö
rotation
VVÖ ç
,
VVç é
	transform
VVè ò
)
VVò ô
;
VVô ö
}WW 
ifZZ 
(ZZ %
projectileToDisableOnFireZZ )
!=ZZ* ,
nullZZ- 1
)ZZ1 2
{[[ %
projectileToDisableOnFire\\ )
.\\) *
	SetActive\\* 3
(\\3 4
false\\4 9
)\\9 :
;\\: ;
Invoke]] 
(]] 
$str]] 3
,]]3 4
$num]]5 6
)]]6 7
;]]7 8
}^^ 
ifaa 
(aa 
sourceaa 
!=aa 
nullaa 
)aa 
{bb 
ifff 
(ff 
sourceff 
.ff 
	transformff #
.ff# $
	IsChildOfff$ -
(ff- .
	transformff. 7
)ff7 8
)ff8 9
{gg 
sourcehh 
.hh 
Playhh 
(hh  
)hh  !
;hh! "
}ii 
elsejj 
{kk 
AudioSourcemm 
newASmm  %
=mm& '
Instantiatemm( 3
(mm3 4
sourcemm4 :
)mm: ;
;mm; <
ifnn 
(nn 
(nn 
newASnn 
=nn  
Instantiatenn! ,
(nn, -
sourcenn- 3
)nn3 4
)nn4 5
!=nn6 8
nullnn9 =
&&nn> @
newASnnA F
.nnF G!
outputAudioMixerGroupnnG \
!=nn] _
nullnn` d
&&nne g
newASnnh m
.nnm n"
outputAudioMixerGroup	nnn É
.
nnÉ Ñ

audioMixer
nnÑ é
!=
nnè ë
null
nní ñ
)
nnñ ó
{oo 
newASqq 
.qq !
outputAudioMixerGroupqq 3
.qq3 4

audioMixerqq4 >
.qq> ?
SetFloatqq? G
(qqG H
$strqqH O
,qqO P
RandomqqQ W
.qqW X
RangeqqX ]
(qq] ^

audioPitchqq^ h
.qqh i
xqqi j
,qqj k

audioPitchqql v
.qqv w
yqqw x
)qqx y
)qqy z
;qqz {
newASrr 
.rr 
pitchrr #
=rr$ %
Randomrr& ,
.rr, -
Rangerr- 2
(rr2 3

audioPitchrr3 =
.rr= >
xrr> ?
,rr? @

audioPitchrrA K
.rrK L
yrrL M
)rrM N
;rrN O
newASuu 
.uu 
PlayOneShotuu )
(uu) *
GunShotClipuu* 5
)uu5 6
;uu6 7
Destroyxx 
(xx  
newASxx  %
.xx% &

gameObjectxx& 0
,xx0 1
$numxx2 3
)xx3 4
;xx4 5
}yy 
}zz 
}{{ 
} 	
private
ÅÅ 
void
ÅÅ (
ReEnableDisabledProjectile
ÅÅ /
(
ÅÅ/ 0
)
ÅÅ0 1
{
ÇÇ 	
reloadSource
ÉÉ 
.
ÉÉ 
Play
ÉÉ 
(
ÉÉ 
)
ÉÉ 
;
ÉÉ  '
projectileToDisableOnFire
ÑÑ %
.
ÑÑ% &
	SetActive
ÑÑ& /
(
ÑÑ/ 0
true
ÑÑ0 4
)
ÑÑ4 5
;
ÑÑ5 6
}
ÖÖ 	
}
ÜÜ 
}áá Ü
ED:\REGame\Assets\BigRookGames\Scripts\Weapons\ProjectileController.cs
	namespace 	
BigRookGames
 
. 
Weapons 
{ 
public 

class  
ProjectileController %
:& '
MonoBehaviour( 5
{ 
public

 
float

 
speed

 
=

 
$num

  
;

  !
public 
	LayerMask 
collisionLayerMask +
;+ ,
public 

GameObject 
rocketExplosion )
;) *
public 
MeshRenderer 
projectileMesh *
;* +
private 
bool 
	targetHit 
; 
public 
AudioSource 
inFlightAudioSource .
;. /
public 
ParticleSystem 
disableOnHit *
;* +
private 
void 
Update 
( 
) 
{ 	
if   
(   
	targetHit   
)   
return   !
;  ! "
	transform## 
.## 
position## 
+=## !
	transform##" +
.##+ ,
forward##, 3
*##4 5
(##6 7
speed##7 <
*##= >
Time##? C
.##C D
	deltaTime##D M
)##M N
;##N O
}$$ 	
private++ 
void++ 
OnCollisionEnter++ %
(++% &
	Collision++& /
	collision++0 9
)++9 :
{,, 	
if.. 
(.. 
!.. 
enabled.. 
).. 
return..  
;..  !
Explode11 
(11 
)11 
;11 
projectileMesh22 
.22 
enabled22 "
=22# $
false22% *
;22* +
	targetHit33 
=33 
true33 
;33 
inFlightAudioSource44 
.44  
Stop44  $
(44$ %
)44% &
;44& '
foreach55 
(55 
Collider55 
col55  
in55! #
GetComponents55$ 1
<551 2
Collider552 :
>55: ;
(55; <
)55< =
)55= >
{66 
col77 
.77 
enabled77 
=77 
false77 #
;77# $
}88 
disableOnHit99 
.99 
Stop99 
(99 
)99 
;99  
Destroy== 
(== 

gameObject== 
,== 
$num==  "
)==" #
;==# $
}>> 	
privateDD 
voidDD 
ExplodeDD 
(DD 
)DD 
{EE 	

GameObjectGG 
newExplosionGG #
=GG$ %
InstantiateGG& 1
(GG1 2
rocketExplosionGG2 A
,GGA B
	transformGGC L
.GGL M
positionGGM U
,GGU V
rocketExplosionGGW f
.GGf g
	transformGGg p
.GGp q
rotationGGq y
,GGy z
nullGG{ 
)	GG Ä
;
GGÄ Å
}JJ 	
}KK 
}LL 