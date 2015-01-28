﻿using UnityEngine;
using System.Collections;
using System;


public class EventEnemyLifeCycle : EventArgs
{
	public GameObject gameObject;
}

public class Enemy : MonoBehaviour {

	int _hp;
	public int HP {
		get {
			return _hp;
		}
		set {
			_hp = value;
			if( _hp <= 0 ) {
				OnDead();
			}
		}
	}
	
	public event EventHandler<EventEnemyLifeCycle> OnDeadCallbacks;


	protected void OnDead() {
		var e = new EventEnemyLifeCycle {gameObject = this.gameObject};
		OnDeadCallbacks(this, e);

		this.transform.parent = null;
		Destroy(this.gameObject);
	}

}