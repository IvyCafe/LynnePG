using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 1000.0f;
	// [Export]
	// public int Damage { get; set; } = 150;

	public Vector2 TargetPosition { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Connect("body_entered", new Callable(this, nameof(_OnBodyEntered)));

		LookAt(GetGlobalMousePosition());
		GetTree().CreateTimer(3).Timeout += QueueFree; // Despawn
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Velocity = TargetPosition * Speed;
		// Velocity = TargetPosition * Speed * (float)delta;
		MoveAndSlide();
	}
	
	// public void _OnBodyEntered(Node2D body)
	// {
	// 	if (body.IsInGroup("enemies"))
	// 	{
	// 		body.GetDamage(Damage);
	// 		QueueFree();
	// 	}
	// }
}
