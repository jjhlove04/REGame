�
<D:\REGame\Assets\Samples\UI Unmask\1.4.0\Demo\Unmask_Demo.cs
	namespace 	
Coffee
 
. 
UIExtensions 
. 
Demos #
{ 
public 

class 
Unmask_Demo 
: 

{ 
[ 	
SerializeField	 
] 
Unmask 
unmask  &
=' (
null) -
;- .
[		 	
SerializeField			 
]		 
Unmask		 
[		  
]		  !
smoothingUnmasks		" 2
=		3 4
new		5 8
Unmask		9 ?
[		? @
$num		@ A
]		A B
;		B C
[

 	
SerializeField

	 
]

 
Graphic

  

transition

! +
=

, -
null

. 2
;

2 3
[ 	
SerializeField	 
] 
Image 
transitionImage .
=/ 0
null1 5
;5 6
[ 	
SerializeField	 
] 
Sprite 

unity_chan  *
=+ ,
null- 1
;1 2
[
SerializeField
]
Sprite
unity_frame
=
null
;
public 
void 
AutoFitToButton #
(# $
bool$ (
flag) -
)- .
{ 	
unmask 
. 
fitOnLateUpdate "
=# $
flag% )
;) *
} 	
public 
void 
SetTransitionColor &
(& '
bool' +
flag, 0
)0 1
{ 	

transition 
. 
color 
= 
flag #
?$ %
Color& +
.+ ,
white, 1
:2 3
Color4 9
.9 :
black: ?
;? @
} 	
public 
void 
SetTransitionImage &
(& '
bool' +
flag, 0
)0 1
{ 	
transitionImage 
. 
sprite "
=# $
flag% )
?* +

unity_chan, 6
:7 8
unity_frame9 D
;D E
transitionImage 
. 

() *
)* +
;+ ,
var 
size 
= 
transitionImage &
.& '

.4 5
rect5 9
.9 :
size: >
;> ?
transitionImage 
. 

.) *
	sizeDelta* 3
=4 5
new6 9
Vector2: A
(A B
$numB E
,E F
sizeG K
.K L
yL M
/N O
sizeP T
.T U
xU V
*W X
$numY \
)\ ]
;] ^
} 	
public!! 
void!! 
EnableSmoothing!! #
(!!# $
bool!!$ (
flag!!) -
)!!- .
{"" 	
foreach## 
(## 
var## 
unmask## 
in##  "
smoothingUnmasks### 3
)##3 4
{$$ 
unmask%% 
.%% 

=%%% &
flag%%' +
?%%, -
$num%%. /
:%%0 1
$num%%2 3
;%%3 4
}&& 
}'' 	
}(( 
})) 