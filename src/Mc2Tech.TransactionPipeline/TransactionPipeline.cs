using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.TransactionPipeline
{
    public class TransactionPipeline<T> : Pipeline where T : DbContext
    {
        private readonly DbContext _context;

        public TransactionPipeline(T context)
        {
            _context = context;
        }

        public override async Task OnCommandAsync<TCommand>(Func<TCommand, CancellationToken, Task> next, TCommand cmd, CancellationToken ct)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(ct);

            await next(cmd, ct);

            await tx.CommitAsync(ct);
        }

        public override async Task<TResult> OnCommandAsync<TCommand, TResult>(Func<TCommand, CancellationToken, Task<TResult>> next, TCommand cmd, CancellationToken ct)
        {
            using var tx = await _context.Database.BeginTransactionAsync(ct);

            var result = await next(cmd, ct);

            await tx.CommitAsync(ct);

            return result;
        }

        //public override async Task<TResult> OnQueryAsync<TQuery, TResult>(Func<TQuery, CancellationToken, Task<TResult>> next, TQuery query, CancellationToken ct)
        //{
        //    await using var tx = await _context.Database.BeginTransactionAsync(ct);

        //    var result = await next(query, ct);

        //    await tx.RollbackAsync(ct);

        //    return result;
        //}
    }
}
