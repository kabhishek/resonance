using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CollisionEvent : UnityEvent<float, IWaveInfo>
{
}

public interface ICollisionInfo
{
	CollisionEvent PlayerCollision
	{
		get;
		set;
	}
}
