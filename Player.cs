using Godot;
using System;

public partial class Player : RigidBody2D
{
	[ExportCategory("Physics")]
	[Export] float bounciness;

	[Export] private GravitySubject gravitySubject;

	[ExportCategory("Mobility Checks")]
	[Export] private float standStillVelocity;
	[Export] private Timer standStillTimer;
	[Export] private float standStileDistance;
	private Vector2 lastPosition;
	private bool standStill = false;
	private Transform2D checkPoint;

	public override void _Ready()
	{
		this.BodyEntered += OnCollisionEntered;
		standStillTimer.Timeout += OnStandStillCheck;
		lastPosition = GlobalPosition;
	}

	public override void _Process(double delta)
	{
		GravityRotation();
		
		if (standStill) {
			gravitySubject.Block(true);
			if (Input.IsActionJustPressed("Click"))
				Jump();
		}
		else {
			gravitySubject.Block(false);
		}
	}


    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        if (Input.IsActionJustPressed("Reset")) {
			ResetToCheckpoint(state);
		}
    }

    private void Jump() {
		SetStandStill(false);
		Vector2 direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
		LinearVelocity = direction * 3f * Mathf.Sqrt(gravitySubject.currentEffector.gravityForce) * Mathf.Sqrt(gravitySubject.currentEffector.outerRadius * 0.05f);
	}

	private void GravityRotation() {
		if (gravitySubject == null || gravitySubject.currentEffector == null)
			return;

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

		if (LinearVelocity.Length() < standStillVelocity && gravitySubject.currentEffector != null) {
			GD.Print("Stand still velocity check passed!");
			SetStandStill(true);
		}
	}

	private void OnStandStillCheck() {
		if (gravitySubject.currentEffector == null) {
			SetStandStill(false);
			return;
		}

		if (GlobalPosition.DistanceTo(lastPosition) < standStileDistance) {
			if (standStill == false) GD.Print("Stand still time check passed!");
			SetStandStill(true);
		}

		lastPosition = GlobalPosition;
	}

	private void SetStandStill(bool b) {
		standStill = b;
		gravitySubject.Block(b);

		if (b) {
			LinearVelocity = Vector2.Zero;
			checkPoint = Transform;	
		}
	}

	private void ResetToCheckpoint(PhysicsDirectBodyState2D state) {
		state.Transform = checkPoint;
	}
}
