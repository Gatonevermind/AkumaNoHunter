#pragma strict
var xRotateSpeed : float = 10;
var yRotateSpeed : float = 10;
var zRotateSpeed : float = 10;
function Start () {

}

function Update () {

transform.Rotate(xRotateSpeed * Time.deltaTime,yRotateSpeed * Time.deltaTime, zRotateSpeed * Time.deltaTime);

}