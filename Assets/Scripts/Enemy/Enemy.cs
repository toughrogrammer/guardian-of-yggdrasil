﻿using UnityEngine;
using System.Collections;
using System;


public class EventEnemyLifeCycle : EventArgs
{
	public GameObject gameObject;
}

public abstract class Enemy : MonoBehaviour {

	public GameObject PrefabExplosionEffect;

	public int MaxHP {
		get;
		set;
	}

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

	public bool IsHPMax {
		get {
			return HP == MaxHP;
		}
	}

	public int DamageCollidedYggdrasil {
		get;
		protected set;
	}

	public int DamageCollidedPlayer {
		get;
		protected set;
	}
	
	public event EventHandler<EventEnemyLifeCycle> OnDeadCallbacks;
	protected RoundManager RoundManager;


	virtual protected void Awake() {
		RoundManager = GameObject.Find ("RoundManager").GetComponent<RoundManager> ();
		RoundManager.OnRoundChangeCallbacks += this.OnRoundChanged;

		// update status by round on awake
		OnRoundChanged( this, new EventRoundChange {round = RoundManager.CurrentRound} );
	}
	
	protected void OnDead() {
		var e = new EventEnemyLifeCycle {gameObject = this.gameObject};
		if (OnDeadCallbacks != null) {
			OnDeadCallbacks(this, e);
		}

		Instantiate(PrefabExplosionEffect, this.transform.position, Quaternion.identity);

		this.transform.parent = null;
		Destroy(this.gameObject);
	}

	protected void OnCollisionEnter(Collision collision) {
		Yggdrasil yggdrasil = collision.collider.gameObject.GetComponent<Yggdrasil> ();
		Player player = collision.collider.gameObject.GetComponent<Player> ();

		if (yggdrasil != null) {
			OnCollidedWithYggdrasil( yggdrasil );
		}
		if (player != null) {
			OnCollidedWithPlayer( player );
		}
	}

	abstract protected void OnCollidedWithYggdrasil (Yggdrasil yggdrasil);

	abstract protected void OnCollidedWithPlayer (Player player);

	abstract protected void OnRoundChanged (object sender, EventRoundChange e);

}
