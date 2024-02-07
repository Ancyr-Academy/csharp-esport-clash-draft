using EsportClash.Core.Players.Model;
using EsportClash.Core.Teams.Model;
using EsportClash.Persistence.SQL.Modules.Players;
using EsportClash.Persistence.SQL.Modules.Teams;

namespace EsportClash.Persistence.SQL.Tests;

public class SmokeTests {
  private readonly DatabaseSetup _db = new();
  private SqlPlayerRepository _playerRepository;
  private SqlTeamRepository _teamRepository;

  [OneTimeSetUp]
  public void BeforeAll() {
     _playerRepository = new SqlPlayerRepository(_db.Context);
     _teamRepository = new SqlTeamRepository(_db.Context);
  }

  [SetUp]
  public void BeforeEach() {
    _playerRepository.ClearAsync().Wait();
    _teamRepository.ClearAsync().Wait();
  }

  [Test]
  public async Task ShouldCreatePlayer() {
    var player = new Player {
      Id = "faker",
      Name = "Faker",
      MainRole = Role.Middle,
    };

    await _playerRepository.CreateAsync(player);

    var fetchedPlayer = await _playerRepository.FindByIdAsync(player.Id);
    Assert.NotNull(fetchedPlayer);
    Assert.That(fetchedPlayer.Id, Is.EqualTo(player.Id));
    Assert.That(fetchedPlayer.Name, Is.EqualTo(player.Name));
    Assert.That(fetchedPlayer.MainRole, Is.EqualTo(player.MainRole));
  }
  
  [Test]
  public async Task ShouldCreateTeam() {
    var player = new Player {
      Id = "faker",
      Name = "Faker",
      MainRole = Role.Middle,
    };
    
    await _playerRepository.CreateAsync(player);
    
    var skt = new Team {
      Id = "skt",
      Name = "SKT"
    };
    
    skt.Join(player, Role.Middle);

    await _teamRepository.CreateAsync(skt);

    var fetchedTeam = await _teamRepository.FindByIdAsync(skt.Id);
    Assert.NotNull(fetchedTeam);
    Assert.That(fetchedTeam.Id, Is.EqualTo(skt.Id));
    Assert.That(fetchedTeam.Name, Is.EqualTo(skt.Name));
    Assert.That(fetchedTeam.HasPlayer(player), Is.True);
  }
}