﻿using UnityEngine;
using System.Collections;

// The player object
public class PlayerController : MonoBehaviour {
	
	// Define health
	public int health = 100;
	public int lives = 5;
	public int deaths = 0;
	
	public float respawn_x = -59.28231F;
	public float respawn_y = 59.11393F;
	public float respawn_z = 53.60716F;
	
	public float death_height = 50;
	public bool death = false;
	
	// Handle damage for the character
	public void damage(int damage){
		health -= damage;
		
		// Handle death if you're out of health
		if (health < 1) {
			handle_death();
			health = 100;
		}
	}
	
	// Handle player update
	void Update(){
		
		// You might be falling!
		if (transform.position.y <= death_height)
			falling ();
	}
	
	// OMG You're falling
	void falling(){
		if (!death) {
			lives -= 1;
			transform.position = new Vector3 (respawn_x, respawn_y, respawn_z);
			
			if (lives <= 0)
				handle_death();
		} else {
			handle_death();
		}
		
	}
	
	// Respawn the character at the spawn point
	void respawn(){
		transform.position = new Vector3 (respawn_x, respawn_y, respawn_z);
		
		// Reset the lives and health
		lives = 5;
		health = 100;
		deaths += 1;
	}
	
	// Check for lives and kill the character if you need too;
	void handle_death(){
		
		// check if dead already
		if (death) {
			respawn();
		} else {
			
			// Reduce the lives
			lives -= 1;
			
			// If you are out of lives, die
			if (lives < 1)
				death = true;
			
			transform.position = new Vector3 (respawn_x, respawn_y, respawn_z);
		}
	}
	
	// Handle damage for the player
	void OnCollisionEnter(Collision col){
		
		//		Debug.Log(col);
		//		Debug.Log(col.gameObject.name);
		
		if (col.gameObject.name == "health_globe") {
			add_health(20);
			Destroy(col.gameObject);
		}
		
		if (col.gameObject.name == "enemy") {
			
			health-=5;
			
			if (health < 1) {
				handle_death();
				health = 100;
			}
		}
		
		if (col.gameObject.name == "spike_trap") {
			health-=20;
			
			if (health < 1) {
				handle_death();
				health = 100;
			}
		}
	}
	
	// Handle adding new health to the user
	void add_health(int new_health){
		int max_health = 100;
		int future_health = health + new_health;
		
		if (future_health >= max_health) {
			health = max_health;
		} else {
			health += new_health;
		}
	}
	
	void OnGUI(){
		//GUI.Button (Rect (20, 20, health, 20), "");
	}
}