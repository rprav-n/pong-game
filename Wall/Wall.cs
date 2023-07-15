using Godot;
using System;

public class Wall : Area2D
{

	[Signal]
	private delegate void ball_out(string name);
	
	[Export]
	private Boolean bouncy = false;

	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		
	}
	
	public void _on_Wall_area_entered(Area2D area) 
	{
		if (area is Ball ball) 
		{
			if (bouncy) 
			{
				ball.direction.y *= -1;
			} else 
			{
				this.EmitSignal("ball_out", this.Name);
				ball.QueueFree();
			}
		}
	}
}
