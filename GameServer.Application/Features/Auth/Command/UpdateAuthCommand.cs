namespace GameServer.Application.Features.Auth.Command
{
    //public class UpdateAccountCommand : IRequest<bool>
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    //public class UpdateInventoryCommandHandler : IRequestHandler<UpdateAccountCommand, bool>
    //{
    //    private readonly IDatabaseConnectionFactory _connectionFactory;

    //    public UpdateInventoryCommandHandler(IDatabaseConnectionFactory connectionFactory)
    //    {
    //        _connectionFactory = connectionFactory;
    //    }

    //    public async Task<bool> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    //    {
    //        const string query = "UPDATE Categories SET Name = @Name WHERE Id = @Id";

    //        using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseType.Account);
    //        var rowsAffected = await connection.ExecuteAsync(query, new { request.Name, request.Id });

    //        return rowsAffected > 0;
    //    }
    //}
}