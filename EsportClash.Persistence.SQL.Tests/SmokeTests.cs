using EsportClash.Core.Players.Model;
using EsportClash.Core.Teams.Model;
using EsportClash.Persistence.SQL.Modules.Players;
using EsportClash.Persistence.SQL.Modules.Teams;

namespace EsportClash.Persistence.SQL.Tests;

public class SmokeTests {
  private readonly DatabaseSetup _db = new DatabaseSetup();
  private SqlPlayerRepository _playerRepository;
  private SqlTeamRepository _teamRepository;

  [OneTimeSetUp]
  public void BeforeAll() {
     _playerRepository = new SqlPlayerRepository(_db.Context);
     _teamRepository = new SqlTeamRepository(_db.Context);
  }

  [SetUp]
  public void BeforeEach() {
    _playerRepository.Clear().Wait();
    _teamRepository.Clear().Wait();
  }

  [Test]
  public async Task PlayerTests() {
    var player = new Player {
      Id = "faker",
      Name = "Faker",
      MainRole = Role.Mid,
    };

    await _playerRepository.CreateAsync(player);

    var fetchedPlayer = await _playerRepository.FindByIdAsync(player.Id);
    Assert.NotNull(fetchedPlayer);
  }
  
  [Test]
  public async Task TeamTests() {
    var player = new Player {
      Id = "faker",
      Name = "Faker",
      MainRole = Role.Mid,
    };
    
    await _playerRepository.CreateAsync(player);
    
    var skt = new Team {
      Id = "skt",
      Name = "SKT"
    };
    
    skt.Join(player, Role.Mid);

    await _teamRepository.CreateAsync(skt);

    var fetchedTeam = await _teamRepository.FindByIdAsync(skt.Id);
    Assert.NotNull(fetchedTeam);
    Assert.That(fetchedTeam.Id, Is.EqualTo(skt.Id));
    Assert.That(fetchedTeam.Name, Is.EqualTo(skt.Name));
    Assert.That(fetchedTeam.HasPlayer(player), Is.True);
  }
}