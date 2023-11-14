using Godot;
using System;

[Tool] //This needs to be a tool for it to be readable by other tools in the editor
public partial class Gravity : Node2D
{
	[ExportGroup("Functionality")]
	[Export] public float outerRadius {get; private set;}
	[Export] public float innerRadius {get; private set;}

	[Export] public float gravityForce {get; private set;}

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
	}
}
