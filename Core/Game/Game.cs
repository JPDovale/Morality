using Morality.Core.Shared.Entities;

namespace Morality.Core.Game;

class GameProps
{
  public required Simulation simulation;
  public required EventLoop event_loop;
}

class CreateGameProps
{
  public int frames_per_second = 60;
  public required Simulation simulation;
}

class Game : Entity<GameProps>
{
  protected Game(GameProps props, UniqueId? id = null) : base(props, id) { }

  public static Game Create(CreateGameProps props, UniqueId? id = null)
  {

    EventLoop eventLoop = EventLoop.Create(new CreateEventLoopProps
    {
      frames_per_second = props.frames_per_second,
      log_frames_per_second = true,
      running = true,
      Middleware = () =>
      {
        props.simulation.Update();
        props.simulation.Render();
      }
    });

    return new Game(new GameProps { event_loop = eventLoop, simulation = props.simulation }, id);
  }

  public void Start()
  {
    StartBasics.Setup(this.props.simulation);
    this.props.event_loop.Start();
  }
}