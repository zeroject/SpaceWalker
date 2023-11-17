using Godot;
using System;

public partial class Asteroid : Node2D
{
	public override void _Ready()
	{
		Rotation = (float) Mathf.DegToRad(GD.RandRange(0f, 360f));
	}
}
