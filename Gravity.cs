using Godot;
using System;
using System.Collections.Generic;

[Tool] //This needs to be a tool for it to be readable by other tools in the editor
public partial class Gravity : Node2D
{
	[ExportGroup("Functionality")]
	[Export] public float outerRadius {get; private set;}
	[Export] public float innerRadius {get; private set;}

	[Export] public float gravityForce {get; private set;}
	[Export] private Curve gravityCurve;

	[ExportGroup("Visuals")]
	[Export] public int gravityRangeResolution {get; private set;}

	public override void _Ready()
	{
		//Important, do not remove! Code goes below this check!
		if (Engine.IsEditorHint())
			return;
	}

	public override void _Process(double delta)
	{
		//Important, do not remove! Code goes below this check!
		if (Engine.IsEditorHint())
			return;

		foreach (GravitySubject subject in GravityRegistry.gravitySubjects)
		{
			float distance = subject.rb.GlobalPosition.DistanceTo(this.GlobalPosition);
			
			if (subject.block)
				return;
			
			if (distance > outerRadius)
				return;
			
			float normalizedDistance = InterpolationUtility.InverseLerp(innerRadius, outerRadius, distance);
			float distanceMultiplier = gravityCurve.Sample(normalizedDistance);
			float calculatedForce = gravityForce * distanceMultiplier;

			Vector2 gravityDirection = (this.GlobalPosition - subject.rb.GlobalPosition).Normalized();

			subject.rb.LinearVelocity += gravityDirection * calculatedForce * (float) delta;
		}
	}
}
