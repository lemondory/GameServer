namespace GameServer.Application.Features.Auth.Command
{
    //public class CreateAuthCommand : IRequest<AuthResponse>
    //{
    //    public string Name { get; set; }
    //}

    //public class CreateAuthCommandHandler : IRequestHandler<CreateAuthCommand, AuthResponse>
    //{
    //    private readonly IDatabaseConnectionFactory _connectionFactory;

    //    public CreateAuthCommandHandler(IDatabaseConnectionFactory connectionFactory)
    //    {
    //        _connectionFactory = connectionFactory;
    //    }

    //    public async Task<AuthResponse> Handle(CreateAuthCommand request, CancellationToken cancellationToken)
    //    {
    //        const string query = "INSERT INTO Categories (Name) VALUES (@Name); SELECT LAST_INSERT_ID();";

    //        using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseType.Account); // Await the task to get IDbConnection
    //        var AuthId = await connection.ExecuteScalarAsync<int>(query, new { request.Name });

    //        return new AuthResponse
    //        {
    //            Id = AuthId,
    //            Name = request.Name
    //        };
    //    }
    //}
}