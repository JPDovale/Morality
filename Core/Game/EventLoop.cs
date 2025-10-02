using System.Diagnostics;
using Morality.Core.Shared.Entities;

namespace Morality.Core.Game;

class EventLoopProps
{
  public required bool running;
  public required int frames_per_second;
  public required bool log_frames_per_second;
  public required Action Middleware;
}

class CreateEventLoopProps
{
  public bool running = true;
  public int frames_per_second = 60;
  public bool log_frames_per_second = true;
  public Action Middleware = () => Console.WriteLine("[X] Middleware not defined");
}

class EventLoop : Entity<EventLoopProps>
{
  protected EventLoop(EventLoopProps props, UniqueId? id = null) : base(props, id) { }

  public static EventLoop Create(CreateEventLoopProps props, UniqueId? id = null)
  {
    return new EventLoop(new EventLoopProps
    {
      frames_per_second = props.frames_per_second,
      log_frames_per_second = props.log_frames_per_second,
      Middleware = props.Middleware,
      running = props.running
    }, id);
  }

  public void Start()
  {
    double targetFrameTime = 1_000_000_000.0 / this.frames_per_second;
    var stopWatch = new Stopwatch();

    int frameCount = 0;
    var fpsTimer = Stopwatch.StartNew();

    Console.WriteLine($"Initializing game loop a {this.frames_per_second} FPS");

    while (this.running)
    {
      long startTicks = stopWatch.ElapsedTicks;
      stopWatch.Restart();

      this.Middleware();

      frameCount++;
      if (fpsTimer.Elapsed.TotalSeconds >= 1)
      {
        if (this.log_frames_per_second)
        {
          Console.WriteLine($"Running a {frameCount} FPS");
        }

        frameCount = 0;
        fpsTimer.Restart();
      }


      long elapsedTicks = stopWatch.ElapsedTicks;
      double elapsedNs = (double)elapsedTicks / Stopwatch.Frequency * 1_000_000_000.0;

      double remaining = targetFrameTime - elapsedNs;

      if (remaining > 0)
      {
        if (remaining > 200_000)
        {
          int sleepMs = (int)(remaining / 1_000_000) - 1;
          if (sleepMs > 0) Thread.Sleep(sleepMs);
        }

        while (true)
        {
          elapsedTicks = stopWatch.ElapsedTicks;
          elapsedNs = (double)elapsedTicks / Stopwatch.Frequency * 1_000_000_000.0;

          if (elapsedNs >= targetFrameTime)
            break;
        }
      }
    }
  }

  public bool running
  {
    get { return this.props.running; }
  }

  public int frames_per_second
  {
    get { return this.props.frames_per_second; }
  }

  public bool log_frames_per_second
  {
    get { return this.props.log_frames_per_second; }
  }

  public Action Middleware
  {
    get { return this.props.Middleware; }
  }
}
