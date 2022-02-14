# Match3

<h2>Controls:</h2> 

Click on ball and slide into desired direction.

Esc - Quit

<h2> WebGL version to check </h2>
https://play.unity.com/mg/other/match3web

<h2>Other</h2>
Most of data used and shared by core mechanics(spawn, destroy on match, movement) is stored in one scriptable object file called BoardData.

With board data being held in separate object each level can be easily adjusted.

There are two levels made so far. Next levels could be easily made with different win conditions like destroying certain amount of specific color, spawning certain amount of special objects, getting certain amount of match combos or just be re-sized with different timings-score to beat.


DOTween was used for movement and collapsing after match.



<h2>Special Objects:</h2>
Color Bomb - destroys all objects of same color

Mass bomb - destroys all objects in area of itself (has to be matched again with same color)

Both objects are spawned for fixed amount of matches.






https://user-images.githubusercontent.com/80861162/153815880-4fbf5be8-24ad-446b-8f7b-42f9f5611714.mp4



https://user-images.githubusercontent.com/80861162/153815901-1d131306-68a9-4e51-b065-a38ae2fd0dd3.mp4

