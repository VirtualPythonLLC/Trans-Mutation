﻿using UnityEngine;
using System.Collections;

/*
 * Player Input
 * 
 * Handles Player input
*/
[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
	bool upPressed;
	bool selectPressed;

	void Start (){
		player = GetComponent<Player> ();
	}

	void Update (){
		if (player.IsDead()) // When dead player cannot do anything
			return;
		float hInput = Input.GetAxisRaw ("Horizontal");
		float vInput = Input.GetAxisRaw ("Vertical");
		Vector2 directionalInput = new Vector2 (hInput, vInput);
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)){
			player.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)){
			player.OnJumpInputUp ();
		}
		if (Input.GetAxisRaw("Fire1") > 0){
			player.GetPlayerWeapon().Fire(vInput, hInput);
		}
		if (Input.GetAxisRaw("Select") > 0 && !selectPressed){
			player.GetPlayerWeapon().ChangeWeapon();
			selectPressed = true;
		}
		if (Input.GetAxisRaw("Select") == 0){
			selectPressed = false;
		}
		if ( vInput > 0){
			if (!upPressed && vInput > 0 && player.IsGrounded()){
				if (player.GetController().collidesWithTrigger() && player.GetController().getHitObject()){
					Switch s = player.GetController().getHitObject().GetComponent<Switch>();
					if (s){
						s.Press();
					}
				}
			}
			upPressed = true;
		}
		if (vInput == 0) //Only can re-press a button if player releases the 'use' input
			upPressed = false;
	}
}
