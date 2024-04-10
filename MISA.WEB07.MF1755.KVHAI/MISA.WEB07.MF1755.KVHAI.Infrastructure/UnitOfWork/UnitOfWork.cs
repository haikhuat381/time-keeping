using Microsoft.Extensions.Configuration;
using MISA.WEB07.MF1755.KVHAI.Application;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.MF1755.KVHAI.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private DbConnection? _connection = null;

        private DbTransaction? _transaction = null;

        private readonly string _connectionString;

        public UnitOfWork(IConfiguration configuration) 
        {
            _connectionString = configuration["ConnectionString"] ?? "";
        }

        public DbConnection Connection => _connection ??= new MySqlConnection(_connectionString);

        public DbTransaction? Transaction => _transaction;
        public void BeginTransaction()
        {
            _connection ??= new MySqlConnection(_connectionString);
            if(_connection.State == ConnectionState.Open)
            {
                _transaction = _connection.BeginTransaction();
            }
            else
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
        }

        public async Task BeginTransactionAsync()
        {
            _connection ??= new MySqlConnection(_connectionString);
            if (Connection.State == ConnectionState.Open)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
            else
            {
                await _connection.OpenAsync();
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            Dispose();
        }

        public async Task CommitTransactionAsync()
        {
            if(_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            await DisposeAsync();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
        }

        public void RollBackTransaction()
        {
            _transaction?.Rollback();
            Dispose();
        }

        public async Task RollBackTransactionAsync()
        {
            if(_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();
        }
    }
}
