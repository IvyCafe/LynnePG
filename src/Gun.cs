using Godot;
using System;

public partial class Gun : Node2D
{
	[Export]
	float shootSpeed = 2.0f;

	bool canShoot = true;
	Vector2 bulletDirection = new(1, 0);
	private Marker2D _marker;
	private Timer _timer;
	readonly PackedScene BULLET = GD.Load<PackedScene>("res://src/bullet.tscn");

	public override void _Ready()
	{
		Connect("timeout", new Callable(this, nameof(_OnShootSpeedTimerTimeout)));

		_marker = GetNode<Marker2D>("Marker2D");
		_timer = GetNode<Timer>("ShootSpeedTimer");

		_timer.WaitTime = 1.0f / shootSpeed;
	}

	public override void _PhysicsProcess(double delta)
	{
		// SetupDirection();
		// LookAt(GetGlobalMousePosition());
		// MoveAndSlide();
	}

	public void _OnShootSpeedTimerTimeout()
	{
		canShoot = true;
	}

	public void Shoot()
	{
		if (canShoot)
		{
			canShoot = false;
			_timer.Start();

			Bullet bulletNode = (Bullet)BULLET.Instantiate();

			bulletNode.SetDirection(bulletDirection);
			GetTree().Root.AddChild(bulletNode);
			bulletNode.GlobalPosition = _marker.GlobalPosition;
		}
	}

	public void SetupDirection(Vector2 direction)
	{
		bulletDirection = direction;

		Vector2 mousePosition = GetGlobalMousePosition();
		float mouseRad = mousePosition.Y / mousePosition.X;
		float mouseDeg = Mathf.RadToDeg(mouseRad);
		RotationDegrees = mouseDeg;
	}
}
