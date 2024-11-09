namespace CustomControlsDemo.Controls;

public class CircularProgressBarDrawable : IDrawable
{
  public Color ProgressColor { get; set; } = Colors.Blue;

  public double Progress { get; set; }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    float lineWidth = (float)Math.Round(dirtyRect.Height * 0.1);
    float centerX = dirtyRect.Width / 2;
    float centerY = dirtyRect.Height / 2;
    float radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 - lineWidth / 2;

    canvas.StrokeSize = lineWidth;

    DrawBackgroundCircle(canvas, lineWidth, centerX, centerY, radius);
    DrawProgressCircle(canvas, lineWidth, centerX, centerY, radius);
    DrawProgressText(canvas, dirtyRect, radius);

  }

  private void DrawProgressText(ICanvas canvas, RectF dirtyRect, float radius)
  {
    // Draw the progress text
    float fontSize = radius * 0.66f;
    canvas.FontSize = fontSize;
    canvas.FontColor = ProgressColor;

    // float verticalPosition = centerY + fontSize/3.5f;
    //canvas.DrawString($"{Progress*100:0}", centerX, verticalPosition,HorizontalAlignment.Center); 
    canvas.DrawString($"{Progress * 100:0}", 0, 0, dirtyRect.Width, dirtyRect.Height, HorizontalAlignment.Center, VerticalAlignment.Center);
  }

  private void DrawProgressCircle(ICanvas canvas, float lineWidth, float centerX, float centerY, float radius)
  {
    // Draw the progress circle
    canvas.StrokeColor = ProgressColor;
    if (Math.Round(Progress, 2) < 1)
    {
      var startAngle = 90;
      var endAngle = startAngle - (float)(Progress * 360);
      var arcHeight = (float)radius * 2;
      var arcWidth = arcHeight;
      var paddingLeft = centerX - radius;
      var paddingTop = centerY - radius; // lineWidth / 2;
      canvas.DrawArc(paddingLeft, paddingTop, arcWidth, arcHeight, startAngle, endAngle, true, false);
    }
    else
    {
      canvas.DrawCircle(centerX, centerY, radius);
    }
  }

  private static void DrawBackgroundCircle(ICanvas canvas, float lineWidth, float centerX, float centerY, float radius)
  {
    // Draw the background circle
    canvas.StrokeColor = Colors.LightGray;
    canvas.StrokeSize = lineWidth;
    canvas.DrawCircle(centerX, centerY, radius);
  }
}
