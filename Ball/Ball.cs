using Godot;
using System;

public class Ball : Area2D
{

	private int speed = 100;
	public Vector2 direction = Vector2.Left;
	
	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(float delta)
	{
		speed += (int)delta * 2;
		this.GlobalPosition += speed * direction * delta;
	}
}
