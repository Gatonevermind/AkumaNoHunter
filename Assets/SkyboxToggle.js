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
renderer.material.SetTexture("_Cube", summerdayCubemap);
}

function Update () {

if (Input.GetKey(KeyCode.Alpha1)) {
	RenderSettings.skybox = summerdaySkybox;
	renderer.material.SetTexture("_Cube", summerdayCubemap);
}

if (Input.GetKey(KeyCode.Alpha2)) {
	RenderSettings.skybox = stormIsComingSkybox;
	renderer.material.SetTexture("_Cube", stormIsComingCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha3)) {
	RenderSettings.skybox = sunsetSemiCloudySkybox;
	renderer.material.SetTexture("_Cube", sunsetSemiCloudyCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha4)) {
	RenderSettings.skybox = rainyDayLondonSkybox;
	renderer.material.SetTexture("_Cube", rainyDayLondonCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha5)) {
	RenderSettings.skybox = semicloudy_afternoonSkybox;
	renderer.material.SetTexture("_Cube", semicloudy_afternoonCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha6)) {
	RenderSettings.skybox = rainyDayMiddlesbroughSkybox;
	renderer.material.SetTexture("_Cube", rainyDayMiddlesbroughCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha7)) {
	RenderSettings.skybox = semiCloudySunsetSkybox;
	renderer.material.SetTexture("_Cube", semiCloudySunsetCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha8)) {
	RenderSettings.skybox = moonShineSkybox;
	renderer.material.SetTexture("_Cube", moonShineCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha9)) {
	RenderSettings.skybox = clearSkySkybox;
	renderer.material.SetTexture("_Cube", clearSkyCubemap);
	
}

if (Input.GetKey(KeyCode.Alpha0)) {
	RenderSettings.skybox = sunriseStarrySkySkybox;
	renderer.material.SetTexture("_Cube", sunriseStarrySkyCubemap);
	
}

}