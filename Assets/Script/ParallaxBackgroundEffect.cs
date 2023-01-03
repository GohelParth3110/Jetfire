using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundEffect : MonoBehaviour
{

	[SerializeField] private Renderer background;
	[SerializeField] private float speedBG;
	[SerializeField] private Renderer Midground;
	[SerializeField] private float speedMG;
	[SerializeField] private Renderer Forceground;
	[SerializeField] private float speedFG;

	private float offset = 0;

	[SerializeField]private Transform targetCamera;
	private float startPosX;

	private void Start()
	{
		startPosX = targetCamera.position.x;
	}

	private void FixedUpdate()
	{
		float diff = targetCamera.position.x - startPosX;


        offset = (diff * speedBG) % 1;
		background.material.mainTextureOffset = new Vector2(offset, background.material.mainTextureOffset.y);


        offset = (diff * speedMG) % 1;
		Midground.material.mainTextureOffset = new Vector2(offset, Midground.material.mainTextureOffset.y);

		offset = (diff * speedFG) % 1;
		Forceground.material.mainTextureOffset = new Vector2(offset, Forceground.material.mainTextureOffset.y);

	}
}
