namespace CustomControlsDemo.Controls;

public class CircularProgressBarDrawable : IDrawable
{
  private readonly IProgress _progressBar;

  public CircularProgressBarDrawable(IProgress progressBar)
  {
    _progressBar = progressBar;
  }
  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    float lineWidth = (float)Math.Round(dirtyRect.Height * 0.1);
    float centerX = dirtyRect.Width / 2;
    float centerY = dirtyRect.Height / 2;
    float radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 - lineWidth / 2;

    canvas.StrokeSize = lineWidth;

    // Draw the background circle
    canvas.StrokeColor = Colors.LightGray;
    canvas.StrokeSize = lineWidth;
    canvas.DrawCircle(centerX, centerY, radius);

    // Draw the progress circle
    canvas.StrokeColor = _progressBar.ProgressColor;
    if (Math.Round(_progressBar.Progress, 2) < 1)
    {
      var startAngle = 90;
      var endAngle = startAngle - (float)(_progressBar.Progress * 360);
      var arcHeight = (float)radius * 2;
      var arcWidth = arcHeight;
      var x = centerX - radius;
      var y = lineWidth / 2;
      canvas.DrawArc(x, y, arcWidth, arcHeight, startAngle, endAngle, true, false);
    }
    else
    {
      canvas.DrawCircle(centerX, centerY, radius);
    }

    // Draw the progress text
    float fontSize = radius * 0.66f;
    canvas.FontSize = fontSize;
    canvas.FontColor = _progressBar.ProgressColor;

    // float verticalPosition = centerY + fontSize/3.5f;
    //canvas.DrawString($"{_progressBar.Progress*100:0}", centerX, verticalPosition,HorizontalAlignment.Center); 
    canvas.DrawString($"{_progressBar.Progress * 100:0}", 0, 0, dirtyRect.Width, dirtyRect.Height, HorizontalAlignment.Center, VerticalAlignment.Center);

  }
}
