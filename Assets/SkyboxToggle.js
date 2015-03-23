#pragma strict

var summerdaySkybox:Material;
var summerdayCubemap:Cubemap;
var stormIsComingSkybox:Material;
var stormIsComingCubemap:Cubemap;
var sunsetSemiCloudySkybox:Material;
var sunsetSemiCloudyCubemap:Cubemap;
var rainyDayLondonSkybox:Material;
var rainyDayLondonCubemap:Cubemap;
var semicloudy_afternoonSkybox:Material;
var semicloudy_afternoonCubemap:Cubemap;
var rainyDayMiddlesbroughSkybox:Material;
var rainyDayMiddlesbroughCubemap:Cubemap;
var semiCloudySunsetSkybox:Material;
var semiCloudySunsetCubemap:Cubemap;
var moonShineSkybox:Material;
var moonShineCubemap:Cubemap;
var clearSkySkybox:Material;
var clearSkyCubemap:Cubemap;
var sunriseStarrySkySkybox:Material;
var sunriseStarrySkyCubemap:Cubemap;


function Start () {
RenderSettings.skybox = summerdaySkybox;
GetComponent.<Renderer>().material.SetTexture("_Cube", summerdayCubemap);
}

function Update () {

if (Input.GetKey(KeyCode.Alpha1)) {
	RenderSettings.skybox = summerdaySkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", summerdayCubemap);
}

if (Input.GetKey(KeyCode.Alpha2)) {
	RenderSettings.skybox = stormIsComingSkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", stormIsComingCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha3)) {
	RenderSettings.skybox = sunsetSemiCloudySkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", sunsetSemiCloudyCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha4)) {
	RenderSettings.skybox = rainyDayLondonSkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", rainyDayLondonCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha5)) {
	RenderSettings.skybox = semicloudy_afternoonSkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", semicloudy_afternoonCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha6)) {
	RenderSettings.skybox = rainyDayMiddlesbroughSkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", rainyDayMiddlesbroughCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha7)) {
	RenderSettings.skybox = semiCloudySunsetSkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", semiCloudySunsetCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha8)) {
	RenderSettings.skybox = moonShineSkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", moonShineCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha9)) {
	RenderSettings.skybox = clearSkySkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", clearSkyCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha0)) {
	RenderSettings.skybox = sunriseStarrySkySkybox;
	GetComponent.<Renderer>().material.SetTexture("_Cube", sunriseStarrySkyCubemap);
	
}

}