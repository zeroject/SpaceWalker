using Godot;
using System;

public partial class Player : RigidBody2D
{

	[ExportCategory("Physics")]
	[Export] float bounciness;

	[Export] private GravitySubject gravitySubject;
	[Export] private Timer standStillTimer;
	private Vector2 lastPosition;
	private bool standStill = false;
	public override void _Ready()
	{
		this.BodyEntered += OnCollisionEntered;
		standStillTimer.Timeout += OnStandStillCheck;
		lastPosition = GlobalPosition;
	}

	public override void _Process(double delta)
	{
		GravityRotation();
		if (standStill)
			gravitySubject.Block(true);
	}

    private void GravityRotation() {
		if (gravitySubject == null)
			return;

		if (gravitySubject.currentEffector == null) {
			//SPIN!
			return;
		}
		Vector2 direction = (gravitySubject.currentEffector.GlobalPosition - GetChild<Node2D>(0).GlobalPosition).Normalized();
		float angle = direction.Angle();
		GetChild<Node2D>(0).Rotation = angle - Mathf.DegToRad(90);
	}

	private void OnCollisionEntered(Node body) {
		if (!(body is Node2D))
			return;

		Vector2 bounceDirection = LinearVelocity.Bounce((GlobalPosition - ((Node2D) body).GlobalPosition).Normalized()).Normalized();
		float speed = LinearVelocity.Length() * bounciness;
		LinearVelocity = bounceDirection * speed;
	}

	private void OnStandStillCheck() {
		if (lastPosition.DistanceTo(GlobalPosition) < 0.25f)
			standStill = true;
		lastPosition = GlobalPosition;
	}
}
