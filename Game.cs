using Godot;
using System;

public class Game : Node2D
{

	private PackedScene Ball;
	private Node2D BallContainer;
	private Position2D BallSpawnPosition;
	private Timer ResetTimer;
	private Label ScoreLabel;
	private Position2D LeftPaddleStartPosition;
	private Position2D RightPaddleStartPosition;
	private Area2D LeftPaddle;
	private Area2D RightPaddle;
	
	private const int WINCOUNT = 2;
	
	private Vector2 score = new Vector2(0, 0);
	
	public override void _Ready()
	{
		Ball = ResourceLoader.Load<PackedScene>("res://Ball/Ball.tscn");
		BallContainer = GetNode<Node2D>("BallContainer");
		BallSpawnPosition = GetNode<Position2D>("BallContainer/BallSpawnPosition");
		ResetTimer = GetNode<Timer>("ResetTimer");
		ScoreLabel = GetNode<Label>("Graphics/ScoreLabel");
		
		LeftPaddle = GetNode<Area2D>("PaddleContainer/LeftPaddle");
		RightPaddle = GetNode<Area2D>("PaddleContainer/RightPaddle");
		LeftPaddleStartPosition = GetNode<Position2D>("PaddleContainer/LeftPaddleStartPosition");
		RightPaddleStartPosition = GetNode<Position2D>("PaddleContainer/RightPaddleStartPosition");
		
		_on_ResetTimer_timeout(); // This is a timeout signal 
	}

	public void _on_ball_out(string name) 
	{
		switch (name)
		{	
			case "LeftWall":
				score.y += 1;
				break;
			case "RightWall":
				score.x += 1;
				break;
			default:
				break;
		}
		UpdateScoreLabel();
		if (score.x >= WINCOUNT || score.y >= WINCOUNT) 
		{
			GameOver();
		} else 
		{
			ResetTimer.Start();	
		}
		
	}
	
	public void _on_ResetTimer_timeout() 
	{
		
		var ball = Ball.Instance<Area2D>();
		
		//ball.GlobalPosition = new Vector2(320, 180); // This positions the ball at the center
		ball.GlobalPosition = BallSpawnPosition.GlobalPosition;

		//BallContainer.AddChild(ball);
		BallContainer.CallDeferred("add_child", ball);
	}
	
	public void UpdateScoreLabel() 
	{
		ScoreLabel.Text = string.Format("{0} - {1}", score.x, score.y);
	}


	public void GameOver() 
	{
		score = Vector2.Zero;
		UpdateScoreLabel();
		ResetPaddlePosition();
		ResetTimer.Start();
	}
	
	private void ResetPaddlePosition() 
	{
		LeftPaddle.GlobalPosition = LeftPaddleStartPosition.GlobalPosition;
		RightPaddle.GlobalPosition = RightPaddleStartPosition.GlobalPosition;
	}

}
