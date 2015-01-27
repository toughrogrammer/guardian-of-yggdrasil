﻿using UnityEngine;
using System.Collections;

public class FlightController : MonoBehaviour
{
	public float RotationPowerRoll = 0.3f;
	public float RotationPowerYaw = 0.3f;
	public float RotationPowerPitch = 0.3f;

	public float UnitPerMeter = 10;
    public float KilometerPerHour = 180.0f;
	public float RollDampingFactor = 1.0f;

	const int MeterPerKilometer = 1000;
	const int SecondsPerHour = 3600;


    void Start() {

    }

	void Update () {
		Vector3 dir = new Vector3(-Input.GetAxis("Vertical") * RotationPowerRoll, Input.GetAxis("Horizontal") * RotationPowerYaw);
		this.transform.Rotate(dir);

		Vector3 euler = this.transform.eulerAngles;
		float rollDamping;
		if( euler.z > 180 ) {
			rollDamping = 360 - euler.z;
		} else {
			rollDamping = -euler.z;
		}
		this.transform.Rotate(0, 0, rollDamping * Time.deltaTime * RollDampingFactor);


		float meterPerSecond = KilometerPerHour * MeterPerKilometer / SecondsPerHour;
		float unitPerSecond = meterPerSecond / UnitPerMeter;
		float fps = 1.0f / Time.deltaTime;
		float speedFactor = unitPerSecond / fps;
		this.transform.position += this.transform.forward * speedFactor;
	}
}