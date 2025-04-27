namespace GameServer.Application.Features.Auth.Command
{
    //public class DeleteAccountCommand : IRequest<bool>
    //{
    //    public int Id { get; set; }
    //    public DeleteAccountCommand(int id)
    //    {
    //        Id = id;
    //    }
    //}

    //public class DeleteInventoryCommandHandler : IRequestHandler<DeleteAccountCommand, bool>
    //{
    //    private readonly IDatabaseConnectionFactory _connectionFactory;

    //    public DeleteInventoryCommandHandler(IDatabaseConnectionFactory connectionFactory)
    //    {
    //        _connectionFactory = connectionFactory;
    //    }

    //    public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    //    {
    //        const string query = "DELETE FROM Categories WHERE Id = @Id";

    //        using var connection = await _connectionFactory.CreateConnectionAsync(DatabaseType.Account);
    //        var rowsAffected = await connection.ExecuteAsync(query, new { request.Id });

    //        return rowsAffected > 0;
    //    }
    //}
}