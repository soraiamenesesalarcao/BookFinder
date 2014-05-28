#pragma strict
var scrollSpeed = 0.25;
function FixedUpdate(){
    var offset = Time.time * scrollSpeed;
    renderer.material.mainTextureOffset = Vector2(0,-offset);
    renderer.material.SetTextureOffset("_BumpMap", Vector2(0,-offset));
}