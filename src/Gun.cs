using Godot;
using System;

public partial class Gun : Sprite2D
{
	private Marker2D _marker_2d;
	readonly PackedScene BULLET = GD.Load<PackedScene>("res://src/bullet.tscn");
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_marker_2d = GetNode<Marker2D>("Marker2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		LookAt(GetGlobalMousePosition());
	}

	public void Shoot()
	{
		Bullet bullet = BULLET.Instantiate<Bullet>();
		bullet.Position = _marker_2d.GlobalPosition;
		bullet.TargetPosition = (GetGlobalMousePosition() - _marker_2d.GlobalPosition).Normalized();
		GetTree().Root.AddChild(bullet);
	}
}
