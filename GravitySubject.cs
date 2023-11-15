using Godot;
using System;

public partial class GravitySubject : Node
{

	public bool block {get; private set;}
	public RigidBody2D rb {get; private set;}
	public Gravity currentEffector {get; private set;}

	public override void _Ready()
	{
		if (!(GetParent() is RigidBody2D)) {
			GD.PrintErr("GravitySubject must be a child of a rigidbody2D!");
			return;
		}	
		
		rb = GetParent<RigidBody2D>();

		GravityRegistry.AddGravitySubject(this);
	}


	public void Block(bool b) {
		block = b;
	}

	public void SetEffector(Gravity effector) {
		currentEffector = effector;
	}
}
