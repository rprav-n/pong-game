using Godot;
using System;

enum PaddleLocation 
{
	LEFT,
	RIGHT
}

public class Paddle : Area2D
{

	[Export]
	private PaddleLocation paddleLocation = PaddleLocation.LEFT;
	private const int SPEED = 100;
	private string inputName;

	public override void _Ready()
	{
		inputName = paddleLocation == PaddleLocation.LEFT ? "left" : "right";
	}

	public override void _PhysicsProcess(float delta)
	{
		
		if (Input.IsActionPressed(inputName + "_move_up")) 
		{
			this.GlobalPosition -= new Vector2(0, SPEED * delta);
		}
		
		if (Input.IsActionPressed(inputName + "_move_down")) 
		{
			this.GlobalPosition += new Vector2(0, SPEED * delta);	
		}
		
		this.GlobalPosition = new Vector2(this.GlobalPosition.x, (float)Mathf.Clamp((int)this.GlobalPosition.y, 0, 360));
		
	}
	
	public void _on_Paddle_area_entered(Area2D area) 
	{
		
		if (area is Ball ball) 
		{
			
			ball.direction.x *= -1;
			ball.direction.y = (ball.GlobalPosition.y - this.GlobalPosition.y)/16;

		}
	}

}
